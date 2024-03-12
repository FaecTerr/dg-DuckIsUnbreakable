using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff|Blocks")]
    public class DeathBlock : InvisBlock
    {
        public DeathBlock(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/InvisBlockDeath.png"), 16, 16);
            _editorName = "Death Invis Block";
            _graphic = _sprite;
            _sprite.AddAnimation("income", 1f, false, new int[] {
                7,
                6,
                5,
                4,
                3,
                2,
                1
            });
            _sprite.AddAnimation("idle", 1f, false, new int[0]);
            _sprite.AddAnimation("out", 1f, false, new int[] {
                1,
                2,
                3,
                4,
                5,
                6,
                7
            });
        }
        public override void Update()
        {
            base.Update();
            if(solid == true)
            {
                foreach(Duck d in Level.CheckRectAll<Duck>(topLeft, bottomRight))
                {
                    d.Kill(new DTCrush(d));
                }
                foreach (Ragdoll d in Level.CheckRectAll<Ragdoll>(topLeft, bottomRight))
                {
                    d._duck.Kill(new DTCrush(d._duck));
                }
            }
        }
    }
}
