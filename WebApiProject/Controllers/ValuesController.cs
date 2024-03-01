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
        public ActionResult<User> CreateUser(List<User> users)
        {
            foreach (var user in users)
            {
                user.Id = _users.Any()
                    ? _users.Max(card => card.Id) + 1
                    : 0; // 防呆，如果没有用戶則從 0 開始
                _users.Add(user);
                Console.WriteLine($"ID:{user.Id},Name:{user.Name},Email:{user.Email},DateOfBirth:{user.DateOfBirth}");
            }
            return CreatedAtAction(nameof(GetUsers), users);
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
