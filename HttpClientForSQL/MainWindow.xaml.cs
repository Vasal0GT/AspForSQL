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
            Id_label.Content = answer;
        }
    }
}
