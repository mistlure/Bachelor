using Bachelor.Components;
using Bachelor.Config;
using Bachelor.Core;

namespace Bachelor.Helpers
{
    public static class WalletHelper
    {
        public static bool HasEnoughCoins(World world, string actionId)
        {
            if (!world.TryGetSingleton<WalletComponent>(out var walletEntity))
            {
                return false;
            }

            var wallet = world.TryGetComponent<WalletComponent>(walletEntity);
            return wallet.HasValue && wallet.Value.Balance >= ActionCostConfig.GetCost(actionId);
        }
    }
}
