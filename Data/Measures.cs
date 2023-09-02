using System.Collections.Generic;
using System.Linq;

namespace DishPrice.Data
{
    class Measures
    {
        public static List<string> ListAmountType = new List<string> { "шт", "кг", "г", "л", "мл" };
        public static List<string> ListAmountTypeNOTtoCalculation = new List<string> { "шт", "л", "мл" };
        public static List<List<string>> ListForConvert = new List<List<string>>();

        private static void FillLists()
        {
            if (ListForConvert.Count == 0)
            {
                ListForConvert.Add(new List<string> { "кг", "л" });       // міри вищого порядку   // мв
                ListForConvert.Add(new List<string> { "г", "мл" });       // міри нижчого          // мн
                ListForConvert.Add(new List<string> { "1000", "1000" });     // співвідношення мн / мв
            }
        }

        public static double GetPrice(Ingridient ingridient)
        {
            FillLists();

            if (!ListAmountType.Contains(ingridient.AmountType))
                return -1000;

            return ingridient.FullPrice / ingridient.AmountPrimal;
        }

        public static void ConvertToHigher(Ingridient ingridient)
        {
            FillLists();

            // якщо міра задана нижчим порядком, то потрібно перетворити її у вищий
            int index = Measures.ListForConvert[1].IndexOf(ingridient.AmountType);   // пошук у списку з мірами нп
            if (index >= 0)
            {
                ingridient.AmountType = Measures.ListForConvert[0][index].ToString();
                string transf = Measures.ListForConvert[2][index];
                ingridient.AmountPrimal /= double.Parse(transf);
            }
        }
    }
}
