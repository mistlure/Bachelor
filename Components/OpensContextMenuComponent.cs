using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Components
{
    public struct ContextMenuOption
    {
        public string ActionId;
        public string IconKey;
    }

    public struct OpensContextMenuComponent : IComponent
    {
        public List<ContextMenuOption> Options { get; set; }
    }
}
