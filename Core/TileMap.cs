using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Core
{
    public class TileMap
    {
        private readonly Dictionary<(int x, int y), List<int>> _tileMap = new();

        public void RegisterEntity(int x, int y, int entityId)
        {
            var position = (x, y);
            if (!_tileMap.ContainsKey(position))
            {
                _tileMap[position] = new List<int>();
            }

            if (!_tileMap[position].Contains(entityId))
            {
                _tileMap[position].Add(entityId);
            }
        }

        public void UnregisterEntity(int x, int y, int entityId)
        {
            var position = (x, y);
            if (_tileMap.ContainsKey(position))
            {
                _tileMap[position].Remove(entityId);

                if (_tileMap[position].Count == 0)
                {
                    _tileMap.Remove(position);
                }
            }
        }

        public IEnumerable<int> GetEntityIdsAtPosition(int x, int y)
        {
            var position = (x, y);
            if (_tileMap.TryGetValue(position, out var entityIds))
            {
                return entityIds;
            }

            return Enumerable.Empty<int>();
        }
    }
}
