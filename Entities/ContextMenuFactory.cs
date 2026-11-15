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

            world.AddComponent(entity, new ContextMenuComponent
            {
                TargetEntityId = targetEntityId,
                Options = options,
                SelectedIndex = options.Count > 1 ? 1 : 0
            });

            return entity;
        }
    }
}
