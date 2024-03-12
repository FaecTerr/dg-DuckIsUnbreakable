using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
//using System.Xml.Linq;

// The title of your mod, as displayed in menus
[assembly: AssemblyTitle("DuckUnbreakable")]

// The author of the mod
[assembly: AssemblyCompany("FaecTerr")]

// The description of the mod
[assembly: AssemblyDescription("Adds stands. Quack to use or long press quack to super move.")]

// The mod's version
[assembly: AssemblyVersion("1.0.0.0")]



namespace DuckGame.DuckUnbreakable
{            
    
    //workshopIDFacade = 2094212553;
    public class DuckUnbreakable : Mod
    {
        internal static DuckUnbreakableMain mm;
        // The mod's priority; this property controls the load order of the mod.
        public override Priority priority
        {
            get { return base.priority; }
        }

        //public override ulong workshopIDFacade => base.workshopIDFacade;

        // This function is run before all mods are finished loading.
        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();
        }

        // This function is run after all mods are loaded.
        protected override void OnPostInitialize()
        {
            Thread thread = new Thread(new ThreadStart(ExecuteOnceLoaded));
            thread.Start();
            NetMessageAdder.UpdateNetmessageTypes();
            base.OnPostInitialize();
            DuckUnbreakable.copyLevels();
        }
        private void ExecuteOnceLoaded()
        {
            while (Level.current == null || (!(Level.current.ToString() == "DuckGame.TitleScreen") && !(Level.current.ToString() == "DuckGame.TeamSelect2")))
            {
                Thread.Sleep(200);
            }
            UnlockData stands = new UnlockData
            {
                name = "Start with stand",
                id = "STANDS",
                type = UnlockType.Modifier,
                cost = 0,
                description = "Starts all duck with random stand",
                longDescription = "...",
                icon = 3,
                priceTier = UnlockPrice.Cheap
            };
            FieldInfo f = typeof(Unlocks).GetField("_allUnlocks", BindingFlags.Static | BindingFlags.NonPublic);
            ((List<UnlockData>)f.GetValue(null)).Add(stands);
            Unlocks.modifierToByte[stands.id] = (byte)Unlocks.allUnlocks.Count;
            foreach (Profile p in Profiles.universalProfileList)
            {
                if (!Unlocks.IsUnlocked("STANDS", p))
                {
                    p.unlocks.Add("STANDS");
                }
            }
            DuckUnbreakable.mm = new DuckUnbreakableMain(this);
        }
        private byte[] GetMD5Hash(byte[] sourceBytes)
        {
            return new MD5CryptoServiceProvider().ComputeHash(sourceBytes);
        }
        private static bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }
            int iterations = (int)Math.Ceiling((double)first.Length / 8.0);
            using (FileStream fs = first.OpenRead())
            {
                using (FileStream fs2 = second.OpenRead())
                {
                    byte[] one = new byte[8];
                    byte[] two = new byte[8];
                    for (int i = 0; i < iterations; i++)
                    {
                        fs.Read(one, 0, 8);
                        fs2.Read(two, 0, 8);
                        if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private static void copyLevels()
        {
            string levelFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\DuckUnbreakable");
            if (!Directory.Exists(levelFolder))
            {
                Directory.CreateDirectory(levelFolder);
            }
            foreach (string sourcePath in Directory.GetFiles(Mod.GetPath<DuckUnbreakable>("Levels")))
            {
                string destPath = Path.Combine(levelFolder, Path.GetFileName(sourcePath));
                bool file_exists = File.Exists(destPath);
                if (!file_exists || !DuckUnbreakable.FilesAreEqual(new FileInfo(sourcePath), new FileInfo(destPath)))
                {
                    if (file_exists)
                    {
                        File.Delete(destPath);
                    }
                    File.Copy(sourcePath, destPath);
                }
            }
        }
    }
}


