using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    //[EditorGroup("Faecterr's|Stuff")]
    public class Crossbow : Holdable
    {
        protected Vec2 _wallPoint = default(Vec2);
        protected Sprite _sightHit;
        private Tex2D _laserTex;
        public SpriteMap _sprite;
        private int ammo = 2;
        private bool loadState = false;
        private Vec2 _lastHit = Vec2.Zero;
        protected Vec2 _grappleTravel = default(Vec2);
        private float a;
        public Crossbow()
        {
            center = new Vec2(7f, 7f);
            _sprite = new SpriteMap(GetPath("Sprites/Things/Weapons/Crossbow.png"), 34, 10);
            graphic = _sprite;
            collisionOffset = new Vec2(-12f, -5f);
            collisionSize = new Vec2(34f, 10f);
            weight = 0.9f;
            _laserTex = Content.Load<Tex2D>("pointerLaser");
        }
        public override void DrawGlow()
        {
            if (owner != null)
            {
                Duck d = owner as Duck;
                Graphics.DrawTexturedLine(_laserTex, position+ new Vec2(8f*offDir, -2f), _lastHit, Color.Red, 0.5f, base.depth - 1);
                if (_sightHit != null && d.inputProfile.Down("SHOOT"))
                {
                    _sightHit.color = Color.Magenta;
                    Graphics.Draw(_sightHit, _wallPoint.x, _wallPoint.y);
                }
            }
            base.DrawGlow();
        }
        public override void Update()
        {
            base.Update();

            if (owner != null)
            {
                Vec2 pos = Offset(collisionOffset);
                ATTracer tracer = new ATTracer();
                float dist = tracer.range = 320f;
                tracer.penetration = 0.1f;
                Bullet b = new Bullet(pos.x + 16f * offDir, pos.y - 2f, tracer, a, owner, false, -1f, true, true);
                _wallPoint = b.end;
                _grappleTravel = b.travelDirNormalized;
                dist = (pos - _wallPoint).length;
                if (dist < 320f)
                {
                    _lastHit = _wallPoint;
                }
                else
                {
                    _lastHit = _wallPoint;
                }
                Duck d = owner as Duck;
                if(d.inputProfile.Down("SHOOT") == false)
                {
                    a = 0;
                    if(offDir < 0)
                    {
                        a = 180;
                    }
                }
                if(d.inputProfile.Down("SHOOT") && ammo > 0)
                {
                    d.immobilized = true;
                    if (d.inputProfile.Down("DOWN"))
                    {
                        a -= 1f * offDir;
                    }
                    if (d.inputProfile.Down("UP"))
                    {
                        a += 1f * offDir;
                    }
                }
                if (d.inputProfile.Released("SHOOT") && ammo > 0)
                {
                    ammo--;
                    //Level.Add(new Bullet(pos.x, pos.y, new ATSniper(), a, null, false, 320, true, true));
                    d.immobilized = false;
                    if(_lastHit != null)
                    {
                        Level.Add(new FreezedAir(_lastHit.x, _lastHit.y, new Vec2(0f, 0.2f), 1f, 15f, 10f));
                        
                    }
                    a = 0;
                    if (offDir < 0)
                    {
                        a = 180;
                    }
                }
            }
            else
            {
                a = 0;
                if (offDir < 0)
                {
                    a = 180;
                }
            }
        }
    }
}