﻿using AutoMapper;
using Count.Models;
using Count.Services;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Count.App.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public FoodController(IFoodService service, IUserService userService, IMapper mapper)
        {
            _service = service;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> AllFoodss(string? searchString)
        {
            var list = new List<Food>();
            if (!String.IsNullOrEmpty(searchString))
                list = await _service.FilterFoods(searchString);
            else
                list = await _service.AllFoods();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CreateFood()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AllFoodss", "Food");
            }
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var food = new Food();
            food.CreatedById = user.Id;

            return View(food);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFood(Food model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            List<Food> list = await _service.AllFoods();
            var check = list.FirstOrDefault(l => l.Name == model.Name);
            if (check != null)
            {
                ModelState.AddModelError("FoodAlreadyExists", "Food with this name is already in the database!");
                return View(model);
            }

            var checkQuantityandCalories = model.Quantity > 0 && model.Calories > 0;
            if (!checkQuantityandCalories)
            {
                ModelState.AddModelError("AddQuantityAndCalories", "Add calories and quantity!");
                return View(model);
            }

            var food = _mapper.Map<Food>(model);
            await _service.CreateFood(food);
            return RedirectToAction("AllFoodss", "Food");

        }
        [HttpGet]
        public async Task<IActionResult> EditFood(int id)
        {
            var user = await _userService.FindUserByUsername(User.Identity.Name);
            var food = await _service.FindFood(id);
            food.CreatedById = user.Id;
            return View(food);
        }
        [HttpPost]
        public async Task<IActionResult> EditFood(Food model)
        {
            if (ModelState.IsValid)
            {
                var food = _mapper.Map<Food>(model);
                await _service.EditFood(food);
                return RedirectToAction("AllFoodss", "Food");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var food = await _service.FindFood(id);
            return View(food);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFood(Food model)
        {
            if (ModelState.IsValid)
            {
                var food = _mapper.Map<Food>(model);
                await _service.DeleteFood(food);
                return RedirectToAction("AllFoodss", "Food");
            }
            return View();
        }
    }
}
