using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;

namespace Bachelor.Systems
{
    public class MapGenerationSystem
    {
        public void GenerateWorld(World world, int sizeX, int sizeY)
        {
            // Fill the entire world with water tiles
            for (int x = 0; x < sizeX; x++)
            {
                for(int y = 0; y < sizeY; y++)
                {
                    var entity = world.CreateEntity();

                    world.AddComponent(entity, new PositionComponent { X = x, Y = y });
                    world.AddComponent(entity, new TileComponent { TileType = Enums.TileType.Water });

                    world.RegisterTile(x, y, entity.Id);
                }
            }



            // Spawn a random grass tile
            var marginPercent = 0.3f;
            System.Random randomiser = new System.Random();

            int minX = (int)(sizeX * marginPercent);
            int maxX = sizeX - minX;
            int minY = (int)(sizeY * marginPercent);
            int maxY = sizeY - minY;

            int targetX = randomiser.Next(minX, maxX);
            int targetY = randomiser.Next(minY, maxY);

            var entityIds = world.GetEntityIdsAtPosition(targetX, targetY);
            if(entityIds.Any())
            {
                foreach(var id in entityIds)
                {
                    var entity = new Bachelor.Entities.Entity(id);
                    if (world.HasComponent<TileComponent>(entity))
                    {
                        world.AddComponent(entity, new TileComponent { TileType = Enums.TileType.Grass });
                        break;
                    }
                }
            }
        }
    }
}
