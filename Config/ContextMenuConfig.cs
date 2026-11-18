using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;
using Bachelor.Enums;

namespace Bachelor.Config
{
    public static class ContextMenuConfig
    {
        public static List<ContextMenuOption> GetTileOptions(TileType tileType)
        {
            return tileType switch
            {
                TileType.Water => new List<ContextMenuOption>
                {
                    new ContextMenuOption { ActionId = ActionIds.Close, IconKey = "ExitIcon" },
                    new ContextMenuOption { ActionId = ActionIds.SetGrass, IconKey = "GrassTileIcon" },
                    new ContextMenuOption { ActionId = ActionIds.SetSand, IconKey = "SandTileIcon" },
                },
                TileType.Grass => new List<ContextMenuOption>
                {
                    new ContextMenuOption { ActionId = ActionIds.Close, IconKey = "ExitIcon" },
                    new ContextMenuOption { ActionId = ActionIds.SetSeedbed, IconKey = "SeedbedTileIcon" },
                },
                TileType.Seedbed => new List<ContextMenuOption>
                {
                    new ContextMenuOption { ActionId = ActionIds.Close, IconKey = "ExitIcon" },
                },
                TileType.Sand => new List<ContextMenuOption>
                {
                    new ContextMenuOption { ActionId = ActionIds.Close, IconKey = "ExitIcon" },
                },
                _ => null,
            };
        }
    }
}
