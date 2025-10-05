namespace TreasureHunting.Model
{
    /// <summary>
    /// The map (input data)
    /// </summary>
    public class TreasureMap
    {
        /// <summary>
        /// primary key
        /// </summary>
        public int ID { get; set; }
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

        /// <summary>
        /// date and time when map was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
