using System;
using System.Windows.Controls;
using DishPrice.UserXAMLElements;

namespace DishPrice.Data
{
    public class Ingridient
    {
        string name;            // назва
        double fullPrice;       // ціна за деяку кількість, що вказується користувачем
        double price;           // кінцева ціна за одиницю | = валюта / кількість
        string description;     // опис
        string imgPath;         // шлях до зображення на диску
        double amountPrimal;    // кількість, яка вказувалася при додаванні інгрідієнта
        double amount;          // актуальна кількість

        string amountType;      // міра
        public string Name
        {
            get => name;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    name = "Без назви";
                else
                    name = value;
            }

        }
        public double Price
        {
            get
            {
                price = Measures.GetPrice(this);
                return price;
            }
        }
        public double FullPrice { get => fullPrice; set => fullPrice = value; }
        public string Description
        {
            get => description;
            set
            {
                if (String.IsNullOrEmpty(value))
                    description = String.Empty;
                else
                    description = value;
            }
        }
        public string ImgPath
        {
            get
            {
                if (imgPath == null)
                    return Services.GetDefaultImg().Source.ToString();

                if (imgPath.Contains("file:///"))
                {
                    imgPath = imgPath.Remove(imgPath.IndexOf("file:///"), "file:///".Length);
                }

                return imgPath;
            }
            set => imgPath = value;
        }
        public double Amount { get => amount; set => amount = value; }
        public double AmountPrimal
        {
            get => amountPrimal;
            set
            {
                if (value < 0)
                    value = 0;

                amount = value;
                amountPrimal = value;
            }
        }
        public string AmountType
        {
            get
            {
                if (amountType == null)
                    amountType = "NONE";
                return amountType;
            }
            set
            {
                //Measures.ChangeAmountTypeToGeneral(this);
                amountType = value;
            }
        }

        public Ingridient()
        {
            Name = "Без назви";
            FullPrice = 0;
            AmountPrimal = 0;
            Amount = 0;
            AmountType = "кг";
            // ImgPath = Services.GetDefaultImg().Source.ToString();
        }

        public void DisplayTo(WrapPanel wrapPanel)
        {
            CardIngrid card = new CardIngrid(this);
            wrapPanel.Children.Add(card);
        }
    }
}
