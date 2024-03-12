using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    internal class FireParticle : Thing
    {
        public MagiciansR own;
        private SpriteMap _sprite;
        public float Angle;
        private float Speed;
        private int time = 30;
        public FireParticle(float xval, float yval, MagiciansR MR, float Ang) : base(xval, yval)
        {
            _sprite = new SpriteMap("smallFire", 16, 16);
            graphic = _sprite;
            angle = Rando.Float(2 * (float)Math.PI);
            center = new Vec2(8f, 8f);
            depth = 0.5f;
            Speed = Rando.Float(5f, 6f);
            Angle = Ang;
            own = MR;
        }
        public override void Update()
        {
            base.Update();
            time--;
            x += Speed * (float)Math.Cos((Math.PI *Angle) / 180);
            y += Speed * (float)Math.Sin((Math.PI * Angle) / 180);
            if (own != null)
            {
                if (Level.CheckCircle<Block>(position, 1f) != null)
                {
                    //Level.Remove(this);
                }

                else
                    foreach (MaterialThing materialThing in Level.CheckCircleAll<MaterialThing>(position, 6f))
                    {
                        if (materialThing.isServerForObject && materialThing != own && materialThing != own.owner && !(materialThing is FluidPuddle))
                        {
                            if (!materialThing.onFire && materialThing.flammable > 0f && materialThing.heat > 0.5f)
                                materialThing.Burn(materialThing.position, own.owner);
                            materialThing.DoHeatUp(0.1f);
                        }
                    }
            }
            if (isServerForObject && (time < 0 || x > Level.current.bottomRight.x + 200 || x < Level.current.topLeft.x - 200))
            {
                Level.Remove(this);
            }
        }
    }
}
