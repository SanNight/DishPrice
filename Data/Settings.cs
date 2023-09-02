using System.IO;
using System.Windows.Media;

namespace DishPrice.Data
{
    public static class Settings
    {
        public static string RecipesPath = Path.Combine(Directory.GetCurrentDirectory(), "Saves", "Recipes.json");
        public static string fontFamily = "Comic Sans MS";
        public static Brush ColorItem  = Brushes.Aquamarine;
        public static Brush ColorDishIngridients = Brushes.SkyBlue;
    }
}
