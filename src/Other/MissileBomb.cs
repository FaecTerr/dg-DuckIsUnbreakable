using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class MissileBomb : PhysicsObject
    {
        public Sprite _sprite;
        public MissileBomb(float xval, float yval) : base(xval, yval)
        {
            _sprite = new Sprite("missile", 0f, 0f);
            _sprite.CenterOrigin();
            graphic = _sprite;
            center = _sprite.center;
            _sprite.angleDegrees = 90;
            angleDegrees = 90;
            collisionOffset = new Vec2(-3f, -5f);
            collisionSize = new Vec2(6f, 10f);
        }
        public override void Touch(MaterialThing with)
        {
            if(with != null)
            {
                if (!(with is Holdable))
                {
                    ATMissileShrapnel shrap = new ATMissileShrapnel();
                    shrap.MakeNetEffect(position, false);
                    Random rand = null;
                    if (Network.isActive && isLocal)
                    {
                        rand = Rando.generator;
                        Rando.generator = new Random(NetRand.currentSeed);
                    }
                    List<Bullet> firedBullets = new List<Bullet>();
                    for (int i = 0; i < 12; i++)
                    {
                        float dir = (float)i * 30f - 10f + Rando.Float(20f);
                        shrap = new ATMissileShrapnel();
                        shrap.range = 15f + Rando.Float(5f);
                        Vec2 shrapDir = new Vec2((float)Math.Cos((double)Maths.DegToRad(dir)), (float)Math.Sin((double)Maths.DegToRad(dir)));
                        Bullet bullet = new Bullet(x + shrapDir.x * 8f, y - shrapDir.y * 8f, shrap, dir, null, false, -1f, false, true);
                        bullet.firedFrom = this;
                        firedBullets.Add(bullet);
                        Level.Add(bullet);
                        Level.Add(Spark.New(x + Rando.Float(-8f, 8f), y + Rando.Float(-8f, 8f), shrapDir + new Vec2(Rando.Float(-0.1f, 0.1f), Rando.Float(-0.1f, 0.1f)), 0.02f));
                        Level.Add(SmallSmoke.New(x + shrapDir.x * 8f + Rando.Float(-8f, 8f), y + shrapDir.y * 8f + Rando.Float(-8f, 8f)));
                    }
                    if (Network.isActive && isLocal)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 0, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                    if (Network.isActive && isLocal)
                    {
                        Rando.generator = rand;
                    }
                    IEnumerable<Window> windows = Level.CheckCircleAll<Window>(position, 30f);
                    foreach (Window w in windows)
                    {
                        if (isLocal)
                        {
                            Thing.Fondle(w, DuckNetwork.localConnection);
                        }
                        if (Level.CheckLine<Block>(position, w.position, w) == null)
                        {
                            w.Destroy(new DTImpact(this));
                        }
                    }
                    IEnumerable<PhysicsObject> phys = Level.CheckCircleAll<PhysicsObject>(position, 70f);
                    foreach (PhysicsObject p in phys)
                    {
                        if (isLocal && owner == null)
                        {
                            Thing.Fondle(p, DuckNetwork.localConnection);
                        }
                        if ((p.position - position).length < 30f)
                        {
                            p.Destroy(new DTImpact(this));
                        }
                        p.sleeping = false;
                        p.vSpeed = -2f;
                    }
                    HashSet<ushort> idx = new HashSet<ushort>();
                    IEnumerable<BlockGroup> blokzGroup = Level.CheckCircleAll<BlockGroup>(position, 50f);
                    foreach (BlockGroup block in blokzGroup)
                    {
                        if (block != null)
                        {
                            BlockGroup group = block;
                            new List<Block>();
                            foreach (Block bl in group.blocks)
                            {
                                if (Collision.Circle(position, 28f, bl.rectangle))
                                {
                                    bl.shouldWreck = true;
                                    if (bl is AutoBlock)
                                    {
                                        idx.Add((bl as AutoBlock).blockIndex);
                                    }
                                }
                            }
                            group.Wreck();
                        }
                    }
                    IEnumerable<Block> blokz = Level.CheckCircleAll<Block>(position, 28f);
                    foreach (Block block2 in blokz)
                    {
                        if (block2 is AutoBlock)
                        {
                            block2.skipWreck = true;
                            block2.shouldWreck = true;
                            if (block2 is AutoBlock)
                            {
                                idx.Add((block2 as AutoBlock).blockIndex);
                            }
                        }
                        else if (block2 is Door || block2 is VerticalDoor)
                        {
                            Level.Remove(block2);
                            block2.Destroy(new DTRocketExplosion(null));
                        }
                    }
                    if (Network.isActive && isLocal && idx.Count > 0)
                    {
                        Send.Message(new NMDestroyBlocks(idx));
                    }
                }
            }
            Level.Remove(this);
            base.Touch(with);
        }
    }
}
