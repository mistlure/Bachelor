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
                new ContextMenuOption { ActionId = "close", IconKey = "ExitIcon" },
                new ContextMenuOption { ActionId = "set_grass", IconKey = "GrassTileIcon" },
                new ContextMenuOption { ActionId = "set_sand", IconKey = "SandTileIcon" },
            };
        }
    }
}
