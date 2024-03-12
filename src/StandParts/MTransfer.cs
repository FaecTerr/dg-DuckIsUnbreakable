using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class MTransfer : PhysicsObject
    {
        public SpriteMap _sprite;
        public Duck ownerDuck;

        public MTransfer(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Stands/MTransfer.png"), 12, 8, false);
            graphic = _sprite;
            center = new Vec2(6f, 4f);
            collisionOffset = new Vec2(-6f, -4f);
            collisionSize = new Vec2(12f, 8f);
            thickness = 0f;
            weight = 0f;
        }

        public override void Draw()
        {
            if(ownerDuck != null)
            {
                if (Level.CheckLine<Block>(position, ownerDuck.position) == null)
                {
                    float bulletrange = 0;
                    if (ownerDuck.holdObject != null && ownerDuck.holdObject is Gun && (ownerDuck.holdObject as Gun).ammoType != null)
                    {
                        bulletrange = (ownerDuck.holdObject as Gun).ammoType.range;
                    }
                    if ((ownerDuck.position - position).length < bulletrange)
                    {
                        Graphics.DrawLine(ownerDuck.position, position, Color.Aqua * 0.3f, 1f);

                        Duck d = Level.Nearest<Duck>(position.x, position.y, ownerDuck);
                        AutoBlock ab = Level.CheckLine<AutoBlock>(position, d.position);
                        if (d != null && ab == null && (position - d.position).length < bulletrange)
                        {
                            Graphics.DrawLine(d.position, position, Color.OrangeRed * 0.3f, 1f);
                        }
                    }
                }
            }
            base.Draw();
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (ownerDuck != null)
            {
                Duck d = Level.Nearest<Duck>(position.x, position.y, ownerDuck);
                AutoBlock ab = Level.CheckLine<AutoBlock>(position, d.position);
                if (d != null && ab == null && (position - d.position).length < bullet.range)
                {
                    //bullet.angle = Maths.PointDirection(position, d.position);
                    //bullet.start = position;
                    //bullet.end = d.position + new Vec2(Rando.Float(d.hSpeed)*16f, Rando.Float(d.vSpeed)*16f);
                    /*Bullet bullet1 = bullet;
                    Level.Remove(bullet);
                    bullet1.position = position;
                    bullet1.travelStart = position;
                    bullet1.travelEnd = d.position;
                    Level.Add(bullet1);*/
                    bullet.position = position;
                    bullet.travelStart = position;
                    bullet.travelEnd = d.position + new Vec2(Rando.Float(d.hSpeed) * 16f, Rando.Float(d.vSpeed) * 16f);
                    bullet.travelDirNormalized = d.position - position;
                }
            }
            return base.Hit(bullet, hitPos);
        }
    }
}
