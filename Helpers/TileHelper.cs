using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Enums;

namespace Bachelor.Helpers
{
    public static class TileHelper
    {
        public static bool SetTileType(World world, Entity tile, TileType tileType)
        {
            if (!world.HasComponent<TileComponent>(tile))
            {
                return false;
            }

            world.AddComponent(tile, new TileComponent { TileType = tileType });
            world.RemoveComponent<OpensContextMenuComponent>(tile);
            return true;
        }
    }
}
