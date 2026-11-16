using System;
using System.Collections.Generic;
using Bachelor.Core;
using Bachelor.Entities;
using Bachelor.Enums;
using Bachelor.Helpers;

namespace Bachelor.Config
{
    public static class ActionHandlerConfig
    {
        private static readonly Dictionary<string, Func<World, Entity, bool>> Handlers = new()
        {
            [ActionIds.Close] = (world, target) => true,
            [ActionIds.SetGrass] = (world, target) => TileHelper.SetTileType(world, target, TileType.Grass),
            [ActionIds.SetSand] = (world, target) => TileHelper.SetTileType(world, target, TileType.Sand),
        };

        public static bool TryGetHandler(string actionId, out Func<World, Entity, bool> handler)
        {
            return Handlers.TryGetValue(actionId, out handler);
        }
    }
}
