using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;
using Microsoft.Xna.Framework.Input;

namespace Bachelor.Systems
{
    public class CursorInputSystem
    {
        public void HandleKeyboard(World world)
        {
            var cursorIds = world.GetEntityIdsWith<CursorComponent>().ToList();
            var command = Keyboard.GetState();

            if (cursorIds.Count == 0)
            {
                return;
            }

            int cursorId = cursorIds[0];
            var entity = new Entity(cursorId);
            var position = world.TryGetComponent<PositionComponent>(entity);
            if (!position.HasValue)
            {
                return;
            }

            var pos = position.Value;
            bool positionChanged = false;
            if (command.IsKeyDown(Keys.W) || command.IsKeyDown(Keys.Up))
            {
                pos.Y -= 1;
                positionChanged = true;
            }
            if (command.IsKeyDown(Keys.A) || command.IsKeyDown(Keys.Left))
            {
                pos.X -= 1;
                positionChanged = true;
            }
            if (command.IsKeyDown(Keys.S) || command.IsKeyDown(Keys.Down))
            {
                pos.Y += 1;
                positionChanged = true;
            }
            if (command.IsKeyDown(Keys.D) || command.IsKeyDown(Keys.Right))
            {
                pos.X += 1;
                positionChanged = true;
            }

            if (positionChanged)
            {
                world.AddComponent(entity, pos);
            }
        }
    }
}
