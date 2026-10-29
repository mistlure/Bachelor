using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;

namespace Bachelor.Entities
{
    public class TileFactory
    {
        public Entity CreateWaterTile(World world, TileMap tileMap, int x, int y)
        {
            var entity = world.CreateEntity();

            world.AddComponent(entity, new PositionComponent { X = x, Y = y });
            world.AddComponent(entity, new TileComponent { TileType = Enums.TileType.Water });
            world.AddComponent(entity, new OpensContextMenuComponent
            {
                Options = Config.ContextMenuConfig.GetWaterTileOptions()
            });

            tileMap.RegisterEntity(x, y, entity.Id);

            return entity;
        }

        public Entity CreateGrassTile(World world, TileMap tileMap, int x, int y)
        {
            var entity = world.CreateEntity();

            world.AddComponent(entity, new PositionComponent { X = x, Y = y });
            world.AddComponent(entity, new TileComponent { TileType = Enums.TileType.Grass });

            tileMap.RegisterEntity(x, y, entity.Id);

            return entity;
        }
    }
}
