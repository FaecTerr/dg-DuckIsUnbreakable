using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff|Blocks")]
    public class InvisBlock : Block
    {
        public EditorProperty<int> order;
        public EditorProperty<int> maxOrder;
        public EditorProperty<float> duration;
        public EditorProperty<bool> main;
        public EditorProperty<bool> swap;
        public bool mainExist = false;
        private float _time = 0f;
        private int currentOrder = 0;
        public SpriteMap _sprite;
        private bool init = false;

        public InvisBlock(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/InvisBlock"), 16, 16);
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            base.depth = -0.5f;
            _editorName = "Invis Block";
            thickness = 4f;
            _sprite.AddAnimation("income", 1f, false, new int[] {
                7,
                6,
                5,
                4,
                3,
                2,
                1
            });
            _sprite.AddAnimation("idle", 1f, false, new int[0]);
            _sprite.AddAnimation("out", 1f, false, new int[] {
                1,
                2,
                3,
                4,
                5,
                6,
                7
            });
            physicsMaterial = PhysicsMaterial.Metal;
            order = new EditorProperty<int>(0, this, 0f, 8f, 1f, null);
            maxOrder = new EditorProperty<int>(0, this, 0f, 8f, 1f, null);
            duration = new EditorProperty<float>(1f, this, 0.1f, 60f, 0.2f);
            main = new EditorProperty<bool>(false);
            swap = new EditorProperty<bool>(false);
            _sprite.SetAnimation("out");
            graphic = _sprite;
        }
        public virtual void GoInvis()
        {
            collisionOffset = new Vec2(8f, 8f);
            collisionSize = new Vec2(-16f, -16f);
            ignoreCollisions = true;
            solid = false;
            _sprite.SetAnimation("out");
            IEnumerable<PhysicsObject> things = Level.CheckLineAll<PhysicsObject>(base.topLeft + new Vec2(-2f, -3f), base.topRight + new Vec2(2f, -3f));
            using (IEnumerator<PhysicsObject> enumerator = things.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PhysicsObject thing = enumerator.Current;
                    thing._sleeping = false;
                    if (thing.grounded)
                        thing.vSpeed += 0.1f;
                }
            }
        }
        public virtual void ReturnInvis()
        {
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            ignoreCollisions = false;
            solid = true;
            _sprite.SetAnimation("income");
            IEnumerable<PhysicsObject> things = Level.CheckLineAll<PhysicsObject>(base.topLeft + new Vec2(-2f, -3f), base.topRight + new Vec2(2f, -3f));
            using (IEnumerator<PhysicsObject> enumerator = things.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PhysicsObject thing = enumerator.Current;
                    thing._sleeping = false;
                    if (thing.grounded)
                        thing.vSpeed += 0.1f;
                }
            }
        }
        public override void Update()
        {
            base.Update();
            if (init == false)
            {
                init = true;
                if (swap == true)
                {
                    ReturnInvis();
                }
                else
                {
                    GoInvis();
                }
            }
            /*foreach(InvisBlock ib in Level.current.things[typeof(InvisBlock)])
            {
                if (ib != null && ib != this && main == false && mainExist == false)
                {
                    if (ib.main == true || ib.mainExist == true)
                    {
                        mainExist = true;
                        return;
                    }
                    else
                    {
                        ib.mainExist = true;
                        main = true;
                    }
                }
            }*/
            if (main == true)
            {
                if (_time <= 0f)
                {
                    if (currentOrder != maxOrder)
                    {
                        _time = duration;
                        currentOrder++;
                    }
                    else
                    {
                        _time = duration;
                        currentOrder = 0;
                    }
                }
                else
                {
                    _time -= 0.01666666f;
                }
                foreach (InvisBlock ib in Level.current.things[typeof(InvisBlock)])
                {
                    if (ib != null)
                    {
                        if (ib.order == currentOrder)
                        {
                            if (swap == true)
                            {
                                ib.GoInvis();
                                IEnumerable<PhysicsObject> things = Level.CheckLineAll<PhysicsObject>(base.topLeft + new Vec2(-10f, -2f), base.topRight + new Vec2(10f, -4f));
                                using (IEnumerator<PhysicsObject> enumerator = things.GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        PhysicsObject thing = enumerator.Current;
                                        thing._sleeping = false;
                                        if (thing.grounded)
                                            thing.vSpeed += 0.02f;
                                    }
                                }
                            }
                            else
                            {
                                ib.ReturnInvis();
                                IEnumerable<PhysicsObject> things = Level.CheckLineAll<PhysicsObject>(base.topLeft + new Vec2(-10f, -2f), base.topRight + new Vec2(10f, -4f));
                                using (IEnumerator<PhysicsObject> enumerator = things.GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        PhysicsObject thing = enumerator.Current;
                                        thing._sleeping = false;
                                        if (thing.grounded)
                                            thing.vSpeed += 0.02f;
                                    }
                                }
                            }
                        }
                        else if ((ib.order + 1 == currentOrder && ib.order != maxOrder) || (ib.order == maxOrder && currentOrder == 0))
                        {
                            if (swap == true)
                            {
                                ib.ReturnInvis();
                                IEnumerable<PhysicsObject> things = Level.CheckLineAll<PhysicsObject>(base.topLeft + new Vec2(-8f, -2f), base.topRight + new Vec2(8f, -4f));
                                using (IEnumerator<PhysicsObject> enumerator = things.GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        PhysicsObject thing = enumerator.Current;
                                        thing._sleeping = false;
                                        if (thing.grounded)
                                            thing.vSpeed += 0.02f;
                                    }
                                }
                            }
                            else
                            {
                                ib.GoInvis();
                                IEnumerable<PhysicsObject> things = Level.CheckLineAll<PhysicsObject>(base.topLeft + new Vec2(-8f, -2f), base.topRight + new Vec2(8f, -4f));
                                using (IEnumerator<PhysicsObject> enumerator = things.GetEnumerator())
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        PhysicsObject thing = enumerator.Current;
                                        thing._sleeping = false;
                                        if(thing.grounded)
                                            thing.vSpeed += 0.02f;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
