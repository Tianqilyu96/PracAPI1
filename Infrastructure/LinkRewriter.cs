using Microsoft.AspNetCore.Mvc;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Infrastructure
{
    public class LinkRewriter
    {
        private readonly IUrlHelper urlHelper;

        public LinkRewriter(IUrlHelper urlHelper) //Inject IUrlHelper service
        {
            this.urlHelper = urlHelper;
        }

        // method will take a Link object that has the RouteName and RouteValues properties set
        // and convert it or rewrite it as a Link object that has a URL in the Href property.
        public Link Rewirte(Link origial)
        {
            if (origial == null) return null;

            return new Link
            {
                Href = urlHelper.Link(origial.RouteName, origial.RouteValue),
                Method = origial.Method,
                Relations = origial.Relations
            };
        }
    }
}
