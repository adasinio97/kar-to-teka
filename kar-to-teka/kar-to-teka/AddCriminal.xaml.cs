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

        private void Zatwierdz(object sender, RoutedEventArgs e)
        {

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
            System.Diagnostics.Debug.WriteLine(imie.Text);
            System.Diagnostics.Debug.WriteLine(nazwisko.Text);
            StartMongo.collectionPrzestepcy.InsertOne(kryminalista);
        }

    }
}
