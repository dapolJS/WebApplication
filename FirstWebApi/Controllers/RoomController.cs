using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    public class RoomController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RoomController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("api/CreateRoom")]
        public ActionResult CreateRoom()
        {
            Room room = new Room();
            return Ok(room.UniqueKey);
        }

    }
}
