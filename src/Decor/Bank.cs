using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    [EditorGroup("Faecterr's|Details")]
    public class Bank : PhysicsObject
    {
        protected SpriteMap _sprite;
        private bool hitted = false;
        private int time;

        public Bank(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Decorative/Conserv.png"), 10, 15, false);
            _sprite.frame = Rando.Int(0, 14)*2;
            graphic = _sprite;
            center = new Vec2(5f, 12f);
            collisionSize = new Vec2(10f, 15f);
            collisionOffset = new Vec2(-5f, -12f);
            base.depth = -0.5f;
            _canFlip = false;
            gravMultiplier = 0.7f;
            thickness = 0f;
            weight = 0f;
            flammable = 0.3f;
            bouncy = 0.5f;
            base.hugWalls = WallHug.Floor;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            int i = Rando.Int(0, 1);
            if (hitted == false)
            {
                if (i == 1)
                {
                    SFX.Play(GetPath("SFX/CanDrop1.wav"), 1f, 0f, 0.0f, false);
                }
                if (i == 2)
                {
                    SFX.Play(GetPath("SFX/CanDrop2.wav"), 1f, 0f, 0.0f, false);
                }
                _sprite.frame += 1;
                hitted = true;
            }
            if (bullet.owner != null)
            {
                hSpeed += bullet.bulletSpeed / 5f * bullet.owner.offDir + Rando.Float(3f);
            }
            else
            {
                hSpeed += bullet.bulletSpeed / 5f * Rando.Float(-2f, 2f);
            }
            vSpeed -= Rando.Float(2f);
            time = Rando.Int(100, 200);
            if (hitted == true && time > 0)
            {
                angle += Rando.Float(0.05f)*time;
                time--;
            }
            return base.Hit(bullet, hitPos);
        }
        public override void Update()
        {
            base.Update();

            if (hitted == true && time > 0)
            {
                angle += Rando.Float(0.01f) * time;
                time--;
            }
        }
    }
}