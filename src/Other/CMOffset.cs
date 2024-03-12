using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    public class CMOffset : Thing
    {
        public CMOffset(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Empty.png"), 16, 16, false);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _canFlip = false;
            _visibleInGame = false;
        }
        public override void Update()
        {
            base.Update();
            _visibleInGame = false;
        }
    }
}