namespace TreasureHunting.Model
{
    /// <summary>
    /// Request model for creating a new map
    /// </summary>
    public class CreateMapRequest
    {
        /// <summary>
        /// number of rows
        /// </summary>
        public int RowsCount { get; set; }

        /// <summary>
        /// number of columns
        /// </summary>
        public int ColumnsCount { get; set; }

        /// <summary>
        /// number of types of treasure chest
        /// </summary>
        public int TypesOfChestCount { get; set; }

        /// <summary>
        /// matrix in JSON
        /// </summary>
        public string? Matrix { get; set; }
    }
}
