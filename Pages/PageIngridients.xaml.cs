using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DishPrice.Data;

namespace DishPrice.Frames
{
    public partial class PageIngridients : Page
    {
        static BindingList<Ingridient> curListIngridients; // список інгрідієнтів на цій сторінці

        public PageIngridients() { InitializeComponent(); }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            curListIngridients = MainWindow.listIngridients;
            Services.DisplayItems(MainWindow.listIngridients, wpIngr);
            MainWindow.listIngridients.RaiseListChangedEvents = true;
            MainWindow.listIngridients.ListChanged += ListIngridients_ListChanged;

            curListIngridients.RaiseListChangedEvents = true;
            curListIngridients.ListChanged += CurListIngridients_ListChanged;
        }

        private void CurListIngridients_ListChanged(object sender, ListChangedEventArgs e)
        {
            wpIngr.Children.Clear();
            Services.DisplayItems(curListIngridients, wpIngr);
        }

        private void ListIngridients_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            wpIngr.Children.Clear();
            Services.Searching(tbSearch.Text, wpIngr, curListIngridients);

            if (MainWindow.listIngridients.Count == 0)
                wpDetails.Children.Clear();

            curListIngridients = MainWindow.listIngridients;

            ServicesFileIO.SaveList(MainWindow.listIngridients);
            Services.DisplayItems(MainWindow.listIngridients, wpIngr);
        }

        private void ShowDetailiziedCard(object sender, System.Windows.Input.MouseButtonEventArgs e) // при натисканні на малу картку
        {
            Services.ShowCardDetailsIngrid(wpDetails, sender);
        }

        private void cbSortParam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string newSelection = ((ComboBoxItem)cbSortParam.SelectedItem).Content.ToString();
            //if (newSelection == "Назва")
            //{
            //    curListIngridients = new BindingList<Ingridient>(curListIngridients.OrderByDescending(x => x.Name).ToList());
            //}
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Services.Searching(tbSearch.Text, wpIngr, curListIngridients);
        }
    }
}
