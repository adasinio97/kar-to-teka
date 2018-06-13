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
    public partial class EditCriminal : Window
    {
        private bool isDateKnown = true;
        private bool isWanted = false;
        public string doEdycji { get; set; }
        internal List<Criminal> info = new List<Criminal>();

        internal EditCriminal(List<Criminal> par1, string par2)
        {
            InitializeComponent();
            info = par1;
            doEdycji = par2;
            foreach (var x in info)
            {
                if (x.id == doEdycji)
                {
                    nazwiskoe.Text = x.nazwisko;
                    imiee.Text = x.imie.Substring(0, x.imie.Length - nazwiskoe.Text.Length);
                    if (x.data_urodzenia != "")
                    {
                        data_urodzeniae.DisplayDate = DateTime.ParseExact(x.data_urodzenia.Substring(0, 10), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        data_urodzeniae.SelectedDate = data_urodzeniae.DisplayDate;
                    }
                    urodzeniee.Text = x.miejsce_urodzenia;
                    
                        zamieszkaniee.Text = x.miejsce_zamieszkania;

                    pseudonime.Text = x.pseudonim;


                }
            }

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
            
           
            var kryminalista = new BsonDocument();

            foreach (TextBox textBox in FindVisualChildren<TextBox>(window))
            {
                if (textBox.Text == " " || textBox.Text == null)
                {
                    areTextboxesNull = true;
                }
            }

            if (!String.IsNullOrWhiteSpace(imiee.Text) || !String.IsNullOrEmpty(imiee.Text))
            {
                BsonElement element = new BsonElement("imie", new BsonString(imiee.Text));
                kryminalista.Add(element);
            }
            if (!String.IsNullOrWhiteSpace(nazwiskoe.Text) || !String.IsNullOrEmpty(nazwiskoe.Text))
            {
                BsonElement element = new BsonElement("nazwisko", new BsonString(nazwiskoe.Text));
                kryminalista.Add(element);
            }
            if (!String.IsNullOrWhiteSpace(pseudonime.Text) || !String.IsNullOrEmpty(pseudonime.Text))
            {
                BsonElement element = new BsonElement("pseudonim", new BsonString(pseudonime.Text));
                kryminalista.Add(element);
            }
            if (!String.IsNullOrWhiteSpace(urodzeniee.Text) || !String.IsNullOrEmpty(urodzeniee.Text))
            {
                BsonElement element = new BsonElement("miejsce_urodzenia", new BsonString(urodzeniee.Text));
                kryminalista.Add(element);
            }
            if (!String.IsNullOrWhiteSpace(zamieszkaniee.Text) || !String.IsNullOrEmpty(zamieszkaniee.Text))
            {
                BsonElement element = new BsonElement("miejsce_zamieszkania", new BsonString(zamieszkaniee.Text));
                kryminalista.Add(element);
            }

            if (isDateKnown == true)
            {
                BsonElement element = new BsonElement("data_urodzenia", new BsonDateTime((DateTime)data_urodzeniae.SelectedDate));
                kryminalista.Add(element);
            }
            if ((bool)poszukiwany.IsChecked)
            {
                BsonElement element = new BsonElement("poszukiwany", new BsonBoolean(true));
                kryminalista.Add(element);
            }
            else
            {
                BsonElement element = new BsonElement("poszukiwany", new BsonBoolean(false));
                kryminalista.Add(element);
            }

            if (areTextboxesNull == false)
            {
                MessageBoxResult isUserSure = MessageBox.Show("Czy na pewno chcesz dodać obiekt do bazy danych, bez wszystkich wypełnionych pól?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (isUserSure == MessageBoxResult.Yes && imiee.Text!="" &&nazwiskoe.Text!="")
                {
                    StartMongo.collectionPrzestepcy.FindOneAndReplaceAsync(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(doEdycji.Substring(4))),kryminalista);

                    var getData = new StringBuilder();
                    getData.Append("Zaktualizowano: " + imiee.Text + ", ");
                    getData.Append(nazwiskoe.Text + ", ");
                    getData.Append("pseudonim: " + pseudonime.Text + " w bazie danych.");
                    MessageBox.Show(getData.ToString(), "Rezultat");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Nie można edytować. Podaj przynajmniej imię i nazwisko!", "Rezultat");
                    DialogResult = false;
                }
            } 
        }

        private void isDateChecked(object sender, RoutedEventArgs e)
        {
            isDateKnown = false;
        }
        private void isDateUnchecked(object sender, RoutedEventArgs e)
        {
            isDateKnown = true;
        }

        private void isWantedChecked(object sender, RoutedEventArgs e)
        {
            isWanted = true;
        }
        private void isWantedUnhecked(object sender, RoutedEventArgs e)
        {
            isWanted = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartMongo.collectionPrzestepcy.FindOneAndDeleteAsync(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(doEdycji.Substring(4))));
            MessageBox.Show("Usunięto z bazy!");
            DialogResult = true;
        }
    }
}
