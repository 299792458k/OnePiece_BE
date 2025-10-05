namespace TreasureHunting.Model
{
    /// <summary>
    /// Generic API response wrapper
    /// </summary>
    /// <typeparam name="T">Type of data being returned</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        public int HttpStatus { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Error message if any
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Indicates if the request was successful
        /// </summary>
        public bool Success => HttpStatus >= 200 && HttpStatus < 300;
    }
}
