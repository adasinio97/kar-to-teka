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
using System.Windows.Shapes;

namespace kar_to_teka
{
    /// <summary>
    /// Interaction logic for AddCrimes.xaml
    /// </summary>
    public partial class AddCrimes : Window
    {
        public AddCrimes()
        {
            InitializeComponent();
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

        private void addCrime(object sender, RoutedEventArgs e)
        {
            Window window = this;
            var crime = new BsonDocument();
            bool areTextboxesNull = true;

            foreach (TextBox textBox in FindVisualChildren<TextBox>(window))
            {
                if (String.IsNullOrEmpty(textBox.Text) || String.IsNullOrWhiteSpace(textBox.Text))
                {
                    areTextboxesNull = true;
                }
            }

            if (!String.IsNullOrWhiteSpace(numer.Text) || !String.IsNullOrEmpty(numer.Text))
            {
                BsonElement element = new BsonElement("numer_paragrafu", new BsonInt32(Convert.ToInt32(numer.Text)));
                crime.Add(element);
            }
            if (!String.IsNullOrWhiteSpace(nazwa.Text) || !String.IsNullOrEmpty(nazwa.Text))
            {
                BsonElement element = new BsonElement("nazwa", new BsonString(nazwa.Text));
                crime.Add(element);
            }
            if (!String.IsNullOrWhiteSpace(opis.Text) || !String.IsNullOrEmpty(opis.Text))
            {
                BsonElement element = new BsonElement("opis", new BsonString(opis.Text));
                crime.Add(element);
            }

            if (areTextboxesNull == false)
            {
                StartMongo.collectionPrzestepstwa.InsertOne(crime);

                var getData = new StringBuilder();
                getData.Append("Dodano nowe przestepstwo: paragraf numer" + numer.Text + ", o nazwie \"");
                getData.Append(nazwa.Text + "\" ");
                getData.Append("do bazy danych.");
                MessageBox.Show(getData.ToString(), "Rezultat");
            }
            else
            {
                MessageBoxResult isUserSure = MessageBox.Show("Czy na pewno chcesz dodać obiekt do bazy danych, bez wszystkich wypełnionych pól?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (isUserSure == MessageBoxResult.Yes && numer.Text != " " && nazwa.Text != " ")
                {
                    StartMongo.collectionPrzestepstwa.InsertOne(crime);

                    var getData = new StringBuilder();
                    getData.Append("Dodano nowe przestepstwo: paragraf numer " + numer.Text + ", o nazwie \"");
                    getData.Append(nazwa.Text + "\" ");
                    getData.Append("do bazy danych.");
                    MessageBox.Show(getData.ToString(), "Rezultat");
                }
                else
                {
                    MessageBox.Show("Nie dodano nowego przestępstwa do bazy danych. Podaj przynajmniej numer paragrafu i nazwę przestępstwa!", "Rezultat");
                }
            }
        }
    }
}
