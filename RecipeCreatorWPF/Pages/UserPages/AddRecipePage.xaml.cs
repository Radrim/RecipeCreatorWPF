using Microsoft.Win32;
using RecipeCreatorWPF.ADO;
using RecipeCreatorWPF.Pages.AdminPages;
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

namespace RecipeCreatorWPF.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for AddRecipePage.xaml
    /// </summary>
    public partial class AddRecipePage : Page
    {
        public AddRecipePage()
        {
            InitializeComponent();
            FillType();
            FillIngredients();
        }

        public byte[] RecipeImage;

        private void FillType()
        {
            cbType.Items.Clear();
            cbType.ItemsSource = App.Connection.FoodType.ToList();
        }

        private void FillIngredients()
        {
            lvIngredients.Items.Clear();
            lvIngredients.ItemsSource = App.Connection.Ingredient.ToList();
        }

        private void btnTakeImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                    RecipeImage = File.ReadAllBytes(dialog.FileName);

                    imgRecipe.Source = (new ImageSourceConverter()).ConvertFromString(dialog.FileName) as ImageSource;
                }
            }
            catch
            {
                MessageBox.Show("Только фото!", "Ошибка");
            }
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            bool IsAllDigits(string s) => int.TryParse(s, out int i);

            if (IsAllDigits(tbCookTime.Text))
            {
                if (!IsAllDigits(tbName.Text))
                {
                    if (tbName.Text != String.Empty && tbCookTime.Text != String.Empty && tbDescription.Text != String.Empty &&
                        tbCookMethod.Text != String.Empty && cbType.SelectedItem != null && imgRecipe.Source != null && lvIngredients.SelectedItem != null)
                    {

                        Recipe recipe = new Recipe() 
                        {
                            Name = tbName.Text,
                            CookTime = Convert.ToInt32(tbCookTime.Text),
                            Description = tbDescription.Text,
                            CookMethod = tbCookMethod.Text,
                            RecipeImage = RecipeImage,
                            FoodType_ID = (cbType.SelectedItem as FoodType).FoodType_ID,
                            User_ID = App.CurrentUser.User_ID,
                        };

                        App.Connection.Recipe.Add(recipe);

                        foreach (var item in lvIngredients.SelectedItems) 
                        {
                            App.Connection.Recipe_Ingredient.Add(new Recipe_Ingredient()
                            {
                                Recipe_ID = recipe.Recipe_ID,
                                Ingredient_ID = (item as Ingredient).Ingredient_ID,
                                Weight = (item as Ingredient).Weight
                            });
                        }

                        App.Connection.SaveChanges();

                        MessageBox.Show("Рецепт отправлен на рассмотрение!", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.Navigate(new MainClientPage());
                    }

                    else
                    {
                        MessageBox.Show("Заполните все данные!", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("Название не может содержать только числа!", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Время приготовления должно содержать только числа!", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.GoBack();
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            try
            {
                List<Ingredient> ingredients = App.Connection.Ingredient.ToList();
                lvIngredients.ItemsSource = ingredients
                    .Where(x => string.Join(" ", x.Name)
                    .ToLower()
                    .Contains(tbSearch.Text.ToLower()))
                    .ToList();
            }
            catch { }
        }
    }
}
