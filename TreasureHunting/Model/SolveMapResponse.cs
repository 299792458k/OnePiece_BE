namespace TreasureHunting.Model
{
    /// <summary>
    /// Response model for SolveMap API
    /// </summary>
    public class SolveMapResponse
    {
        /// <summary>
        /// Minimal fuel needed to reach the treasure
        /// </summary>
        public double MinFuel { get; set; }

        /// <summary>
        /// Path to reach the treasure in format: (1,1) => (2,3) => (3,5)
        /// </summary>
        public string Path { get; set; }
    }
}
