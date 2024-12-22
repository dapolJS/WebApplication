using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    public class RoomController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly DataContextEF _dataEF;

        public RoomController(DataContext dataContext, DataContextEF dataContextEF)
        {
            _dataContext = dataContext;
            _dataEF = dataContextEF;
        }

        [HttpPost("api/CreateRoom")]
        public ActionResult CreateRoom()
        {
            Room room = new Room();
            return Ok(room.UniqueKey);
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
