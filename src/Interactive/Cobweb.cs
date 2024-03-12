using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff|Blocks")]
    public class Cobweb : MaterialThing
    {
        private float _burnt;
        SpriteMap _sprite;
        public EditorProperty<float> vMod;
        public EditorProperty<float> hMod;
        public Cobweb(float xpos, float ypos) : base(xpos, ypos)
        {
            _maxHealth = 10f;
            _hitPoints = 10f;
            _sprite = new SpriteMap(GetPath("Sprites/Things/Cobweb.png"), 16, 16, false);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            graphic = _sprite;
            hMod = new EditorProperty<float>(0.9f, this, 0.5f, 1f, 0.04f);
            vMod = new EditorProperty<float>(0.9f, this, 0.5f, 1f, 0.04f);
            
        }
        public override void Update()
        {
            if (_hitPoints <= 0f && !base._destroyed)
            {
                Destroy(new DTImpact(this));
            }
            if (_onFire && _burnt < 0.9f)
            {
                float c = 1f - burnt;
                if (_hitPoints > c * _maxHealth)
                {
                    _hitPoints = c * _maxHealth;
                }
                _sprite.color = new Color(c, c, c);
            }
            foreach (PhysicsObject d in Level.CheckRectAll<PhysicsObject>(topLeft, bottomRight))
            {
                d.velocity = new Vec2(d.velocity.x * hMod, d.velocity.y < 0 ? d.velocity.y * vMod : d.velocity.y);
            }
            base.Update();
        }
    }
}
