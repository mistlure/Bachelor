using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Core;

namespace Bachelor.Entities
{
    public class GlobalGameStateFactory
    {
        public Entity CreateGlobalGameState(World world)
        {
            var test = world.GetEntityIdsWith<GlobalStateComponent>().ToList();
            if (test.Any())
            {
                throw new InvalidOperationException("Error! Attempting to create a second global game state.");
            }

            var entity = world.CreateEntity();

            world.AddComponent(entity, new CursorWorldModeComponent());

            return entity;
        }
    }
}
