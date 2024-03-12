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
    public class Bottle : PhysicsObject
    {
        protected SpriteMap _sprite;
        public Bottle(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Decorative/Bottles.png"), 7, 19, false);
            _sprite.frame = Rando.Int(0, 2);
            graphic = _sprite;
            center = new Vec2(4f, 9f);
            collisionSize = new Vec2(6f, 18f);
            collisionOffset = new Vec2(-3f, -9f);
            base.depth = -0.5f;
            _canFlip = false;
            thickness = 0f;
            weight = 0f;
            flammable = 0.3f;
            base.hugWalls = WallHug.Floor;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (base.isServerForObject)
            {
                for (int i = 0; i < 9; i++)
                {
                    Level.Add(new BottleParticles(x, y));
                }
            }

                int j = Rando.Int(0, 1);
                if (j == 1)
                {
                    SFX.Play(GetPath("SFX/GlassBreak1.wav"), 1f, 0f, 0.0f, false);
                }
                if (j == 2)
                {
                    SFX.Play(GetPath("SFX/GlassBreak2.wav"), 1f, 0f, 0.0f, false);
                }

            Level.Remove(this);
            return base.Hit(bullet, hitPos);
        }
    }
}