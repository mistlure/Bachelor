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

            var cursorIds = world.GetEntityIdsWith<CursorComponent>().ToList();
            if (cursorIds.Count == 0)
            {
                return;
            }

            var cursorEntity = new Entity(cursorIds[0]);
            var position = world.TryGetComponent<PositionComponent>(cursorEntity);
            if (!position.HasValue)
            {
                return;
            }

            int x = position.Value.X;
            int y = position.Value.Y;

            // (!) GC constant load
            _highlightedIds = tileMap.GetEntityIdsAtPosition(x, y).ToList();
        }

        public IReadOnlyList<int> GetHighlightedIds()
        {
            return _highlightedIds;
        }
    }
}
