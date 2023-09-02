using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DishPrice.Data
{
    public class ServicesImage
    {
        public static string ImgDefaultPath = "C:\\Users\\ПК\\source\\repos\\DishPrice\\DishPrice\\bin\\Debug\\Res\\no img.png";

        public static BitmapImage GetBMImage(string imgPath)
        {
            Uri fileUri;

            if (File.Exists(imgPath))
                fileUri = new Uri(imgPath, UriKind.Absolute);
            else
                fileUri = new Uri(ImgDefaultPath, UriKind.Absolute);

            return new BitmapImage(fileUri);
        }
    }
}
