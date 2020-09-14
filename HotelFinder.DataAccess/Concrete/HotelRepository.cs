using HotelFinder.DataAccess.Abstract;
using HotelFinderEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinder.DataAccess.Concrete
{
    public class HotelRepository : IHotelRepository
    {
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            using var HotelDbContext = new HotelDbContext();
            HotelDbContext.Hotels.Add(hotel);
            await HotelDbContext.SaveChangesAsync();
            return hotel;
        }

        public async Task DeleteHotel(int id)
        {
            using var HotelDbContext = new HotelDbContext();
            var DeleteHotel =await GetHotelById(id);
            HotelDbContext.Hotels.Remove(DeleteHotel);
            await HotelDbContext.SaveChangesAsync();
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            using var HotelDbContext = new HotelDbContext();
            return await HotelDbContext.Hotels.ToListAsync();
        }
        /*Bu senkron bir işlem
          public List<Hotel> GetAllHotels()
        {
            using var HotelDbContext = new HotelDbContext();
            return HotelDbContext.Hotels.ToList();
        }
        */
        public async Task<Hotel> GetHotelById(int id)
        {
            using var HotelDbContext = new HotelDbContext();
            return await HotelDbContext.Hotels.FindAsync(id);
            //find primary key olduğu için kullanabildik diğer türlü firstalldefault kullanmamız gerekir.

        }

        public async Task<Hotel> GetHotelByName(string name)
        {
            using var HotelDbContext = new HotelDbContext();
            return await HotelDbContext.Hotels.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            //primary key olmadığı için FirstorDefault u kullandık
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            using var HotelDbContext = new HotelDbContext();
            HotelDbContext.Hotels.Update(hotel);
            await HotelDbContext.SaveChangesAsync();

            return hotel;
        }
    }
}
