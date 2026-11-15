using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor.Entities
{
    public readonly struct Entity
    {
        public int Id { get; }
        public Entity(int id)
        {
            Id = id;
        }
    }
}
