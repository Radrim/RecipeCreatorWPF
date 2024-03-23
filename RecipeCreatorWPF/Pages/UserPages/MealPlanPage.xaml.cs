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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeCreatorWPF.Pages.UserPages
{
    /// <summary>
    /// Interaction logic for MealPlanPage.xaml
    /// </summary>
    public partial class MealPlanPage : Page
    {
        public MealPlanPage()
        {
            InitializeComponent();
            FillGenders();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void FillGenders() 
        {
            List<string> genders = new List<string>() 
            {
                "Мужской",
                "Женский"
            };

            cbGender.Items.Refresh();
            cbGender.ItemsSource = genders;
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (cbGender.SelectedItem != null && tbAge.Text != String.Empty && tbHeight.Text != 
                String.Empty && tbWeight.Text != String.Empty) 
            {
                string gender = cbGender.SelectedItem.ToString();
                tbResult.Text = Calc(gender);
            }

            else 
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private string Calc(string gender) 
        {
            double height = double.Parse(tbHeight.Text);
            double weight = double.Parse(tbWeight.Text);
            double age = double.Parse(tbAge.Text);

            string res = "";
            switch (gender)
            {
                case "Мужской":
                    res = "Ваша норма потребления " + (weight * 10 + height * 6.25 - age * 5 - 5).ToString() + " калорий в день.";
                    break;

                case "Женский":
                    res = "Ваша норма потребления " + (weight * 10 + height * 6.25 - age * 5 - 161).ToString() + " калорий в день.";
                    break;
            }

            return res;
        }
    }
}
