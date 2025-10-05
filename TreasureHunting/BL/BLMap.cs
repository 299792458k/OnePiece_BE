using TreasureHunting.DL;
using TreasureHunting.Model;

namespace TreasureHunting.BL
{
    /// <summary>
    /// Business logic layer
    /// </summary>
    public class BLMap : IBLMap
    {
        private readonly IDLMap _dl;

        public BLMap(IDLMap dl)
        {
            _dl = dl;
        }

        /// <summary>
        /// Get all maps (input history)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreasureMap> GetMaps()
        {
            return _dl.GetMaps();
        }

        /// <summary>
        /// Get mapdata by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TreasureMap GetMap(int id)
        {
            return _dl.GetMap(id);
        }

        /// <summary>
        /// Save input (create a map)
        /// </summary>
        /// <param name="mapData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public TreasureMap CreateMap(TreasureMap mapData)
        {
            if (!ValidateMapData(mapData, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
            return _dl.CreateMap(mapData);
        }

        /// <summary>
        /// validate data of map input
        /// </summary>
        /// <param name="mapData"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool ValidateMapData(TreasureMap mapData, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (mapData == null || string.IsNullOrEmpty(mapData.Matrix))
            {
                errorMessage = "Map data cannot be null";
                return false;
            }

            if ((mapData.RowsCount < 1 || mapData.RowsCount > 500) || (mapData.ColumnsCount < 1 || mapData.ColumnsCount > 500))
            {
                errorMessage = "RowsCount and ColumnsCount must be in range 1 to 500";
                return false;
            }

            if (mapData.TypesOfChestCount < 1 || mapData.TypesOfChestCount > mapData.RowsCount * mapData.ColumnsCount)
            {
                errorMessage = "Invalid number types of chest";
                return false;
            }

            if (string.IsNullOrWhiteSpace(mapData.Matrix))
            {
                errorMessage = "Matrix cannot be empty";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Find mimimum fuel needed 
        /// </summary>
        /// <param name="mapData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public SolveMapResponse SolveMap(TreasureMap mapData)
        {
            // validate mapdata
            if (!ValidateMapData(mapData, out string errorMessage))
            { 
                throw new ArgumentException(errorMessage);
            }

            // convert matrixData from string
            var chestNumbers = mapData.Matrix?.Split(',').Select(cellData => int.Parse(cellData.Trim())).ToList();
            if(chestNumbers == null || chestNumbers.Count == 0)
            {
                throw new ArgumentException("Invalid input data");
            } else
            {
                // cellsByValue: ditionary of cells by chest number
                var cellsByValue = new Dictionary<int, List<Cell>>();

                // Beginning cell, starts from (1,1)
                cellsByValue.Add(0, new List<Cell>
                {
                    new Cell
                    {
                        PosX = 1,
                        PosY = 1,
                        Value = chestNumbers[0],
                        MinFuel = 0,
                        Path = "(1,1)"
                    }
                });

                // turn index from chest to 2D Cell
                for(int chestIndex = 0; chestIndex < chestNumbers.Count; chestIndex++)
                {
                    var cell = new Cell
                    {
                        PosX = chestIndex / mapData.ColumnsCount + 1, // Matrix starts with position (1,1)
                        PosY = chestIndex % mapData.ColumnsCount + 1, // Matrix starts with position (1,1)
                        Value = chestNumbers[chestIndex]
                    };
                    // add cell to cellsByValue
                    if (cellsByValue.ContainsKey(cell.Value))
                    {
                        cellsByValue[cell.Value].Add(cell);
                    } else
                    {
                        cellsByValue.Add(cell.Value, new List<Cell> { cell });
                    }
                }

                // implement the traverse logic using Dynamic Programming
                for (var chestValue = 1; chestValue <= mapData.TypesOfChestCount; chestValue++)
                {
                    if (!cellsByValue.ContainsKey(chestValue) || !cellsByValue.ContainsKey(chestValue - 1)
                        || !cellsByValue.ContainsKey(mapData.TypesOfChestCount) || cellsByValue[mapData.TypesOfChestCount].Count > 1)
                    {
                        throw new ArgumentException("Invalid input data");
                    } else
                    {
                        foreach(var cell in cellsByValue[chestValue])
                        {
                            double minFuel = double.MaxValue;
                            string bestPath = "";
                            foreach(var previousCell in cellsByValue[chestValue - 1])
                            {
                                if(previousCell.MinFuel >= minFuel) // pruning (possibly use eager pruning break;)
                                {
                                    continue;
                                }
                                var distance = CalculateDistance(cell, previousCell); // can use memorization to reduce complexity
                                var totalFuel = previousCell.MinFuel + distance;
                                if (totalFuel < minFuel)
                                {
                                    minFuel = totalFuel;
                                    bestPath = previousCell.Path + $" => ({cell.PosX},{cell.PosY})";
                                }
                            }
                            cell.MinFuel = minFuel;
                            cell.Path = bestPath;
                        }
                    }
                }
                var finalCell = cellsByValue[mapData.TypesOfChestCount][0];
                return new SolveMapResponse
                {
                    MinFuel = finalCell.MinFuel,
                    Path = finalCell.Path
                };
            }
            return new SolveMapResponse { MinFuel = 0, Path = "" };
        }
        /// <summary>
        /// Calculate Euclidean distance between two cells
        /// </summary>
        /// <param name="other">The other cell</param>
        /// <returns>Euclidean distance</returns>
        public double CalculateDistance(Cell cell1, Cell cell2)
        {
            var dx = cell1.PosX - cell2.PosX;
            var dy = cell1.PosY - cell2.PosY;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// delete input history by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMap(int id)
        {
            return _dl.DeleteMap(id);
        }
    }
}
