using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Components
{
    public struct ContextMenuComponent : IComponent
    {
        public int TargetEntityId;
        public List<ContextMenuOption> Options;
        public int SelectedIndex;
    }
}
