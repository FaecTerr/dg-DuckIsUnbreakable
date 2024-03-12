using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class Stand : Equipment
    {
        protected bool usingCharging;
        public float StandoPower = 0f;
        protected SpriteMap _sprite;
        protected SpriteMap _pickupSprite;
        private float SFXplay;
        private bool init;
        protected Vec2 _barrelOffsetTL = default(Vec2);
        protected float _punchForce = 0f;
        public float punch;
        public float chargemod = 1f;
        public string standName = "";
        private StateBinding _StandPower = new StateBinding("StandoPower", -1, false, false);
        private StandUserOutline SUO;
        private float SUOalpha;

        public Duck StandOwner;

        public Vec3 standColor = new Vec3(255, 255, 255);

        public Stand(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(16f, 16f);
            _pickupSprite = new SpriteMap(GetPath("Sprites/Stands/AllStands.png"), 10, 15);
            _sprite = new SpriteMap(GetPath("Sprites/Stands/AllStands.png"), 10, 15, false);
            base.graphic = _sprite;
            collisionOffset = new Vec2(-6f, -6f);
            collisionSize = new Vec2(12f, 12f);
            _autoOffset = false;
            thickness = 0.1f;
            _equippedDepth = 4;
        }
        public SpriteMap sprite
        {
            get
            {
                return _sprite;
            }
            set
            {
                _sprite = value;
            }
        }
        protected override bool OnDestroy(DestroyType type = null)
        {
            return false;
        }

        public SpriteMap pickupSprite
        {
            get
            {
                return _pickupSprite;
            }
            set
            {
                _pickupSprite = value;
            }
        }
        public override void Update()
        {
            Duck d = owner as Duck;

            if (!init)
            {
                init = true;
                //_sprite.frame = _pickupSprite.frame;
            }

            if (_equippedDuck != null && !destroyed)
            {
                solid = false;
                visible = true;
                alpha = 0;
            }
            else
            {
                solid = true;
                visible = true;
                alpha = 1;
            }
            if (destroyed)
            {
                base.alpha -= 0.05f;
            }
            if (base.alpha <= 0f)
            {
                //Level.Remove(this);
            }
            if (_equippedDuck == null)
            {
                StandOwner = null;
                Level.Remove(SUO);
                SUO = null;
            }
            if (_equippedDuck != null)
			{
                StandOwner = d;
                /*if (ThemePlayer.standname == "")
                {
                     ThemePlayer.standname = standName;
                }*/

                /*foreach (ThemePlayer tp in Level.current.things[typeof(ThemePlayer)])
                {
                    if (tp == null && standName != "")
                    {
                        Level.Add(new ThemePlayer(standName));
                    }
                    if (tp != null)
                    {
                        if (ThemePlayer.standname == "" && standName != "")
                        {
                            ThemePlayer.standname = standName;
                            init = false;
                        }
                    }
                }*/
                /*if (init == false && standName != "")
                {
                    if (standName == "EMP" || standName == "HP" || standName == "HG" || standName == "MR" || standName == "SC"
                        || standName == "SP" || standName == "TW")
                    {
                        SFX.Play(GetPath("SFX/JotaroTheme.wav"), 1f, 0f, 0f, false);
                    }
                    else if (standName == "CD" || standName == "EA" || standName == "KQ" || standName == "TH" || standName == "DS")
                    {
                        SFX.Play(GetPath("SFX/JosukeTheme.wav"), 1f, 0f, 0f, false);
                    }
                    else if (standName == "AER" || standName == "MTL" || standName == "PH" || standName == "TGD")
                    {
                        SFX.Play(GetPath("SFX/GiornoTheme.wav"), 1f, 0f, 0f, false);
                    }
                    else if (standName == "MT" || standName == "WS")
                    {
                        SFX.Play(GetPath("SFX/JolyneTheme.wav"), 1f, 0f, 0f, false);
                    }
                    init = true;
                }*/
                if (SUO == null && d != null)
                {
                    //Making visual effect
                    SUO = new StandUserOutline(x, y, standColor);
                    Level.Add(SUO);
                    SUO.anchor = d;
                    SUO.owner = _equippedDuck;
                    SUO.offDir = _equippedDuck.offDir;
                }
                if (SUO != null && d != null)
                {
                    SUO.position = SUO.anchorPosition;
                    SUO.offDir = _equippedDuck.offDir;
                    if (StandoPower > 0f)
                    {
                        SUOalpha = 3f;
                        if (SUOalpha > 0f && SUO.alpha < 0.45f)
                        {
                            SUO.alpha += 0.025f;
                        }
                        else if (SUOalpha > 0f)
                        {
                            SUO.alpha = 0.5f;
                        }
                    }
                    SUOalpha -= 0.0166f;
                    if (SUOalpha <= 0f && SUO.alpha > 0f)
                    {
                        SUO.alpha -= 0.02f;
                    }
                }
                if (d.inputProfile.Pressed("QUACK"))
                {
                    OnPressMove();
                }
                if (!d.inputProfile.Down("QUACK") && !d.inputProfile.Pressed("QUACK") && !d.inputProfile.Released("QUACK"))
                {
                    OnNothing();
                }
                if (d.inputProfile.Down("QUACK"))
                {
                    //Charging modificator (faster or longer)
                    StandoPower += 0.01666666f * chargemod; 
                    if (StandoPower > 0f)
                    {
                        OnHoldMove();
                        if (StandoPower > 1.5f)
                        {
                            OnShortHold();
                            if (StandoPower > 3f)
                            {
                                OnLongHold();
                                if (StandoPower > 4.5f)
                                {
                                    OnHighHold();
                                }
                            }
                        }
                    }
                    //sound notifications
                    if (StandoPower >= 1.5f - 0.033f * chargemod && StandoPower <= 1.5f)
                    {
                        
                        if (usingCharging && d.profile.localPlayer)
                        {
                            SFX.Play("switchchannel", 1f, 0f, 0f, false);
                            Level.Add(new Menacing());
                        }
                    }
                    if (StandoPower >= 3f - 0.033f * chargemod && StandoPower <= 3f)
                    {
                        
                        if (usingCharging && d.profile.localPlayer)
                        {
                            SFX.Play("switchchannel", 1f, 0f, 0f, false);
                            Level.Add(new Menacing() { fram = 1});
                        }
                    }
                    if (StandoPower >= 4.5f - 0.033f * chargemod && StandoPower <= 4.5f)
                    {
                        
                        if (usingCharging && d.profile.localPlayer)
                        {
                            SFX.Play("switchchannel", 1f, 0f, 0f, false);
                            Level.Add(new Menacing() { fram = 2});
                        }
                    }
                }
                if (d.inputProfile.Down("QUACK"))
                {
                    OnHoldMove();
                }
                if (StandoPower < 1.5) 
                {
                    if (d.inputProfile.Released("QUACK") && StandoPower < 1.5f)
                    {
                        StandoPower = 0f;
                        OnStandMove();
                        ApplyPunch();
                    }
                }
                else if (StandoPower >= 1.5f) 
                {
                    if (d.inputProfile.Released("QUACK") && StandoPower >= 4.5f) 
                    {
                        StandoPower = 0f;
                        OnSpecialMoveLong();
                        ApplyPunch();
                    }
                    else if (d.inputProfile.Released("QUACK") && StandoPower >= 3f) 
                    {
                        StandoPower = 0f;
                        OnSpecialMove();
                        ApplyPunch();
                    }
                    else if (d.inputProfile.Released("QUACK") && StandoPower >= 1.5f) 
                    {
                        StandoPower = 0f;
                        OnSpecialMoveFast();
                        ApplyPunch();
                    }
                    if (StandoPower > 5f)
                        StandoPower = 5f;
                }
                else
                    return;
            }
            base.Update();
        }

        public virtual void OnSpecialMove()
        {

        }


        public virtual void OnStandMove()
        {
            
        }

        public virtual void OnSpecialMoveFast()
        {

        }

        public virtual void OnSpecialMoveLong()
        {

        }

        public virtual void OnHoldMove()
        {

        }

        public virtual void OnShortHold()
        {

        }

        public virtual void OnLongHold()
        {

        }

        public virtual void OnHighHold()
        {

        }

        public virtual void OnPressMove()
        {

        }

        public virtual void OnNothing()
        {

        }

        public Vec2 barrelOffset
        {
            get
            {
                return _barrelOffsetTL - center + _extraOffset;
            }
        }
        public Vec2 barrelVector
        {
            get
            {
                return Offset(barrelOffset) - Offset(barrelOffset + new Vec2(-1f, 0f));
            }
        }
        public void ApplyPunch()
        {
            if (owner != null)
            {
                if (_punchForce > 0f)
                {
                    if (owner is Duck && (owner as Duck).ragdoll != null && (owner as Duck).HasEquipment(typeof(FancyShoes)) && (owner as Duck).ragdoll.part2 != null)
                    {
                        Duck duckOwner = owner as Duck;
                        Vec2 dir = -barrelVector * _punchForce;
                        duckOwner.ragdoll.part2.hSpeed += dir.x;
                        duckOwner.ragdoll.part2.vSpeed += dir.y;
                    }
                    else
                    {
                        Vec2 dir2 = -barrelVector * _punchForce;
                        if (Math.Sign(owner.hSpeed) != Math.Sign(dir2.x) || Math.Abs(dir2.x) > Math.Abs(owner.hSpeed))
                        {
                            owner.hSpeed = -dir2.x;
                        }
                        Duck duckOwner2 = owner as Duck;
                        if (duckOwner2 != null)
                        {
                            owner.vSpeed += dir2.y;
                        }
                        else
                        {
                            owner.vSpeed += dir2.y;
                        }
                    }
                }
                punch = 1f;
            }
        }
    }
}
