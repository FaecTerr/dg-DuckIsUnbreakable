using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class TheGratefulDead : Stand
    {
        public List<Duck> ducks = new List<Duck>();
        public bool toggle = false;
        public SpriteMap _target;
        public TheGratefulDead(float xval, float yval) : base(xval, yval)
        {
            _target = new SpriteMap(GetPath("Sprites/Stands/CircleZones.png"), 17, 17);
            _target.alpha = 0.2f;
            _target.CenterOrigin();
            _pickupSprite.frame = 15;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _editorName = "The Grateful Dead";
            _sprite.frame = 15;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _editorName = "The Grateful Dead";
            standName = "TGD";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            chargemod = 1f;
            standColor = new Vec3(255, 0, 220);
        }
        public override void Update()
        {
            base.Update();
            if (_equippedDuck == null)
            {
                toggle = false;
                if (prevOwner != null)
                {
                    foreach (TGDold tgd in Level.current.things[typeof(TGDold)])
                    {
                        if (prevOwner is Duck)
                        {
                            if (tgd.causedBy == (prevOwner as Duck))
                            {
                                Level.Remove(tgd);
                            }
                        }
                    }
                }
                _target.scale = new Vec2(1f, 1f);
            }
        }
        public override void OnStandMove()
        {
            base.OnStandMove();
            if (_equippedDuck != null)
            {
                if (!toggle)
                {
                    _target.scale = new Vec2(1f,1f);
                    toggle = true;
                    foreach (Duck d in Level.current.things[typeof(Duck)])
                    {
                        if (d != null && d != _equippedDuck)
                        {
                            if (!ducks.Contains(d))
                            {
                                ducks.Add(d);
                            }
                        }
                    }
                    foreach (Ragdoll r in Level.current.things[typeof(Ragdoll)])
                    {
                        if (r != null && r._duck != _equippedDuck)
                        {
                            if (!ducks.Contains(r._duck))
                            {
                                ducks.Add(r._duck);
                            }
                        }
                    }
                    foreach (Duck duck in ducks)
                    {
                        if (!duck.dead && !TGDold.OldingDucks.ContainsKey(duck))
                        {
                            Level.Add(new TGDold(duck, _equippedDuck));
                        }
                    }
                }
                else if (toggle)
                {
                    toggle = false;
                    foreach (TGDold tgd in Level.current.things[typeof(TGDold)])
                    {
                        if (tgd.causedBy == _equippedDuck)
                        {
                            Level.Remove(tgd);
                        }
                    }
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
            if (toggle == true && owner != null)
            {
                Graphics.Draw(_target, owner.position.x, owner.position.y);
                _target.angleDegrees -= 1;
                _target.scale += new Vec2(0.03f, 0.03f);
            }           
        }
    }
}
