using System;
using System.Collections.Generic;
using System.Linq;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Enums;

namespace Bachelor.Systems
{
    public class ActionExecutionSystem
    {
        private readonly Dictionary<string, Action<World, Entity>> _handlers;

        public ActionExecutionSystem()
        {
            _handlers = new Dictionary<string, Action<World, Entity>>
            {
                ["close"] = (world, target) => { },
                ["set_grass"] = (world, target) => SetTileType(world, target, TileType.Grass),
                ["set_sand"] = (world, target) => SetTileType(world, target, TileType.Sand),
            };
        }

        public void Update(World world)
        {
            foreach (var requestId in world.GetEntityIdsWith<ActionRequestComponent>().ToList())
            {
                var requestEntity = new Entity(requestId);
                var request = world.TryGetComponent<ActionRequestComponent>(requestEntity);

                if (request.HasValue && _handlers.TryGetValue(request.Value.ActionId, out var handler))
                {
                    handler(world, new Entity(request.Value.TargetEntityId));
                }

                world.DestroyEntity(requestEntity);
            }
        }

        private static void SetTileType(World world, Entity target, TileType tileType)
        {
            if (!world.HasComponent<TileComponent>(target))
            {
                return;
            }

            world.AddComponent(target, new TileComponent { TileType = tileType });
            world.RemoveComponent<OpensContextMenuComponent>(target);
        }
    }
}
