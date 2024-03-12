using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class HazeSmoke : Thing
    {
        public Vec2 move;
        public float angleIncrement;
        public float scaleDecrement;
        public float fastGrowTimer;
        public float Timer;
        public Duck ownerDuck;
        private Vec2 direction = new Vec2(0f, 0f);

        public HazeSmoke(float xval, float yval, Duck own, Vec2 dir = new Vec2()) : base(xval, yval)
        {
            velocity = new Vec2(Rando.Float(-0.1f, 0.1f), Rando.Float(-0.1f, 0.1f));
            xscale = Rando.Float(0.22f, 0.23f);
            yscale = xscale;
            angle = Maths.DegToRad(Rando.Float(360f));
            fastGrowTimer = 1.65f;
            Timer = 4f;
            angleIncrement = Maths.DegToRad(Rando.Float(2f) - 1f);
            scaleDecrement = 0.00015f;
            move = new Vec2(Rando.Float(-0.001f, 0.001f), Rando.Float(-0.001f, 0.001f));

            GraphicList graphicList = new GraphicList();

            Sprite graphic1 = new Sprite("smoke", 0.0f, 0.0f);
            graphic1.depth = 1f;
            graphic1.CenterOrigin();
            graphic1.color = Color.Blue;
            graphic1.alpha = 0.3f;
            graphicList.Add(graphic1);

            Sprite graphic2 = new Sprite("smokeBack", 0.0f, 0.0f);
            graphic2.depth = 0.1f;
            graphic2.CenterOrigin();
            graphic2.color = Color.Blue;
            graphic2.alpha = 0.3f;
            graphicList.Add(graphic2);

            graphic = graphicList;

            center = new Vec2(0.0f, 0.0f);
            depth = 1f;
            direction = dir;
            ownerDuck = own;
        }

        public virtual void ToxicDuck()
        {
            List<Duck> ducks = new List<Duck>();
            float radius = xscale;

            foreach (Duck duck in Level.CheckCircleAll<Duck>(position, radius * 12f))
            {
                if (!ducks.Contains(duck) && duck != ownerDuck && !duck.HasEquipment(typeof(PurpleHaze)))
                {
                    ducks.Add(duck);
                }
            }

            foreach (Ragdoll ragdoll in Level.CheckCircleAll<Ragdoll>(position, radius * 12f))
            {
                if (!ducks.Contains(ragdoll._duck) && ragdoll._duck != ownerDuck && !ragdoll._duck.HasEquipment(typeof(PurpleHaze)))
                {
                    ducks.Add(ragdoll._duck);
                }
            }

            foreach (Duck duck in ducks)
            {
                if (!duck.dead && !PHT.ToxicDucks.ContainsKey(duck))
                {
                    Level.Add(new PHT(duck));
                }
            }

        }

        public override void Update()
        {
            angle += angleIncrement;

            if (Timer > 0)
            {
                Timer -= 0.01f;
            }
            else
            {
                xscale -= scaleDecrement;
                scaleDecrement += 0.0001f;
            }
            if (fastGrowTimer > 0)
            {
                fastGrowTimer -= 0.05f;
                xscale += 0.05f;
            }
            yscale = xscale;
            position += move + direction;
            direction *= 0.975f;
            position += velocity;
            velocity *= new Vec2(0.95f, 0.95f);

            if (xscale < 0.100000001490116)
            {
                Level.Remove(this);
            }
            ToxicDuck();
        }
    }
}
