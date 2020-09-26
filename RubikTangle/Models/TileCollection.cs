using RubikTangle.Helpers;
using RubikTangle.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RubikTangle.Models
{
    public class TileCollection : ITileCollection
    {
        private const string TILEURIBASE = "pack://application:,,,/RubikTangle;component/Tiles/{0}.jpg";
        private List<Tile> tiles = new List<Tile>();

        public IReadOnlyList<Tile> Tiles
        {
            get
            {
                return tiles.AsReadOnly();
            }
        }

        public TileCollection()
        {
            tiles.Add(new Tile()
            {
                ImageName = "Tile #1",
                ImageUri = FormatImageUri("Tile01"),
                ConnTop = new TileConn { ConnType = TileConnType.BlueRed, CompatibleType = TileConnType.RedBlue },
                ConnRight = new TileConn { ConnType = TileConnType.GreenBlue, CompatibleType = TileConnType.BlueGreen },
                ConnBottom = new TileConn { ConnType = TileConnType.RedYellow, CompatibleType = TileConnType.YellowRed },
                ConnLeft = new TileConn { ConnType = TileConnType.GreenYellow, CompatibleType = TileConnType.YellowGreen }
            });

            tiles.Add(new Tile()
            {
                ImageName = "Tile #2",
                ImageUri = FormatImageUri("Tile02"),
                ConnTop = new TileConn { ConnType = TileConnType.RedYellow, CompatibleType = TileConnType.YellowRed },
                ConnRight = new TileConn { ConnType = TileConnType.BlueYellow, CompatibleType = TileConnType.YellowBlue },
                ConnBottom = new TileConn { ConnType = TileConnType.GreenRed, CompatibleType = TileConnType.RedGreen },
                ConnLeft = new TileConn { ConnType = TileConnType.BlueGreen, CompatibleType = TileConnType.GreenBlue }
            });

            tiles.Add(new Tile()
            {
                ImageName = "Tile #3",
                ImageUri = FormatImageUri("Tile03"),
                ConnTop = new TileConn { ConnType = TileConnType.GreenYellow, CompatibleType = TileConnType.YellowGreen },
                ConnRight = new TileConn { ConnType = TileConnType.BlueRed, CompatibleType = TileConnType.RedBlue },
                ConnBottom = new TileConn { ConnType = TileConnType.GreenRed, CompatibleType = TileConnType.RedGreen },
                ConnLeft = new TileConn { ConnType = TileConnType.YellowBlue, CompatibleType = TileConnType.BlueYellow }
            });

            tiles.Add(new Tile()
            {
                ImageName = "Tile #4",
                ImageUri = FormatImageUri("Tile04"),
                ConnTop = new TileConn { ConnType = TileConnType.YellowRed, CompatibleType = TileConnType.RedYellow },
                ConnRight = new TileConn { ConnType = TileConnType.GreenBlue, CompatibleType = TileConnType.BlueGreen },
                ConnBottom = new TileConn { ConnType = TileConnType.YellowBlue, CompatibleType = TileConnType.BlueYellow },
                ConnLeft = new TileConn { ConnType = TileConnType.RedGreen, CompatibleType = TileConnType.GreenRed }
            });

            tiles.Add(new Tile()
            {
                ImageName = "Tile #5",
                ImageUri = FormatImageUri("Tile05"),
                ConnTop = new TileConn { ConnType = TileConnType.RedGreen, CompatibleType = TileConnType.GreenRed },
                ConnRight = new TileConn { ConnType = TileConnType.YellowBlue, CompatibleType = TileConnType.BlueYellow },
                ConnBottom = new TileConn { ConnType = TileConnType.RedYellow, CompatibleType = TileConnType.YellowRed },
                ConnLeft = new TileConn { ConnType = TileConnType.BlueGreen, CompatibleType = TileConnType.GreenBlue }
            });

            tiles.Add(new Tile()
            {
                ImageName = "Tile #6",
                ImageUri = FormatImageUri("Tile06"),
                ConnTop = new TileConn { ConnType = TileConnType.RedGreen, CompatibleType = TileConnType.GreenRed },
                ConnRight = new TileConn { ConnType = TileConnType.BlueRed, CompatibleType = TileConnType.RedBlue },
                ConnBottom = new TileConn { ConnType = TileConnType.GreenYellow, CompatibleType = TileConnType.YellowGreen },
                ConnLeft = new TileConn { ConnType = TileConnType.BlueYellow, CompatibleType = TileConnType.YellowBlue }
            });

            tiles.Add(new Tile()
            {
                ImageName = "Tile #7",
                ImageUri = FormatImageUri("Tile07"),
                ConnTop = new TileConn { ConnType = TileConnType.BlueYellow, CompatibleType = TileConnType.YellowBlue },
                ConnRight = new TileConn { ConnType = TileConnType.RedGreen, CompatibleType = TileConnType.GreenRed },
                ConnBottom = new TileConn { ConnType = TileConnType.BlueGreen, CompatibleType = TileConnType.GreenBlue },
                ConnLeft = new TileConn { ConnType = TileConnType.YellowRed, CompatibleType = TileConnType.RedYellow }
            });

            tiles.Add(new Tile()
            {
                ImageName = "Tile #8",
                ImageUri = FormatImageUri("Tile08"),
                ConnTop = new TileConn { ConnType = TileConnType.YellowRed, CompatibleType = TileConnType.RedYellow },
                ConnRight = new TileConn { ConnType = TileConnType.BlueGreen, CompatibleType = TileConnType.GreenBlue },
                ConnBottom = new TileConn { ConnType = TileConnType.YellowBlue, CompatibleType = TileConnType.BlueYellow },
                ConnLeft = new TileConn { ConnType = TileConnType.GreenRed, CompatibleType = TileConnType.RedGreen }
            });

            tiles.Add(new Tile()
            {
                ImageName = "Tile #9",
                ImageUri = FormatImageUri("Tile09"),
                ConnTop = new TileConn { ConnType = TileConnType.YellowGreen, CompatibleType = TileConnType.GreenYellow },
                ConnRight = new TileConn { ConnType = TileConnType.BlueRed, CompatibleType = TileConnType.RedBlue },
                ConnBottom = new TileConn { ConnType = TileConnType.YellowRed, CompatibleType = TileConnType.RedYellow },
                ConnLeft = new TileConn { ConnType = TileConnType.GreenBlue, CompatibleType = TileConnType.BlueGreen }
            });
        }

        public void Shuffle()
        {
            foreach (var item in tiles)
            {
                int rotateCount = Extensions.Random.Next(0, 4);

                if (rotateCount > 0)
                {
                    for (int i = 0; i < rotateCount; i++)
                        item.Rotate();
                }
            }

            tiles = tiles.RandomPermutation().ToList();
        }

        private Uri FormatImageUri(string filename)
        {
            return new Uri(string.Format(TILEURIBASE, filename), UriKind.Absolute);
        }
    }
}
