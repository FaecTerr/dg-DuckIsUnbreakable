using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class StarPlatinum : Stand, IDrawToDifferentLayers
    {
        public MaterialSelection _selectionMaterial;
        private float counter;
        private float SFXplay;
        private float punchDelay;
        private float StandCooldown = 0f;
        public bool punching = false;
        public bool timeIsStopped = false;
        public float timeStandCooldown;
        public bool activate = false;
        public StateBinding _stopBinds = new StateBinding("timeIsStopped", -1, false, false);
        public StateBinding _timeBinds = new StateBinding("timeStandCooldown", -1, false, false);
        public StateBinding _actBind = new StateBinding("activate", -1, false, false);
        public StateBinding _punchSP = new StateBinding("punching", -1, false, false);
        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
        {
            GetPath<DuckUnbreakable>("SFX/BigOra.wav"),
        });

        public NetSoundEffect _netLoad2 = new NetSoundEffect(new string[]
        {
            GetPath<DuckUnbreakable>("SFX/Ora.wav"),
        });

        SpriteMap vfx = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Stands/TimeStopOverlay.png"), 32, 18);
        SpriteMap cooldownSprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Stands/TimeStopPlay.png"), 16, 16);
        public StarPlatinum(float xval, float yval) : base(xval, yval)
        {
            cooldownSprite.CenterOrigin();
            cooldownSprite.frame = 2;

            standName = "StarPlatinum";
            _pickupSprite.frame = 14;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _sprite.frame = 14;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 2f;
            standName = "SP";
            standColor = new Vec3(255, 242, 0);
            _editorName = "Star Platinum";

            usingCharging = true;
        }
        public override void OnSpecialMoveLong()
        {
            OnSpecialMove();
            base.OnSpecialMoveLong();
        }
        public override void OnSpecialMove()
        {
            if (owner != null)
            {
                if (StandCooldown <= 0f)
                {
                    timeStandCooldown = 4f;
                    StandCooldown = 6f;
                    timeIsStopped = true;
                    Level.Add(new TimeStopMarker());

                    SFX.Play(GetPath("SFX/SPzaWarudo.wav"), 1f, 0f, 0f, false);
                }
            }
            base.OnSpecialMove();
        }
        public override void OnSpecialMoveFast()
        {
            OnStandMove();
            base.OnSpecialMoveFast();
        }
        public override void OnStandMove()
        {
            if (duck.ragdoll == null && owner != null)
            {
                if (counter < 6f && punchDelay <= 0f)
                {
                    SFXplay = Rando.Float(1f);
                    if (SFXplay < 0.25f)
                    {
                        SFX.Play(GetPath("SFX/Ora.wav"), 0.8f, 0f, 0f, false);
                    }
                    else if (SFXplay < 0.5f)
                    {
                        SFX.Play(GetPath("SFX/Ora2.wav"), 1f, 0f, 0f, false);
                    }
                    else if (SFXplay < 0.75f)
                    {
                        SFX.Play(GetPath("SFX/Ora4.wav"), 1f, 0f, 0f, false);
                    }
                    else
                    {
                        SFX.Play(GetPath("SFX/Ora5.wav"), 1f, 0f, 0f, false);
                    }
                    _punchForce = 3f;
                    counter += 1f;
                }
                else if (counter >= 6f && punchDelay <= 0f)
                {
                    SFXplay = Rando.Float(1f);
                    if (SFXplay < 0.5f)
                    {
                        SFX.Play(GetPath("SFX/BigOra.wav"), 0.8f, 0f, 0f, false);
                    }
                    else
                    {
                        SFX.Play(GetPath("SFX/BigOra2.wav"), 1f, 0f, 0f, false);
                    }
                    _punchForce = 6f;
                }
                if (counter < 6f && duck != null && punchDelay <= 0f)
                {

                    if (!(isLocal))
                    {
                        punchDelay += 6f;
                        punching = true;
                    }
                    else
                    {
                        Level.Add(new ForceWave(x + offDir * 12f, y - 10f, offDir, 0.15f, 4f, owner.vSpeed * 0f, duck));
                        punchDelay += 6f;
                    }
                }
                else if (punchDelay <= 0f && counter >= 6)
                {
                    punchDelay = 30f;
                    counter = 0;
                }
            }
            base.OnStandMove();
        }
     
        public override void Update()
        {
            if (counter == 0f && punchDelay == 5f)
            {
                SFX.Play(GetPath("SFX/YareYare.wav"), 1f, 0f, 0f, false);
            }
            if (punching == true && owner != null)
            {
                Level.Add(new ForceWave(x + offDir * 12f, y - 10f, offDir, 0.15f, 4f, owner.vSpeed*0f, duck));
                punching = false;
            }
            /*if (owner != null)
            {
                if (owner.enablePhysics == false)
                    owner.enablePhysics = true;
            }*/
            if (punchDelay > 0f)
            {
                punchDelay -= 1f;
                _punchForce = 0;
            }
            if (timeIsStopped == false)
                StandCooldown -= 0.01666666f;
            if (timeIsStopped == true || timeStandCooldown > 0f)
                timeStandCooldown -= 0.03222222f;
            if (timeIsStopped == true)
            {
                if (timeStandCooldown > 0f && timeStandCooldown < 0.04f)
                {
                    activate = true;
                }
                foreach (Bullet current in Level.current.things[typeof(Bullet)])
                {
                    current.active = false;
                }
                foreach (PhysicsObject current in Level.current.things[typeof(PhysicsObject)])
                {
                    if (current != null && owner != null)
                    {
                        Duck duck = current as Duck;
                        if (!(current.Equals(owner)))
                        {
                            current.enablePhysics = false;
                        }

                        if (current is Duck && !current.Equals(owner))
                        {
                            if (duck.isServerForObject)
                            {

                                Holdable holdObject = duck.holdObject;
                                if (Network.isServer && Network.isActive)
                                    Fondle(holdObject);

                                if (holdObject != null)
                                {
                                    duck.ThrowItem(false);
                                    current.enablePhysics = false;
                                }
                                Fondle(holdObject);
                            }
                        }
                    }
                }
            }
            else if (timeIsStopped == false && activate == true)
            {
                foreach (Bullet current in Level.current.things[typeof(Bullet)])
                {
                    current.active = true;
                }
                foreach (PhysicsObject current in Level.current.things[typeof(PhysicsObject)])
                {
                    if (current != null)
                    {
                        current.enablePhysics = true;
                    }
                    activate = false;
                }
                timeIsStopped = false;
            }
            if (timeStandCooldown <= 0f)
            {
                if (timeIsStopped)
                {
                    Level.Add(new TimeStopMarker() { fram = 1 });
                }
                timeIsStopped = false;
            }
            else if (timeStandCooldown > 0f)
            {
                timeIsStopped = true;
            }
            base.Update();
        }

        public void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (timeIsStopped)
                {
                    vfx.scale = Level.current.camera.size / new Vec2(32, 18) + new Vec2(0.1f, 0.1f);                    
                    vfx.alpha = 0.5f;
                    Graphics.Draw(vfx, Level.current.camera.position.x, Level.current.camera.position.y, 1);
                }
                else
                {
                    if (StandCooldown > 0)
                    {
                        Vec2 textScale = Level.current.camera.size / new Vec2(320, 180);
                        Vec2 tR = Level.current.camera.position; 
                        cooldownSprite.scale = textScale;
                        cooldownSprite.alpha = 0.5f;
                        tR += new Vec2(Level.current.camera.size.x * 1f, 0);
                        string cooldownTime = "";
                        cooldownTime += "00:0";
                        cooldownTime += (int)StandCooldown;
                        cooldownTime += ":";
                        string num = Math.Round(StandCooldown % 1, 2).ToString();
                        string x = "";
                        bool afterDot = false;
                        for (int i = 0; i < num.Length; i++)
                        {
                            if (afterDot)
                            {
                                x += num[i];
                            }
                            if (num[i] == '.')
                            {
                                afterDot = true;
                            }
                        }
                        while(x.Length < 2)
                        {
                            x += "0";
                        }
                        cooldownTime += x;
                        tR += textScale * new Vec2(-(cooldownTime.Length + 1) * 8, 6);
                        Graphics.DrawStringOutline(cooldownTime, tR, Color.White * 0.5f, Color.Black * 0.5f, 1, null, textScale.x);
                        Graphics.Draw(cooldownSprite, tR.x - 16, tR.y + 8);
                    }
                }
            }
        }
    }
}
