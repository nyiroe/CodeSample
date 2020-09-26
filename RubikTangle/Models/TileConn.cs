namespace RubikTangle.Models
{
    public struct TileConn
    {
        public TileConnType ConnType { get; set; }
        public TileConnType CompatibleType { get; set; }
    }
}
