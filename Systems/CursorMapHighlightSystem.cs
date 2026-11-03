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
        private List<int> _highlightedIds = new();
        public void ComputeHighlight(World world, TileMap tileMap)
        {
            _highlightedIds.Clear();

            int? cursorId = world.GetEntityIdsWith<CursorComponent>().FirstOrDefault();
            if (!cursorId.HasValue)
            {
                return;
            }
            var cursorEntity = new Entity(cursorId.Value);

            var position = world.TryGetComponent<PositionComponent>(cursorEntity);
            if (!position.HasValue)
            {
                return;
            }

            int x = position.Value.X;
            int y = position.Value.Y;

            var entitiesAtCell = tileMap.GetEntityIdsAtPosition(x, y);
            if (entitiesAtCell != null)
            {
                _highlightedIds.AddRange(entitiesAtCell);
            }
        }

        public IReadOnlyList<int> GetHighlightedIds()
        {
            return _highlightedIds;
        }
    }
}
