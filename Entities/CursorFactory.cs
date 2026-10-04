using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;

namespace Bachelor.Entities
{
    public class CursorFactory
    {
        public Entity CreateCursor(World world, int positionX, int positionY)
        {
            var test = world.GetEntityIdsWith<CursorComponent>().ToList();
            if (test.Any())
            {
                throw new InvalidOperationException("Error! Attempting to create a second cursor.");
            }

            var entity = world.CreateEntity();

            world.AddComponent(entity, new PositionComponent { X = positionX, Y = positionY });
            world.AddComponent(entity, new CursorComponent());

            return entity;
        }
    }
}
