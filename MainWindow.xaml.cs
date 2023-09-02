using DishPrice.Data;
using DishPrice.Frames;
using DishPrice.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace DishPrice
{
    public partial class MainWindow : Window
    {
        public static BindingList<Ingridient> listIngridients;
        public static BindingList<Dish> listDishes;

        PageMain pageMain = new PageMain();
        PageIngridients pageIngridients;
        PageDishes pageDishes;
        PageAddDish pageAddDish;

        public MainWindow() { InitializeComponent(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frameMain.Content = pageMain;

            ServicesFileIO.CheckFile();
            listIngridients = ServicesFileIO.LoadIngridientsList();
            listDishes = ServicesFileIO.LoadDishesList();

            pageIngridients = new PageIngridients();
            pageAddDish = new PageAddDish();
            pageDishes = new PageDishes();
        }

        private void AddIngridient_Click(object sender, RoutedEventArgs e)
        {
            WinIngridAdd addIngridientWindow = new WinIngridAdd();
            addIngridientWindow.ShowDialog();
        }

        private void goPageMain(object sender, RoutedEventArgs e)
        {
            frameMain.Content = pageMain;
        }

        private void goPageAddDish(object sender, RoutedEventArgs e)
        {
            frameMain.Content = pageAddDish;
        }

        private void goPageIngridients(object sender, RoutedEventArgs e)
        {
            frameMain.Content = pageIngridients;
        }

        private void goPageDishes(object sender, RoutedEventArgs e)
        {
            frameMain.Content = pageDishes;
        }

        Brush colorActive = Brushes.LightCyan;
        Brush colorTrans = Brushes.Transparent;

        private void ChangeBg(Page page, Button button)
        {
            if (frameMain.Content == page)
                button.Background = colorActive;
            else
                button.Background = colorTrans;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ChangeBg(pageMain, btnMainPage);
            ChangeBg(pageIngridients, btnShowIngridients);
            ChangeBg(pageDishes, btnShowDishes);
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnAddIngridient.Background = Brushes.Red;
        }
    }
}
