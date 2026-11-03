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
using Bachelor.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bachelor.Systems
{
    public class RenderSystem
    {
        public void Draw(SpriteBatch spriteBatch, World world, Dictionary<TileType, Texture2D[]> tileTextures, IEnumerable<int> highlightedIds)
        {
            var highlightedEntityIds = new HashSet<int>(highlightedIds);

            foreach (var entityId in world.GetAllEntityIds())
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

                        var screenPosition = IsometricHelper.ToScreenCoordinates(position.Value.X, position.Value.Y);



                        Color drawColor = highlightedEntityIds.Contains(entityId)
                        ? new Color(200, 235, 255, 180)
                        : Color.White;

                        spriteBatch.Draw(texture, screenPosition, drawColor);
                    }
                }
            }    
        }
    }
}
