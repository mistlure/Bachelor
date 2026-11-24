using System;
using System.Linq;
using Bachelor.Components;
using Bachelor.Config;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Helpers;

namespace Bachelor.Systems
{
    public class ActionExecutionSystem
    {
        public void Update(World world)
        {
            foreach (var requestId in world.GetEntityIdsWith<ActionRequestComponent>().ToList())
            {
                var requestEntity = new Entity(requestId);
                var request = world.TryGetComponent<ActionRequestComponent>(requestEntity);

                if (request.HasValue && ActionHandlerConfig.TryGetHandler(request.Value.ActionId, out var handler))
                {
                    ExecuteAction(world, request.Value, handler);
                }

                world.DestroyEntity(requestEntity);
            }
        }

        private static void ExecuteAction(World world, ActionRequestComponent request, Func<World, Entity, bool> handler)
        {
            if (!world.TryGetSingleton<WalletComponent>(out var walletEntity))
            {
                return;
            }

            if (!WalletHelper.HasEnoughCoins(world, request.ActionId))
            {
                return;
            }

            if (!handler(world, new Entity(request.TargetEntityId)))
            {
                return;
            }

            var wallet = world.TryGetComponent<WalletComponent>(walletEntity).Value;
            wallet.Balance -= ActionCostConfig.GetCost(request.ActionId);
            world.AddComponent(walletEntity, wallet);
        }
    }
}
