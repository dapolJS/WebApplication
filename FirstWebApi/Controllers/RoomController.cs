using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    public class RoomController : ControllerBase
    {
        private readonly DataContextEF _dataEF;

        public RoomController(DataContextEF dataContextEF)
        {
            _dataEF = dataContextEF;
        }

        [HttpGet("/api/GetRooms/")]
        public ActionResult<Room> GetRooms()
        {
            var rooms = _dataEF.Room.ToList();
            return Ok(rooms);
        }

        [HttpPost("/api/AddRoom")]
        public ActionResult AddRoom()
        {
            Room room = new Room();
            _dataEF.Room.Add(room);
            if(_dataEF.SaveChanges() > 0)
            {
                return Ok(room.UniqueKey);
            }
            return BadRequest("Could not add room using data context EF!");
        }

    }
}
