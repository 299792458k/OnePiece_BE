namespace TreasureHunting.Model
{
    /// <summary>
    /// The cell(island) in the map
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Horizontal position
        /// </summary>
        public int PosX { get; set; }

        /// <summary>
        /// Vertical position
        /// </summary>
        public int PosY { get; set; }

        /// <summary>
        /// Chest Value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Minimal Fuel needed to reach this cell, this is the dp value
        /// </summary>
        public double MinFuel { get; set; }

        /// <summary>
        /// The Path to reach cell by minimal fuel (for tracking purpose)
        /// </summary>
        public string Path { get; set; }
    }
}
