using RubikTangle.Interfaces;
using RubikTangle.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RubikTangle.Services
{
    public class SolvePuzzleService : ISolvePuzzleService
    {
        private int matrixSize;
        private int successElementCount;
        private Tile[] originalTiles;
        private int sleepTime;
        private CancellationToken token;

        public ObservableCollection<Tile> Solution { get; } = new ObservableCollection<Tile>();

        private bool IsCompleted
        {
            get
            {
                return Solution.Count == successElementCount;
            }
        }

        public void DisplayTileSet(IEnumerable<Tile> tiles)
        {
            Solution.Clear();

            foreach (Tile tile in tiles)
                Solution.Add(tile);
        }

        public async Task<bool> SolveAsync(IEnumerable<Tile> tiles, int matrixSize, int sleepTime, CancellationToken token)
        {
            Solution.Clear();

            this.matrixSize = matrixSize;
            this.sleepTime = sleepTime;
            this.token = token;
            this.successElementCount = matrixSize * matrixSize;
            originalTiles = tiles.ToArray();

            return await SolveAsyncDo(tiles);
        }

        private async Task<bool> SolveAsyncDo(IEnumerable<Tile> tiles)
        {
            // iterating through all tiles as start until a solution is found
            foreach (Tile tile in tiles)
            {
                // trying start tiles to rotate in all direction
                for (int i = 0; i < 4; i++)
                {
                    tile.Rotate();
                    await ProcessTilesAsync(tile);

                    if (IsCompleted)
                        break;

                    Solution.Remove(tile);
                }

                if (IsCompleted)
                    break;
            }

            return IsCompleted;
        }

        private async Task<bool> ProcessTilesAsync(Tile workingTile)
        {
            // safety runtime check which should always pass
            TileEvaluator tileEvaluator = new TileEvaluator(Solution, workingTile, matrixSize);
            if (!tileEvaluator.IsTileFits)
            {
                throw new Exception("Tile does not fit here");
            }

            Solution.Add(workingTile);
            await Task.Delay(sleepTime, token);

            var tiles = originalTiles.Except(Solution);

            var matchingTiles = GetMatchingTiles(tiles);

            bool success = IsCompleted;

            // iterating through matching tiles from remaining
            foreach (MatchingTile matchingTile in matchingTiles)
            {
                // rotating back to original matching direction (tiles are maintaining their rotation state)
                matchingTile.Tile.RotateTo(matchingTile.TileRotate);

                success = await ProcessTilesAsync(matchingTile.Tile);

                if (!success)
                    Solution.Remove(matchingTile.Tile);

                if (IsCompleted)
                    break;
            }

            return success;
        }

        private IEnumerable<MatchingTile> GetMatchingTiles(IEnumerable<Tile> tiles)
        {
            List<MatchingTile> matchingList = new List<MatchingTile>();

            foreach (Tile tile in tiles)
            {
                TileEvaluator tileEvaluator = new TileEvaluator(Solution, tile, matrixSize);

                // found a matching tile
                if (DoesFit(tileEvaluator))
                {
                    matchingList.Add(new MatchingTile() { Tile = tile, TileRotate = tile.TileRotate });
                }
            }

            return matchingList;
        }

        private bool DoesFit(TileEvaluator tileEvaluator)
        {
            bool fits = tileEvaluator.IsTileFits;
            if (!fits)
            {
                // tile not fitting, trying to rotate
                for (int i = 0; i < 4; i++)
                {
                    tileEvaluator.Tile.Rotate();

                    if (tileEvaluator.IsTileFits)
                    {
                        fits = true;
                        break;
                    }
                }
            }

            return fits;
        }
    }
}
