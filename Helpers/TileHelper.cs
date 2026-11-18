using Bachelor.Components;
using Bachelor.Config;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Enums;

namespace Bachelor.Helpers
{
    public static class TileHelper
    {
        public static bool TryChangeTileType(World world, Entity tile, TileType tileType)
        {
            if (!world.HasComponent<TileComponent>(tile))
            {
                return false;
            }

            SetTileType(world, tile, tileType);
            return true;
        }

        public static void SetTileType(World world, Entity tile, TileType tileType)
        {
            world.AddComponent(tile, new TileComponent { TileType = tileType });

            world.RemoveComponent<OpensContextMenuComponent>(tile);

            var options = ContextMenuConfig.GetTileOptions(tileType);
            if (options != null)
            {
                world.AddComponent(tile, new OpensContextMenuComponent { Options = options });
            }
        }
    }
}
