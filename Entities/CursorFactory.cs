using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;

namespace Bachelor.Entities
{
    public static class CursorFactory
    {
        public static Entity CreateCursor(World world, int positionX, int positionY)
        {
            var entity = world.CreateEntity();

            world.AddComponent(entity, new PositionComponent { X = positionX, Y = positionY });
            world.AddComponent(entity, new CursorComponent());

            return entity;
        }
    }
}
