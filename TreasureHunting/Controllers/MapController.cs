using Microsoft.AspNetCore.Mvc;
using System.Net;
using TreasureHunting.BL;
using TreasureHunting.Model;

namespace TreasureHunting.Controllers
{
    [ApiController]
    [Route("map")]
    public class MapController : ControllerBase
    {
        private readonly ILogger<MapController> _logger;
        private readonly IBLMap _bl;

        public MapController(ILogger<MapController> logger, IBLMap bl)
        {
            _logger = logger;
            _bl = bl;
        }

        /// <summary>
        /// Get all maps
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<TreasureMap>>> GetMaps()
        {
            try
            {
                var maps = _bl.GetMaps();
                return Ok(new ApiResponse<IEnumerable<TreasureMap>>
                {
                    HttpStatus = (int)HttpStatusCode.OK,
                    Data = maps,
                    ErrorMessage = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting maps");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<IEnumerable<TreasureMap>>
                {
                    HttpStatus = (int)HttpStatusCode.InternalServerError,
                    Data = null,
                    ErrorMessage = "An error occurred while processing your request"
                });
            }
        }

        /// <summary>
        /// Get map by id
        /// </summary>
        /// <param name="id">map id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<TreasureMap>> GetMap(int id)
        {
            try
            {
                var map = _bl.GetMap(id);
                if (map == null)
                {
                    return NotFound(new ApiResponse<TreasureMap>
                    {
                        HttpStatus = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Map with id {id} not found"
                    });
                }
                return Ok(new ApiResponse<TreasureMap>
                {
                    HttpStatus = (int)HttpStatusCode.OK,
                    Data = map,
                    ErrorMessage = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting map with id {Id}", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<TreasureMap>
                {
                    HttpStatus = (int)HttpStatusCode.InternalServerError,
                    Data = null,
                    ErrorMessage = "An error occurred while processing your request"
                });
            }
        }

        /// <summary>
        /// Create new map (save input data)
        /// </summary>
        /// <param name="request">map data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResponse<TreasureMap>> CreateMap([FromBody] CreateMapRequest request)
        {
            try
            {
                var mapData = new TreasureMap
                {
                    RowsCount = request.RowsCount,
                    ColumnsCount = request.ColumnsCount,
                    TypesOfChestCount = request.TypesOfChestCount,
                    Matrix = request.Matrix
                };
                var createdMap = _bl.CreateMap(mapData);
                return Ok(new ApiResponse<TreasureMap>
                {
                    HttpStatus = (int)HttpStatusCode.OK,
                    Data = createdMap,
                    ErrorMessage = null
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation failed while creating map");
                return BadRequest(new ApiResponse<TreasureMap>
                {
                    HttpStatus = (int)HttpStatusCode.BadRequest,
                    Data = null,
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating map");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<TreasureMap>
                {
                    HttpStatus = (int)HttpStatusCode.InternalServerError,
                    Data = null,
                    ErrorMessage = "An error occurred while processing your request"
                });
            }
        }

        /// <summary>
        /// Solve map - find minimal fuel needed to reach treasure
        /// </summary>
        /// <param name="mapData">map data</param>
        /// <returns></returns>
        [HttpPost("solve")]
        public ActionResult<ApiResponse<SolveMapResponse>> SolveMap([FromBody] TreasureMap mapData)
        {
            try
            {
                var result = _bl.SolveMap(mapData);
                return Ok(new ApiResponse<SolveMapResponse>
                {
                    HttpStatus = (int)HttpStatusCode.OK,
                    Data = result,
                    ErrorMessage = null
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid Map Data");
                return BadRequest(new ApiResponse<SolveMapResponse>
                {
                    HttpStatus = (int)HttpStatusCode.BadRequest,
                    Data = null,
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while solving map");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<SolveMapResponse>
                {
                    HttpStatus = (int)HttpStatusCode.InternalServerError,
                    Data = null,
                    ErrorMessage = "An error occurred while processing your request"
                });
            }
        }

        /// <summary>
        /// Delete map by id
        /// </summary>
        /// <param name="id">map id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<string>> DeleteMap(int id)
        {
            try
            {
                var result = _bl.DeleteMap(id);
                if (!result)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        HttpStatus = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Map with id {id} not found"
                    });
                }
                return Ok(new ApiResponse<string>
                {
                    HttpStatus = (int)HttpStatusCode.OK,
                    Data = $"Map with id {id} deleted successfully",
                    ErrorMessage = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting map with id {Id}", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<string>
                {
                    HttpStatus = (int)HttpStatusCode.InternalServerError,
                    Data = null,
                    ErrorMessage = "An error occurred while processing your request"
                });
            }
        }
    }
}