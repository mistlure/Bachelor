using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;

namespace Bachelor.Systems
{
    public class MapGenerationSystem
    {
        public void GenerateWorld(World world, int sizeX, int sizeY)
        {
            for(int x = 0; x < sizeX; x++)
            {
                for(int y = 0; y < sizeY; y++)
                {
                    var entity = world.CreateEntity();

                    world.AddComponent(entity, new PositionComponent { X = x, Y = y });
                    world.AddComponent(entity, new TileComponent { TileType = Enums.TileType.Water });

                    world.RegisterTile(x, y, entity.Id);
                }
            }
        }
    }
}
