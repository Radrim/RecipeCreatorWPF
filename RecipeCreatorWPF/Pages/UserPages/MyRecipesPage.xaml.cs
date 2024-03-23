using RecipeCreatorWPF.ADO;
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

namespace RecipeCreatorWPF.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for MyRecipesPage.xaml
    /// </summary>
    public partial class MyRecipesPage : Page
    {
        public MyRecipesPage()
        {
            InitializeComponent();
            FillRecipes();
        }

        private void FillRecipes() 
        {
            lvRecipes.Items.Refresh();
            lvRecipes.ItemsSource = App.Connection.Recipe.Where(x => x.User_ID == App.CurrentUser.User_ID).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRecipes.SelectedItem != null)
                NavigationService.Navigate(new RecipeInfoPage(lvRecipes.SelectedItem as Recipe));
        }
    }
}
