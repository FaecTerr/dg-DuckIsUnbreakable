using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class PurpleHaze : Stand, IDrawToDifferentLayers
    {
        public StateBinding _capsule = new StateBinding("capsule", -1, false, false);
        public StateBinding _timer = new StateBinding("timer", -1, false, false);
        public int capsule = 5;
        public int maxCapsules = 6;
        public float timer = 0f;
        public float capsulerecharge = 6f;
        public PurpleHaze(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 12;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _sprite.frame = 12;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _editorName = "Purple Haze";
            standName = "PH";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(0, 34, 229);
        }
        public override void Update()
        {
            base.Update();
            if (capsule < maxCapsules && timer <= 0f)
            {
                timer = capsulerecharge;
                capsule += 1;
            }
            if (timer > 0f)
            {
                timer -= 0.01666666f;
            }
        }
        public override void OnSpecialMove()
        {
            OnStandMove();
            base.OnSpecialMove();
        }
        public override void OnSpecialMoveFast()
        {
            OnStandMove();
            base.OnSpecialMoveFast();
        }
        public override void OnSpecialMoveLong()
        {
            OnStandMove();
            base.OnSpecialMoveLong();
        }
        public override void OnStandMove()
        {
            base.OnStandMove();
            if (owner != null)
            {
                Duck d = owner as Duck;
                Vec2 dir = new Vec2(0f, 0f);
                if (d.inputProfile.Down("UP"))
                {
                    dir += new Vec2(0f, -1f);
                }
                if (d.inputProfile.Down("DOWN"))
                {
                    dir += new Vec2(0f, 1f);
                }
                if (d.inputProfile.Down("RIGHT"))
                {
                    dir += new Vec2(1f, 0f);
                }
                if (d.inputProfile.Down("LEFT"))
                {
                    dir += new Vec2(-1f, 0f);
                }
                AutoBlock ab = Level.CheckLine<AutoBlock>(position, position + 24f * dir);
                AutoPlatform ap = Level.CheckLine<AutoPlatform>(position, position + 24f * dir);
                Platform p = Level.CheckLine<Platform>(position, position + 24f * dir);
                Block b = Level.CheckLine<Block>(position, position + 24f * dir);
                Duck duck = Level.CheckLine<Duck>(position, position + 24f * dir, owner);
                if ((ab != null || duck != null || ap != null || p != null || b != null) && capsule > 0)
                {
                    if (Network.isServer && Network.isActive)
                    {
                        Send.Message(new NMhazeSmoke(position, d, dir));

                        Level.Add(new HazeSmoke(position.x, position.y, d, dir));
                    }
                    if (!Network.isActive) {
                        Level.Add(new HazeSmoke(position.x, position.y, d, dir));
                    }
                    capsule -= 1;
                }
            }
        }

        public void OnDrawLayer(Layer layer)
        {
            if(layer == Layer.Foreground)
            {
                if (StandOwner != null)
                {
                    if (StandOwner.profile.localPlayer)
                    {
                        int part1 = (int)(maxCapsules * 0.5f);
                        int part2 = maxCapsules - part1;

                        Vec2 pos = Level.current.camera.position;
                        Vec2 size = Level.current.camera.size;

                        SpriteMap _capsule = new SpriteMap(GetPath("Sprites/Stands/spheres.png"), 17, 17);
                        _capsule.CenterOrigin();
                        _capsule.scale = size / new Vec2(320, 180) * 1.25f;

                        for (int i = 0; i < part1; i++)
                        {
                            Vec2 cp = pos + new Vec2(_capsule.width * _capsule.scale.x * 0.5f + _capsule.width * _capsule.scale.x * i, size.y - _capsule.height * _capsule.scale.y * 0.5f);
                            _capsule.frame = (i >= capsule) ? 1 : 0;
                            Graphics.Draw(_capsule, cp.x, cp.y);
                        }
                        for (int i = 0; i < part2; i++)
                        {
                            Vec2 cp = pos + new Vec2(size.x - (_capsule.width * _capsule.scale.x * 0.5f + _capsule.width * _capsule.scale.x * i), size.y - _capsule.height * _capsule.scale.y * 0.5f);
                            _capsule.frame = (i + part1 >= capsule) ? 1 : 0;
                            Graphics.Draw(_capsule, cp.x, cp.y);
                        }
                    }

                    if (StandOwner.inputProfile.Down("QUACK") && capsule > 0)
                    {
                        Duck d = StandOwner;
                        Vec2 dir = new Vec2(0f, 0f);
                        float diri = 1.5f;
                        if (d.inputProfile.Down("UP"))
                        {
                            dir += new Vec2(0f, -1f);
                            diri -= 0.5f;
                        }
                        if (d.inputProfile.Down("DOWN"))
                        {
                            dir += new Vec2(0f, 1f);
                            diri -= 0.5f;
                        }
                        if (d.inputProfile.Down("RIGHT"))
                        {
                            dir += new Vec2(1f, 0f);
                            diri -= 0.5f;
                        }
                        if (d.inputProfile.Down("LEFT"))
                        {
                            dir += new Vec2(-1f, 0f);
                            diri -= 0.5f;
                        }

                        if (dir != new Vec2(0, 0))
                        {
                            SpriteMap _arrow = new SpriteMap(GetPath("Sprites/Stands/HazeDirection.png"), 12, 24);
                            _arrow.center = new Vec2(6, 12);
                            _arrow.angleDegrees = Maths.PointDirection(new Vec2(0, 0), new Vec2(dir.x * (-1), dir.y)) - 90;
                            _arrow.alpha = 0.1f;
                            AutoBlock ab = Level.CheckLine<AutoBlock>(position, position + 24f * dir);
                            AutoPlatform ap = Level.CheckLine<AutoPlatform>(position, position + 24f * dir);
                            Platform p = Level.CheckLine<Platform>(position, position + 24f * dir);
                            Block b = Level.CheckLine<Block>(position, position + 24f * dir);
                            Duck duck = Level.CheckLine<Duck>(position, position + 24f * dir, owner);
                            if ((ab != null || duck != null || ap != null || p != null || b != null) && capsule > 0)
                            {
                                _arrow.alpha = 0.6f;
                            }
                            Graphics.Draw(_arrow, d.position.x + dir.x * diri * 24, d.position.y + dir.y * diri * 24, 1f);
                        }
                    }
                }
            }
        }
    }
}
