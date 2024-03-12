using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Xml.Linq;


namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff")]
    public class MusicAutomat : Thing
    {
        SpriteMap _sprite;
        public int musicnum = 0;
        public bool reset = false;
        //public EditorProperty<string> musicname;
        public string musicname = "Nothing";
        public string Music = "Nothing";
        public EditorProperty<int> style;
        public bool init;
        public StateBinding _musBinds = new StateBinding("musicnum", -1, false, false);
        public StateBinding _nameBind = new StateBinding("musicname", -1, false, false);

        public MusicAutomat(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/MusicAutomat.png"), 30, 41, true);
            _sprite.frame = 0;
            graphic = _sprite;
            center = new Vec2(15f, 20.5f);
            collisionSize = new Vec2(30f, 40f);
            collisionOffset = new Vec2(-15f, -20f);
            base.depth = -0.75f;
            style = new EditorProperty<int>(0, this, 0f, 4f, 1f, null, false, false);
            _canFlip = true;
            _sprite.AddAnimation("style1idle", 1f, true, new int[] {
                0
            });
            _sprite.AddAnimation("style1playing", 0.1f, true, new int[]{
                1,
                2
            });
            _sprite.AddAnimation("style2idle", 1f, true, new int[] {
                3
            });
            _sprite.AddAnimation("style2playing", 0.1f, true, new int[]{
                4,
                5
            });
            _sprite.AddAnimation("style3idle", 1f, true, new int[] {
                6
            });
            _sprite.AddAnimation("style3playing", 0.1f, true, new int[]{
                7,
                8
            });
            _sprite.AddAnimation("style4idle", 1f, true, new int[] {
                9
            });
            _sprite.AddAnimation("style4playing", 0.1f, true, new int[]{
                10,
                11
            });
            base.hugWalls = WallHug.Floor;
            //musicname = new EditorProperty<string>("Nothing", null, 0f, 1f, 0.1f, null, false, false);
        }

        public override ContextMenu GetContextMenu()
        {
            new FieldBinding(this, "Music", 0f, 1f, 0.1f);
            EditorGroupMenu menu = base.GetContextMenu() as EditorGroupMenu;
            ContextTextbox nameBox = new ContextTextbox("Music", null, new FieldBinding(this, "musicname", 0f, 1f, 0.1f));
            menu.AddItem(nameBox);
            return menu;
        }

        public override BinaryClassChunk Serialize()
        {
            BinaryClassChunk element = base.Serialize();
            element.AddProperty("Music", musicname);
            return element;
        }
        public override bool Deserialize(BinaryClassChunk node)
        {
            base.Deserialize(node);
            musicname = node.GetProperty<string>("Music");
            return true;
        }

        public override DXMLNode LegacySerialize()
        {
            DXMLNode element = base.LegacySerialize();
            element.Add(new DXMLNode("Music", musicname));
            return element;
        }

        public override bool LegacyDeserialize(DXMLNode node)
        {
            base.LegacyDeserialize(node);
            DXMLNode getNode = node.Element("Music");
            if (getNode != null)
            {
                musicname = getNode.Value;
            }
            return true;
        }

        public override void Initialize()
        {
            if (!(Level.current is Editor))
            {
            }
        }

        public override void Update()
        {
            base.Update();
            if(musicname == null)
            {
                musicname = "";
            }
            if (init == false && musicname == "")
            {
                if (style == 0 || style == 1)
                {
                    _sprite.SetAnimation("style1idle");
                }
                else if (style == 2)
                {
                    _sprite.SetAnimation("style2idle");
                }
                else if (style == 3)
                {
                    _sprite.SetAnimation("style3idle");
                }
                else if (style == 4)
                {
                    _sprite.SetAnimation("style4idle");
                }
                init = true;
            }
            Duck d = Level.CheckRect<Duck>(topLeft, bottomRight);
            if (d != null && musicname == "")
            {
                if ((d.inputProfile.Pressed("GRAB") || d.inputProfile.Pressed("UP")) && musicname != "Null")
                {
                    if (style == 0)
                    {
                        musicnum = Rando.Int(1, 11);
                        _sprite.SetAnimation("style1playing");
                    }
                    else if (style == 1)
                    {
                        musicnum = Rando.Int(7, 9);
                        _sprite.SetAnimation("style1playing");
                    }
                    else if (style == 2)
                    {
                        musicnum = Rando.Int(4, 6);
                        _sprite.SetAnimation("style2playing");
                    }
                    else if (style == 3)
                    {
                        musicnum = Rando.Int(1, 3);
                        _sprite.SetAnimation("style3playing");
                    }
                    else if (style == 4)
                    {
                        musicnum = Rando.Int(9, 11);
                        _sprite.SetAnimation("style4playing");
                    }
                    reset = true;
                }
            }
            else if (musicname != "")
            {
                if (init == false)
                {
                    reset = true;
                    init = true;
                    if (style == 0 || style == 1)
                    {
                        _sprite.SetAnimation("style1playing");
                    }
                    else if (style == 2)
                    {
                        _sprite.SetAnimation("style2playing");
                    }
                    else if (style == 3)
                    {
                        _sprite.SetAnimation("style3playing");
                    }
                    else if (style == 4)
                    {
                        _sprite.SetAnimation("style4playing");
                    }
                }
                if (musicname == "Null")
                {
                    musicnum = 0;
                }
                else if (musicname == "Random")
                {
                    musicnum = Rando.Int(1, 9);
                }
                else if (musicname == "Mega Man" || musicname == "Intro1")
                {
                    musicnum = 1;
                    style = 3;
                }
                else if (musicname == "Duck Tales" || musicname == "Intro2" || musicname == "Moon")
                {
                    musicnum = 2;
                    style = 3;
                }
                else if (musicname == "Megadrive Rick" || musicname == "Never Gonna Give You Up" || musicname == "Never Gonna")
                {
                    musicnum = 3;
                    style = 3;
                }
                else if (musicname == "Chase" || musicname == "Intro4")
                {
                    musicnum = 4;
                    style = 2;
                }
                else if (musicname == "Stand Proud" || musicname == "Intro3")
                {
                    musicnum = 5;
                    style = 2;
                }
                else if (musicname == "Traitor's Requiem" || musicname == "Intro5")
                {
                    musicnum = 6;
                    style = 2;
                }
                else if (musicname == "Final Countdown" || musicname == "Intro7")
                {
                    musicnum = 7;
                    style = 1;
                }
                else if (musicname == "Space Dandy" || musicname == "Intro6")
                {
                    musicnum = 8;
                    style = 3;
                }
                else if (musicname == "Hit The Floor" || musicname == "DJ Electrohead" || musicname == "Katana Zero")
                {
                    musicnum = 9;
                    style = 4;
                }
                else if (musicname == "Coffin" || musicname == "Astronomia" || musicname == "Coffin Dance")
                {
                    musicnum = 10;
                    style = 4;
                }
                else if (musicname == "Beavis" || musicname == "Butthead" || musicname == "Debut" || musicname == "Landon")
                {
                    musicnum = 11;
                    style = 4;
                }
                else
                {
                    musicname = "";
                    init = false;
                }
            }
            if (musicnum == 1)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAmegaman.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 2)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAducktales.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 3)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAMegadriveRick.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 4)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAchase.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 5)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAstandproud.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 6)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAtraitorsrequiem.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 7)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAfinalcountdown.wav"), 0.7f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 8)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAspacedandy.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 9)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAhitthefloor.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 10)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAastronomia.wav"), 1f, 0f, 0.0f, true);
                }
            }
            else if (musicnum == 11)
            {
                if (reset == true)
                {
                    SFX.StopAllSounds();
                    reset = false;
                    SFX.Play(GetPath("SFX/MAbeavisbutthead.wav"), 1f, 0f, 0.0f, true);
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
            graphic.flipH = flipHorizontal;
        }
    }
}
