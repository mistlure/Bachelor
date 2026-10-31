using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;

namespace Bachelor.Entities
{
    public class ContextMenuFactory
    {
        public Entity CreateContextMenu(World world, int targetEntityId, List<ContextMenuOption> options)
        {
            var entity = world.CreateEntity();

            var targetEntity = new Entity(targetEntityId);
            var targetPos = world.TryGetComponent<PositionComponent>(targetEntity);

            if (targetPos.HasValue)
            {
                world.AddComponent(entity, targetPos.Value);
            }

            world.AddComponent(entity, new ContextMenuComponent
            {
                TargetEntityId = targetEntityId,
                Options = options,
                SelectedIndex = 1
            });

            return entity;
        }
    }
}
