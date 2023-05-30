using AutoMapper;
using Count.Models;
using Count.Services;
using Count.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                var meal = _mapper.Map<Meal>(model);
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
        public async Task<IActionResult> AddFoodToMeal(int id)//---id of the meal
        {
            var meal = await _service.FindMeal(id);
            //CreateMealFoodVM newMF = new CreateMealFoodVM();//to make a list of foods for multiple
            MealFood newMF = new MealFood();
            newMF.MealId = meal.Id;

            IEnumerable<Food> list = await _serviceFood.AllFoods();

            ViewBag.Food = new SelectList(list, "Name", "Name", newMF.FoodId);
            //newMF.Foods = list; 

            //ViewBag.FoodList = new MultiSelectList(list, "Name","Name", newMF.Foods);
            return View(newMF);

        }
        [HttpPost]
        public async Task<IActionResult> AddFoodToMeal(/*CreateMealFoodVM*/MealFood model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var food = await _serviceFood.FindFoodByName(model.Food.Name);
            model.FoodId = food.Id;
            await _service.CreateMealFood(model);
            //if (model.Foods.Count == 0)
            //{
            //    return RedirectToAction("AllFoodsOfMeal", "Meal", new { id = model.MealId });
            //}
            //else
            //{
            //    foreach (Food food in model.Foods)
            //    {
            //        var mf = new MealFood();
            //        mf.FoodId = food.Id;
            //        mf.MealId = model.MealId;
            //        await _service.CreateMealFood(mf);
            //    }
            //}
            return RedirectToAction("AllFoodsOfMeal", "Meal", new { id = model.MealId });
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
