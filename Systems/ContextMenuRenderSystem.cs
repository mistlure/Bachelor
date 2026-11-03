using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Draw(World world, SpriteBatch spriteBatch, Dictionary<string, Texture2D[]> uiTextures)
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

            float startX = screenPosition.X;
            float startY = screenPosition.Y;
            float cellSpacing = 20f;

            for (int i = 0; i < menu.Options.Count; i++)
            {
                var option = menu.Options[i];

                if (uiTextures.TryGetValue(option.IconKey, out var iconTextures) && iconTextures != null && iconTextures.Length > 0)
                {
                    Texture2D iconTexture = iconTextures[0];
                    Vector2 iconPosition = new Vector2(startX + (i * cellSpacing), startY);

                    bool isSelected = (i == menu.SelectedIndex);

                    if (isSelected)
                    {
                        spriteBatch.Draw(
                            iconTexture,
                            iconPosition + new Vector2(0, -2),
                            Color.White
                        );
                    }
                    else
                    {
                        spriteBatch.Draw(
                            iconTexture,
                            iconPosition,
                            Color.DarkGray
                        );
                    }
                }
            }
        }
    }
}