using AutoMapper;
using Count.Models;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(User model)
        {
            var user = _mapper.Map<User>(model);
            if(model.Sex== "F")
            if (ModelState.IsValid)
            {
                await _service.EditUser(user);
                return RedirectToAction("Profile", "User");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProfile()
        {
            var user = await _service.FindUserByUsername(User.Identity.Name);
            await _service.DeleteUser(user.UserName);
            return RedirectToAction("Profile", "User");
        }

    }
}
