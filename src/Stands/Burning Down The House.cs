using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class BDTH : Stand, IDrawToDifferentLayers
    {
        private float StandCooldown = 0f;
        private bool overheating = false;
        private float heating;

        private float currentCD;

        public BDTH(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 19;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 19;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            base.graphic = _sprite;
            _editorName = "BDTH";
            standName = "BDTH";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(255, 63, 0);

            usingCharging = true;
        }
        public override void OnStandMove()
        {
            if (StandCooldown < 0f)
            {
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y, 2f));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y + 16f, 2f));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y + 32f, 2f)); //br
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y - 16f, 2f));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y - 32f, 2f)); //tr
                Level.Add(new ShortTimeBlock(position.x + 16f, position.y - 32f, 2f));
                Level.Add(new ShortTimeBlock(position.x, position.y - 32f, 2f));
                Level.Add(new ShortTimeBlock(position.x - 16f, position.y - 32f, 2f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y - 32f, 2f)); //tl
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y - 16f, 2f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y, 2f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y + 16f, 2f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y + 32f, 2f)); //bl
                Level.Add(new ShortTimeBlock(position.x - 16f, position.y + 32f, 2f));
                Level.Add(new ShortTimeBlock(position.x, position.y + 32f, 2f));
                Level.Add(new ShortTimeBlock(position.x + 16f, position.y + 32f, 2f));
                Level.Add(new ShortTimeBackground(position.x, position.y, 2f));
                StandCooldown = 4f;
                currentCD = 2;
            }
            base.OnStandMove();
        }
        public override void OnSpecialMoveFast()
        {
            if (StandCooldown < 0f)
            {
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y + 16f));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y + 32f)); //br
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y - 16f));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y - 32f)); //tr
                Level.Add(new ShortTimeBlock(position.x + 16f, position.y - 32f));
                Level.Add(new ShortTimeBlock(position.x, position.y - 32f));
                Level.Add(new ShortTimeBlock(position.x - 16f, position.y - 32f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y - 32f)); //tl
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y - 16f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y + 16f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y + 32f)); //bl
                Level.Add(new ShortTimeBlock(position.x - 16f, position.y + 32f));
                Level.Add(new ShortTimeBlock(position.x, position.y + 32f));
                Level.Add(new ShortTimeBlock(position.x + 16f, position.y + 32f));
                Level.Add(new ShortTimeBackground(position.x, position.y));
                StandCooldown = 7f;
                currentCD = 5;
            }
            base.OnSpecialMoveFast();
        }
        public override void OnSpecialMove()
        {
            //I don't know why I did not this thgrough some function, that looks terrible
            if (StandCooldown < 0f)
            {
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y, 8f));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y + 16f, 8f));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y + 32f, 8f)); //br
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y - 16f, 8f));
                Level.Add(new ShortTimeBlock(position.x + 32f, position.y - 32f, 8f)); //tr
                Level.Add(new ShortTimeBlock(position.x + 16f, position.y - 32f, 8f));
                Level.Add(new ShortTimeBlock(position.x, position.y - 32f, 8f));
                Level.Add(new ShortTimeBlock(position.x - 16f, position.y - 32f, 8f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y - 32f, 8f)); //tl
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y - 16f, 8f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y, 8f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y + 16f, 8f));
                Level.Add(new ShortTimeBlock(position.x - 32f, position.y + 32f, 8f)); //bl
                Level.Add(new ShortTimeBlock(position.x - 16f, position.y + 32f, 8f));
                Level.Add(new ShortTimeBlock(position.x, position.y + 32f, 8f));
                Level.Add(new ShortTimeBlock(position.x + 16f, position.y + 32f, 8f));
                Level.Add(new ShortTimeBackground(position.x, position.y, 8f));
                StandCooldown = 10f;
                currentCD = 8;
            }
            base.OnSpecialMove();
        }
        public override void OnSpecialMoveLong()
        {
            OnSpecialMove();
        }
        public override void Update()
        {
            base.Update();
            if(StandCooldown >= 0f)
            {
                StandCooldown -= 0.01666666f;
            }            
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Foreground)
            {
                if(_equippedDuck != null && _equippedDuck.profile.localPlayer)
                {
                    if (StandCooldown > 2)
                    {
                        float cd = (StandCooldown - 2) / currentCD;

                        Vec2 pos = Level.current.camera.position;
                        Vec2 size = Level.current.camera.size;

                        SpriteMap _b = new SpriteMap(GetPath("Sprites/Stands/ball.png"), 17, 17);
                        _b.CenterOrigin();
                        _b.scale = size / new Vec2(320, 180) * 0.5f;

                        Vec2 cp = pos + new Vec2(size.x * cd, size.y - _b.height * _b.scale.y * 0.5f);

                        _b.angle = cd * 3.14f * 18.82f; //320 / 17
                        
                        if(StandCooldown < 3)
                        {
                            _b.alpha = (StandCooldown - 2);
                        }

                        Graphics.Draw(_b, cp.x, cp.y);

                    }
                }
            }
        }
    }
}
