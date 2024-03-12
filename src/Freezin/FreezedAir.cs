using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class FreezedAir : Thing
    {
        public Vec2 move;
        public float angleIncrement;
        public float scaleDecrement;
        public float fastGrowTimer;
        public float Timer;
        public float Sizer;

        public FreezedAir(float xval, float yval, Vec2 direct, float range, float sizer = 1f, float timer = 1f) : base(xval, yval)
        {
            velocity = new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f));
            xscale = Rando.Float(0.15f, 0.30f);
            yscale = xscale;
            angle = Maths.DegToRad(Rando.Float(360f));
            fastGrowTimer = 0.65f;
            Timer = 0.2f*timer;
            angleIncrement = Maths.DegToRad(Rando.Float(-1f, 1f));
            scaleDecrement = 0.00015f*sizer;
            Sizer = sizer;
            GraphicList graphicList = new GraphicList();

            Sprite graphic1 = new Sprite("smoke", 0.0f, 0.0f);
            graphic1.depth = 1f;
            graphic1.CenterOrigin();
            graphic1.color = Color.Turquoise;
            graphic1.alpha = 0.3f;
            graphicList.Add(graphic1);

            graphic = graphicList;

            center = new Vec2(0.0f, 0.0f);
            depth = 1f;
            move = direct * range;
            if (range <= 0f)
            {
                move = direct * 2f;
            }
        }
        public virtual void FreezeObject()
        {
            List<Duck> ducks = new List<Duck>();
            float radius = xscale;

            foreach (Duck duck in Level.CheckCircleAll<Duck>(position, radius * 12f))
            {
                if (duck.onFire == true)
                {
                    duck.onFire = false;
                }
                if (/*duck.heat < -2f &&*/ !ducks.Contains(duck))
                {
                    ducks.Add(duck);
                }
            }
            foreach (Ragdoll ragdoll in Level.CheckCircleAll<Ragdoll>(position, radius * 12f))
            {
                if (ragdoll._duck.onFire == true)
                {
                    ragdoll._duck.onFire = false;
                }
                if (/*ragdoll._duck.heat < -2f &&*/ !ducks.Contains(ragdoll._duck))
                {
                    ducks.Add(ragdoll._duck);
                }
            }
            foreach (Duck duck in ducks)
            {
                if (!duck.dead && !duck.HasEquipment(typeof(MagiciansR)) && !duck.HasEquipment(typeof(WarmArmor)))
                {
                    Level.Add(new FreezedDuck(position.x, position.y, duck));
                }
            }
            foreach (PhysicsObject po in Level.CheckCircleAll<PhysicsObject>(position, radius * 12f))
            {
                if (po.heat > -2f)
                    po.heat -= 0.1f;
            }
        }
        public override void Update()
        {
            base.Update();
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
            position += move;
            move *= new Vec2(0.99f, 0.99f);
            position += velocity;
            velocity *= new Vec2(0.9f, 0.9f);

            if (xscale < 0.100000001490116)
            {
                Level.Remove(this);
            }
            FreezeObject();
        }
    }
}
