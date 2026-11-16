using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor.Components;

namespace Bachelor.Config
{
    public static class ContextMenuConfig
    {
        public static List<ContextMenuOption> GetWaterTileOptions()
        {
            return new List<ContextMenuOption>
            {
                new ContextMenuOption { ActionId = ActionIds.Close, IconKey = "ExitIcon" },
                new ContextMenuOption { ActionId = ActionIds.SetGrass, IconKey = "GrassTileIcon" },
                new ContextMenuOption { ActionId = ActionIds.SetSand, IconKey = "SandTileIcon" },
            };
        }
    }
}
