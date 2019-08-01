using Microsoft.AspNetCore.Mvc;
using RestaurantsAPI.Models;
using RestaurantsAPI.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RestaurantsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
       

        private readonly RestaurantService _restaurantService;
        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;

        }

        [HttpGet(Name = "GetRestaurants")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Restaurant>>> Get()
        {
            var restaurents=await _restaurantService.Get();
            if(restaurents.Count==0)
            {
                return NotFound("Restaurant is not found");
            }
            
            return Ok(restaurents);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Restaurant>> GetRestaurantAsync([FromRoute] string id)
        {
            var restaurant = await _restaurantService.Get(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }

            else
            {
                return NotFound("Item you are searching is not found");
            }
        }

        [HttpGet("search/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Restaurant>> SearchRestaurantAsync([FromRoute] string id)
        {
            var restaurant = await _restaurantService.SearchRestaurants(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }

            else
            {
                return NotFound("Item you are searching is not found");
            }
        }

        [HttpGet("rating/{rating}", Name = "GetRestaurantByRatings")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Restaurant>> GetRestaurantByRatingsAsync([FromRoute] double rating)
        {
            var restaurant = await _restaurantService.GetFilterByRating(rating);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }

            else
            {
                return NotFound("Item you are searching is not found");
            }
        }

       
        [HttpPost(Name = "AddRestaurant")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<Restaurant> AddRestaurantAsync(Restaurant restaurant)
        {
            //_restaurantDBContext.Restaurant.InsertOne(restaurant);

            //return CreatedAtRoute("GetRestaurants", new { id = restaurant.RestaurentId.ToString() }, restaurant);
            if (ModelState.IsValid)
            {
                _restaurantService.Create(restaurant);
                return CreatedAtRoute("GetRestaurants", new { id = restaurant.id.ToString() }, restaurant);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Get /api/events

        //[HttpGet(Name = "GetAll")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[HttpGet]
        //public ActionResult<List<Restaurant>> GetRestaurants()
        //{
        //    //Restaurant restaurant = new Restaurant
        //    //{
        //    //    RestaurantName="ABC Restaruant",
        //    //    Location="Chennai",
        //    //    Contact=98989899
        //    //};
        //    //_restaurantDBContext.Restaurant.InsertOne(restaurant);
        //    var restaurants = _restaurantDBContext.Restaurant.Find(FilterDefinition<Restaurant>.Empty)
        //        .Sort(Builders<Restaurant>.Sort.Descending("RestaurantName"))
        //        .Skip(10 * 0)
        //        .Limit(10)
        //        .ToList<Restaurant>();
        //    return Ok(restaurants);
        //}




    }
}