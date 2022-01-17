using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Assingment.Model
{
    public class OmdbSearch
    {
        [BsonId]
        public ObjectId _Id { get; set; }

        public string search_token { get; set; }

        public string imdbID { get; set; }

        //time it took to process the request
        public double processing_time_ms { get; set; }

      
        //[BsonElement("ts")] // use the abbreviation in serialization
        //public BsonTimestamp timestamp { get; set; }
        public double timestamp { get; set; }

        //IP Address of the client performing the request
        public string ip_address { get; set; }

    }
}
