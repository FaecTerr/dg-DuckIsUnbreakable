using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    [EditorGroup("Faecterr's|Special")]
    public class FreezingPipe : Thing
    {
        private SpriteMap _sprite;
        public EditorProperty<float> delay;
        public EditorProperty<float> duration;
        public EditorProperty<float> range;
        public EditorProperty<bool> fromstart;
        public EditorProperty<int> orientation;
        private float time = 0f;
        private float freezing = 0f;
        private bool isFreezing;
        private int counter = 0;
        public int mFrame;
        public FreezingPipe(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Blocks/FreezePipe.png"), 16, 16, false);
            _sprite.frame = 0;
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            base.depth = -0.7f;
            _canFlip = true;
            base.hugWalls = (WallHug.Floor | WallHug.Ceiling | WallHug.Left | WallHug.Right);
            delay = new EditorProperty<float>(10f, null, 0.5f, 50f, 0.1f);
            duration = new EditorProperty<float>(10f, null, 0.5f, 50f, 0.1f);
            range = new EditorProperty<float>(2f, null, 0.2f, 10f, 0.1f);
            fromstart = new EditorProperty<bool>(true);
            orientation = new EditorProperty<int>(0, null, 0f, 3f, 1f);
        }

        public override void EditorPropertyChanged(object property)
        {
            base.EditorPropertyChanged(property);
            angleDegrees = 90f * orientation;
        }

        public override void Initialize()
        {
            base.Initialize();
            if (Level.current is Editor)
            {
                return;
            }
            isFreezing = !fromstart;
        }

        public override void Update()
        {
            base.Update();
            if (mFrame > 0)
            {
                mFrame--;
            }
            angleDegrees = 90f * orientation;
            if (isFreezing == false && time > 0f)
            {
                time -= 0.01667f;
            }
            else if (isFreezing == false)
            {
                isFreezing = true;
                SFX.Play(GetPath("SFX/SteamFadeIn.wav"), 1f, 0f, 0.0f, false);            
                freezing = duration;
            }
            if (isFreezing == true)
            {
                if (mFrame < 1)
                {
                    SFX.Play(GetPath("SFX/SteamMiddle.wav"), 1f, 0f, 0.0f, false);
                    mFrame = 60;
                }
                if (freezing > 0f)
                {
                    freezing -= 0.01667f;
                    Vec2 dir = new Vec2(0f, 0f);
                    if (orientation == 0)
                    {
                        dir = new Vec2(0f, -1f);
                    }
                    else if (orientation == 1)
                    {
                        dir = new Vec2(1f, 0f);
                    }
                    else if (orientation == 2)
                    {
                        dir = new Vec2(0f, 1f);
                    }
                    else if (orientation == 3)
                    {
                        dir = new Vec2(-1f, 0f);
                    }
                    if (counter < 3)
                    {
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                        Level.Add(new FreezedAir(position.x, position.y, dir, range));
                    }
                }
                else
                {
                    if(isFreezing == true)
                        SFX.Play(GetPath("SFX/SteamFadeOut.wav"), 1f, 0f, 0.0f, false);
                    isFreezing = false;
                    time = delay;
                }
            }
        }
        public override void Draw()
        {
            _sprite.angleDegrees = 90f * orientation;
            base.Draw();
        }
    }
}
