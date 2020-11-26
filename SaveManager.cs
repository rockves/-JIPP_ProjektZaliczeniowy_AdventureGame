using System;
using System.IO;
using System.Diagnostics;

namespace Zaliczenie
{
    /// <summary>
    /// Class for saving and loading object's
    /// </summary>
    class SaveManager
    {
        private string savePath;
        private string saveFileName;
        public string SavePath
        {
            get
            {
                return this.savePath;
            }
            set
            {
                if(value != "")
                {
                    this.savePath = value;
                }
            }
        }
        public string SaveFileName
        {
            get
            {
                return this.saveFileName;
            }
            set
            {
                if (value != "")
                {
                    this.saveFileName = value;
                }
            }
        }
        public SaveManager(string _saveFileName)
        {
            this.SavePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            this.SaveFileName = _saveFileName;
        }
        public SaveManager(string _savePath, string _saveFileName)
        {
            this.SavePath = _savePath;
            this.SaveFileName = _saveFileName;
        }
        public void Zapisz(Bohater bohater)
        {
            using (StreamWriter streamWriter = new StreamWriter(SaveFileName))
            {
                streamWriter.WriteLine(bohater.Nazwa);
                streamWriter.WriteLine(bohater.Pieniadze);
                streamWriter.WriteLine(bohater.ExpDoNastepnegoPoziomu);
                streamWriter.WriteLine(bohater.HP);
                streamWriter.WriteLine(bohater.Sila);
                streamWriter.WriteLine(bohater.Odpornosc);
                streamWriter.WriteLine(bohater.PunktyAkcji);
                streamWriter.WriteLine(bohater.Poziom);
                streamWriter.WriteLine(bohater.Doswiadczenie);
            }
            //SavePath + "\\" + SaveFileName
        }
        public Bohater Wczytaj()
        {
            string[] daneBohatera = new string[9];
            try
            {
                using (StreamReader streamReader = new StreamReader(SaveFileName))
                {
                    for (int i = 0; i < 9; i++)
                    {
                        daneBohatera[i] = streamReader.ReadLine();
                    }
                }
                float[] statystyki = new float[daneBohatera.Length - 1];
                for (int i = 0; i < daneBohatera.Length - 1; i++)
                {
                    statystyki[i] = Convert.ToSingle(daneBohatera[i+1]);
                }
                string imieBohatera = daneBohatera[0];
                Bohater wczytanyBohater = new Bohater(imieBohatera, statystyki[0], statystyki[1], statystyki[2], statystyki[3], statystyki[4], (int)statystyki[5], (int)statystyki[6], statystyki[7]);
                return wczytanyBohater;
            }
            catch
            {
                return null;
            }
        }
    }
}
