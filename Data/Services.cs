using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;

using DishPrice.Frames;
using DishPrice.UserXAMLElements;

namespace DishPrice.Data
{
    class Services
    {
        // DISPLAY
        public static void ShowMsg(string msg)
        {
            string caption = "dlya duda";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(msg, caption, button, icon, MessageBoxResult.Yes);
        }

        // формування меню інгрідієнтів
        public static void DisplayItems(BindingList<Ingridient> list, WrapPanel parent)
        {
            parent.Children.Clear();
            foreach (Ingridient ingridient in list)
            {
                CardIngrid itemCard = new CardIngrid(ingridient);
                parent.Children.Add(itemCard);
            }
        }

        // формування меню страв
        public static void DisplayItems(BindingList<Dish> list, WrapPanel parent)
        {
            parent.Children.Clear();
            foreach (Dish dish in list)
            {
                CardDish itemCard = new CardDish(dish);
                parent.Children.Add(itemCard);
            }
        }

        // формування меню інгрідієнтів для страви (в меню створення страви)
        public static void DisplayCardLine(Ingridient ingridient)
        {
            CardLineIngrid card = new CardLineIngrid(ingridient);
            PageAddDish.listCardLine.Add(card);
        }

        // пошук інгрідієнтів та виведення
        public static void Searching(string searchWord, WrapPanel wrapPanel, BindingList<Ingridient> ingridList)
        {
            wrapPanel.Children.Clear();

            foreach (Ingridient ingrid in ingridList)
            {
                string ingrName = ingrid.Name.ToLower();
                string searchLower = searchWord.ToLower();
                if (ingrName.Contains(searchLower) || searchLower.Contains(ingrName))
                {
                    ingrid.DisplayTo(wrapPanel);
                }
            }
        }

        // SHOW DETAILS

        public static void ShowCardDetailsIngrid(WrapPanel panelRight, object sender)
        {
            CardIngrid cardIngrid = (CardIngrid)sender;
            Ingridient ingridient = cardIngrid.ItemIngridient;
            CardDetailiziedIngrid cardDetailized = new CardDetailiziedIngrid(ingridient, panelRight);

            panelRight.Children.Clear();
            panelRight.Children.Add(cardDetailized);
        }

        public static void ShowCardDetailsIngrid(WrapPanel panelRight, Ingridient ingridient)
        {
            CardDetailiziedIngrid cardDetailized = new CardDetailiziedIngrid(ingridient, panelRight);

            panelRight.Children.Clear();
            panelRight.Children.Add(cardDetailized);
        }

        public static void ShowCardDetailsDish(WrapPanel panelRight, object sender)
        {
            CardDish cardDish = (CardDish)sender;
            Dish dish = cardDish.ItemDish;
            CardDetailizedDish cardDetailized = new CardDetailizedDish(dish);

            panelRight.Children.Clear();
            panelRight.Children.Add(cardDetailized);
        }



        // GET-методи

        public static Image GetDefaultImg()
        {
            Image image = new Image();
            BitmapImage bi3 = new BitmapImage();

            //bi3.BeginInit();
            //bi3.UriSource =
            //    new Uri("C:\\Users\\ПК\\source\\repos\\DishPrice\\DishPrice\\bin\\Debug\\Res\\no img.png",
            //    UriKind.Absolute); // Додати зображення в проект
            //bi3.EndInit();
            //image.Source = bi3;

            image.Stretch = Stretch.Uniform;
            return image;
        }

        // ComboBox з мірами із списку  Measures.ListAmountType
        public static ComboBox GetAmountTypeComboBox()
        {
            ComboBox comboBox = new ComboBox();
            foreach (string amountType in Measures.ListAmountType) comboBox.Items.Add(amountType);
            return comboBox;
        }


        // ANOTHER //

        public static void CollapseLbIfEmpty(Label label)
        {
            if (label.Content == null ||
                String.IsNullOrWhiteSpace(label.Content.ToString()))
            {
                label.Visibility = Visibility.Collapsed;
            }
        }
    }
}
