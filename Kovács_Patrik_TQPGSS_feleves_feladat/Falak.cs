using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kovács_Patrik_TQPGSS_feleves_feladat
{
    class Falak
    {
        private int tablaHossz;
        private int dbFalak;
        private int dbPontok;
        private int[,] matrix;

        public int TablaHossz
        {
            get
            {
                return tablaHossz;
            }
            set
            {
                tablaHossz = value;
            }
        }
        public int DbFalak
        {
            get
            {
                return dbFalak;
            }
            set
            {
                dbFalak = value;
            }
        }
        public int DBPontok
        {
            get
            {
                return dbPontok;
            }
            set
            {
                dbPontok = value;
            }
        }
        public int[,] Matrix
        {
            get
            {
                return matrix;
            }
            set
            {
                matrix = value;
            }
        }
        public Falak(int hossz, int falak, int pontok)
        {
            this.TablaHossz = hossz;
            this.dbFalak = falak;
            this.dbPontok = pontok;
        }
        public Falak(int[,] matrix)
        {
            this.Matrix = matrix;
        }
        private void tablazatFeltoltes()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[i, j] = 0;
                }
            }
        }
        public void falFeltoltes(int[,] falak)
        {
            for (int k = 0; k < falak.GetLength(0); k++)
            {
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        if (i == falak[k, 0] && j == falak[k, 1])
                        {

                            if (falak[k, 2] == 1) // fugguleges fal
                            {
                                for (int m = 0; m < i + 1; m++)
                                {
                                    if (m >= falak[k, 0] - falak[k, 3] + 1)
                                    {
                                        Matrix[m, j] = 1;
                                    }
                                }
                            }
                            else // vizszintes fal
                            {
                                for (int m = 0; m < falak[k, 1] + falak[k, 2]; m++)
                                {
                                    if (m >= falak[k, 1])
                                    {
                                        Matrix[i, m] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void feltoltesSzamokkal()
        {
            int jelenlegi = 2;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    bool belepettE = false;
                    if (Matrix[i, j] != 1)
                    {
                        if (benneVanE(i - 1, j, Matrix.GetLength(0)) && benneVanE(i, j - 1, Matrix.GetLength(0)))
                        {
                            int felso = Matrix[i - 1, j];
                            int bal = Matrix[i, j - 1];
                            if (felso != 1 && bal != 1 && bal != felso)
                            {
                                Matrix[i, j] = bal;
                                Osszevonas(Matrix, felso, bal);
                                belepettE = true;
                            }
                        }
                        if (belepettE == false)
                        {
                            if (benneVanE(i - 1, j, Matrix.GetLength(0)) && Matrix[i - 1, j] != 1)
                            {
                                Matrix[i, j] = Matrix[i - 1, j];
                            }
                            else
                            {
                                if (benneVanE(i, j - 1, Matrix.GetLength(0)) && Matrix[i, j - 1] != 1)
                                {
                                    Matrix[i, j] = Matrix[i, j - 1];
                                }
                                else
                                {
                                    Matrix[i, j] = jelenlegi;
                                    jelenlegi++;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Osszevonas(int[,] matrix, int felso, int bal)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == felso)
                    {
                        matrix[i, j] = bal;
                    }
                }
            }
        }
        private bool benneVanE(int x, int y, int hossz)
        {
            return x >= 0 && x < hossz && y >= 0 && y < hossz;
        }
        public bool megoldasE(int x)
        {
            if (x == 1)
            {
                return false;
            }
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                if (Matrix[i, 0] == x || Matrix[i, Matrix.GetLength(0) - 1] == x)
                {
                    return true;
                }
                if (Matrix[0, i] == x || Matrix[Matrix.GetLength(0) - 1, i] == x)
                {
                    return true;
                }
            }
            return false;
        }

        //public void kimenet(int x, int y)
        //{
        //    StreamWriter sw = new StreamWriter("FALAK.KI.txt", true, Encoding.Default);
        //    if (megoldasE(Matrix[x, y]))
        //    {
        //        sw.WriteLine("IGEN");
        //    }
        //    else
        //    {
        //        sw.WriteLine("NEM");
        //    }
        //    sw.Close();
        //}
        public void debugPrint()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] == 1)
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
