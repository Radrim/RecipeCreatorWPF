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
    /// Interaction logic for RecipeInfoPage.xaml
    /// </summary>
    public partial class RecipeInfoPage : Page
    {
        private Recipe currentRecipe;
        List<int> oldData = new List<int>();

        public RecipeInfoPage(Recipe recipe)
        {
            InitializeComponent();
            currentRecipe = recipe;
            this.DataContext = currentRecipe;
            FillOldValue();
            FillIngredients();
        }

        private void FillOldValue() 
        {
            foreach (Recipe_Ingredient item in App.Connection.Recipe_Ingredient.Where(x => x.Recipe_ID == currentRecipe.Recipe_ID).ToList())
            {
                oldData.Add(item.Weight);
            }
        }

        private void FillIngredients() 
        {
            if (currentRecipe != null)
            {
                lvIngredients.Items.Refresh();
                lvIngredients.ItemsSource = App.Connection.Recipe_Ingredient.Where(x => x.Recipe_ID == currentRecipe.Recipe_ID).ToList();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tbCountDishes_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsAllDigits(string s) => int.TryParse(s, out int i);

            if (IsAllDigits(tbCountDishes.Text)) 
            {
                CalcCount(tbCountDishes.Text);   
            }

            if (tbCountDishes.Text == "")
            {
                CalcCount("1");
            }
        }

        private void CalcCount(string count) 
        {
            List<Recipe_Ingredient> ingredients = App.Connection.Recipe_Ingredient.Where(x => x.Recipe_ID == currentRecipe.Recipe_ID).ToList();
            int countDishes = Convert.ToInt32(count);

            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Weight = oldData[i];
            }

            foreach (Recipe_Ingredient item in ingredients) 
            {
                item.Weight *= countDishes;
            }

            lvIngredients.Items.Refresh();
            lvIngredients.ItemsSource = ingredients;
        }
    }
}
