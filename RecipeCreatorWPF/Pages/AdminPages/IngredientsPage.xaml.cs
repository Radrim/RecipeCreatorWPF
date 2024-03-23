using RecipeCreatorWPF.ADO;
using RecipeCreatorWPF.Pages.UserPages;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for IngredientsPage.xaml
    /// </summary>
    public partial class IngredientsPage : Page
    {
        public IngredientsPage()
        {
            Loaded += IngredientsPage_Loaded;
            InitializeComponent();
            FillIngredients();
        }

        private void IngredientsPage_Loaded(object sender, RoutedEventArgs e)
        {
            FillIngredients();
        }

        private void FillIngredients() 
        {
            lvIngredients.Items.Refresh();
            lvIngredients.ItemsSource = App.Connection.Ingredient.ToList();
        }
        private void lvIngredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvIngredients.SelectedItem != null)
                NavigationService.Navigate(new IngredientInfoPage(lvIngredients.SelectedItem as Ingredient));
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddIngredientPage());
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
