using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Services
{

    // Right now the controller is directly interacting with the data context.
    // It's a better pattern to move this data access code to a service class
    // and keep the controller as thin as possible.
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<Room> GetRoomAsync(Guid id);
    }
}
