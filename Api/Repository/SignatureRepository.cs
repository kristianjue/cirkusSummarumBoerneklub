﻿using Core;
using MongoDB.Driver;

namespace Api.Repository
{
    public class SignatureRepository : ISignatureRepository
    {
        private IMongoDatabase database;
        private IMongoCollection<Signature> SignatureCollection;
        private string connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

        public SignatureRepository(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase("CirkusSummarum");
            SignatureCollection = database.GetCollection<Signature>("Signature");
        }

        public void CreateSignature(Signature signature)
        {
            SignatureCollection.InsertOne(signature);
        }

        public List<Signature> GetAllSignatures()
        {
            return SignatureCollection.Find(FilterDefinition<Signature>.Empty).ToList();
        }

        public Signature GetSignatureById(string id)
        {
            var filter = Builders<Signature>.Filter.Eq(signature => signature.Id, id);
            return SignatureCollection.Find(filter).SingleOrDefault();
        }


    }
}
