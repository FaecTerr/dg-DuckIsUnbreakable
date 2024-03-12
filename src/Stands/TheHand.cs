using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class TheHand : Stand
    {
        private float StandCooldown = 0f;
        public bool punching = false;
        public StateBinding _punchCrazyD = new StateBinding("punching", -1, false, false);
        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
{
            GetPath<DuckUnbreakable>("SFX/Erasing.wav"),
});

        public TheHand(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 16;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _sprite.frame = 16;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _editorName = "The Hand";
            standName = "TH";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(0, 34, 229);


            usingCharging = true;
        }
        public override void Update()
        {
            base.Update();
            if (StandCooldown > 0f)
            {
                StandCooldown -= 0.02f;
            }
            if (punching == true)
            {
                punching = false;
                Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, false));
            }
        }
        public override void OnSpecialMove()
        {
            base.OnSpecialMove();
            OnSpecialMoveFast();
        }
        public override void OnSpecialMoveLong()
        {
            base.OnSpecialMoveLong();
            OnSpecialMoveFast();
        }
        public override void OnSpecialMoveFast()
        {
            base.OnSpecialMoveFast();
            Duck duck = owner as Duck;
            if (StandCooldown <= 0f && duck.ragdoll == null)
            {
                StandCooldown = 0.6f;
                Duck d = Level.Nearest<Duck>(base.x, base.y, owner);
                if (d != null && (d.position - position).length < 160f && (d.position - position).length > 32f)
                {

                    owner.position = d.position;
                    owner.velocity = new Vec2(0f, 1f);
                    Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, true));
                    SFX.Play(GetPath("SFX/Erasing.wav"), 1f, 0f, 0f, false);
                }
                else
                {
                    Gun g = Level.Nearest<Gun>(base.x, base.y, owner);
                    if (g != null && (g.position - position).length < 160f && (g.position - position).length > 32f)
                    {
                        StandCooldown = 0.4f;
                        owner.position = g.position;
                        owner.velocity = new Vec2(0f, 1f);
                        Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, true));
                        SFX.Play(GetPath("SFX/Erasing.wav"), 1f, 0f, 0f, false);
                    }
                    else
                    {
                        Holdable h = Level.Nearest<Holdable>(base.x, base.y, owner);
                        if (h != null && (h.position - position).length < 160f && (h.position - position).length > 32f)
                        {
                            StandCooldown = 0.3f;
                            owner.position = h.position;
                            owner.velocity = new Vec2(0f, 1f);
                            Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, true));
                            SFX.Play(GetPath("SFX/Erasing.wav"), 1f, 0f, 0f, false);
                        }
                        else
                        {
                            StandCooldown = 0.2f;
                            if (isLocal)
                            {
                                Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, false));
                                SFX.Play(GetPath("SFX/Erasing.wav"), 1f, 0f, 0f, false);
                            }
                            else
                            {
                                punching = true;
                            }
                        }
                    }
                }
            }
        }
        public override void OnStandMove()
        {
            base.OnStandMove();
            Duck duck = owner as Duck;
            if (StandCooldown <= 0f && duck.ragdoll == null)
            {
                StandCooldown = 0.6f;
                Duck d = Level.Nearest<Duck>(base.x, base.y, owner);
                if (d != null && (d.position - position).length < 160f && (d.position - position).length > 32f)
                {
                    d.position = owner.position + new Vec2(8f, 0f) * offDir;
                    d.velocity = new Vec2(0f, 1f);
                    Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, true));
                    SFX.Play(GetPath("SFX/Erasing.wav"), 1f, 0f, 0f, false);
                }
                else
                {
                    Gun g = Level.Nearest<Gun>(base.x, base.y, owner);
                    if (g != null && (g.position - position).length < 160f && (g.position - position).length > 32f)
                    {
                        StandCooldown = 0.4f;
                        g.position = owner.position;
                        g.velocity = new Vec2(0f, 1f);
                        Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, true));
                        SFX.Play(GetPath("SFX/Erasing.wav"), 1f, 0f, 0f, false);
                    }
                    else
                    {
                        Holdable h = Level.Nearest<Holdable>(base.x, base.y, owner);
                        if (h != null && (h.position - position).length < 160f && (h.position - position).length > 32f)
                        {
                            StandCooldown = 0.3f;
                            h.position = owner.position;
                            h.velocity = new Vec2(0f, 1f);
                            Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, true));
                            SFX.Play(GetPath("SFX/Erasing.wav"), 1f, 0f, 0f, false);
                        }
                        else
                        {
                            StandCooldown = 0.2f;
                            if (isLocal)
                            {
                                Level.Add(new TheHandForce(base.x + (float)offDir * 24f, base.y, (int)offDir, 0.15f, 0f, owner.vSpeed * 0f, base.duck, false));
                                SFX.Play(GetPath("SFX/Erasing.wav"), 1f, 0f, 0f, false);
                            }
                            else
                            {
                                punching = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
