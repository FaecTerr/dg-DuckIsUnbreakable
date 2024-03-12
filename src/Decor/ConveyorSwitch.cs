using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff|Trails")]
    public class ConveyorSwitch : MaterialThing
    {
        public SpriteMap _sprite;
        private int activator = 0;
        private bool swap = false;
        public ConveyorSwitch(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Blocks/ConveyorSwitch.png"), 16, 18, false);
            graphic = _sprite;
            center = new Vec2(8f, 9f);
            collisionSize = new Vec2(10f, 18f);
            collisionOffset = new Vec2(-5f, -9f);
            thickness = 0.6f;
            _sprite.AddAnimation("to right", 0.4f, false, new int[] {
            9,
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
            0
            });
            _sprite.AddAnimation("to left", 0.4f, false, new int[] {
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9
            });
        }
        public override void Update()
        {
            base.Update();
            foreach(ConveyorBlock c in Level.CheckCircleAll<ConveyorBlock>(position, 16))
            {
                if (c.link == true)
                {
                    c.swap = swap;
                    c.activateOther = 1;
                    if (swap == true)
                    {
                        //c.mod = -1;
                    }
                    else
                    {
                        //c.mod = 1;
                    }
                }
            }
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (hitPos.x > position.x)
            {
                swap = true;
                if(_sprite.currentAnimation != "to left")
                {
                    _sprite.SetAnimation("to left");
                }
            }
            if(hitPos.x < position.x)
            {
                swap = false;
                if (_sprite.currentAnimation != "to right")
                {
                    _sprite.SetAnimation("to right");
                }
            }
            return base.Hit(bullet, hitPos);
        }
    }
}