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
            if (_dataEF.SaveChanges() > 0)
            {
                return Ok(room.UniqueKey);
            }
            return BadRequest("Could not add room using data context EF!");
        }

        [HttpDelete("/api/DeleteRoom{Id}")]
        public ActionResult DeleteRoom(int Id)
        {
            Room existingRoom = _dataEF.Room.FirstOrDefault(x => x.Id == Id);
            IEnumerable<Room> allRooms = _dataEF.Room.Where(x => x != null);

            if (existingRoom != null) // Deletes room by Id
            {
                _dataEF.Room.Remove(existingRoom);
                _dataEF.SaveChanges();
                return Ok($"Sucessfully DELETED room with \n Id : {Id}");
            }
            else if (Id == 0) // Deletes all rooms in Id is 0
            {
                int roomsCount = allRooms.Count();
                _dataEF.Room.RemoveRange(allRooms);
                _dataEF.SaveChanges();
                return Ok($"Successfully DELETED all : {roomsCount} Rooms!");
            }
            else
            {
                return NotFound($"Room by Id : {Id} not found!");
            }
        }

    }
}
