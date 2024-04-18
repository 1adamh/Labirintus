using System;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Kovács_Patrik_TQPGSS_feleves_feladat
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] adatok = File.ReadAllLines("FALAK.BE.txt");
            string[] darabol = adatok[0].Split(' ');
            Falak elsosor = new Falak(int.Parse(darabol[0]), int.Parse(darabol[1]), int.Parse(darabol[2]));
            int[,] matrix = new int[elsosor.TablaHossz, elsosor.TablaHossz];
            Falak Matrix = new Falak(matrix);
            int[,] falakTomb = new int[elsosor.DbFalak, 4];
            for (int i = 1; i < elsosor.DbFalak + 1; i++)
            {
                string[] darabol2 = adatok[i].Split(' ');
                falakTomb[i - 1, 0] = elsosor.TablaHossz - int.Parse(darabol2[1]);
                falakTomb[i - 1, 1] = int.Parse(darabol2[0]) - 1;
                falakTomb[i - 1, 2] = int.Parse(darabol2[2]);
                falakTomb[i - 1, 3] = int.Parse(darabol2[3]);
                //adswgfwejgiweufgzfzewfwefzewgfwegfzwewegfwegfwezufgwezufgzweufgwezgfzweugfzuwetfgweffefef
            }
            Matrix.falFeltoltes(falakTomb);
            Matrix.feltoltesSzamokkal();
            Matrix.debugPrint();
            StreamWriter sw = new StreamWriter("FALAK.KI.txt", false, Encoding.Default);
            for (int i = adatok.Length - elsosor.DBPontok; i < adatok.Length; i++)
            {
                string[] darabol3 = adatok[i].Split(' ');
                int x = matrix.GetLength(0) - int.Parse(darabol3[1]);
                int y = int.Parse(darabol3[0]) - 1;

                //Matrix.kimenet(x, y);
                if (Matrix.megoldasE(matrix[x, y]))
                {
                    sw.WriteLine("IGEN");
                }
                else
                {
                    sw.WriteLine("NEM");
                }
            }
            sw.Close();
        }
    }
}
