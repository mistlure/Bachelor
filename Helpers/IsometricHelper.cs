using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Bachelor.Config;

namespace Bachelor.Helpers
{
    public static class IsometricHelper
    {
        private static readonly float HalfTileWidth = GameSettings.TileWidth / 2f;
        private static readonly float HalfTileHeight = GameSettings.TileHeight / 2f;

        public static Vector2 ToScreenCoordinates(int x, int y, float yOffset = 0f)
        {
            float screenX = (x - y) * HalfTileWidth + GameSettings.TempOffsetWidth;
            float screenY = (x + y) * HalfTileHeight + GameSettings.TempOffsetHeight + yOffset;

            return new Vector2(screenX, screenY);
        }
    }
}
