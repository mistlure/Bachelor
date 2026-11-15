using System.Linq;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;
using Microsoft.Xna.Framework.Input;

namespace Bachelor.Systems
{
    public class ContextMenuTriggerSystem
    {
        public void HandleInput(World world, TileMap tileMap, InputState input)
        {
            var worldModeIds = world.GetEntityIdsWith<CursorWorldModeComponent>();
            if (!worldModeIds.Any())
            {
                return;
            }

            if (!input.IsKeyJustPressed(Keys.F, Keys.Space))
            {
                return;
            }

            if (!world.TryGetSingleton<CursorComponent>(out var cursorEntity))
            {
                return;
            }

            var cursorPosition = world.TryGetComponent<PositionComponent>(cursorEntity);
            if (!cursorPosition.HasValue)
            {
                return;
            }

            int targetX = cursorPosition.Value.X;
            int targetY = cursorPosition.Value.Y;

            int? targetEntityId = null;
            foreach (var entityId in tileMap.GetEntityIdsAtPosition(targetX, targetY))
            {
                if (world.HasComponent<OpensContextMenuComponent>(new Entity(entityId)))
                {
                    targetEntityId = entityId;
                    break;
                }
            }

            if (!targetEntityId.HasValue)
            {
                return;
            }

            var targetEntity = new Entity(targetEntityId.Value);
            var optionsOpt = world.TryGetComponent<OpensContextMenuComponent>(targetEntity);
            if (!optionsOpt.HasValue)
            {
                return;
            }

            var menuFactory = new ContextMenuFactory();
            menuFactory.CreateContextMenu(world, targetEntityId.Value, optionsOpt.Value.Options);


            var globalEntity = new Entity(worldModeIds.First());
            world.RemoveComponent<CursorWorldModeComponent>(globalEntity);
            world.AddComponent(globalEntity, new ContextMenuModeComponent());

            if (input.IsKeyJustPressed(Keys.F)) input.Consume(Keys.F);
            if (input.IsKeyJustPressed(Keys.Space)) input.Consume(Keys.Space);
        }
    }
}