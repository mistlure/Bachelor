using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;
using Bachelor.Entities;
using Microsoft.Xna.Framework.Input;

namespace Bachelor.Systems
{
    public class ContextMenuInputSystem
    {
        public void HandleInput(World world, InputState input)
        {
            if (!world.TryGetSingleton<ContextMenuModeComponent>(out var globalEntity)) return;
            if (!world.TryGetSingleton<ContextMenuComponent>(out var menuEntity)) return;

            var menuOpt = world.TryGetComponent<ContextMenuComponent>(menuEntity);
            if (!menuOpt.HasValue) return;

            var menu = menuOpt.Value;
            bool stateChanged = false;

            if (input.IsKeyJustPressed(Keys.A, Keys.Left))
            {
                menu.SelectedIndex--;
                if (menu.SelectedIndex < 0) menu.SelectedIndex = menu.Options.Count - 1;
                stateChanged = true;
            }

            if (input.IsKeyJustPressed(Keys.D, Keys.Right))
            {
                menu.SelectedIndex++;
                if (menu.SelectedIndex >= menu.Options.Count) menu.SelectedIndex = 0;
                stateChanged = true;
            }

            if (input.IsKeyJustPressed(Keys.Escape))
            {
                CloseMenu(world, menuEntity, globalEntity);
                return;
            }

            if (input.IsKeyJustPressed(Keys.Space, Keys.Enter))
            {
                RequestSelectedOption(world, menu);
                CloseMenu(world, menuEntity, globalEntity);
                return;
            }

            if (stateChanged)
            {
                world.AddComponent(menuEntity, menu);
            }
        }

        private void CloseMenu(World world, Entity menuEntity, Entity globalEntity)
        {
            world.DestroyEntity(menuEntity);

            if (world.HasComponent<ContextMenuModeComponent>(globalEntity))
            {
                world.RemoveComponent<ContextMenuModeComponent>(globalEntity);
                world.AddComponent(globalEntity, new CursorWorldModeComponent());
            }
        }

        private void RequestSelectedOption(World world, ContextMenuComponent menu)
        {
            var option = menu.Options[menu.SelectedIndex];
            var request = world.CreateEntity();
            world.AddComponent(request, new ActionRequestComponent
            {
                ActionId = option.ActionId,
                TargetEntityId = menu.TargetEntityId
            });
        }
    }
}