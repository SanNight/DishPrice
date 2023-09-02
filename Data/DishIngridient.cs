using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishPrice.Data
{
    public class DishIngridient
    {
        Ingridient ingridient;
        double amount;

        DishIngridient()
        {
            ingridient = new Ingridient();
            amount = 0;
        }

        public DishIngridient(Ingridient ingridient)
        {
            this.ingridient = ingridient;
        }
        public Ingridient Ingridient { get => ingridient; set => ingridient = value; }
        public double Amount
        {
            get => amount;
            set
            {
                if (value < 0) value = 0;
                amount = value;
            }

        }
    }
}
