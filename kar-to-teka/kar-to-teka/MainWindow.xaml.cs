using MongoDB.Bson;
using MongoDB.Driver;
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
        internal StartMongo startMongo = new StartMongo();

        public MainWindow()
        {
            InitializeComponent();

            startMongo.startMongoAsync();

            przestepcy.DataContext = startMongo.listCriminals;
            przestepcy.DisplayMemberPath = "imie";
            przestepcy.SelectedValuePath = "id";

            //przestepcy.ItemsSource = startMongo.listPrzestepcy;
        }

        private void addPrzestepca(object sender, RoutedEventArgs e)
        {
            Window newWindow = new AddCriminal();
            if (newWindow.ShowDialog() == true)
                System.Windows.MessageBox.Show("Operacja dodania zakończona pomyślnie");
            else
                System.Windows.MessageBox.Show("Coś poszło nie tak!");
            this.refreshvoid();

        }

        private void dropCollections(object sender, RoutedEventArgs e)
        {

        }

        private void chooseCriminal(object sender, SelectionChangedEventArgs e)
        {
            foreach (var x in startMongo.listCriminals)
            {
                if (x.id == przestepcy.SelectedValue)
                {
                    c2.Text = x.nazwisko;
                    c1.Text = x.imie.Substring(0, x.imie.Length - c2.Text.Length);
                    if (x.data_urodzenia != "")
                        c3.Text = x.data_urodzenia.Substring(0, 10);
                    else
                        c3.Text = "Nieznana";
                    if (x.miejsce_urodzenia != "")
                        c4.Text = x.miejsce_urodzenia;
                    else
                        c4.Text = "Brak danych!";
                    if (x.miejsce_zamieszkania != "")
                        c5.Text = x.miejsce_zamieszkania;
                    else
                        c5.Text = "Brak danych!";

                    if (x.poszukiwany==true)
                        c6.Text = "Przestępca jest poszukiwany!";
                    else
                        c6.Text = "Nie";
                    if (x.pseudonim != "")
                        c7.Text = x.pseudonim;
                    else
                        c7.Text = "Brak danych!";


                }
            }
        }

        private void refresh(object sender, RoutedEventArgs e)
        {
            startMongo.updateLists();
            przestepcy.Items.Refresh();
        }

        internal void refreshvoid()
        {
            startMongo.updateLists();
            przestepcy.Items.Refresh();
        }

        private void apdejt(object sender, RoutedEventArgs e)
        {
            if (przestepcy.SelectedValue == null)
            { System.Windows.MessageBox.Show("Najpierw wybierz przestępcę!");
                return;
            }


            EditCriminal okno = new EditCriminal(startMongo.listCriminals, (string)przestepcy.SelectedValue);


            if (okno.ShowDialog() == true)
                    System.Windows.MessageBox.Show("Operacja modyfikacji zakończona pomyślnie");
            else
                System.Windows.MessageBox.Show("Coś poszło nie tak!");
            this.refreshvoid();
        }
    }
}
