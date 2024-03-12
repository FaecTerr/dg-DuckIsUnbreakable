using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff")]
    public class weakFloor : Block, IPlatform, ISequenceItem, IDontMove
    {
        public SpriteMap _sprite;
        public float totalWeight;
        public EditorProperty<int> floorHeight;
        public StateBinding _weightBind = new StateBinding("totalWeight", -1, false, false);
        public weakFloor(float xpos, float ypos) : base(xpos, ypos)
        {
            floorHeight = new EditorProperty<int>(2, this, 1f, 24f, 1f, null, false, false);
            //valid = new EditorProperty<bool>(false, this, 0f, 1f, 0.1f, null, false, false);
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/weakFloor.png"), 32, 16);
            base.graphic = _sprite;
            //_barSprite = new Sprite("windowBars", 8f, 1f);
            // _borderSprite = new Sprite("window32border", 0f, 0f);
            base.sequence = new SequenceItem(this);
            base.sequence.type = SequenceItemType.Goody;
            center = new Vec2(16f, 8f);
            collisionSize = new Vec2(32f, 16f);
            collisionOffset = new Vec2(-16f, -12f);
            base.depth = -0.5f;
            _editorName = "Weak Floor";
            thickness = 0.3f;
            base.alpha = 0.7f;
            //base.breakForce = 3f;
            _canFlip = false;
            hugWalls = (WallHug.Left | WallHug.Right);
            _translucent = true;
            UpdateHeight();
        }
        public override void EditorPropertyChanged(object property)
        {
            UpdateHeight();
        }
        public virtual void UpdateHeight()
        {
            float high = (float)floorHeight.value * 16f;
            //center = new Vec2(high/2, 4f);
            collisionSize = new Vec2(high, 8f);
            collisionOffset = new Vec2(-high/2, -8f);
            if (high == 0)
            {
                collisionSize = new Vec2(32f, 8f);
                collisionOffset = new Vec2(-16f, -8f);
                xscale = 1f;
            }
            else
            {
                xscale = high / 32f;
            }
        }
        public override void Initialize()
        {
            UpdateHeight();
        }

        public virtual void Broke()
        {
            Level.Remove(this);
            if (base.isServerForObject)
            {
                for (int i = 0; i < 6; i++)
                {
                    Level.Add(new WoodParts(x, y));
                }
            }
            SFX.Play(GetPath("SFX/FloorBroke.wav"), 1f);
            foreach (PhysicsObject p in Level.CheckRectAll<PhysicsObject>(topLeft, topRight + new Vec2(0f, -4f)))
            {
                if (p != null)
                {
                    p.velocity = new Vec2(0f, 1f);
                }
            }
        }

        public override void Update()
        {
            base.Update();
            float high = (float)floorHeight.value * 16f;
            if (high != 0)
                _sprite.xscale = high / 32f;
            /*if (totalWeight >= 10f)
            {
                Level.Remove(this);
                if (base.isServerForObject)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Level.Add(new WoodParts(x, y));
                    }
                }
            }*/
            foreach (PhysicsObject h1 in Level.CheckRectAll<PhysicsObject>(topLeft, new Vec2(topRight.x, topRight.y + 4f)))
            {
                if (h1 != null)
                {
                    foreach (PhysicsObject h2 in Level.CheckRectAll<PhysicsObject>(topLeft, new Vec2(topRight.x, topRight.y + 4f)))
                    {
                        if (h2 != null && h2 != h1)
                        {
                            if (h1.grounded && h2.grounded)
                            {
                                totalWeight = 10f;
                                Broke();
                                h1.velocity = h2.velocity = new Vec2(0f, 1f);
                                _sprite.frame = 3;
                            }
                        }
                        if ((h2 == null || h2 == h1) && h1.grounded)
                        {
                            totalWeight = h1.weight;
                            _sprite.frame = 1;
                        }
                    }
                }
                if (h1 == null || !h1.grounded)
                {
                    totalWeight = 0f;
                    _sprite.frame = 0;
                }
            }
        }
    }
}
