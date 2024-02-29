using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models;
using System.Collections.Generic;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<User> _users = new List<User>(); // 這裡我們暫時用 List 來存放用戶，實際應用中可以改為資料庫

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _users.Add(new User
            {
                Id = _users.Any()
                   ? _users.Max(card => card.Id) + 1
                   : 0, // 臨時防呆，如果沒東西就從 0 開始
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
            });
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            var existingUser = _users.Find(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = user.DateOfBirth;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _users.Remove(user);
            return NoContent();
        }
    }
}
