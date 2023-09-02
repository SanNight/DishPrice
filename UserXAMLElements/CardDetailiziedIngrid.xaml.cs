using System;
using System.Windows;
using System.Windows.Controls;
using DishPrice.Data;
using DishPrice.Windows;

namespace DishPrice.UserXAMLElements
{
    public partial class CardDetailiziedIngrid : UserControl
    {
        Ingridient itemIngridient;
        WrapPanel parentPanel;

        public CardDetailiziedIngrid()
        {
            InitializeComponent();
            ItemIngridient = new Ingridient();
        }

        public CardDetailiziedIngrid(Ingridient ingridient, WrapPanel wrapPanel)
        {
            InitializeComponent();
            ItemIngridient = ingridient;
        }


        // EVENTS //

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            parentPanel = (WrapPanel)this.Parent;
            ItemIcon.Source = ServicesImage.GetBMImage(itemIngridient.ImgPath);
            ItemName = itemIngridient.Name;
            ItemDescription = itemIngridient.Description;
            itemPrice.Content = itemIngridient.Price.ToString();
            itemAmountType.Content = itemIngridient.AmountType;

            Services.CollapseLbIfEmpty(itemDescription);
            Services.CollapseLbIfEmpty(itemSynonims);
            Services.CollapseLbIfEmpty(itemTags);
        }

        private void btnEditIngrid_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            StackPanel stackPanel = (StackPanel)btn.Parent;
            DockPanel dockPanel = (DockPanel)stackPanel.Parent;
            Grid grid = (Grid)dockPanel.Parent;
            CardDetailiziedIngrid itCardDetail = (CardDetailiziedIngrid)grid.Parent;

            WinIngridAdd winIngridAdd = new WinIngridAdd(itCardDetail.ItemIngridient);
            winIngridAdd.ShowDialog();
        }

        private void btnDelIngrid_Click(object sender, RoutedEventArgs e)
        {
            CardDetailiziedIngrid itCardDetail = GetCardDetail(sender);                             // отримання картки
            int indexToDel = MainWindow.listIngridients.IndexOf(itCardDetail.ItemIngridient);       // пошук в списку інгрідієнтів 

            if (indexToDel >= 0)
                MainWindow.listIngridients.RemoveAt(indexToDel);                                    // інгр. знайдено
            else
            {
                MessageBox.Show("Не знайдено, не видалено.");
                return;
            }

            // переключення на 
            if (indexToDel < MainWindow.listIngridients.Count && indexToDel >= 0)           // наступний елемент
                Services.ShowCardDetailsIngrid(parentPanel, MainWindow.listIngridients[indexToDel]);
            else if(indexToDel-1 < MainWindow.listIngridients.Count && indexToDel > 0)     // попередній
                Services.ShowCardDetailsIngrid(parentPanel, MainWindow.listIngridients[indexToDel - 1]);
        }


        // ВЛАСТИВОСТІ //

        public Ingridient ItemIngridient { get => itemIngridient; set => itemIngridient = value; }
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
                    itemDescription.Content = String.Empty;
                    return;
                }

                itemDescription.Content = value;
            }
        }

        // USER'S METHODS //

        private CardDetailiziedIngrid GetCardDetail(object sender)
        {
            Button btn = (Button)sender;
            StackPanel stackPanel = (StackPanel)btn.Parent;
            DockPanel dockPanel = (DockPanel)stackPanel.Parent;
            Grid grid = (Grid)dockPanel.Parent;

            return (CardDetailiziedIngrid)grid.Parent;
        }
    }
}
