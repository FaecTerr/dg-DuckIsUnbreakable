using System;

namespace DuckGame.DuckUnbreakable
{
    public class DUDoorOffHinges : PhysicsObject
    {
        
        public float _throwSpin;
        private bool sounded;
        public StateBinding _throwSpinBinding = new StateBinding("_throwSpin", -1, false, false);

        public DUDoorOffHinges(float xpos, float ypos) : base(xpos, ypos)
        {
            _collisionSize = new Vec2(8f, 8f);
            _collisionOffset = new Vec2(-4f, -6f);
            center = new Vec2(16f, 16f);
            base.collideSounds.Add("rockHitGround");
            weight = 2f;
        }
        
        public override void Initialize()
        {
            RefreshSprite();
            base.Initialize();
        }
        
        public void MakeEffects()
        {
            if (!sounded)
            {
                Level.Add(SmallSmoke.New(base.x, base.y + 2f));
                Level.Add(SmallSmoke.New(base.x, base.y - 16f));
                SFX.Play("doorBreak", 1f, 0f, 0f, false);
                for (int i = 0; i < 8; i++)
                {
                    WoodDebris woodDebris = WoodDebris.New(base.x - 8f + Rando.Float(16f), base.y - 8f + Rando.Float(16f));
                    woodDebris.hSpeed = ((Rando.Float(1f) > 0.5f) ? 1f : -1f) * Rando.Float(3f);
                    woodDebris.vSpeed = -Rando.Float(1f);
                    Level.Add(woodDebris);
                }
                sounded = true;
            }
        }
        
        private void RefreshSprite()
        {
            graphic = new SpriteMap(GetPath("Sprites/Things/KCDoors/doorFucked.png"), 32, 32, false);
        }
        
        public override void Update()
        {
            if (Network.isActive && !sounded && visible)
            {
                MakeEffects();
            }
            base.angleDegrees = _throwSpin;
            center = new Vec2(16f, 16f);
            _throwSpin %= 360f;
            if (offDir > 0)
            {
                _throwSpin = Lerp.Float(_throwSpin, 90f, 12f);
            }
            else
            {
                _throwSpin = Lerp.Float(_throwSpin, -90f, 12f);
            }
            base.Update();
        }
    }
}
