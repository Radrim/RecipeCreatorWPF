using RecipeCreatorWPF.ADO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RecipeCreatorWPF.Pages.CommonPages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            bool IsAllDigits(string s) => int.TryParse(s, out int i);

            if (!IsAllDigits(tbSurname.Text) && !IsAllDigits(tbName.Text) && !IsAllDigits(tbPatronymic.Text))
            {
                if (tbSurname.Text != "" && tbName.Text != "" && tbPatronymic.Text != "" &&
                    tbLogin.Text != "" && pbPassword.Password != "")
                {
                    Account account = App.Connection.Account.FirstOrDefault(x => x.Login == tbLogin.Text);

                    if (account != null)
                    {
                        MessageBox.Show("Пользователь с таким логином уже создан!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    else
                    {
                        User newUser = new User()
                        {
                            Surname = tbSurname.Text,
                            Name = tbName.Text,
                            Patronymic = tbPatronymic.Text,
                            Role_ID = 2
                        };
                        App.Connection.User.Add(newUser);

                        Account newAccount = new Account()
                        {
                            Login = tbLogin.Text,
                            Password = pbPassword.Password,
                            User_ID = newUser.User_ID
                        };

                        App.Connection.Account.Add(newAccount);
                        App.Connection.SaveChanges();

                        MessageBox.Show("Вы успешно зарегистрировались!");
                        NavigationService.Navigate(new AuthPage());
                    }
                }

                else
                {
                    MessageBox.Show("Некорректные данные! Все поля должны быть заполнены", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else MessageBox.Show("ФИО не может содержать числа!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
