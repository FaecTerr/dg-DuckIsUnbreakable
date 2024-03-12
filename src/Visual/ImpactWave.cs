using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class ImpactWave : Thing
    {
        private float radius;
        private float size;
        private float time = 1f;
        private bool reverse;
        private Color color;
        private float currentRadius;
        public ImpactWave(float xpos, float ypos, float rad, float siz, float tim, Color col, bool rev) : base(xpos, ypos)
        {
            position.x = xpos;
            position.y = ypos;
            radius = rad;
            depth = -0.5f;
            alpha = 1f;
            time = tim;
            size = siz;
            reverse = rev;
            currentRadius = radius;
            layer = Layer.Foreground;
        }

        public override void Update()
        {
            base.Update();
            if (time > 0f)
            {
                time -= 0.01666666f;
            }
            if (time <= 0f)
            {
                Level.Remove(this);
            }
            if (reverse == false)
            {
                currentRadius = Lerp.Float(currentRadius, size, size / 8);
                /*if (currentRadius > radius)
                {
                    Level.Remove(this);
                }*/
            }
            if (reverse == true)
            {
                currentRadius = Lerp.Float(currentRadius, 0, size / 8);
                /*if (currentRadius < 0f)
                {
                    Level.Remove(this);
                }*/
            }
        }
        public override void Draw()
        {
            Graphics.DrawCircle(position, currentRadius, color, size-currentRadius, 1f, 32); 
            base.Draw();
        }
    }
}
