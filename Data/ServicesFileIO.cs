using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;

namespace DishPrice.Data
{
    class ServicesFileIO
    {
        public static string DIRECTORY = Path.Combine(Directory.GetCurrentDirectory(), "Saves");
        public static string FILE_INGR = Path.Combine(DIRECTORY, "Ingridients.json");
        public static string FILE_DISHES = Path.Combine(DIRECTORY, "Dishes.json");
        public static string FILE_CONFIGURATION = Path.Combine(Directory.GetCurrentDirectory(), "Config.txt");

        private static void CheckSaveDirectory()
        {
            if (!Directory.Exists(DIRECTORY)) Directory.CreateDirectory(DIRECTORY);
        }
        public static void CheckFile()
        {
            CheckSaveDirectory();

            if (!File.Exists(FILE_INGR)) File.Create(FILE_INGR);
            if (!File.Exists(FILE_DISHES)) File.Create(FILE_DISHES);
        }

        public static void SaveList(BindingList<Ingridient> objectToSave)
        {
            File.WriteAllText(FILE_INGR, JsonConvert.SerializeObject(objectToSave));
        }

        public static void SaveList(BindingList<Dish> objectToSave)
        {
            File.WriteAllText(FILE_DISHES, JsonConvert.SerializeObject(objectToSave));
        }

        public static BindingList<Ingridient> LoadIngridientsList()
        {
            using (var reader = File.OpenText(FILE_INGR))
            {
                string listFromFile = reader.ReadToEnd();

                if (String.IsNullOrEmpty(listFromFile)) return new BindingList<Ingridient>();
                else
                    return JsonConvert.DeserializeObject<BindingList<Ingridient>>(listFromFile);
            }
        }

        public static BindingList<Dish> LoadDishesList()
        {
            using (var reader = File.OpenText(FILE_DISHES))
            {
                string listFromFile = reader.ReadToEnd();

                if (String.IsNullOrEmpty(listFromFile)) return new BindingList<Dish>();
                else
                    return JsonConvert.DeserializeObject<BindingList<Dish>>(listFromFile);
            }
        }
    }
}
