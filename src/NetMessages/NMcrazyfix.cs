using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class NMcrazyfix : NMEvent
    {
        public Holdable h;
        public CrazyDiamond d;

        public NMcrazyfix()
        {

        }

        public NMcrazyfix(Holdable hold, CrazyDiamond duck)
        {
            h = hold;
            d = duck;
        }
        public override void Activate()
        {
            if (h != null && d != null)
            {
                d.DoFix(h);
            }
        }
    }
}
