using FirstWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly DataContextEF _dataEf;

        public RoomController(DataContextEF dataContextEf)
        {
            _dataEf = dataContextEf;
        }

        [HttpGet("/api/GetRooms")]
        public ActionResult<Room> GetRooms()
        {
            var rooms = _dataEf.Room.ToList();
            return Ok(rooms);
        }

        [HttpPost("/api/AddRoom")]
        public ActionResult AddRoom()
        {
            Room room = new Room();
            _dataEf.Room.Add(room);
            if (_dataEf.SaveChanges() > 0)
            {
                return Ok(room);
            }
            return BadRequest("Could not add room using data context EF!");
        }

        [HttpDelete("/api/DeleteRoom/{id}")]
        public ActionResult DeleteRoom(int id)
        {
            Room existingRoom = _dataEf.Room.FirstOrDefault(x => x.Id == id);
            IEnumerable<Room> allRooms = _dataEf.Room.Where(x => x != null);

            if (existingRoom != null) // Deletes room by id
            {
                _dataEf.Room.Remove(existingRoom);
                _dataEf.SaveChanges();
                return Ok($"Successfully DELETED room with \n Id : {id}");
            }
            else if (id == 0) // Deletes all rooms in id is 0
            {
                int roomsCount = allRooms.Count();
                _dataEf.Room.RemoveRange(allRooms);
                _dataEf.SaveChanges();
                return Ok($"Successfully DELETED all : {roomsCount} Rooms!");
            }
            else
            {
                return NotFound($"Room by Id : {id} not found!");
            }
        }
    }
}
