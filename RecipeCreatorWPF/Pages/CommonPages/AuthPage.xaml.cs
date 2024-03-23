using RecipeCreatorWPF.ADO;
using RecipeCreatorWPF.Pages.AdminPages;
using RecipeCreatorWPF.Pages.UserPages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RecipeCreatorWPF.Pages.CommonPages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void btnGoToRegistrationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLogin.Text) && !String.IsNullOrEmpty(pbPassword.Password))
            {
                var account = App.Connection.Account.FirstOrDefault(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);

                if (account != null)
                {
                    var user = App.Connection.User.FirstOrDefault(x => x.User_ID == account.User_ID);

                    App.CurrentUser = user;

                    switch (App.Connection.Role.FirstOrDefault(x => x.Role_ID == user.Role_ID).Title)
                    {
                        case "Администратор":
                            NavigationService.Navigate(new MainAdminPage());
                            break;
                        case "Пользователь":
                            NavigationService.Navigate(new MainClientPage());
                            break;
                        default:
                            MessageBox.Show("Произошла ошибка. Попробуйте снова позже!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
