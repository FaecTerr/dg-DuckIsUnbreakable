using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff|Trails")]
    public class Minecart : Block, IPlatform
    {
        private SpriteMap _sprite;
        private bool Railed;
        private float _Spin;
        private float moveTime;
        private EditorProperty<bool> auto;
        private EditorProperty<float> speed;
        private new bool right;
        public int mFrame;

        public Minecart(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(16f, 14f);
            collisionOffset = new Vec2(-16f, -2f);
            collisionSize = new Vec2(32f, 4f);
            depth = -0.6f;
            _sprite = new SpriteMap(GetPath("Sprites/Tilesets/Minecart.png"), 32, 16, false);
            base.graphic = _sprite;
            weight = 0.9f;
            auto = new EditorProperty<bool>(false);
            speed = new EditorProperty<float>(1f, this, 0.1f, 20f, 0.1f);
        }
        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSoftImpact(with, from);
            if (with != null && Railed == true && auto == false)
            {
                if(from == ImpactedFrom.Left || from == ImpactedFrom.Right)
                {
                    moveTime = with.hSpeed;
                    with.hSpeed *= 1f;
                }
            }
            else if (with != null && Railed == false && auto == false)
            {
                if (from == ImpactedFrom.Left || from == ImpactedFrom.Right)
                {
                    moveTime = with.hSpeed*0.5f;
                    with.hSpeed *= 0.2f;
                }
            }
        }
        public override void Update()
        {
            base.Update();
            if (mFrame > 0)
            {
                mFrame--;
            }
            if (auto == false)
            {
                if (moveTime != 0)
                {
                    position.x += 0.7f * moveTime;
                    moveTime *= 0.95f;
                    if (mFrame < 1)
                    {
                        SFX.Play(GetPath("SFX/Minecart.wav"), 1f, 0f, 0.0f, false);
                        mFrame = 30;
                    }
                }
            }
            if (auto == true)
            {
                if (mFrame < 1)
                {
                    SFX.Play(GetPath("SFX/Minecart.wav"), 1f, 0f, 0.0f, false);
                    mFrame = 30;
                }
                if (right == true)
                {
                    position.x += speed;
                    moveTime = speed / 0.7f;
                }
                else
                {
                    position.x += -speed;
                    moveTime = -speed / 0.7f;
                    
                }
            }
            graphic.flipH = !right;
            grounded = true;
            base.angleDegrees = _Spin;
            _Spin %= 360f;
            Railed = false;
            float yMove = 0;
            Rails rail = Level.CheckLine<Rails>(position, position + new Vec2(0f, 8f));
            if (rail != null)
            {
                Railed = true;
                position.y = rail.position.y + 2;
                if (rail.isDiagonal == true)
                {
                    if (rail.isRight == true)
                    {
                        yMove = position.x + (rail.y - (position.x - rail.position.x) - 8f);
                        position.y = rail.y - (position.x - rail.position.x) - 8f;
                        _Spin = Lerp.Float(_Spin, -45f, 8f*speed+1f);
                    }
                    else
                    {
                        yMove = position.y - (rail.y + (position.x - rail.position.x) - 8f);
                        position.y = rail.y + (position.x - rail.position.x) - 8f;
                        _Spin = Lerp.Float(_Spin, 45f, 8f*speed+1f);
                    }
                }
                else
                {
                    _Spin = Lerp.Float(_Spin, 0f, 8f * speed + 1f);
                }
                if (rail.isEnd)
                {
                    if (auto == false)
                    {
                        if (rail.isRight)
                        {
                            if (moveTime > -0.1f)
                            {
                                hSpeed *= 0f;
                                moveTime = 0f;

                            }
                        }
                        else
                        {
                            if (moveTime < 0.1f)
                            {
                                hSpeed *= 0f;
                                moveTime = 0f;
                            }
                        }
                    }
                    else if (auto == true)
                    {
                        if (rail.isRight)
                        {
                            right = false;
                        }
                        else
                        {
                            right = true;
                        }
                    }
                }
            }
            else
            {
                Block block = Level.CheckLine<Block>(position, position + new Vec2(0f, 2f));
                position.y += 0.8f;
            }
            foreach (PhysicsObject p in Level.CheckRectAll<PhysicsObject>(topLeft - new Vec2(2, 4), bottomRight + new Vec2(2, -4)))
            {

                if ((moveTime > 0.1f || moveTime < -0.1f))
                {
                    p.position.x += 0.7f * moveTime;
                    if (p.grounded)
                    {
                        p.position.y = position.y - (p.center.y) - 1;
                    }

                    if(((p.position.x > topRight.x && moveTime > 0.1f) || (p.position.x < left && moveTime < -0.1f)))
                    {
                        p.position.x -= 0.7f * moveTime;
                    }
                }        
            }
        }
    }
}
