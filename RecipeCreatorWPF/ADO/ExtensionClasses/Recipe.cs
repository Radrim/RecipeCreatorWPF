using IronBarCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RecipeCreatorWPF.ADO
{
    public partial class Recipe
    {
        public string Status => isChecked == null ? "На рассмотрении" : isChecked == true ? "Добавлен" : "Отклонен";
        public BitmapImage QRCode
        {
            get
            {
                using (var m = QRCodeWriter.CreateQrCode($"{App.Connection.Account.FirstOrDefault(x => x.User_ID == this.User_ID).Login}",
                      300, QRCodeWriter.QrErrorCorrectionLevel.Medium)
                  .ToPngStream())
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = m;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
        }
    }
}
