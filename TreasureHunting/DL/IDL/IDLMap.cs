using TreasureHunting.Model;

namespace TreasureHunting.DL
{
    /// <summary>
    /// Data access layer interface
    /// </summary>
    public interface IDLMap
    {
        /// <summary>
        /// get all maps
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreasureMap> GetMaps();

        /// <summary>
        /// get map by id
        /// </summary>
        /// <param name="id">map id</param>
        /// <returns></returns>
        public TreasureMap GetMap(int id);

        /// <summary>
        /// save map
        /// </summary>
        /// <param name="mapData">data of map</param>
        /// <returns></returns>
        public TreasureMap CreateMap(TreasureMap mapData);

        /// <summary>
        /// delete map by id
        /// </summary>
        /// <param name="id">map id</param>
        /// <returns>true if deleted successfully</returns>
        public bool DeleteMap(int id);
    }
}
