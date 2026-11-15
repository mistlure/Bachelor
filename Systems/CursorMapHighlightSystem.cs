using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;

namespace Bachelor.Systems
{
    public class CursorMapHighlightSystem
    {
        private readonly List<int> _highlightedIds = new();

        public void ComputeHighlight(World world, TileMap tileMap)
        {
            ClearHighlight(world);

            if (!world.TryGetSingleton<CursorComponent>(out var cursorEntity))
            {
                return;
            }

            var position = world.TryGetComponent<PositionComponent>(cursorEntity);
            if (!position.HasValue)
            {
                return;
            }

            int x = position.Value.X;
            int y = position.Value.Y;

            foreach (var entityId in tileMap.GetEntityIdsAtPosition(x, y))
            {
                var entity = new Entity(entityId);
                if (!world.HasComponent<TileComponent>(entity))
                {
                    continue;
                }

                world.AddComponent(entity, new HighlightedComponent());
                _highlightedIds.Add(entityId);
            }
        }

        private void ClearHighlight(World world)
        {
            foreach (var entityId in _highlightedIds)
            {
                world.RemoveComponent<HighlightedComponent>(new Entity(entityId));
            }

            _highlightedIds.Clear();
        }
    }
}
