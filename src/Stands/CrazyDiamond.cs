using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class CrazyDiamond : Stand
    {
        private float counter;
        private float SFXplay;
        private float punchDelay;
        public bool punching = false;
        public bool fixingItem = false;
        public new Holdable h;
        public Duck duckOwner;
        public bool netFix = false;
        public StateBinding _punchCrazyD = new StateBinding("punching", -1, false, false);
        public StateBinding _deleteFix = new StateBinding("fixingItem", -1, false, false);
        public StateBinding _FixPunch = new StateBinding("netFix", -1, false, false);
        public StateBinding _itemBind = new StateBinding("h", -1, false, false);
        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
{
            GetPath<DuckUnbreakable>("SFX/Dora.wav"),
});

        public NetSoundEffect _netLoad2 = new NetSoundEffect(new string[]
        {
            GetPath<DuckUnbreakable>("SFX/Dorara.wav"),
        });
        public CrazyDiamond(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 2;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _sprite.frame = 2;
            thickness = 0.1f;
            _editorName = "Crazy Diamond";
            _equippedDepth = 4;
            _punchForce = 2f;
            standName = "CD";
            standColor = new Vec3(0, 229, 229);

            usingCharging = true;
        }
        public override void OnSpecialMove()
        {
            OnSpecialMoveFast();
            base.OnSpecialMove();
        }
        public override void OnSpecialMoveLong()
        {
            OnSpecialMoveFast();
            base.OnSpecialMoveLong();
        }
        public override void OnSpecialMoveFast()
        {
            if (duckOwner != null)
            {
                if (duckOwner.holdObject != null)
                {
                    h = Activator.CreateInstance(duckOwner.holdObject.GetType(), Editor.GetConstructorParameters(duckOwner.holdObject.GetType())) as Holdable;
                    if (h is RagdollPart || h is TrappedDuck || h is Equipment)
                        return;
                    else
                    {
                        if (h != null)
                        {
                            if(Network.isServer && Network.isActive)
                            {
                                DuckNetwork.SendToEveryone(new NMcrazyfix(h,  this));
                            }
                            h.offDir = duckOwner.holdObject.offDir;
                            h.angle = duckOwner.holdObject.angle;
                            h.position = duckOwner.holdObject.position;
                            Level.Remove(duckOwner.holdObject);
                            duckOwner.holdObject = null;
                            if(isServerForObject)
                                Level.Add(h);
                            duckOwner.GiveHoldable(h);
                            duckOwner.holdObject = h;
                            SFX.Play(GetPath("SFX/CrazieDiamond.wav"), 1f, 0f, 0f, false);
                        }
                        base.OnSpecialMoveFast();
                    }
                }
                else if (duckOwner.holdObject == null)
                {
                    Level.Add(new StandForceWave(x + offDir * 24f, y, offDir, 0.15f, 4f, owner.vSpeed * 0f, duck, true));
                    SFX.Play(GetPath("SFX/CrazieDiamond.wav"), 1f, 0f, 0f, false);
                    netFix = true;
                }
            }
        }
        
        public void DoFix(Holdable h)
        {
            if (h != null)
            {
                h.offDir = duckOwner.holdObject.offDir;
                h.angle = duckOwner.holdObject.angle;
                h.position = duckOwner.holdObject.position;
                Level.Remove(duckOwner.holdObject);
                duckOwner.holdObject = null;
                if(isServerForObject)
                    Level.Add(h);
                duckOwner.GiveHoldable(h);
                duckOwner.holdObject = h;
                SFX.Play(GetPath("SFX/CrazieDiamond.wav"), 1f, 0f, 0f, false);
            }
        }

        public override void OnStandMove()
        {
            if (duck.ragdoll == null && owner != null)
            {
                if (counter >= 10f && punchDelay <= 0f)
                {
                    SFX.Play(GetPath("SFX/Dora.wav"), 0.8f, 0f, 0f, false);
                    _punchForce = 4f;
                }
                else if (counter > 0f && punchDelay <= 0f)
                {
                    SFXplay = Rando.Float(1f);
                    SFX.Play(GetPath("SFX/Dorara.wav"), 0.8f, 0f, 0f, false);
                    _punchForce = 2f;
                    counter += 1f;
                }
                else if (counter <= 0f && punchDelay <= 0f)
                {
                    SFX.Play(GetPath("SFX/Dora.wav"), 0.8f, 0f, 0f, false);
                }
                if (counter == 10f)
                {
                    counter = 0f;
                }
                if (counter < 10f && duck != null && punchDelay <= 0f)
                {
                    Level.Add(new StandForceWave(x + offDir * 24f, y, offDir, 0.15f, 4f, owner.vSpeed * 0f, duck, false));
                    punchDelay = 15f;
                }
                else if (punchDelay <= 0f)
                    punchDelay = 10f;
            }
            base.OnStandMove();
        }
        public override void Update()
        {
            if (owner != null)
            {
                duckOwner = owner as Duck;
                if (punching == true && netFix == false)
                {
                    punching = false;
                    Level.Add(new StandForceWave(x + offDir * 24f, y, offDir, 0.15f, 4f, owner.vSpeed * 0f, duck, false));
                }
                if (netFix == true)
                {
                    netFix = false;
                    SFX.Play(GetPath("SFX/CrazieDiamond.wav"), 1f, 0f, 0f, false);
                    Level.Add(new StandForceWave(x + offDir * 24f, y, offDir, 0.15f, 4f, owner.vSpeed * 0f, duck, true));
                }
                if (punchDelay > 0f)
                    punchDelay -= 1f;
            }
            base.Update();
        }
    }
    public class CrazyDi : CrazyDiamond
    {
        public CrazyDi(float xval, float yval) : base(xval, yval)
        {
        }
    }
}
