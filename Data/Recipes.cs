using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;

namespace DishPrice.Data
{
    public class Recipes
    {
        public string Name;
        public string Description;

        public static BindingList<Recipes> GetListFromFile()
        {
            using (var reader = File.OpenText(Settings.RecipesPath))
            {
                string listFromFile = reader.ReadToEnd();

                if (String.IsNullOrEmpty(listFromFile)) return new BindingList<Recipes>();
                else
                    return JsonConvert.DeserializeObject<BindingList<Recipes>>(listFromFile);
            }
        }

        public static void SaveList(BindingList<Recipes> listToSave)
        {
            File.WriteAllText(Settings.RecipesPath, JsonConvert.SerializeObject(listToSave));
        }
    }
}
