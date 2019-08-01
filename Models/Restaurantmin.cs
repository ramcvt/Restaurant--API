using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RestaurantsAPI.Models
{
    public class Restaurantmin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string imageUrl { get; set; }
        public long contact { get; set; }
        public string availability { get; set; }
        public double rating { get; set; }
    }
}
