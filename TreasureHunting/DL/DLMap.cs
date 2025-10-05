using Microsoft.EntityFrameworkCore;
using TreasureHunting.DL.DBContext;
using TreasureHunting.Model;

namespace TreasureHunting.DL
{
    /// <summary>
    /// Data access layer implementation
    /// </summary>
    public class DLMap : IDLMap
    {
        private readonly AppDbContext _context;

        public DLMap(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all maps
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreasureMap> GetMaps()
        {
            return _context.TreasureMaps.ToList();
        }

        /// <summary>
        /// Get map by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TreasureMap GetMap(int id)
        {
            return _context.TreasureMaps.FirstOrDefault(m => m.ID == id);
        }

        /// <summary>
        /// create map
        /// </summary>
        /// <param name="mapData"></param>
        /// <returns></returns>
        public TreasureMap CreateMap(TreasureMap mapData)
        {
            mapData.CreatedDate = DateTime.Now;
            _context.TreasureMaps.Add(mapData);
            _context.SaveChanges();
            return mapData;
        }

        /// <summary>
        /// delete map by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMap(int id)
        {
            var map = _context.TreasureMaps.FirstOrDefault(m => m.ID == id);
            if (map == null)
            {
                return false;
            }
            _context.TreasureMaps.Remove(map);
            _context.SaveChanges();
            return true;
        }
    }
}
