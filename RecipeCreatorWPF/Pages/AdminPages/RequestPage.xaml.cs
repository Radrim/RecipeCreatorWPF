using RecipeCreatorWPF.ADO;
using RecipeCreatorWPF.Pages.UserPages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        public RequestPage()
        {
            InitializeComponent();
            FillRecipesRequest();
        }

        private void FillRecipesRequest() 
        {
            lvRecipes.Items.Refresh();
            lvRecipes.ItemsSource = App.Connection.Recipe.Where(x => x.isChecked == null).ToList();
        }

        private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRecipes.SelectedItem != null)
                NavigationService.Navigate(new RecipeInfoPage(lvRecipes.SelectedItem as Recipe));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Recipe recipe = button.DataContext as Recipe;

            recipe.isChecked = true;
            App.Connection.Recipe.AddOrUpdate(recipe);
            App.Connection.SaveChanges();

            MessageBox.Show("Рецепт добавлен!", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
            FillRecipesRequest();
        }

        private void btnDisagree_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Recipe recipe = button.DataContext as Recipe;

            recipe.isChecked = false;
            App.Connection.Recipe.AddOrUpdate(recipe);
            App.Connection.SaveChanges();

            MessageBox.Show("Рецепт отклонен!", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
            FillRecipesRequest();
        }
    }
}
