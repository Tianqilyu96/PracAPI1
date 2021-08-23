using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using PracAPI1.Infrastructure;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PracAPI1.Filters
{
    public class LinkRewritingFilter : IAsyncResultFilter
    {
        private readonly IUrlHelperFactory helperFactory; //The IUrlHelperFactory will let us create IUrlHelpers on the fly.

        public LinkRewritingFilter(IUrlHelperFactory helperFactory)
        {
            this.helperFactory = helperFactory;
        }

        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var asObjectResult = context.Result as ObjectResult; //cast model to object result

            //check skip conditions
            // if the status code of the request is greater than 400, then it's some error and it's not gonna have any links on it.
            // Or if the value of the response is actually null or not an object result.
            // Or it isn't null, but if we try to cast it to a Resource,
            // which we'll pull in from Models, that's null, which means it's not a Resource type, it's some other type
            bool shouldSkip = asObjectResult?.StatusCode >= 400
                || asObjectResult?.Value == null
                || asObjectResult?.Value as Resource == null;

            if (shouldSkip)
            {
                return next();
            }

            var rewriter = new LinkRewriter(helperFactory.GetUrlHelper(context));
            RewriteAllLinks(asObjectResult.Value, rewriter);

            return next();
        }

        public static void RewriteAllLinks(object model, LinkRewriter rewriter)
        {
            if (model == null) return;
            var allProperties = model
                .GetType().GetTypeInfo()
                .GetAllProperties().Where(p => p.CanRead)
                .ToArray();

            var linkProperties = allProperties.Where(p => p.CanWrite && p.PropertyType == typeof(Link));

            foreach(var linkProperty in linkProperties)
            {
                var rewritten = rewriter.Rewirte(linkProperty.GetValue(model) as Link);
                if (rewritten == null) continue;
                linkProperty.SetValue(model, rewritten);
            }
            var arrayProperties = allProperties.Where(p => p.PropertyType.IsArray);

            RewriteLinksInArrays(arrayProperties, model, rewriter);

            var objectProperties = allProperties
                .Except(linkProperties)
                .Except(arrayProperties);
            RewriteLinksInNestedObjects(objectProperties, model, rewriter);
        }

        private static void RewriteLinksInNestedObjects(
            IEnumerable<PropertyInfo> objectProperties,
            object model,
            LinkRewriter rewriter)
        {
            foreach (var objectProperty in objectProperties)
            {
                if (objectProperty.PropertyType == typeof(string))
                {
                    continue;
                }

                var typeInfo = objectProperty.PropertyType.GetTypeInfo();
                if (typeInfo.IsClass)
                {
                    RewriteAllLinks(objectProperty.GetValue(model), rewriter);
                }
            }
        }

        private static void RewriteLinksInArrays(
            IEnumerable<PropertyInfo> arrayProperties,
            object model,
            LinkRewriter rewriter)
        {

            foreach (var arrayProperty in arrayProperties)
            {
                var array = arrayProperty.GetValue(model) as Array ?? new Array[0];

                foreach (var element in array)
                {
                    RewriteAllLinks(element, rewriter);
                }
            }
        }
    }
}
