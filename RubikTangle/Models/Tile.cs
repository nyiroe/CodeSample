using System;
using System.Linq;

namespace RubikTangle.Models
{
    public class Tile
    {
        private static readonly TileRotateType[] rotateElements = Enum.GetValues(typeof(TileRotateType)).Cast<TileRotateType>().ToArray();

        public string ImageName { get; set; }
        public Uri ImageUri { get; set; }
        public TileRotateType TileRotate { get; set; }
        public TileConn ConnTop { get; set; }
        public TileConn ConnRight { get; set; }
        public TileConn ConnBottom { get; set; }
        public TileConn ConnLeft { get; set; }

        public void Rotate()
        {
            // rotating image property
            if (TileRotate == rotateElements.Max())
            {
                TileRotate = rotateElements.Min();
            }
            else
            {
                int index = (int)TileRotate;
                index++;
                TileRotate = (TileRotateType)index;
            }

            // rotating metadata
            var tempConnLeft = ConnLeft;

            ConnLeft = ConnBottom;
            ConnBottom = ConnRight;
            ConnRight = ConnTop;
            ConnTop = tempConnLeft;
        }

        public void RotateTo(TileRotateType tileRotate)
        {
            while (TileRotate != tileRotate)
                Rotate();
        }
    }
}
