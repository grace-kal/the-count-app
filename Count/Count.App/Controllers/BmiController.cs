using AutoMapper;
using Count.App.Models;
using Count.Models;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

            var healthCheckModel = new HealthCheckModel
            {
                UserBmis = list
            };

            if (list.Count == 0)
                return View(healthCheckModel);

            var latestBmi = list.MaxBy(x => x.Date);
            var latestBmiInfo = new LatetsBmiInfo
            {
                LatestBmi = latestBmi
            };

            if (user.GoalWeight > 0)
                latestBmiInfo.DistanceFromGoalWeight = Math.Abs(latestBmi.Weight - user.GoalWeight);

            switch (latestBmi.Bmi)
            {
                case Bmi.Normal:
                    latestBmiInfo.Status = "Your weight is normal for your height!";
                    break;
                case Bmi.Overweight:
                    latestBmiInfo.Status = "You're overweight for your height!";
                    //TODO latestBmiInfo.HealthyWeight = ;
                    break;
                case Bmi.Underweight:
                    latestBmiInfo.Status = "You're underweight for your height!";
                    break;
                default: return View(healthCheckModel);
            }

            healthCheckModel.LatestBmiInfo = latestBmiInfo;
            return View(healthCheckModel);
        }
        [HttpGet]
        public async Task<IActionResult> CreateBmi()
        {
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var bmi = new BmiUser
            {
                UserId = user.Id
            };

            return View(bmi);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBmi([FromForm] BmiUser model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateBmi(model);
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
                await _service.EditBmi(model);
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
                await _service.DeleteBmi(model);
                return RedirectToAction("HealthCheck", "Bmi");
            }
            return View();
        }
    }
}
