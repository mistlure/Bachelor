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
            if (!world.TryGetSingleton<ContextMenuComponent>(out var menuEntity))
            {
                return;
            }

            var menuOpt = world.TryGetComponent<ContextMenuComponent>(menuEntity);
            if (!menuOpt.HasValue)
            {
                return;
            }

            var menu = menuOpt.Value;

            var positionOpt = world.TryGetComponent<PositionComponent>(new Entity(menu.TargetEntityId));
            if (!positionOpt.HasValue)
            {
                return;
            }

            var pos = positionOpt.Value;

            

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
                    bool hasEnoughCoins = WalletHelper.HasEnoughCoins(world, option.ActionId);

                    Color color = isSelected ? Color.White : Color.DarkGray;
                    if (!hasEnoughCoins)
                    {
                        color *= GameSettings.DisabledOptionBrightness;
                    }

                    spriteBatch.Draw(
                        iconTexture,
                        isSelected ? iconPosition + new Vector2(0, -2) : iconPosition,
                        color
                    );
                }
            }
        }
    }
}