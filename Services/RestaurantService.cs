using RestaurantsAPI.Infrastructure;
using RestaurantsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RestaurantsAPI.Services
{
    public class RestaurantService:IRestaurantService
    {
        private RestaurantDBContext _restaurantDBContext;
        public RestaurantService(RestaurantDBContext restaurantDBContext)
        {
            _restaurantDBContext = restaurantDBContext;
        }

        public async Task<List<Restaurant>> Get() {
            var projection = Builders<Restaurant>.Projection.Exclude(r => r.food);
            var restaurants = await _restaurantDBContext.Restaurant.Find(restaurant => true)
                                    .Project<Restaurant>(Builders<Restaurant>.Projection.Exclude(r => r.food))
                                   .ToListAsync();
                                   
            return restaurants;
        }

        public async Task<Restaurant> Get(string restaurantId)
        {
          var restaurant= await _restaurantDBContext.Restaurant.Find<Restaurant>(rest => rest.name == restaurantId).ToListAsync();
            return restaurant.FirstOrDefault();
        }

        public async Task<List<Restaurant>> SearchRestaurants(string searchkey)
        {
            var projection = Builders<Restaurant>.Projection.Exclude(r => r.food);
            var restaurants = await _restaurantDBContext.Restaurant.Find(res => res.name.ToLower().StartsWith(searchkey.ToLower()) || res.location.ToLower().StartsWith(searchkey.ToLower()))
                                   .Project<Restaurant>(Builders<Restaurant>.Projection.Exclude(r => r.food))
                                  .ToListAsync();

            return restaurants;
        }

        public async Task<Restaurant> GetFilterByRating(double ratings)
        {
            var restaurant = await _restaurantDBContext.Restaurant.FindAsync<Restaurant>(rest => rest.rating == ratings);
            return restaurant.FirstOrDefault();
        }

        public Restaurant Create(Restaurant restaurant)
        {
            //restaurant = new Restaurant
            //{
            //    RestaurantName = "ABC Restaurent",
            //    Location = "Chennai",
            //    Contact = 40,

            //};
            _restaurantDBContext.Restaurant.InsertOne(restaurant);
            return restaurant;
        }
    }
}
