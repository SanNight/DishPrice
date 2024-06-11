using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DishPrice.Data;

namespace DishPrice.UserXAMLElements
{
    public partial class CardIngrid : UserControl
    {
        Ingridient itemIngridient;

        public CardIngrid()
        {
            InitializeComponent();
            ItemIngridient = new Ingridient();
        }
        public CardIngrid(Ingridient ingridient)
        {
            InitializeComponent();
            ItemIngridient = ingridient;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ItemIcon.Source = ServicesImage.GetBMImage(itemIngridient.ImgPath);
            ItemName = itemIngridient.Name;

            if (itemIngridient.Price == -1)
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
        public Ingridient ItemIngridient { get => itemIngridient; set => itemIngridient = value; }
    }
}
