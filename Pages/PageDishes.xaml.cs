using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DishPrice.Data;

namespace DishPrice.Frames
{
    public partial class PageDishes : Page
    {
        public PageDishes() { InitializeComponent(); }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Services.DisplayItems(MainWindow.listDishes, wpDishes);
            MainWindow.listDishes.RaiseListChangedEvents = true;
            MainWindow.listDishes.ListChanged += ListDishes_ListChanged;
        }


        //  EVENTS //

        // деталі карти страви
        private void CardDish_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Services.ShowCardDetailsDish(wpDetails, sender);
        }



        // ANOTHER EVENTS //
        private void ListDishes_ListChanged(object sender, ListChangedEventArgs e)
        {
            // збереження та виведення елементів списку
            wpDishes.Children.Clear();
            ServicesFileIO.SaveList(MainWindow.listDishes);
            Services.DisplayItems(MainWindow.listDishes, wpDishes);
        }
    }
}
