using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Config;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bachelor.Systems
{
    public class RenderSystem
    {
        public void Draw(SpriteBatch spriteBatch, World world, Dictionary<TileType, Texture2D[]> tileTextures)
        {
            foreach(var entityId in world.GetAllEntityIds())
            {
                // (!) Creates an object every frame for each tile
                var entity = new Entity(entityId);

                var position = world.TryGetComponent<PositionComponent>(entity);
                var tile = world.TryGetComponent<TileComponent>(entity);

                if(position != null && tile != null)
                {
                    if (tileTextures.TryGetValue(tile.Value.TileType, out var textures) && textures != null &&
                        textures.Length != 0)
                    {
                        var texture = textures[0];

                        int tileWidth = GameSettings.TileWidth;
                        int tileHeight = GameSettings.TileHeight;

                        // (!) Divides every frame (hard)
                        float screenX = (position.Value.X - position.Value.Y) * (tileWidth / 2f);
                        float screenY = (position.Value.X + position.Value.Y) * (tileHeight / 2f);

                        var screenPosition = new Vector2(screenX, screenY) + new Vector2(GameSettings.TempOffsetWidth, GameSettings.TempOffsetHeight);

                        spriteBatch.Draw(texture, screenPosition, Color.White);
                    }
                }
            }    
        }
    }
}
