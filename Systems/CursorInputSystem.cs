using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Config;
using Bachelor.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bachelor.Systems
{
    public class CursorInputSystem
    {
        private KeyboardState _previousKeyboardState;
        public void HandleKeyboard(World world)
        {
            var cursorIds = world.GetEntityIdsWith<CursorComponent>().ToList();
            if (cursorIds.Count == 0)
            {
                return;
            }

            var currentKeyboardState = Keyboard.GetState();

            int cursorId = cursorIds[0];
            var entity = new Entity(cursorId);
            var position = world.TryGetComponent<PositionComponent>(entity);
            if (!position.HasValue)
            {
                _previousKeyboardState = currentKeyboardState;
                return;
            }

            var pos = position.Value;
            bool positionChanged = false;



            if (InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.W) ||
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Up))
            {
                pos.Y -= 1;
                positionChanged = true;
            }
            if (InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.A) ||
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Left))
            {
                pos.X -= 1;
                positionChanged = true;
            }
            if (InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.S) ||
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Down))
            {
                pos.Y += 1;
                positionChanged = true;
            }
            if (InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.D) ||
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Right))
            {
                pos.X += 1;
                positionChanged = true;
            }

            _previousKeyboardState = currentKeyboardState;

            if (positionChanged)
            {
                pos.X = Math.Clamp(pos.X, 0, GameSettings.MapWidth - 1);
                pos.Y = Math.Clamp(pos.Y, 0, GameSettings.MapHeight - 1);

                world.AddComponent(entity, pos);
            }
        }
    }
}
