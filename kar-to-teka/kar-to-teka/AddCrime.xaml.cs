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
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    /// 
    public partial class AddCrime : Window
    {
        public int dlugosc = 0;
        string winny = "";

        class paragraf
        {
            public int numer { get; set; }
            public string nazwa { get; set; }
        }
        List<paragraf> listParagrafy= new List<paragraf> { };

        internal AddCrime(string par1)
        {
            winny = par1;

            var getPrzestepstwa = StartMongo.collectionPrzestepstwa.Find(new BsonDocument()).ToList();
            foreach (var x in getPrzestepstwa)
            {
                string tmp1 = x.GetElement("numer_paragrafu").ToString().Substring(16);
                string tmp2 = tmp1 + " " + x.GetElement("nazwa").ToString().Substring(6);
                int numer = Convert.ToInt32(tmp1);
                paragraf pom = new paragraf();
                pom.numer = numer;
                pom.nazwa = tmp2;
                listParagrafy.Add(pom);

            }

            InitializeComponent();

            paragrafik.DataContext = listParagrafy;
            paragrafik.DisplayMemberPath = "nazwa";
            paragrafik.SelectedValuePath = "numer";
        }

        private void Zatwierdz(object sender, RoutedEventArgs e)
        {

            if (paragrafik.SelectedValue == null)
            { System.Windows.MessageBox.Show("Najpierw wybierz paragraf");

                return;
            }

            BsonDocument doDodania= new BsonDocument();

                BsonElement element1 = new BsonElement("winny", new BsonString(winny.Substring(4)));
            BsonElement element2 = new BsonElement("paragraf", new BsonInt32((int)paragrafik.SelectedValue));
            BsonElement element3 = new BsonElement("data_skazania", new BsonDateTime((DateTime)data_skaz.SelectedDate));
            BsonElement element4 = new BsonElement("dlugosc_wyroku", new BsonInt32((int)pasek.Value));
            BsonElement element5 = new BsonElement("opis", new BsonString(Dodatkowy.Text));
            doDodania.Add(element1);
            doDodania.Add(element2);
            doDodania.Add(element3);
            doDodania.Add(element4);
            doDodania.Add(element5);

            StartMongo.collectionPopelnione.InsertOne(doDodania);

            DialogResult = true;
        }
    }
}
