using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff|Trails")]
    public class ConveyorBlock : Block
    {
        public EditorProperty<float> speed;
        public EditorProperty<bool> Swap;
        public EditorProperty<bool> link;
        private SpriteMap _sprite;
        private bool init = false;
        public int mod = 1;
        public bool swap;

        public int activateOther = 0;
        public List<ConveyorBlock> parent = new List<ConveyorBlock>();

        public ConveyorBlock(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/Conveyor"), 16, 8);
            center = new Vec2(8f, 4f);
            collisionOffset = new Vec2(-8f, -3f);
            collisionSize = new Vec2(16f, 6f);
            base.depth = -0.5f;
            _editorName = "Conveyor Block";
            hugWalls = WallHug.Floor;
            _sprite.AddAnimation("idle", 0.2f, true, new int[] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7
            });
            _sprite.AddAnimation("backward", 0.2f, true, new int[] {
                7,
                6,
                5,
                4,
                3,
                2,
                1,
                0
            });
            speed = new EditorProperty<float>(1f, this, 0.1f, 60f, 0.2f);
            Swap = new EditorProperty<bool>(false);
            link = new EditorProperty<bool>(false);
            _sprite.SetAnimation("idle");
            graphic = _sprite;
        }
        public override void EditorPropertyChanged(object property)
        {
            base.EditorPropertyChanged(property);
            if(swap == false)
            {
                _sprite.SetAnimation("idle");
            }
            else
            {
                _sprite.SetAnimation("backward");
            }
        }
        public override void Update()
        {
            if(init==false)
            {
                init = true;
                swap = Swap;
            }
            if ((swap == false && mod == 1) || ((swap == true && mod == -1)) && _sprite.currentAnimation != "idle")
            {
                _sprite.SetAnimation("idle");
            }
            else if ((swap == true && mod == 1) || ((swap == false && mod == -1)) && _sprite.currentAnimation != "backward")
            {
                _sprite.SetAnimation("backward");
            }

            base.Update();
            if (link == true)
            {
                if (activateOther == 1)
                {
                    foreach (ConveyorBlock conv in Level.CheckCircleAll<ConveyorBlock>(position, 16f))
                    {
                        if(conv != this && !parent.Contains(conv))
                        {
                            conv.swap = swap;
                            conv.parent.Add(this);
                            conv.activateOther=1;
                            if(swap == true)
                            {
                                //conv.mod = -1;
                            }
                            else
                            {
                                //conv.mod = 1;
                            }
                        }
                    }
                }
            }
            foreach (PhysicsObject po in Level.CheckRectAll<PhysicsObject>(topLeft, bottomRight))
            {
                if (po != null)
                {
                    if (swap == false)
                    {
                        if (po.hSpeed < speed * 2)
                        {
                            po.hSpeed += speed * mod;
                        }
                    }
                    else
                    {
                        if (po.hSpeed > -speed * 2)
                        {
                            po.hSpeed -= speed * mod;
                        }
                    }
                }
            }
        }
    }
}