using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DishPrice.Data;

namespace DishPrice.UserXAMLElements
{
    public partial class CardDetailizedDish : UserControl
    {
        Dish itemDish;
        public CardDetailizedDish()
        {
            InitializeComponent();
            itemDish = new Dish();
        }

        public CardDetailizedDish(Dish dish)
        {
            InitializeComponent();
            itemDish = dish;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            spIngridList.Children.Clear();

            ItemIcon.Source = ServicesImage.GetBMImage(itemDish.ImgPath);
            ItemName = itemDish.Name;
            ItemDescription = itemDish.Description;

            itemPrice.Content = itemDish.Price.ToString();
            itemWeight.Content = itemDish.GetDishWeight();

            // інгрідієнт для списку інгрідієнтів
            foreach (var dishIngrid in ItemDish.DishIngridients)
            {
                Border border = GetBorderDishIngrid();
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(1);

                Label lbIngridName = GetLabelDishIngrid(dishIngrid.Ingridient.Name);
                Label lbIngridAmount = GetLabelDishIngrid(dishIngrid.Amount.ToString());
                Label lbIngridAmountType = GetLabelDishIngrid(dishIngrid.Ingridient.AmountType);
                string dishIngridPrice = (dishIngrid.Ingridient.Price * dishIngrid.Amount).ToString();
                Label lbIngridPrice = GetLabelDishIngrid(dishIngridPrice);

                stackPanel.Children.Add(lbIngridName);
                stackPanel.Children.Add(lbIngridAmount);
                stackPanel.Children.Add(lbIngridAmountType);
                stackPanel.Children.Add(lbIngridPrice);

                spIngridList.Children.Add(border);
                border.Child = stackPanel;
            }

            // приховання
            Services.CollapseLbIfEmpty(itemDescription);
            Services.CollapseLbIfEmpty(itemTags);
            Services.CollapseLbIfEmpty(itemSynonims);
        }

        private Label GetLabelDishIngrid(string content)
        {
            Label label = new Label();
            label.Content = content;

            label.Padding = new Thickness(1);
            label.Margin = new Thickness(1);

            return label;
        }

        private Border GetBorderDishIngrid()
        {
            Border border = new Border();
            border.Padding = new Thickness(1);
            border.BorderBrush = Brushes.White;
            border.CornerRadius = new CornerRadius(3);
            border.BorderThickness = new Thickness(1);
            border.HorizontalAlignment = HorizontalAlignment.Center;

            return border;
        }

        private void btnEditDish_Click(object sender, RoutedEventArgs e)
        {
            //Button btn = (Button)sender;
            //StackPanel stackPanel = (StackPanel)btn.Parent;
            //DockPanel dockPanel = (DockPanel)stackPanel.Parent;
            //Grid grid = (Grid)dockPanel.Parent;
            //ItemCardDetailized itCardDetail = (ItemCardDetailized)grid.Parent;

            //AddIngridient addIngridientWin = new AddIngridient(itCardDetail.ItemIngridient);
            //addIngridientWin.ShowDialog();
        }

        private void btnDelDish_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            StackPanel stackPanel = (StackPanel)btn.Parent;
            DockPanel dockPanel = (DockPanel)stackPanel.Parent;
            Grid grid = (Grid)dockPanel.Parent;
            CardDetailizedDish itCardDetail = (CardDetailizedDish)grid.Parent;

            int indexToDel = MainWindow.listDishes.IndexOf(this.ItemDish);       // пошук в списку інгрідієнтів 
            if (indexToDel != -1) { MainWindow.listDishes.RemoveAt(indexToDel); return; }      // інгр. 
        }


        // Властивості

        public Dish ItemDish { get => itemDish; set => itemDish = value; }
        public Image ItemIcon
        {
            get => itemIcon;
            set => itemIcon = value;
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
        public string ItemDescription
        {
            get => itemName.Content.ToString();
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    itemDescription.Content = "Без опису";
                    return;
                }

                itemDescription.Content = value;
            }
        }
    }
}
