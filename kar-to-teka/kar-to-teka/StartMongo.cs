using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kar_to_teka
{
    class StartMongo
    {
        public static IMongoCollection<BsonDocument> collectionPrzestepstwa = null;
        public static IMongoCollection<BsonDocument> collectionPrzestepcy = null;
        public static IMongoCollection<BsonDocument> collectionMiejsca = null;

        public MongoClient client = new MongoClient();
        public List<string> listPrzestepcy = new List<string>();
        public List<Criminal> listCriminals = new List<Criminal>();

        public async Task startMongoAsync()
        {
            /* await db.CreateCollectionAsync("przestepstwa");
            await db.CreateCollectionAsync("przestepcy");
            await db.CreateCollectionAsync("miejsca"); */

            IMongoDatabase db = client.GetDatabase("kartoteka");

            collectionPrzestepstwa = db.GetCollection<BsonDocument>("przestepstwa");
            collectionPrzestepcy = db.GetCollection<BsonDocument>("przestepcy");
            collectionMiejsca = db.GetCollection<BsonDocument>("miejsca");

            updateLists();

            var testDocument = new BsonDocument
            {
                {"imie", new BsonString("Jan")},
                {"nazwisko", new BsonString("Paweł")},
                {"data_urodzenia", new BsonDateTime(new DateTime(1939, 09, 01))},
                {"miejsce_urodzenia", new BsonString("Koło")},
                {"miejsce_zameldowania", new BsonString("Białystok")},
                {"poszukiwany", new BsonBoolean(true)}
            };

            // collectionPrzestepcy.InsertOne(testDocument);
        }

        public void updateLists()
        {
            listPrzestepcy.Clear();
            listCriminals.Clear();

            var getPrzestepcy = collectionPrzestepcy.Find(new BsonDocument()).ToList();
            foreach (var x in getPrzestepcy)
            {
                var name = x.GetElement("imie");
                var surname = x.GetElement("nazwisko");
                string fullName = name.ToString().Substring(5) + " " + surname.ToString().Substring(9);
                listPrzestepcy.Add(fullName);

                Criminal criminal = new Criminal();
                criminal.id = x.GetElement("_id").ToString();
                if (x.GetElement("imie").ToString() == null)
                {
                    criminal.imie = "";
                }
                else
                {
                    criminal.imie = x.GetElement("imie").ToString().Substring(5);
                }
                if (x.GetElement("nazwisko").ToString() == null)
                {
                    criminal.nazwisko = "";
                }
                else
                {
                    criminal.nazwisko = x.GetElement("nazwisko").ToString().Substring(9);
                    criminal.imie = x.GetElement("imie").ToString().Substring(5) + " " + criminal.nazwisko;
                }

                var v = x.GetValue("data_urodzenia", null);
                if (v == null)
                {
                    criminal.data_urodzenia = "";
                }
                else
                {
                    criminal.data_urodzenia = x.GetElement("data_urodzenia").ToString().Substring(15);
                }
                var v1 = x.GetValue("miejsce_urodzenia", null);
                if (v1==null)
                {
                    criminal.miejsce_urodzenia = "";
                }
                else
                {
                    criminal.miejsce_urodzenia = x.GetElement("miejsce_urodzenia").ToString().Substring(18);
                }

                var v2 = x.GetValue("miejsce_zamieszkania", null);
                if (v2 == null)
                {
                    criminal.miejsce_zamieszkania = "";
                }
                else
                {
                    criminal.miejsce_zamieszkania = x.GetElement("miejsce_zamieszkania").ToString().Substring(21);
                }
                criminal.poszukiwany = Convert.ToBoolean(x.GetElement("poszukiwany").ToString().Substring(12));
                var v3 = x.GetValue("pseudonim", null);
                if (v3 == null)
                {
                    criminal.pseudonim = "";
                }
                else
                {
                    criminal.pseudonim = x.GetElement("pseudonim").ToString().Substring(10);
                }

                listCriminals.Add(criminal);

            }  
        }
    }
}
