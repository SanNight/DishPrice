using System.ComponentModel;

namespace DishPrice.Data
{
    public class Dish : Ingridient
    {
        private BindingList<DishIngridient> dishIngridients;
        private double price;
        private string imgPath;

        public new double Price
        {
            get
            {
                price = 0;

                foreach (var dishIngrid in dishIngridients)
                {
                    // ціна * кількість 
                    if (double.IsNaN(dishIngrid.Amount))
                        dishIngrid.Amount = 0;

                    price += dishIngrid.Ingridient.Price * dishIngrid.Amount;
                }

                return price;
            }
        }
        public new string ImgPath
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
        public BindingList<DishIngridient> DishIngridients
        {
            get => dishIngridients; set => dishIngridients = value;
        }

        public Dish()
        {
            Name = "Без назви";
            DishIngridients = new BindingList<DishIngridient>();
        }

        public string GetDishWeight()
        {
            double totalWeight = 0;
            string remark = string.Empty;

            foreach (var ingrid in dishIngridients)
            {
                if (!Measures.ListAmountTypeNOTtoCalculation.Contains(ingrid.Ingridient.AmountType))
                    totalWeight += ingrid.Amount;
                else
                    remark = " *";
            }

            return totalWeight.ToString() + remark;
        }
    }
}
