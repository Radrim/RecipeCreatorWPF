using Microsoft.Win32;
using RecipeCreatorWPF.ADO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for IngredientInfoPage.xaml
    /// </summary>
    public partial class IngredientInfoPage : Page
    {
        private Ingredient _ingredient;
        public byte[] IngredientImage;
        public IngredientInfoPage(Ingredient ingredient)
        {
            _ingredient = ingredient;
            _ingredient.IngredientImage = ingredient.IngredientImage;
            InitializeComponent();
            DataContext = ingredient;
            cbFoodUnit.ItemsSource = App.Connection.FoodUnit.ToList();
            cbFoodUnit.SelectedValue = ingredient.FoodUnit;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text != String.Empty && cbFoodUnit.SelectedItem != null)
            {
                if (IngredientImage != null)
                {
                    App.Connection.Ingredient.AddOrUpdate(new Ingredient()
                    {
                        Ingredient_ID = _ingredient.Ingredient_ID,
                        Name = tbName.Text,
                        FoodUnit_ID = (cbFoodUnit.SelectedItem as FoodUnit).FoodUnit_ID,
                        IngredientImage = IngredientImage,
                    });

                    App.Connection.SaveChanges();

                    MessageBox.Show("Ингредиент успешно изменен!", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new IngredientsPage());
                }

                else
                {
                    App.Connection.Ingredient.AddOrUpdate(new Ingredient()
                    {
                        Ingredient_ID = _ingredient.Ingredient_ID,
                        Name = tbName.Text,
                        FoodUnit_ID = (cbFoodUnit.SelectedItem as FoodUnit).FoodUnit_ID,
                        IngredientImage = _ingredient.IngredientImage,
                    });

                    App.Connection.SaveChanges();

                    MessageBox.Show("Ингредиент успешно изменен!", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new IngredientsPage());
                }
            }

            else
            {
                MessageBox.Show("Заполните все данные!", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
