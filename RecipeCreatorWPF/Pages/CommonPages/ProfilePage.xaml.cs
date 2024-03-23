using RecipeCreatorWPF.ADO;
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

namespace RecipeCreatorWPF.Pages.CommonPages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            FillUserData();
        }

        private void FillUserData()
        {
            tbSurname.Text = App.CurrentUser.Surname;
            tbName.Text = App.CurrentUser.Name;
            tbPatronymic.Text = App.CurrentUser.Patronymic;
            tbLogin.Text = App.Connection.Account.FirstOrDefault(x => x.User_ID == App.CurrentUser.User_ID).Login;
            tbPassword.Text = App.Connection.Account.FirstOrDefault(x => x.User_ID == App.CurrentUser.User_ID).Password;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text != String.Empty)
            {
                App.Connection.Account.FirstOrDefault(x => x.User_ID == App.CurrentUser.User_ID).Password = tbPassword.Text;
                App.Connection.User.AddOrUpdate(App.CurrentUser);
                App.Connection.SaveChanges();

                MessageBox.Show("Пароль успешно изменен!", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            else
            {
                MessageBox.Show("Пароль не может быть пустым!", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            NavigationService.Navigate(new AuthPage());
            NavigationService.RemoveBackEntry();
        }
    }
}
