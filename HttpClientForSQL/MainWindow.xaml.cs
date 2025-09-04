using AspForSQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HttpClientForSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _accessToken;
        Library localLibrary = new Library();
        public MainWindow(string token)
        {
            _accessToken = token;
            InitializeComponent();
        }

        public async Task CombineToLibrary()
        { 
            localLibrary.Name = Name_TB.Text;
            localLibrary.Year = Int32.Parse(Year_TB.Text);
            localLibrary.Author = Author_TB.Text;
        }

        private async void Post_Button_Click(object sender, RoutedEventArgs e)
        {
            APIServer aPIServer = new APIServer();
            await CombineToLibrary();
            int answer = await aPIServer.PostLibraryAsync(localLibrary);
            StatusCode_Label.Content = answer;
            await ChangeStatusColor(answer);
        }

        private async Task ChangeStatusColor(int content)
        {
            string str = content.ToString();
            char first = str[0];
            switch (first) 
            {
                case '2':
                    StatusCode_Label.Foreground = Brushes.Green;
                        break;
                case '4':
                    StatusCode_Label.Foreground = Brushes.Red;
                    break;
                default:
                    StatusCode_Label.Foreground= Brushes.Purple;
                    break;
            }
                
        }

        private async void Get_Button_Click(object sender, RoutedEventArgs e)
        {
            APIServer api = new APIServer();
            int id = Int32.Parse(Id_TB.Text);
            var answer = await api.GetLibraryAsync(id);
            GetResult_TB.Text = $"{answer.Author}, {answer.Name}, {answer.Year}";

        }

        private async void Put_Button_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            { 
                APIServer api = new APIServer();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Library lib = new Library
                    {
                        Author = AuthorForPut_TB.Text,
                        Name = NameForPut_TB.Text,
                        Year = Int32.Parse(YearForPut_TB.Text),
                        Id = Int32.Parse(IdForPut_TB.Text)
                    };


                    var id = Int32.Parse(IdForPut_TB.Text);

                    var answer = api.UpdateLibraryAsync(id, lib);
                    StatusCode_Label.Content = answer;
                    Console.WriteLine(answer);
                });

                //ChangeStatusColor(answer);
            });

        }

        private async void GetAll_Button_Click(object sender, RoutedEventArgs e)
        {
            APIServer api = new APIServer();
            api.editHeader(_accessToken);
            var answer = await api.GetLibrariesAsync();
            string json = JsonConvert.SerializeObject(answer, Formatting.Indented);
            GetAll_TB.Text = json;
        }
    }
}
