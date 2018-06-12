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

        public async Task startMongoAsync()
        {
            /* await db.CreateCollectionAsync("przestepstwa");
            await db.CreateCollectionAsync("przestepcy");
            await db.CreateCollectionAsync("miejsca"); */

            IMongoDatabase db = client.GetDatabase("kartoteka");

            collectionPrzestepstwa = db.GetCollection<BsonDocument>("przestepstwa");
            collectionPrzestepcy = db.GetCollection<BsonDocument>("przestepcy");
            collectionMiejsca = db.GetCollection<BsonDocument>("miejsca");

            // await collectionPrzestepstwa.Find(new BsonDocument()).ForEachAsync(x => listPrzestepcy.Add(x.ToString()));
            var getPrzestepcy = collectionPrzestepcy.Find(new BsonDocument()).ToList();
            foreach (var x in getPrzestepcy)
            {
                var name = x.GetElement("imie");
                var surname = x.GetElement("nazwisko");
                string fullName = name.ToString().Substring(5) + " " + surname.ToString().Substring(9);
                listPrzestepcy.Add(fullName);
            }

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
    }
}
