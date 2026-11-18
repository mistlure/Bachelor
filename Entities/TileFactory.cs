using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Enums;
using Bachelor.Helpers;

namespace Bachelor.Entities
{
    public class TileFactory
    {
        public Entity CreateTile(World world, int x, int y, TileType tileType)
        {
            var entity = world.CreateEntity();

            world.AddComponent(entity, new PositionComponent { X = x, Y = y });
            TileHelper.SetTileType(world, entity, tileType);

            return entity;
        }
    }
}
