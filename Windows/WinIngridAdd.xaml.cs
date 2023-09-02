using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DishPrice.Data;

namespace DishPrice.Windows
{
    public partial class WinIngridAdd : Window
    {
        int indexToSave;
        List<TextBox> listTextBox = new List<TextBox>();

        public WinIngridAdd()
        {
            InitializeComponent();
            indexToSave = -1;
            imgIngr = Services.GetDefaultImg();
        }

        public WinIngridAdd(Ingridient ingridient)
        {
            // перезапис
            if (MainWindow.listIngridients.Contains(ingridient))
                indexToSave = MainWindow.listIngridients.IndexOf(ingridient);

            InitializeComponent();

            imgIngr = Services.GetDefaultImg();
            tbIngrName.Text = ingridient.Name;
            tbIngrPrise.Text = ingridient.FullPrice.ToString();
            tbIngrAmount.Text = ingridient.AmountPrimal.ToString();
            lbImgPath.Content = ingridient.ImgPath;

            // перенести цей позор 
            Uri fileUri;
            if (File.Exists(ingridient.ImgPath))
                fileUri = new Uri(ingridient.ImgPath, UriKind.Absolute);
            else
                fileUri = new Uri("C:\\Users\\ПК\\source\\repos\\DishPrice\\DishPrice\\bin\\Debug\\Res\\no img.png", UriKind.Absolute);

            imgIngr.Source = new BitmapImage(fileUri);
            //

            for (int i = 0; i < Measures.ListAmountType.Count; i++)
            {
                if (ingridient.AmountType == Measures.ListAmountType[i])
                {
                    cbIngrAmountType.SelectedIndex = i;
                    break;
                }
                else
                    cbIngrAmountType.SelectedIndex = -1;
            }
            //cbIngrAmountType.SelectedIndex = cbIngrAmountType.Items.IndexOf(ingridient.AmountType);

            tbIngrDiscription.Text = ingridient.Description;
        }



        // USER'S METHODS //
        private void btnSaveIngrid_Click(object sender, RoutedEventArgs e)
        {
            listTextBox.Add(tbIngrName);
            listTextBox.Add(tbIngrPrise);
            listTextBox.Add(tbIngrAmount);

            // перевірки введених значент
            bool error = false;
            foreach (var textBox in listTextBox)
            {
                if (String.IsNullOrWhiteSpace(textBox.Text))
                {
                    error = true;
                    textBox.BorderBrush = Brushes.Red;
                }
                else
                    textBox.BorderBrush = Brushes.Black;
            }

            if (error) { return; }


            // створення інгрідієнта
            Ingridient newIngrid = new Ingridient();
            newIngrid.Name = tbIngrName.Text;
            newIngrid.Description = tbIngrDiscription.Text;
            newIngrid.FullPrice = Double.Parse(tbIngrPrise.Text);
            newIngrid.AmountPrimal = Double.Parse(tbIngrAmount.Text);
            newIngrid.AmountType = cbIngrAmountType.Text;

            newIngrid.ImgPath = (lbImgPath.Content != null) ?
                lbImgPath.Content.ToString() :
                ServicesImage.ImgDefaultPath;

            Measures.ConvertToHigher(newIngrid);                    // перетворення у кг / л
            double multiplier = 1 / newIngrid.AmountPrimal;
            newIngrid.AmountPrimal *= multiplier;   // приведення до вигляду: (ціна) за 1 кг/л
            newIngrid.FullPrice *= multiplier;

            // додавання
            if (indexToSave == -1)
                MainWindow.listIngridients.Add(newIngrid);
            else
                MainWindow.listIngridients[indexToSave] = newIngrid;

            ServicesFileIO.SaveList(MainWindow.listIngridients);

            Close();
        }


        private void AddIngrImg(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                lbImgPath.Content = new BitmapImage(fileUri);
                imgIngr.Source = new BitmapImage(fileUri);
            }
        }



        // ANOTHER EVENTS //

        int curPosition;
        // кількість
        private void tbIngrAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(tbIngrPrise.Text)) return;

            double result;
            if (!double.TryParse(tbIngrAmount.Text, out result))
            {
                if (tbIngrAmount.Text.Length > 0)
                {
                    // позиція курсора до видалення
                    // видалення нечислового символа
                    // переміщення карсора на місце, де було видалено символ

                    curPosition = tbIngrAmount.SelectionStart - 1;
                    tbIngrAmount.Text = tbIngrAmount.Text.Remove(curPosition, 1);
                    tbIngrAmount.SelectionStart = curPosition;
                }
            }
        }

        // ціна
        private void tbIngrPrise_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrEmpty(tbIngrPrise.Text)) return;

            double result;
            if (!double.TryParse(tbIngrPrise.Text, out result))
            {
                if (tbIngrPrise.Text.Length > 0)
                {
                    // позиція курсора до видалення
                    // видалення нечислового символа
                    // переміщення карсора на місце, де було видалено символ
                    curPosition = tbIngrPrise.SelectionStart - 1;
                    tbIngrPrise.Text = tbIngrPrise.Text.Remove(curPosition, 1);
                    tbIngrPrise.SelectionStart = curPosition;
                }
            }
        }
    }
}
