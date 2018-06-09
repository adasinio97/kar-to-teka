﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public async Task startMongoAsync()
        {
            var client = new MongoClient();

            IMongoDatabase db = client.GetDatabase("kartoteka");

            /* await db.CreateCollectionAsync("przestepstwa");
            await db.CreateCollectionAsync("przestepcy");
            await db.CreateCollectionAsync("miejsca"); */

            collectionPrzestepstwa = db.GetCollection<BsonDocument>("przestepstwa");
            collectionPrzestepcy = db.GetCollection<BsonDocument>("przestepcy");
            collectionMiejsca = db.GetCollection<BsonDocument>("miejsca");

            var testDocument = new BsonDocument
            {
                {"imie", new BsonString("Jan")},
                {"nazwisko", new BsonString("Paweł")},
                {"data_urodzenia", new BsonDateTime(new DateTime(1939, 09, 01))},
                {"miejsce_urodzenia", new BsonString("Koło")},
                {"miejsce_zameldowania", new BsonString("Białystok")},
                {"poszukiwany", new BsonBoolean(true)}
            };

            collectionPrzestepcy.InsertOne(testDocument);
        }
    }
}
