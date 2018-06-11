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

namespace kar_to_teka
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StartMongo startMongo = new StartMongo();
            startMongo.startMongoAsync();
        }

        private void addPrzestepca(object sender, RoutedEventArgs e)
        {
            Window newWindow = new AddCriminal();
            newWindow.Show();
        }

        private void showStatistics(object sender, RoutedEventArgs e)
        {
            Window newWindow = new Statistics();
            newWindow.Show();
        }

        private void dropCollections(object sender, RoutedEventArgs e)
        {

        }
    }
}
