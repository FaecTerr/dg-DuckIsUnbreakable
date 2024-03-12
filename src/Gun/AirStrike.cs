using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Guns")]
    public class AirStrike : Holdable, IDrawToDifferentLayers
    {
        public SpriteMap _sprite;
        public SpriteMap _target;
        public Vec2 pos;
        public bool charging;
        public List<Bullet> firedBullets = new List<Bullet>();

        public AirStrike(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Weapons/AirStrikeController.png"), 10, 14, false);
            _target = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Weapons/AirStrikeAim.png"), 17, 17, false);
            _target.CenterOrigin();
            graphic = _sprite;
            center = new Vec2(5f, 7f);
            collisionOffset = new Vec2(-4f, -6f);
            collisionSize = new Vec2(8f, 12f);
        }
        public virtual void Explosion()
        {
            if(isServerForObject)
            Level.Add(new MissileBomb(pos.x, Level.current.topLeft.y-160f));
            Level.Remove(this);
        }
        public override void Update()
        {
            base.Update();
            if(owner != null)
            {
                Duck d = owner as Duck;
                if (d.profile.inputProfile.Released("SHOOT"))
                {
                    d.immobilized = false;
                    Explosion();
                }
                if (d.profile.inputProfile.Down("SHOOT"))
                {
                    d.immobilized = true;
                    if(charging == false)
                    {
                        charging = true;
                        pos = position;
                    }
                    else
                    {
                        Vec2 move = new Vec2();
                        if (d.profile.inputProfile.Down("UP"))
                        {
                            move += new Vec2(0f, -1f);
                        }
                        if (d.profile.inputProfile.Down("DOWN"))
                        {
                            move += new Vec2(0f, 1f);
                        }
                        if (d.profile.inputProfile.Down("LEFT"))
                        {
                            move += new Vec2(-1f, 0f);
                        }
                        if (d.profile.inputProfile.Down("RIGHT"))
                        {
                            move += new Vec2(1f, 0f);
                        }
                        pos += move*2;
                        move = new Vec2(0f, 0f);
                    }
                }
            }
            else if (prevOwner != null)
            {
                Duck d = prevOwner as Duck;
                d.immobilized = false;
            }
        }

        public void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (charging == true)
                {
                    Graphics.Draw(_target, pos.x, pos.y);
                    _target.angleDegrees += 1;
                }
            }
        }
    }
}
