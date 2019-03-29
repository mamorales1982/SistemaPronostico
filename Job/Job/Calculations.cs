using Job.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    public class Calculations
    {
        public static double GetCoordX(Planet planetas, int day)
        {
            //Console.WriteLine("SENO : " + Math.Sin(NUM1 * Math.PI / 180));
            var coseno = Math.Cos((planetas.Speed * day * planetas.DirectionSense) * Math.PI / 180);

            double x = Math.Round((planetas.Radio * coseno), 0);
            return x;
        }

        public static double GetCoordY(Planet planetas, int day)
        {
            var seno = Math.Sin((planetas.Speed * day * planetas.DirectionSense) * Math.PI / 180);
            double y = Math.Round((planetas.Radio * seno), 0);
            return y;
        }


        public static double TrianguloPerimetro(double[] a, double[] b, double[] c)
        {
            // P = L1 + L2 + L3
            double ab = Math.Round(DistanciaEntrePts(a, b), 0);
            double bc = Math.Round(DistanciaEntrePts(b, c), 0);
            double ca = Math.Round(DistanciaEntrePts(c, a), 0);
            double perimeter = ab + bc + ca;
            return perimeter;
        }

        public static double DistanciaEntrePts(double[] a, double[] b)
        {
            // d^2 = (xa-xb)^2 + (ya-yb)^2 
            return Math.Sqrt(Math.Pow((a[0] - b[0]), 2) + Math.Pow((a[1] - b[1]), 2));
        }

        static double alineadosSol;
        public static bool PlanetasAlineados(double[] a, double[] b, double[] c, int precision)
        {
            bool colineales = false;
            // 
            //double mab = (Math.Round((b[1] - a[1])))/(Math.Round((b[0] - a[0])));
            //double mac = (Math.Round((c[1] - a[1])))/(Math.Round((c[0] - a[0])));
            //double mbc = (Math.Round((c[1] - b[1]))) / (Math.Round((c[0] - b[0])));


            //if ((Math.Abs(mac) >= 0.0 && Math.Abs(mac) <= Math.Abs(mab)) && (Math.Abs(mbc) >= 0.0 && Math.Abs(mbc) <= Math.Abs(mab)))
            //{
            //    //mab == mac ||
            //    colineales = true;
            //}
            //else //if (Math.Abs(mab) < 0.02 && Math.Abs() )
            //{
            //    colineales = false;
            //}

            Vector AB = new Vector();
            AB.X = Math.Round((b[0] - a[0]));
            AB.Y = Math.Round((b[1] - a[1]));

            Vector AC = new Vector();
            AC.X = Math.Round((c[0] - a[0]));
            AC.Y = Math.Round((c[1] - a[1]));

            Vector ASol = new Vector();
            ASol.X = Math.Round((0 - a[0]));
            ASol.Y = Math.Round((0 - a[1]));

            var determinante = AB.X * AC.Y - AB.Y * AC.X;
            if (determinante == 0)
            {

                colineales = true;
            }
            else
            {
                colineales = false;

            }

            int decimals = 10;
            if (precision >= 0 && precision < 4)
            {
                decimals = (int)Math.Pow(10, precision);
            }

            return colineales;
        }

        public static bool CalcularPendienteSol(double[] a, double[] b, double[] c)
        {

            bool pendiente = false;
            int sensibilidad = 5;

            //Calculamos la ecuación de la recta dados dos puntos
            double mAP = Math.Round((a[1] - 0), 0) / Math.Round((a[0] - 0), 0);
            double mBP = Math.Round((b[1] - 0), 0) / Math.Round((b[0] - 0), 0);
            //bB = Math.Round(b[1] + (m * b[0]), 0);
            if (mAP == mBP)
            {
                pendiente = true;
            }
            else
            {
                pendiente = false;
            }

            return pendiente;

        }

        public static bool NoAlineacionSol(double[] a, double[] b, double[] c)
        {
            bool noAlineado = false;


            double mba = (Math.Round((a[1] - b[1]))) / (Math.Round((a[0] - b[0])));
            //double mac = (Math.Round((c[1] - a[1])))/(Math.Round((c[0] - a[0])));
            double mbc = (Math.Round((b[1] - c[1]))) / (Math.Round((b[0] - c[0])));
            double mba1Abs = (Math.Abs(mba) - 0.1);
            double mba2Abs = (Math.Abs(mba) + 0.1);
            double mbcAbs = (Math.Abs(mbc));
            if (mba1Abs <= mbcAbs && mbcAbs <= mba2Abs)
            {
                noAlineado = true;
            }

            return noAlineado;
        }


        public static bool PuntoAdentroTriangulo(double[] a, double[] b, double[] c)
        {
            bool orientacion = true;

            var orientacionTriang = ((a[0] - c[0]) * (b[1] - c[1])) - ((a[1] - c[1]) * (b[0] - c[0]));

            //Orientacion de sol
            var ubicacionSol1 = ((a[0] - 0) * (b[1] - 0)) - ((a[1] - 0) * (b[0] - 0));

            var ubicaionSol2 = ((a[0] - c[0]) * (0 - c[1])) - ((a[1] - c[1]) * (0 - c[0]));

            var ubicacionSol3 = ((0 - c[0]) * (b[1] - c[1])) - ((0 - c[1]) * (b[0] - c[0]));

            if (ubicacionSol1 == 0)
            {
                orientacion = true;
            }

            if (orientacionTriang > 0 && ubicacionSol1 > 0 && ubicaionSol2 > 0 && ubicacionSol3 > 0)
            {
                orientacion = true;
            }
            else if (orientacionTriang < 0 && ubicacionSol1 < 0 && ubicaionSol2 < 0 && ubicacionSol3 < 0)
            {
                orientacion = true;
            }
            else
            {
                orientacion = false;
            }

            return orientacion;

        }
    }
}
