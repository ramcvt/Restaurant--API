using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantsAPI.Models
{
    public class Restaurant
    {
        public Restaurant()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string imageUrl { get; set; }
        public long contact { get; set; }
        public string availability { get; set; }
        public double rating { get; set; }
        public List<food> food { get; set; }

    }

    public class food {
        public string type { get; set; }
        public string subtype { get; set; }
        public string dish { get; set; }
        public double price { get; set; }
        public int availability { get; set; }
    }

}
