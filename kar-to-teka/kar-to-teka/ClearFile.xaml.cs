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
    /// Interaction logic for ClearFile.xaml
    /// </summary>
    public partial class ClearFile : Window
    {
        class Crime
        {
            public string idOfCriminal { get; set; }
            public string paragraphNumber { get; set; }
            public string dateOfConviction { get; set; }
            public string lengthOfSentence { get; set; }
            public string descriptionOfCrime { get; set; }
            public string key { get; set; }
        }
        List<Crime> listOfCrimes = new List<Crime>();
        string tempKey;

        internal ClearFile(string parameterOne)
        {
            InitializeComponent();

            var temporaryList = StartMongo.collectionPopelnione.Find(new BsonDocument()).ToList();
            tempKey = parameterOne;
            foreach (var x in temporaryList)
            {
                var tempObject = x.GetElement("winny");
                var tempId = tempObject.ToString().Substring(6);
                var tempParameter = parameterOne.Substring(4);

                if (tempParameter == tempId)
                {
                    Crime temporaryCrime = new Crime();
                    temporaryCrime.key = x.GetValue("_id").ToString();
                    var id = x.GetValue("winny", null);
                    if (id == null)
                    {
                        temporaryCrime.idOfCriminal = "";
                    }
                    else
                    {
                        temporaryCrime.idOfCriminal = x.GetElement("winny").ToString().Substring(6);
                    }

                    var number = x.GetValue("paragraf", null);
                    if (number == null)
                    {
                        temporaryCrime.paragraphNumber = "";
                    }
                    else
                    {
                        temporaryCrime.paragraphNumber = x.GetElement("paragraf").ToString().Substring(9);
                    }

                    var date = x.GetValue("data_skazania", null);
                    if (date == null)
                    {
                        temporaryCrime.dateOfConviction = "";
                    }
                    else
                    {
                        temporaryCrime.dateOfConviction = x.GetElement("data_skazania").ToString().Substring(14, 10);
                    }

                    var length = x.GetValue("dlugosc_wyroku", null);
                    if (length == null)
                    {
                        temporaryCrime.lengthOfSentence = "---";
                    }
                    else
                    {
                        temporaryCrime.lengthOfSentence = x.GetElement("dlugosc_wyroku").ToString().Substring(15);
                    }

                    var description = x.GetValue("opis", null);
                    if (description == null)
                    {
                        temporaryCrime.descriptionOfCrime = "---";
                    }
                    else
                    {
                        temporaryCrime.descriptionOfCrime = x.GetElement("opis").ToString().Substring(5);
                    }

                    listOfCrimes.Add(temporaryCrime);
                }
            }

            wybor.DataContext = listOfCrimes;
            wybor.DisplayMemberPath = "paragraphNumber";
            wybor.SelectedValuePath = "key";
        }

        private void chooseCrime(object sender, SelectionChangedEventArgs e)
        {
            foreach (var x in listOfCrimes)
            {
                if (x.key == (string)wybor.SelectedValue)
                {   
                    numer.Text = x.paragraphNumber;
                    data.Text = x.dateOfConviction;
                    opis.Text = x.descriptionOfCrime;
                }
            }
        }

        private void deleteCrime(object sender, RoutedEventArgs e)
        {
            if (wybor.SelectedValue != null)
            {
                StartMongo.collectionPopelnione.FindOneAndDeleteAsync(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(wybor.SelectedValue.ToString())));
                DialogResult = true;
            }
            else
            {
                System.Windows.MessageBox.Show("Musisz wybrać wpis do usunięcia");
                return;
            }
        }
    }
}

