using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class Stunlight : Thing
    {
        public StateBinding _positionStateBinding = new CompressedVec2Binding("position");
        protected SpriteMap _sprite;
        private float radius;
        private SinWave _pulse1 = Rando.Float(1f, 2f);
        private SinWave _pulse2 = Rando.Float(0.5f, 4f);
        public bool IsLocalDuckAffected
        {
            get;
            set;
        }

        public Stunlight(float xval, float yval, float radius = 160f, float alp = 1f) : base(xval, yval)
        {         
            depth = 1f;
            layer = Layer.Foreground;
            radius = radius;
            SetIsLocalDuckAffected();
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/VFX/StunLight.png"), 64, 64);
            
            _sprite.alpha = alp;
        }

        public virtual void SetIsLocalDuckAffected()
        {
            List<Duck> ducks = new List<Duck>();
            foreach (Duck duck in Level.CheckCircleAll<Duck>(position, radius))
            {
                if (!ducks.Contains(duck))
                {
                    ducks.Add(duck);
                }
            }
            foreach (Ragdoll ragdoll in Level.CheckCircleAll<Ragdoll>(position, radius))
            {
                if (!ducks.Contains(ragdoll._duck))
                {
                    ducks.Add(ragdoll._duck);
                }
            }
            foreach (Duck duck in ducks)
            {
                if (duck.profile.localPlayer)
                {
                    if (Level.CheckLine<Block>(position, duck.position, duck) == null)
                    {
                        IsLocalDuckAffected = true;
                        return;
                    }
                }
            }
            IsLocalDuckAffected = false;
        }

        public override void Update()
        {
            base.Update();
            _sprite.xscale = Level.current.camera.width/63;
            _sprite.yscale = Level.current.camera.height/63;
            _sprite.angleDegrees = 0f + _pulse2 * 0.1f;
        }

        public override void Draw()
        {
            if (IsLocalDuckAffected)
            {
                //updater.ShaderController();
                Graphics.Draw(_sprite, Level.current.camera.position.x-1, Level.current.camera.position.y-1, 1f);
            }
            base.Draw();
        }
    }
}
