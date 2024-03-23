using IronBarCode;
using MarketplaceApp.Classes;
using RecipeCreatorWPF.ADO;
using RecipeCreatorWPF.Pages.CommonPages;
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
    /// Interaction logic for MainClientPage.xaml
    /// </summary>
    public partial class MainClientPage : Page
    {
        private readonly Func<Recipe, bool> _filterQuery = x => true;
        private Func<Recipe, object> _sortQuery = x => x.Recipe_ID;
        private List<Recipe> _sorted;
        private static readonly FoodType _allCategory = new FoodType() { Title = "Все" };
        private List<Recipe> Recipes { get; set; }
        private List<Recipe> SortedRecipes { get; set; }

        public MainClientPage()
        {
            Loaded += MainClientPage_Loaded;
            InitializeComponent();
            GetSortingAndFiltering();
            GetList();
        }

        private void MainClientPage_Loaded(object sender, RoutedEventArgs e)
        {
            GetList();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
        }

        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainClientPage());
        }

        private void GetSortingAndFiltering()
        {
            cbSort.ItemsSource = SortingClass.Methods;

            var categoryList = App.Connection.FoodType.ToList();
            categoryList.Add(_allCategory);
            cbFilter.ItemsSource = categoryList;

            cbSort.SelectedIndex = -1;
            cbFilter.SelectedIndex = -1;
        }

        private void GetList()
        {
            Recipes = App.Connection.Recipe.Where(x => x.isChecked == true).OrderByDescending(x => x.Recipe_ID).ToList();
            SortedRecipes = Recipes;
            lvRecipes.ItemsSource = SortedRecipes;
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            try
            {
                lvRecipes.ItemsSource = _sorted
                    .Where(x => string.Join(" ", x.Name)
                    .ToLower()
                    .Contains(tbSearch.Text.ToLower()))
                    .ToList();
            }
            catch { }
        }

        private void SortingSelect(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    _sortQuery = x => x.Recipe_ID;
                    break;
                case 1:
                    _sortQuery = x => x.CookTime;
                    break;
                case 2:
                    _sortQuery = x => -x.CookTime;
                    break;
            }

            FilterAndSort();
        }

        private void FilteringSelect(object sender, SelectionChangedEventArgs e)
        {
            var categorySortComboBoxSelectedItem = cbFilter.SelectedItem as FoodType;

            if (categorySortComboBoxSelectedItem.Title.Equals("Все"))
                SortedRecipes = Recipes;
            else
                SortedRecipes = Recipes.Where(z => z.FoodType.Equals(categorySortComboBoxSelectedItem)).ToList();
            FilterAndSort();
        }

        private void FilterAndSort()
        {
            _sorted = SortedRecipes.Where(x => _filterQuery(x)).OrderBy(x => _sortQuery(x)).ToList();
            lvRecipes.ItemsSource = SortedRecipes.Where(x => _filterQuery(x)).OrderBy(x => _sortQuery(x)).ToList();

            if (tbSearch.Text != "")
                Search();
        }

        private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRecipes.SelectedItem != null)
            NavigationService.Navigate(new RecipeInfoPage(lvRecipes.SelectedItem as Recipe));
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRecipePage());
        }

        private void btnMyRecipes_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyRecipesPage());
        }

        private void btnMealPlan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MealPlanPage());
        }
    }
}
