using System.Collections.Generic;

namespace Bachelor.Config
{
    public static class ActionCostConfig
    {
        private static readonly Dictionary<string, int> Costs = new()
        {
            [ActionIds.Close] = 0,
            [ActionIds.SetGrass] = 10,
            [ActionIds.SetSand] = 25,
            [ActionIds.SetSeedbed] = 10,
        };

        public static int GetCost(string actionId)
        {
            return Costs.TryGetValue(actionId, out var cost) ? cost : 0;
        }
    }
}
