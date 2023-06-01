using AutoMapper;
using Count.App.Models;
using Count.Models;
using Count.Services;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Count.App.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _service;
        private readonly IFoodService _serviceFood;
        private readonly IDayService _serviceDay;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public MealController(IMealService service, IFoodService serviceFood, IDayService serviceDay, IUserService userService, IMapper mapper)
        {
            _service = service;
            _serviceFood = serviceFood;
            _userService = userService;
            _serviceDay = serviceDay;
            _mapper = mapper;
        }

        //all MealsOfDay are in DayController
        [HttpGet]
        public async Task<IActionResult> CreateMeal(int id)
        {
            var day = await _serviceDay.FindDay(id);
            var meal = new Meal();
            meal.DayId = day.Id;
            return View(meal);

        }
        [HttpPost]
        public async Task<IActionResult> CreateMeal(Meal model)
        {
            if (ModelState.IsValid)
            {
                var meal = new Meal
                {
                    CourceTitle = model.CourceTitle,
                    DayId = model.DayId,
                };
                await _service.CreateMeal(meal);
                return RedirectToAction("AllMealsOfDay", "Day", new { id = model.DayId });
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> EditMeal(int id)
        {
            var meal = await _service.FindMeal(id);
            return View(meal);
        }
        [HttpPost]
        public async Task<IActionResult> EditMeal(Meal model)
        {
            if (ModelState.IsValid)
            {
                var meal = _mapper.Map<Meal>(model);
                await _service.EditMeal(meal);
                return RedirectToAction("AllMealsOfDay", "Day", new { id = model.DayId });
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _service.FindMeal(id);
            return View(meal);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMeal(Meal model)
        {
            if (ModelState.IsValid)
            {
                var meal = _mapper.Map<Meal>(model);
                await _service.DeleteMeal(meal);
                return RedirectToAction("AllMealsOfDay", "Day", new { id = model.DayId });
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddFoodToMeal(int id)//---id of the food
        {
            var food = await _serviceFood.FindFood(id);
            var days = await _serviceDay.AllDaysOfUser(User.Identity.Name);
            var meals = new List<Meal>();
            var addFoodToMeal = new AddFoodToMealBindingModel()
            {
                FoodId = food.Id,
                Food = food,
                ListOfDays = new(),
                ListOfMeals = new()
            };

            foreach (var day in days)
            {

                var meal = await _serviceDay.AllMealsOfDay(day.Id);
                meals.AddRange(meal);
                addFoodToMeal.ListOfDays.Add(
                    new DayCheckListModel()
                    {
                        Id = day.Id,
                        Date = day.Date,
                        Meals = day.Meals
                    }
                );
            }
            foreach (var meal in meals)
            {
                addFoodToMeal.ListOfMeals.Add(
                    new MealChecklistModel()
                    {
                        Id = meal.Id,
                        CourceTitle = meal.CourceTitle,
                        DayId = meal.DayId,
                        IsDeleted = meal.IsDeleted
                    }
                );
            }
            return View(addFoodToMeal);
        }
        [HttpPost]
        public async Task<IActionResult> AddFoodToMeal(AddFoodToMealBindingModel model)
        {
            var check = model.ListOfSelectedMealsIds != null && model.FoodQuantity > 0;
            if (check == false)
            {
                ModelState.AddModelError("SelectMealsAndQuantity", "Select meals and quantity(g)!");
                return RedirectToAction("AddFoodToMeal", "Meal", new { id = model.FoodId });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddFoodToMeal", "Meal", new { id = model.FoodId });
            }

            await _service.AddFoodsToMeal(model.ListOfSelectedMealsIds, model.FoodId, model.FoodQuantity);
            return RedirectToAction("AllUserDays", "Day");
        }
        public async Task<IActionResult> RemoveFoodFromMeal(int id)
        {
            var mealfood = await _service.FindMealFoodById(id);
            await _service.RemoveFoodFromMeal(id);
            return RedirectToAction("AllFoodsOfMeal", "Meal", new { id = mealfood.MealId });
        }
        [HttpGet]
        public async Task<IActionResult> AllFoodsOfMeal(int id)
        {
            var list = await _service.AllFoodsOfMeal(id);
            return View(list);
        }

    }
}
