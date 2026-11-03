using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Helpers;
using Microsoft.Xna.Framework.Input;

namespace Bachelor.Systems
{
    public class ContextMenuInputSystem
    {
        private KeyboardState _previousKeyboardState;

        public void HandleInput(World world)
        {
            int? globalStateId = world.GetEntityIdsWith<ContextMenuModeComponent>().FirstOrDefault();
            if (!globalStateId.HasValue)
            {
                _previousKeyboardState = Keyboard.GetState();
                return;
            }

            int? menuId = world.GetEntityIdsWith<ContextMenuComponent>().FirstOrDefault();
            if (!menuId.HasValue)
            {
                _previousKeyboardState = Keyboard.GetState();
                return;
            }

            var menuEntity = new Entity(menuId.Value);
            var menuOpt = world.TryGetComponent<ContextMenuComponent>(menuEntity);
            if (!menuOpt.HasValue)
            {
                _previousKeyboardState = Keyboard.GetState();
                return;
            }

            var menu = menuOpt.Value;
            var currentKeyboardState = Keyboard.GetState();

            if (world.HasComponent<InputLockComponent>(menuEntity))
            {
                world.RemoveComponent<InputLockComponent>(menuEntity);
                _previousKeyboardState = currentKeyboardState;
                return;
            }

            bool stateChanged = false;

            if (InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.A) ||
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Left))
            {
                menu.SelectedIndex--;
                if (menu.SelectedIndex < 0)
                {
                    menu.SelectedIndex = menu.Options.Count - 1;
                }
                stateChanged = true;
            }

            if (InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.D) ||
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Right))
            {
                menu.SelectedIndex++;
                if (menu.SelectedIndex >= menu.Options.Count)
                {
                    menu.SelectedIndex = 0;
                }
                stateChanged = true;
            }

            if (InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Escape))
            {
                CloseMenu(world, menuEntity, globalStateId.Value);
                _previousKeyboardState = currentKeyboardState;
                return;
            }

            bool isConfirmPressed =
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Space) ||
                InputHelper.IsKeyJustPressed(currentKeyboardState, _previousKeyboardState, Keys.Enter);

            if (isConfirmPressed)
            {
                ExecuteSelectedOption(menu);
                CloseMenu(world, menuEntity, globalStateId.Value);
                _previousKeyboardState = currentKeyboardState;
                return;
            }

            if (stateChanged)
            {
                world.AddComponent(menuEntity, menu);
            }

            _previousKeyboardState = currentKeyboardState;
        }

        private void CloseMenu(World world, Entity menuEntity, int globalStateEntityId)
        {
            world.DestroyEntity(menuEntity);

            var globalEntity = new Entity(globalStateEntityId);
            if (world.HasComponent<ContextMenuModeComponent>(globalEntity))
            {
                world.RemoveComponent<ContextMenuModeComponent>(globalEntity);
                world.AddComponent(globalEntity, new CursorWorldModeComponent());
            }
        }

        private void ExecuteSelectedOption(ContextMenuComponent menu)
        {

        }
    }
}