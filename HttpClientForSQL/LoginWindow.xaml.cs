using System.Threading.Tasks;
using System.Windows;
using HttpClientForSQL;
using AspForSQL;
using AspForSQL;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using HttpClientForSQL.Models;

namespace HttpClientForSQL
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            APIServer api = new APIServer();
        }

        private async void LoginBN_Click(object sender, RoutedEventArgs e)
        {
            await Login();
        }

        private async Task Login()
        {
            await Task.Run(async () =>
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    APIServer api = new APIServer();
                    UserDTOclient userDTO = new UserDTOclient
                    {
                        UserName = LoginTB.Text,
                        Pasword = PasswordTB.Text
                    };

                    var response = await api.LoginAsync(userDTO);
                    if (response != null)
                    {
                        Info.Text = response.AccesToken;

                        MainWindow maiqn = new MainWindow(response.AccesToken);
                        maiqn.Show();
                    }
                    else 
                    {
                        Info.Text = "null";
                    }
                });
            });
        }
    }
}
