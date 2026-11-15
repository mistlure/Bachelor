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
    public class CursorMapMovementSystem
    {
        private KeyboardState _previousKeyboardState;
        public void HandleInput(World world, InputState input)
        {
            if (!world.TryGetSingleton<CursorComponent>(out var entity)) return;

            if (!world.GetEntityIdsWith<CursorWorldModeComponent>().Any()) return;

            var position = world.TryGetComponent<PositionComponent>(entity);
            if (!position.HasValue) return;

            var pos = position.Value;
            bool positionChanged = false;

            if (input.IsKeyJustPressed(Keys.W, Keys.Up)) { pos.Y -= 1; positionChanged = true; }
            if (input.IsKeyJustPressed(Keys.A, Keys.Left)) { pos.X -= 1; positionChanged = true; }
            if (input.IsKeyJustPressed(Keys.S, Keys.Down)) { pos.Y += 1; positionChanged = true; }
            if (input.IsKeyJustPressed(Keys.D, Keys.Right)) { pos.X += 1; positionChanged = true; }

            if (positionChanged)
            {
                pos.X = Math.Clamp(pos.X, 0, GameSettings.MapWidth - 1);
                pos.Y = Math.Clamp(pos.Y, 0, GameSettings.MapHeight - 1);
                world.AddComponent(entity, pos);
            }
        }
    }
}
