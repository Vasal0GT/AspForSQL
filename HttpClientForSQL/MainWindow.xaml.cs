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
using HttpClientForSQL;
using AspForSQL;
using Newtonsoft.Json;

namespace HttpClientForSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Library localLibrary = new Library();
        public MainWindow()
        {
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
    }
}
