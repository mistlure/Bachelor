using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Config;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bachelor.Systems
{
    public class ContextMenuRenderSystem
    {
        public void Draw(World world, SpriteBatch spriteBatch, Texture2D menuTexture)
        {
            int? menuId = world.GetEntityIdsWith<ContextMenuComponent>().FirstOrDefault();
            if (!menuId.HasValue)
            {
                return;
            }

            var menuEntity = new Entity(menuId.Value);

            var positionOpt = world.TryGetComponent<PositionComponent>(menuEntity);
            var menuOpt = world.TryGetComponent<ContextMenuComponent>(menuEntity);

            if (!positionOpt.HasValue || !menuOpt.HasValue)
            {
                return;
            }

            var pos = positionOpt.Value;
            var menu = menuOpt.Value;

            Vector2 screenPosition = IsometricHelper.ToScreenCoordinates(pos.X, pos.Y, -40f);

            spriteBatch.Draw(
                menuTexture,
                screenPosition,
                Color.White
            );
        }
    }
}