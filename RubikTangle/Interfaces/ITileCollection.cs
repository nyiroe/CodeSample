using RubikTangle.Models;
using System.Collections.Generic;

namespace RubikTangle.Interfaces
{
    public interface ITileCollection
    {
        IReadOnlyList<Tile> Tiles { get; }
        void Shuffle();
    }
}
