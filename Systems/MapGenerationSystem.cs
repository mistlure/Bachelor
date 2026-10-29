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
        public void GenerateWorld(World world, TileMap tileMap, int sizeX, int sizeY)
        {
            TileFactory tileFactory = new TileFactory();

            // Fill the entire world with water tiles
            for (int x = 0; x < sizeX; x++)
            {
                for(int y = 0; y < sizeY; y++)
                {
                    tileFactory.CreateWaterTile(world, tileMap, x, y);
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

            var entityIds = tileMap.GetEntityIdsAtPosition(targetX, targetY);
            if(entityIds.Any())
            {
                foreach(var id in entityIds.ToList())
                {
                    var entity = new Bachelor.Entities.Entity(id);
                    if (world.HasComponent<TileComponent>(entity))
                    {
                        world.DestroyEntity(entity);
                        break;
                    }
                }
            }
            tileFactory.CreateGrassTile(world, tileMap, targetX, targetY);
        }
    }
}
