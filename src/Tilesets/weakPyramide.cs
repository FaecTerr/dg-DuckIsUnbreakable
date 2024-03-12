using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class weakPyramide : AutoBlock
    {
        public float damageMultiplier = 1f;
        public StateBinding _hitPointsBinding = new StateBinding("_hitPoints", -1, false, false);
        public StateBinding _healthPointsBinding = new StateBinding("_maxHealth", -1, false, false);
        public StateBinding _damageMultiplierBinding = new StateBinding("damageMultiplier", -1, false, false);

        public weakPyramide(float x, float y) : base(x, y, Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/weakPyramide.png"))
        {
            _editorName = "WeakTile";
            physicsMaterial = PhysicsMaterial.Metal;
            verticalWidthThick = 14f;
            verticalWidth = 15f;
            horizontalHeight = 12f;
            _maxHealth = 4f;
            _hitPoints = 4f;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            _hitPoints -= 1 + bullet.ammo.penetration;
            if (_hitPoints <= 0f)
            {
                Destroy(new DTShot(bullet));
            }
            return base.Hit(bullet, hitPos);
        }
        protected override bool OnDestroy(DestroyType type = null)
        {
            _hitPoints = 0f;
            Level.Remove(this);
            Vec2 flyDir = Vec2.Zero;
            if (type is DTShot)
            {
                flyDir = (type as DTShot).bullet.travelDirNormalized;
            }
            for (int i = 0; i < 6; i++)
            {
                Thing ins = WoodDebris.New(base.x - 8f + Rando.Float(16f), base.y - 8f + Rando.Float(16f));
                ins.hSpeed = ((Rando.Float(1f) > 0.5f) ? 1f : -1f) * Rando.Float(3f) + (float)Math.Sign(flyDir.x) * 0.5f;
                ins.vSpeed = -Rando.Float(1f);
                Level.Add(ins);
            }
            for (int j = 0; j < 5; j++)
            {
                SmallSmoke s = SmallSmoke.New(base.x + Rando.Float(-6f, 6f), base.y + Rando.Float(-6f, 6f));
                s.hSpeed += Rando.Float(-0.3f, 0.3f);
                s.vSpeed -= Rando.Float(0.1f, 0.2f);
                Level.Add(s);
            }
            return true;
        }
        public override void Update()
        {
            base.Update();
            if (damageMultiplier > 1f)

            {
                damageMultiplier -= 0.2f;
            }
            else

            {
                damageMultiplier = 1f;
            }
            if (_hitPoints <= 0f && !_destroyed)
            {
                Destroy(new DTImpact(this));
            }
        }
    }
}
