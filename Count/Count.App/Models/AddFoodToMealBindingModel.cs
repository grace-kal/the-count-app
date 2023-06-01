using Count.Models;

namespace Count.App.Models
{
    public class AddFoodToMealBindingModel
    {
        public int FoodId { get; set; }
        public Food? Food { get; set; }
        public double FoodQuantity { get; set; }
        public List<DayCheckListModel>? ListOfDays { get; set; }
        public List<MealChecklistModel>? ListOfMeals{ get; set; }
        public List<int>? ListOfSelectedMealsIds { get; set; }
    }
}
