using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DishPrice.Data;

namespace DishPrice.UserXAMLElements
{
    public partial class CardDish : UserControl
    {
        Dish itemDish;
        public CardDish()
        {
            InitializeComponent();
            itemDish = new Dish();
        }

        public CardDish(Dish dish)
        {
            InitializeComponent();
            itemDish = dish;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ItemIcon.Source = ServicesImage.GetBMImage(itemDish.ImgPath);
            ItemName = itemDish.Name;

            if (itemDish.Price == -1)
            {
                BorderBrush = Brushes.Red;
                BorderThickness = new Thickness(1);
            }
        }

        public string ItemName
        {
            get => itemName.Content.ToString();
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    itemName.Content = "Без імені";
                    return;
                }

                itemName.Content = value;
            }
        }
        public Image ItemIcon
        {
            get => itemIcon;
            set => itemIcon = value;
        }
        public Dish ItemDish { get => itemDish; set => itemDish = value; }
    }
}
