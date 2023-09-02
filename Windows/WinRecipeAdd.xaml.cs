using System;
using System.Windows;
using DishPrice.Data;

namespace DishPrice.Windows
{
    public partial class WinRecipeAdd : Window
    {
        public WinRecipeAdd() { InitializeComponent(); }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbName.Text) ||
               String.IsNullOrWhiteSpace(tbDescription.Text)) { return; }

            Recipes recipe = new Recipes();
            recipe.Name = tbName.Text;
            recipe.Description = tbDescription.Text;

            WinRecipes.listRec.Add(recipe);
        }
    }
}
