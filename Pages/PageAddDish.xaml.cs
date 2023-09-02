using DishPrice.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading.Tasks;
using DishPrice.UserXAMLElements;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System;
using System.Net.NetworkInformation;

namespace DishPrice.Frames
{
    public partial class PageAddDish : Page
    {
        public static BindingList<CardLineIngrid> listCardLine; // список карт доданих інгрідієнтів

        public PageAddDish() { InitializeComponent(); }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Services.DisplayItems(MainWindow.listIngridients, wpIngridients);
            listCardLine = new BindingList<CardLineIngrid>();

            // події зміни основних списків
            MainWindow.listIngridients.RaiseListChangedEvents = true;
            MainWindow.listIngridients.ListChanged += ListIngridients_ListChanged;
            listCardLine.RaiseListChangedEvents = true;
            listCardLine.ListChanged += listCardLine_ListChanged;
        }


        // EVENTS //
        private void ShowCardDetailiziedIngrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) // при натисканні на картку
        {
            Services.ShowCardDetailsIngrid(wpDetails, sender);
        }

        private void AddIngridToDish_DoubleDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CardIngrid ingridToAdd = (CardIngrid)sender;
            if (IngridAlreadyAdded(ingridToAdd.ItemIngridient)) return;

            Services.DisplayCardLine(ingridToAdd.ItemIngridient);
        }

        // textBoxes для кількості інгрідієнтів
        int curPosition;
        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            CardLineIngrid cardLineIngrid = new CardLineIngrid();
            if (textBox.Parent.GetType() != cardLineIngrid.GetType()) return; // перевірка, чи TB належить списку інгрідієнтів 


            cardLineIngrid = (CardLineIngrid)textBox.Parent;

            if (string.IsNullOrEmpty(cardLineIngrid.ItemAmount.Text)) return;

            double result;
            if (!double.TryParse(cardLineIngrid.ItemAmount.Text, out result))
            {
                if (cardLineIngrid.ItemAmount.Text.Length > 0)
                {
                    // позиція курсора до видалення
                    curPosition = cardLineIngrid.ItemAmount.SelectionStart - 1;

                    // видалення нечислового символа
                    cardLineIngrid.ItemAmount.Text = cardLineIngrid.ItemAmount.Text.Remove(curPosition, 1);

                    // переміщення карсора на місце, де було видалено символ
                    cardLineIngrid.ItemAmount.SelectionStart = curPosition;
                }
            }
        }

        // зміна міри
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            CardLineIngrid itemLineCard = (CardLineIngrid)comboBox.Parent;

            itemLineCard.ItemIngridient.AmountType = comboBox.SelectedItem.ToString();
        }

        private void cnvImgIngr_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                lbImgPath.Content = new BitmapImage(fileUri);
                imgIngr.Source = new BitmapImage(fileUri);
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // перевірки
            bool saveError = false;

            if (invalidString(tbDishName.Text)) saveError = true;

            foreach (var lineCard in listCardLine)
            {
                // textBoxes / comboBoxes
                if (invalidString(lineCard.ItemIngridient.Amount.ToString()) ||
                                  lineCard.ItemIngridient.Amount <= 0) 
                    saveError = true;
                if (invalidString(lineCard.ItemIngridient.Amount.ToString())) 
                    saveError = true;
            }

            if (saveError)
            {
                Brush borderBefore = btnSave.BorderBrush;

                btnSave.BorderBrush = Brushes.Red;
                await Task.Delay(500);
                btnSave.BorderBrush = borderBefore;
                return;
            }


            // формування об'єкта страви 
            Dish dish = new Dish();
            dish.Name = tbDishName.Text;
            dish.Description = tbDishDescription.Text;

            dish.ImgPath = (lbImgPath.Content != null)  ? 
                lbImgPath.Content.ToString()            : 
                ServicesImage.ImgDefaultPath;
            
            foreach (var cardLine in listCardLine)
            {
                DishIngridient dishIngrid = new DishIngridient(cardLine.ItemIngridient);
                dishIngrid.Amount = Double.Parse(cardLine.ItemAmount.Text);
                dish.DishIngridients.Add(dishIngrid);
            }

            // збереження
            MainWindow.listDishes.Add(dish);
            ServicesFileIO.SaveList(MainWindow.listDishes);

            // очищення форми
            tbDishName.Text = null;
            tbDishDescription.Text = null;
            lbxIngridForDish.Items.Clear();
            listCardLine.Clear();
            imgIngr.Source = null;
            lbImgPath.Content = String.Empty;

            btnSave.Background = Brushes.LightCyan;
            await Task.Delay(1000);
            btnSave.Background = Brushes.Transparent;
        }



        // ANOTHER EVENTS //
        private void ListIngridients_ListChanged(object sender, ListChangedEventArgs e)
        {
            wpIngridients.Children.Clear();
            Services.DisplayItems(MainWindow.listIngridients, wpIngridients);
        }

        private void listCardLine_ListChanged(object sender, ListChangedEventArgs e)
        {
            CheckListCount(listCardLine.Count);
            lbxIngridForDish.Items.Clear();

            foreach (var card in listCardLine)
            {
                lbxIngridForDish.Items.Add(card);
            }
        }



        // METHODS //
        private bool IngridAlreadyAdded(Ingridient ingridToAdd)
        {
            // формується список з назвами інгрідієнтів
            BindingList<string> listIngridNames = new BindingList<string>();
            foreach (var card in listCardLine)
            {
                listIngridNames.Add(card.ItemIngridient.Name);
            }

            int index = listIngridNames.IndexOf(ingridToAdd.Name);
            if (index >= 0) // вже додано в страву
            {
                return true;
            }
            else
                return false;
        }

        private void CheckListCount(int count)
        {
            if (count == 0)
            {
                btnSave.IsEnabled = false;
                lbxIngridForDish.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnSave.IsEnabled = true;
                lbxIngridForDish.Visibility = Visibility.Visible;
            }
        }

        private bool invalidString(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return true;
            else return false;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Services.Searching(tbSearch.Text, wpIngridients, MainWindow.listIngridients);
        }
    }
}
