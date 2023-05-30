using AutoMapper;
using Count.Models;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Controllers
{
    public class DayController : Controller
    {
        private readonly IDayService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public DayController(IDayService service, IUserService userService, IMapper mapper)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> AllUserDays()
        {
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var list = await _service.AllDaysOfUser(user.UserName);

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> AllMealsOfDay(int id)//----day id
        {
            var day = await _service.FindDay(id);
            var list = await _service.AllMealsOfDay(day.Id);
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CreateDay()
        {
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var newDay = new Day();
            newDay.Date = DateTime.Now.Date;
            newDay.UserId = user.Id;

            return View(newDay);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDay(Day model)
        {
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            List<Day> list = await _service.AllDaysOfUser(user.UserName);
            foreach (Day day in list)
            {
                if (day.Date.Date == model.Date)
                {
                    if (!day.IsDeleted)
                    {
                        ViewData["DayExists"] = "This day already exists!";
                        return RedirectToAction("AllUserDays", "Day");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                await _service.CreateDay(model);
                return RedirectToAction("AllUserDays", "Day");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> DeleteDay(int id)
        {
            var day = await _service.FindDay(id);
            return View(day);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDay(Day model)
        {
            if (ModelState.IsValid)
            {
                await _service.DeleteDay(model);
                return RedirectToAction("AllUserDays", "Day");
            }
            return View(model);
        }
    }
}
