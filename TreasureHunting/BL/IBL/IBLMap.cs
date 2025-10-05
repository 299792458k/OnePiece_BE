using TreasureHunting.Model;

namespace TreasureHunting.BL
{
    /// <summary>
    /// Business Layer Interface
    /// </summary>
    public interface IBLMap
    {
        /// <summary>
        /// get all maps
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreasureMap> GetMaps();

        /// <summary>
        /// get map by id
        /// </summary>
        /// <returns></returns>
        public TreasureMap GetMap(int id);

        /// <summary>
        /// save map
        /// </summary>
        /// <param name="mapData">data of map</param>
        /// <returns></returns>
        public TreasureMap CreateMap(TreasureMap mapData);

        /// <summary>
        /// find the minimal fuel needed to reach to the treasure target
        /// </summary>
        /// <param name="mapData"></param>
        /// <returns></returns>
        public SolveMapResponse SolveMap(TreasureMap mapData);

        /// <summary>
        /// delete map by id
        /// </summary>
        /// <param name="id">map id</param>
        /// <returns>true if deleted successfully</returns>
        public bool DeleteMap(int id);
    }
}
