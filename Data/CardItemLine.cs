using System;
using System.Windows;
using System.Windows.Controls;

namespace DishPrice.Data
{
    public class CardLineIngrid : DockPanel
    {
        Label itemName = new Label(); // надпис-назва предмета 
        TextBox itemAmount = new TextBox();
        ComboBox itemAmountType = Services.GetAmountTypeComboBox();
        Ingridient itemIngridient;

        public CardLineIngrid()
        {
            ItemIngridient = new Ingridient();

            Height = 26;
            Width = 285;
            Background = Settings.ColorDishIngridients;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            LastChildFill = false;
 
            ItemName.Content = ItemIngridient.Name;
            ItemName.HorizontalAlignment = HorizontalAlignment.Stretch;

            ItemAmount.FontSize = 16;
            ItemAmount.MinWidth = 80;
            ItemAmount.Text = ItemIngridient.Amount.ToString();

            ItemAmountType.MinWidth = 45;
            ItemAmountType.SelectedItem = ItemIngridient.AmountType;

            SetDock(ItemName, Dock.Left);
            SetDock(ItemAmountType, Dock.Right);
            SetDock(ItemAmount, Dock.Right);
            Children.Add(ItemName);
            Children.Add(ItemAmountType);
            Children.Add(ItemAmount);
        }

        public CardLineIngrid(Ingridient ingridient)
        {
            ItemIngridient = ingridient;
            Height = 26;
            Width = 285;
            Background = Settings.ColorDishIngridients;
            HorizontalAlignment = HorizontalAlignment.Left;
            LastChildFill = false;

            ItemName.Content = ItemIngridient.Name;
            ItemName.HorizontalAlignment = HorizontalAlignment.Stretch;

            ItemAmount.FontSize = 14;
            ItemAmount.MinWidth = 80;
            ItemAmount.Text = ItemIngridient.Amount.ToString();

            ItemAmountType.MinWidth = 50;
            ItemAmountType.SelectedItem = ItemIngridient.AmountType;

            DockPanel.SetDock(ItemName, Dock.Left);
            DockPanel.SetDock(ItemAmountType, Dock.Right);
            DockPanel.SetDock(ItemAmount, Dock.Right);

            Children.Add(ItemName);
            Children.Add(ItemAmountType);
            Children.Add(ItemAmount);
        }

        public Label ItemName
        {
            get => itemName;
            set
            {
                if (String.IsNullOrWhiteSpace(value.Content.ToString()))
                {
                    itemName.Content = "Без імені";
                    return;
                }

                itemName.Content = value;
            }
        }
        public Ingridient ItemIngridient { get => itemIngridient; set => itemIngridient = value; }
        public TextBox ItemAmount { get => itemAmount; set => itemAmount = value; }
        public ComboBox ItemAmountType { get => itemAmountType; set => itemAmountType = value; }
    }
}
