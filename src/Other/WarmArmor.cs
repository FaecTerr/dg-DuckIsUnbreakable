using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Equipment")]
    public class WarmArmor : Equipment
    {
        protected SpriteMap _sprite;
        protected Sprite _pickupSprite;
        private float SFXplay;
        protected Vec2 _barrelOffsetTL = default(Vec2);
        public float punch;

        public WarmArmor(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-6f, -4f);
            collisionSize = new Vec2(11f, 8f);
            _equippedCollisionOffset = new Vec2(-7f, -5f);
            _equippedCollisionSize = new Vec2(12f, 11f);
            _sprite = new SpriteMap(GetPath("Sprites/Things/Equipment/warmArmor.png"), 32, 32, false);
            _autoOffset = false;
            thickness = 0.1f;
            _equippedDepth = 3;
            _wearOffset = new Vec2(1f, 1f);
            _pickupSprite = new SpriteMap(GetPath("Sprites/Things/Equipment/warmArmor.png"), 32, 32);
            _pickupSprite.CenterOrigin();
            base.graphic = _pickupSprite;
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            _editorName="Warm armor";
            _isArmor = false;
        }
        protected override bool OnDestroy(DestroyType type = null)
        {
            return false;
        }

        public Sprite pickupSprite
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
            if(d!=null)
            {
                if(d.HasEquipment(typeof(ChestPlate)))
                {
                    UnEquip();
                }
            }
            if (_equippedDuck != null && !destroyed)
            {
                solid = false;
                visible = true;
            }
            else
            {
                solid = true;
                visible = true;
            }
            if (destroyed)
            {
                base.alpha -= 0.05f;
            }
            if (base.alpha <= 0f)
            {
                Level.Remove(this);
            }
            base.Update();
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
    }
}
