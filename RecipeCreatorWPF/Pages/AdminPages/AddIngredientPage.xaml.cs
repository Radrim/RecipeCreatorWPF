using Microsoft.Win32;
using RecipeCreatorWPF.ADO;
using RecipeCreatorWPF.Pages.UserPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeCreatorWPF.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for AddIngredientPage.xaml
    /// </summary>
    public partial class AddIngredientPage : Page
    {
        public AddIngredientPage()
        {
            InitializeComponent();
            FillFoodUnit();
        }

        public byte[] IngredientImage;

        private void FillFoodUnit() 
        {
            cbFoodUnit.Items.Clear();
            cbFoodUnit.ItemsSource = App.Connection.FoodUnit.ToList();
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            bool IsAllDigits(string s) => int.TryParse(s, out int i);

                if (!IsAllDigits(tbName.Text))
                {
                    if (tbName.Text != String.Empty && cbFoodUnit.SelectedItem != null && imgIngredient.Source != null)
                    {
                        App.Connection.Ingredient.Add(new Ingredient()
                        {
                            Name = tbName.Text,
                            IngredientImage = IngredientImage,
                            FoodUnit_ID = (cbFoodUnit.SelectedItem as FoodUnit).FoodUnit_ID,
                        });

                        App.Connection.SaveChanges();

                        MessageBox.Show("Ингредиент успешно добавлен!", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.Navigate(new MainAdminPage());
                    }

                    else
                    {
                        MessageBox.Show("Заполните все данные!", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("Название не может содержать только числа!", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnTakeImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                    IngredientImage = File.ReadAllBytes(dialog.FileName);

                    imgIngredient.Source = (new ImageSourceConverter()).ConvertFromString(dialog.FileName) as ImageSource;
                }
            }
            catch
            {
                MessageBox.Show("Только фото!", "Ошибка");
            }
        }
    }
}
