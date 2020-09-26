using RubikTangle.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace RubikTangle.Interfaces
{
    public interface ISolvePuzzleService
    {
        void DisplayTileSet(IEnumerable<Tile> tiles);
        ObservableCollection<Tile> Solution { get; }
        Task<bool> SolveAsync(IEnumerable<Tile> tiles, int matrixSize, int sleepTime, CancellationToken token);
    }
}
