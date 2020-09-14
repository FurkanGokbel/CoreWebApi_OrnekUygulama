using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelFinder.Business.Abstrat;
using HotelFinder.Business.Concrete;
using HotelFinderEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;

        }
        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotel = await _hotelService.GetAllHotels();
            return Ok(hotel);//200 + data


        }
        /// <summary>
        /// Get Hotels By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]//api/hotels/gethotelbyid/2
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel);//200 + data
            }
            return NotFound();//404
        }
        [HttpGet]
        [Route("[action]/{name}")]//api/hotels/gethotelbyid/2
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel =await _hotelService.GetHotelByName(name);
            if (hotel != null)
            {
                return Ok(hotel);//200 + data
            }
            return NotFound();
        }/*
        [HttpGet]
        [Route("[action]")]//api/hotels/gethotelbyid/2
        public async Task<IActionResult> GetHotelByIdAndName(int id, string name)
        {
            return Ok();//200 + data

        }*/

        /// <summary>
        /// Create an Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            if (ModelState.IsValid)//[ApiController tanımlı olduğu için aslında buna gerek yok]
            {
                var createHotel =await _hotelService.CreateHotel(hotel);
                return CreatedAtAction("Get", new { id = createHotel.Id }, createHotel);//201 + data
            }
            return BadRequest(ModelState);//400 + validation data
        }
        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if (await _hotelService.GetHotelById(hotel.Id) != null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel));//200 + data
            }
            return NotFound();
        }
        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelService.GetHotelById(id) != null)
            {
               await _hotelService.DeleteHotel(id);
                return Ok();//200 + data
            }
            return NotFound();
        }
    }
}
