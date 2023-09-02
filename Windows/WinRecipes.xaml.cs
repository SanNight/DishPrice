using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DishPrice.Data;

namespace DishPrice.Windows
{
    public partial class WinRecipes : Window
    {
        public static BindingList<Recipes> listRec;

        public WinRecipes() { InitializeComponent(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listRec = Recipes.GetListFromFile();
            listRec.RaiseListChangedEvents = true;
            listRec.ListChanged += ListRec_ListChanged;
            DisplayList();
        }

        private void btnAddRecip_Click(object sender, RoutedEventArgs e)
        {
            WinRecipeAdd addIngridientWindow = new WinRecipeAdd();
            addIngridientWindow.Show();
        }

        private void ListRec_ListChanged(object sender, ListChangedEventArgs e)
        {
            DisplayList();
            Recipes.SaveList(listRec);
        }


        // METHODS //

        private void DisplayList()
        {
            if (listRec == null) { return; }

            spListRec.Children.Clear();

            foreach (Recipes recip in listRec)
            {
                Label label = new Label();
                label.Content = recip.Name;
                label.HorizontalAlignment = HorizontalAlignment.Stretch;
                spListRec.Children.Add(label);
            }
        }

        private void lbShowDescr_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SwitchElements(true);

            Label label = (Label)sender;
            foreach (Recipes recip in listRec)
            {
                if (recip.Name == label.Content.ToString())
                {
                    tblRecipInfo.Text = recip.Description;
                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tblRecipInfo.Visibility == Visibility.Collapsed)
                return;

            string info = tblRecipInfo.Text;

            for (int i = 0; i < listRec.Count; i++)
            {
                if (listRec[i].Description == info)
                {
                    listRec.RemoveAt(i);
                    SwitchElements(false);
                    break;
                }
            }
        }

        private void SwitchElements(bool mode)
        {
            if (mode)
            {
                imgDel.Opacity = 1;
                tblRecipInfo.Visibility = Visibility.Visible;
                tblRecipName.Visibility = Visibility.Visible;
                btnDel.IsEnabled = true;
            }
            else
            {
                imgDel.Opacity = 0.1;
                tblRecipInfo.Visibility = Visibility.Collapsed;
                tblRecipName.Visibility = Visibility.Collapsed;
                btnDel.IsEnabled = false;
            }
        }
    }
}
