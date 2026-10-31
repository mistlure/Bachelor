using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Config;
using Bachelor.Helpers;
using Microsoft.Xna.Framework.Input;

namespace Bachelor.Systems
{
    public class ContextMenuTriggerSystem
    {
        private KeyboardState _previousKeyboardState;

        public void HandleInput(World world, TileMap tileMap)
        {
            var worldModeIds = world.GetEntityIdsWith<CursorWorldModeComponent>();
            if (!worldModeIds.Any())
            {
                _previousKeyboardState = Keyboard.GetState();
                return;
            }



            var currentKeyboardState = Keyboard.GetState();

            bool isInteractionPressed =
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.F) ||
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Space);

            _previousKeyboardState = currentKeyboardState;
            if (!isInteractionPressed)
            {
                return;
            }



            var cursorIds = world.GetEntityIdsWith<CursorComponent>().ToList();
            if (cursorIds.Count == 0)
            {
                return;
            }

            int cursorId = cursorIds[0];
            var cursorEntity = new Entity(cursorId);
            var cursorPosition = world.TryGetComponent<PositionComponent>(cursorEntity);

            if (!cursorPosition.HasValue)
            {
                return;
            }

            int targetX = cursorPosition.Value.X;
            int targetY = cursorPosition.Value.Y;



            var entityIdsAtCell = tileMap.GetEntityIdsAtPosition(targetX, targetY);

            int? targetEntityId = null;

            foreach (var entityId in entityIdsAtCell)
            {
                var entity = new Entity(entityId);
                if (world.HasComponent<OpensContextMenuComponent>(entity))
                {
                    targetEntityId = entityId;
                    break;
                }
            }

            if (!targetEntityId.HasValue)
            {
                return;
            }



            int globalStateEntityId = worldModeIds.First();
            var globalEntity = new Entity(globalStateEntityId);
            world.RemoveComponent<CursorWorldModeComponent>(globalEntity);

            world.AddComponent(globalEntity, new ContextMenuModeComponent());

            var targetEntity = new Entity(targetEntityId.Value);
            var menuOptionsComponent = world.TryGetComponent<OpensContextMenuComponent>(targetEntity);

            if (menuOptionsComponent.HasValue)
            {
                ContextMenuFactory menuFactory = new ContextMenuFactory();
                menuFactory.CreateContextMenu(world, targetEntityId.Value, menuOptionsComponent.Value.Options);
            }
        }
    }
}