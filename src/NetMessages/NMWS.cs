using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class NMWS : NMEvent
    {
        public Duck d;
        public bool mem;

        public NMWS()
        {

        }

        public NMWS(Duck target, bool isMemory)
        {
            d = target;
            mem = isMemory;
        }
        public override void Activate()
        {
            if (d != null)
            {
                if (mem)
                {
                    d.immobilized = true;

                    Level.Add(new MemoryDisk(d.position.x, d.position.y));
                }
                else
                {
                    if (d.HasEquipment(typeof(Stand)))
                    {
                        d.Unequip(d.GetEquipment(typeof(Stand)));
                    }
                }
            }
        }
    }
}
