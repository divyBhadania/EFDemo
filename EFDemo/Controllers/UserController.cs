using EF.Service.DTO;
using EF.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            return Ok(await _userService.GetByIdAsync(Convert.ToInt32(User.FindFirst("Id")?.Value)));
        }

        [HttpPost("Insert")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Insert(UserDTO userDTO)
        {
            return Ok(await _userService.InsertAsync(userDTO));
        }

        [HttpPost("update")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(UserUpdateDTO userUpdateDTO)
        {
            return Ok(await _userService.UpdatetAsync(userUpdateDTO));
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _userService.DeleteAsync(id));
        }
    }
}
