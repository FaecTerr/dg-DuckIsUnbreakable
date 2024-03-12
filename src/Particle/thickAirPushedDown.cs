using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class thickAirPushed : Thing
    {
        public int lifeTime = 4;
        public Vec2 pushVector = new Vec2(0, 5);
        public Vec2 moveVector = new Vec2(0, 7);
        public thickAirPushed(float xpos, float ypos) : base(xpos, ypos)
        {

        }

        public override void Update()
        {
            base.Update();
            lifeTime--;
            if(lifeTime <= 0)
            {
                Level.Remove(this);
            }
        }

        public override void Draw()
        {
            Graphics.DrawLine(position, position + pushVector, Color.White * 0.6f, 0.7f);
            position += moveVector;
            base.Draw();
        }
    }
}
