using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Config
{
    public static class GameSettings
    {
        public const int MapWidth = 15;
        public const int MapHeight = 15;

        public const int TileWidth = 30;
        public const int TileHeight = 14;

        public const int StartingCoins = 100;



        public const float DisabledOptionBrightness = 0.35f;

        // Temporary (Used for centering tiles relative to a window)
        public const int TempOffsetWidth = 400;
        public const int TempOffsetHeight = 100;
    }
}
