using System.Windows;
using DishPrice.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using DishPrice.Data;

namespace DishPrice.Frames
{
    public partial class PageMain : Page
    {
        public PageMain() { InitializeComponent(); }

        private void btnRecipes_Click(object sender, RoutedEventArgs e)
        {
            WinRecipes winRecipes = new WinRecipes();
            winRecipes.Show();
        }

        private void openFileIngridients(object sender, RoutedEventArgs e)
        {
            Process.Start(ServicesFileIO.FILE_INGR);
        }

        private void openFileDishes(object sender, RoutedEventArgs e)
        {
            Process.Start(ServicesFileIO.FILE_DISHES);
        }
    }
}
