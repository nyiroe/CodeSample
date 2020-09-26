using RubikTangle.Models;
using System.Collections.Generic;

namespace RubikTangle.Services
{
    public class TileEvaluator
    {
        private readonly IList<Tile> partialSolution;
        private readonly int index;
        private readonly int matrixSize;

        public Tile Tile { get; private set; }

        public bool IsTopMatches
        {
            get
            {
                // checking if needed to match on top
                if (index >= matrixSize)
                {
                    // returning true if matches, false if not
                    Tile upperTile = partialSolution[index - matrixSize];
                    return Tile.ConnTop.ConnType == upperTile.ConnBottom.CompatibleType;
                }
                else
                {
                    // no element on top
                    return true;
                }
            }
        }

        public bool IsLeftMatches
        {
            get
            {
                // checking if needed to match on left
                if (index % matrixSize > 0)
                {
                    // returning true if matches, false if not
                    Tile leftTile = partialSolution[index - 1];
                    return Tile.ConnLeft.ConnType == leftTile.ConnRight.CompatibleType;
                }
                else
                {
                    // no element on top
                    return true;
                }
            }
        }

        public bool IsTileFits
        {
            get
            {
                return IsTopMatches && IsLeftMatches;
            }
        }

        public TileEvaluator(IList<Tile> partialSolution, Tile tile, int matrixSize)
        {
            this.partialSolution = partialSolution;
            this.index = partialSolution.Count;
            this.matrixSize = matrixSize;

            Tile = tile;
        }
    }
}
