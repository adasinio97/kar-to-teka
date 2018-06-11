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
using System.Windows.Shapes;
using MongoDB.Bson;
using MongoDB.Driver;
using kar_to_teka;
using System.Windows.Threading;

namespace kar_to_teka
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddCriminal : Window
    {
        public AddCriminal()
        {
            InitializeComponent();
        }

        private void addPrzestepstwo(object sender, RoutedEventArgs e)
        {
            Window newWindow = new AddCrime();
            newWindow.Show();

        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void Zatwierdz(object sender, RoutedEventArgs e)
        {
            Window window = this;
            var areTextboxesNull = false;

            var kryminalista = new BsonDocument
            {
                {"imie", new BsonString(imie.Text)},
                {"nazwisko", new BsonString(nazwisko.Text)},
                {"pseudonim", new BsonString(pseudonim.Text)},
                //{"data_urodzenia", new BsonDateTime(data_urodzenia)},
                {"miejsce_urodzenia", new BsonString(urodzenie.Text)},
                {"miejsce_zameldowania", new BsonString(zamieszkanie.Text)},
                //{"poszukiwany", new BsonBoolean(poszukiwany)}
            };

            foreach (TextBox textBox in FindVisualChildren<TextBox>(window))
            {
                if (textBox.Text == "")
                {
                    areTextboxesNull = true;
                }
            }

            if (areTextboxesNull == false)
            {
                MessageBoxResult isUserSure = MessageBox.Show("Czy na pewno chcesz dodać obiekt do bazy danych, bez wszystkich wypełnionych pól?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (isUserSure == MessageBoxResult.Yes)
                {
                    StartMongo.collectionPrzestepcy.InsertOne(kryminalista);

                    var getData = new StringBuilder();
                    getData.Append("Dodano nowego przestepce: " + imie.Text + ", ");
                    getData.Append(nazwisko.Text + ", ");
                    getData.Append("pseudonim: " + pseudonim.Text + " do bazy danych.");
                    MessageBox.Show(getData.ToString(), "Rezultat");
                }
                else
                {
                    MessageBox.Show("Nie dodano nowego przestępcy do bazy danych.", "Rezultat");
                }
            }
        }

    }
}
