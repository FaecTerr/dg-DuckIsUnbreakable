using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Guns")]
    public class SlimeThrower : Holdable
    {
        public SpriteMap _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Weapons/SlimeThrower.png"), 24, 14);
        public SpriteMap _ammo= new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Weapons/SlimeAmmo.png"), 5, 4);
        public int ammo = 24;
        public bool charged = true;
        public bool reload;
        public int reloadFrames;
        public int loadedType = 1;

        public float hSpawn;
        public float vSpawn;

        public SlimeThrower(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = _sprite;
            depth = 0.3f;
            center = new Vec2(12, 7);
            _ammo.center = new Vec2(2.5f, 2f);
            _sprite.center = new Vec2(12, 7);
            collisionSize = new Vec2(20, 10);
            weight = 0;
            collisionOffset = new Vec2(-10, -5);
            _editorName = "Slime gun";
        }

        public override void Update()
        {
            if(loadedType == 1)
            {
                if (charged)
                {
                    _sprite.frame = 2;
                }
                else if(ammo <= 0 || !charged)
                {
                    _sprite.frame = 3;
                }
            }
            if (loadedType == -1)
            {
                if (charged)
                {
                    _sprite.frame = 0;
                }
                else if(ammo <= 0 || !charged)
                {
                    _sprite.frame = 1;
                }
            }
            if(reloadFrames > 0)
            {
                reloadFrames--;
                if (loadedType == 1)
                {
                    if (reloadFrames > 15)
                    {
                        _ammo.frame = 29 - (reloadFrames - 15) / 1;
                    }
                    else
                    {
                        _ammo.frame = (reloadFrames) / 1;
                    }
                }
                else
                {
                    if (reloadFrames > 15)
                    {
                        _ammo.frame = 15 - (reloadFrames - 15) / 1;
                    }
                    else
                    {
                        _ammo.frame = (reloadFrames) / 1 + 14;
                    }
                }
            }
            if(reloadFrames <= 0)
            {
                charged = true;
                reload = false;
            }
            if (owner != null)
            {
                Duck d = owner as Duck;
                if (d.inputProfile.Down("SHOOT") || d.inputProfile.Pressed("FIRE"))
                {
                    if(ammo > 0 && charged && !reload)
                    {
                        hSpawn = 4f * offDir;
                        vSpawn = -1f + d.hSpeed * 0.2f;
                        Fire();
                    }
                }
                if (d.inputProfile.Pressed("UP"))
                {
                    if(ammo > 0 && !reload && charged)
                    {
                        loadedType *= -1;
                        Reload();
                    }
                }
            }
            else
            {
                if(ammo <= 0)
                {
                    Level.Add(SmallSmoke.New(base.x, base.y));
                    Level.Add(SmallSmoke.New(base.x + 4f, base.y));
                    Level.Add(SmallSmoke.New(base.x - 4f, base.y));
                    Level.Add(SmallSmoke.New(base.x, base.y + 4f));
                    Level.Add(SmallSmoke.New(base.x, base.y - 4f));
                    Level.Remove(this);
                }
            }
            base.Update();
        }

        public override void Draw()
        {
            Graphics.Draw(_ammo, position.x + 3.5f*offDir, position.y - 3f, 1f);
            base.Draw();
        }
        public void Reload()
        {
            reload = true;
            charged = false;
            reloadFrames = 30;
        }
        public void Fire()
        {
            float dir = 0;
            if (offDir > 0)
                dir = angle;
            else
                dir = (float)(Math.PI)+angle;

            Level.Add(new SlimeBall(position.x + 10f*offDir, position.y - 3f) { loadedType = loadedType, hSpeed = 10f * (float)Math.Cos(dir), vSpeed = 3f * (float)Math.Sin(dir)});
            ammo--;
            charged = false;
            reload = true;
            reloadFrames = 15;
        }
    }
}
