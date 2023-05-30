using AutoMapper;
using Count.Models;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Controllers
{
    public class BmiController : Controller
    {
        private readonly IBmiService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BmiController(IBmiService service, IUserService userService, IMapper mapper)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> HealthCheck()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var list = await _service.AllUserUserBmis(user.Id);

            return View(list);

        }
        [HttpGet]
        public async Task<IActionResult> CreateBmi()
        {
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var bmi = new BmiUser();
            bmi.UserId = user.Id;

            return View(bmi);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBmi([FromForm] BmiUser model)
        {
            if (ModelState.IsValid)
            {
                var bmi = _mapper.Map<BmiUser>(model);
                await _service.CreateBmi(bmi);
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditBmi(int id)
        {
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var bmi = await _service.FindBmi(id);
            if (bmi.UserId != user.Id)
            {
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View(bmi);

        }
        [HttpPost]
        public async Task<IActionResult> EditBmi([FromForm] BmiUser model)
        {
            if (ModelState.IsValid)
            {
                var bmi = _mapper.Map<BmiUser>(model);
                await _service.EditBmi(bmi);
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBmi(int id)
        {
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var bmi = await _service.FindBmi(id);
            if (bmi.UserId != user.Id)
            {
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View(bmi);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBmi([FromForm] BmiUser model)
        {
            if (ModelState.IsValid)
            {
                var bmi = _mapper.Map<BmiUser>(model);
                await _service.DeleteBmi(bmi);
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View();
        }
    }
}
