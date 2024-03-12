using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    [EditorGroup("Faecterr's|Special|Traps")]
    public class Icicle : PhysicsObject
    {
        protected SpriteMap _sprite;
        private List<Thing> _hits = new List<Thing>();
        private bool triggered = false;
        public EditorProperty<int> style;
        public bool init;

        public Icicle(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Blocks/Icicle.png"), 8, 16, false);
            graphic = _sprite;
            center = new Vec2(4f, 8f);
            collisionSize = new Vec2(8f, 16f);
            collisionOffset = new Vec2(-4f, -8f);
            base.depth = -0.5f;
            _editorName = "Icicle";
            _canFlip = false;
            base.hugWalls = WallHug.Ceiling;
            weight = 0f;
            style = new EditorProperty<int>(-1, this, -1, 1, 1);
        }

        public override void Update()
        {
            base.Update();
            if(init == false)
            {
                init = true;
                if (style == -1)
                {
                    (graphic as SpriteMap).frame = Rando.Int(1);
                    return;
                }
                else
                    (graphic as SpriteMap).frame = style.value;
            }
            Duck d = Level.CheckRect<Duck>(new Vec2(position.x - 2f, position.y), new Vec2(position.x+2f, position.y +160f), null);
            if (d != null && triggered == false)
            {
                triggered = true;
                gravMultiplier = 1f;
                enablePhysics = true;
            }
            else if (triggered == false)
            {
                gravMultiplier = 0f;
                vSpeed = 0f;
                enablePhysics = false;
            }
            if (triggered == true)
            {
                IEnumerable<PhysicsObject> hits = Level.CheckRectAll<PhysicsObject>(base.topLeft, base.bottomRight);
                foreach (PhysicsObject hit in hits)
                {
                    if (!_hits.Contains(hit))
                    {
                        Grenade g = hit as Grenade;
                        if (g != null)
                        {
                            g.PressAction();
                        }
                        hit.clip.Add(owner as MaterialThing);
                        if (!hit.destroyed)
                        {
                            hit.Destroy(new DTImpact(this));
                        }

                        _hits.Add(hit);
                    }
                }
            }
            if (grounded)
            {
                SFX.Play(GetPath("SFX/Icicle.wav"), 1f, 0f, 0.0f, false);
                Level.Remove(this);
                Delete();
            }
        }

        public override void EditorPropertyChanged(object property)
        {
            if (style == -1)
            {
                (graphic as SpriteMap).frame = Rando.Int(1);
                return;
            }
            else
                (graphic as SpriteMap).frame = style.value;
        }

        protected void Delete()
        {
            if (base.isServerForObject)
            {
                for (int i = 0; i < 16; i++)
                {
                    Vec2 flyDir = Vec2.Zero;
                    hSpeed = ((Rando.Float(1f) > 0.5f) ? 1f : -1f) * Rando.Float(3f) + (float)Math.Sign(flyDir.x) * 0.5f;
                    vSpeed = -Rando.Float(1f);
                    Thing ins = new SnowFallParticle(base.x - 8f + Rando.Float(16f), base.y - 8f + Rando.Float(16f), new Vec2(hSpeed, vSpeed), false);
                    Level.Add(ins);
                }
                for (int j = 0; j < 5; j++)
                {
                    SmallSmoke s = SmallSmoke.New(base.x + Rando.Float(-6f, 6f), base.y + Rando.Float(-6f, 6f));
                    s.hSpeed += Rando.Float(-0.3f, 0.3f);
                    s.vSpeed -= Rando.Float(0.1f, 0.2f);
                    Level.Add(s);
                }
            }
        }
    }
}
