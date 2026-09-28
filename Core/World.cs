using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using Bachelor.Components;
using Bachelor.Entities;

namespace Bachelor.Core
{
    public class World
    {
        private readonly Dictionary<int, List<IComponent>> _entities = new();
        private readonly Dictionary<(int x, int y), List<int>> _tileMap = new();

        private int _entitiesCount = 0;



        public void RegisterTile(int x, int y, int entityId)
        {
            var position = (x, y);
            if(!_tileMap.ContainsKey(position))
            {
                _tileMap[position] = new List<int>();
            }

            if (!_tileMap[position].Contains(entityId))
            {
                _tileMap[position].Add(entityId);
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



        public Entity CreateEntity()
        {
            var counterValue = _entitiesCount;
            var entity = new Entity(counterValue);
            _entities[entity.Id] = new List<IComponent>();

            _entitiesCount++;
            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            if(!_entities.ContainsKey(entity.Id))
            {
                return;
            }

            var position = TryGetComponent<PositionComponent>(entity);
            if(position != null)
            {
                var coordinates = (position.Value.X, position.Value.Y);
                if(_tileMap.ContainsKey(coordinates))
                {
                    _tileMap[coordinates].Remove(entity.Id);

                    if (_tileMap[coordinates].Count == 0)
                    {
                        _tileMap.Remove(coordinates);
                    }
                }
            }

            _entities.Remove(entity.Id);
        }



        public void AddComponent(Entity entity, IComponent component)
        {
            if(_entities.ContainsKey(entity.Id))
            {
                var components = _entities[entity.Id];
                var type = component.GetType();

                IComponent alreadyExistingComponent = null;
                foreach (var c in components)
                {
                    if(c.GetType() == type)
                    {
                        alreadyExistingComponent = c;
                        break;
                    }
                }
                if(alreadyExistingComponent != null)
                {
                    components.Remove(alreadyExistingComponent);
                }
                components.Add(component);
            }
        }

        public void RemoveComponent<T>(Entity entity) where T : struct, IComponent
        {
            if(_entities.TryGetValue(entity.Id, out var components))
            {
                IComponent alreadyExistingComponent = null;
                foreach (var c in components)
                {
                    if(c is T)
                    {
                        alreadyExistingComponent = c;
                        break;
                    }
                }
                if(alreadyExistingComponent != null)
                {
                    components.Remove(alreadyExistingComponent);
                }
            }
        }

        public IEnumerable<IComponent> GetComponents(Entity entity)
        {
            if(_entities.TryGetValue(entity.Id, out var components))
            {
                return components;
            }
            return Enumerable.Empty<IComponent>();
        }

        public T? TryGetComponent<T>(Entity entity) where T : struct, IComponent
        {
            if (_entities.TryGetValue(entity.Id, out var components))
            {
                foreach(var c in components)
                {
                    if(c is T result)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        public bool HasComponent<T>(Entity entity) where T : struct, IComponent
        {
            return TryGetComponent<T>(entity) != null;
        }

        public IEnumerable<int> GetAllEntityIds()
        {
            return _entities.Keys;
        }
    }
}