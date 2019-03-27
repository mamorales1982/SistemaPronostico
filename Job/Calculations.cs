using Job.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
  public  class Calculations
    {
        public static double getCoordX(Planet planetas, int day)
        {
            //Console.WriteLine("SENO : " + Math.Sin(NUM1 * Math.PI / 180));
            var coseno = Math.Cos((planetas.Speed * day * planetas.DirectionSense) * Math.PI / 180);

            double x = Math.Round((planetas.Radio * coseno), 0);
            return x;
        }

        public static double getCoordY(Planet planetas, int day)
        {
            var seno = Math.Sin((planetas.Speed * day * planetas.DirectionSense) * Math.PI / 180);
            double y = Math.Round((planetas.Radio * seno), 0);
            //double y = Math.Round(planetas.Radio * Math.Sin(planetas.Velocidad * day * planetas.direccion), 2);
            return y;
        }

      
        public static double trianguloPerimetro(double[] a, double[] b, double[] c)
        {
            // P = L1 + L2 + L3
            double ab = Math.Round(distanciaEntrePts(a, b), 0);
            double bc = Math.Round(distanciaEntrePts(b, c), 0);
            double ca = Math.Round(distanciaEntrePts(c, a), 0);
            double perimeter = ab + bc + ca;
            return perimeter;
        }

        public static double distanciaEntrePts(double[] a, double[] b)
        {
            // d^2 = (xa-xb)^2 + (ya-yb)^2 
            return Math.Sqrt(Math.Pow((a[0] - b[0]), 2) + Math.Pow((a[1] - b[1]), 2));
        }

        static double alineadosSol;
        public static bool PlanetasAlineados(double[] a, double[] b, double[] c, int precision)
        {

            bool colineales = false;
            // P1 = bx - ax / by - ay   ^   P2 = cx - bx / cy - by  => P1 = P2
            // 
            //double mab = (Math.Round((b[1] - a[1])))/(Math.Round((b[0] - a[0])));
            //double mac = (Math.Round((c[1] - a[1])))/(Math.Round((c[0] - a[0])));
            //double mbc = (Math.Round((c[1] - b[1]))) / (Math.Round((c[0] - b[0])));
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
                // alineadosSol = AB.X * ASol.Y - AB.Y * ASol.X;
                //  var kk = alineacionSol();

                colineales = true;

            }
            else
            {
                colineales = false;

            }
            //double ba = Math.Round((b[1] - a[1]), 2);
            //double cb = Math.Round((c[1] - b[1]), 2);
            //if (ab == 0 || bc == 0)
            //{
            //    return (ab == 0 && bc == 0);
            //}

            //if (mab == mac )
            //{
            //    colineales = true;
            //}
            //double p1 = ab;
            //double p2 = bc;
            //double p1 = (b[0] - a[0]) / ba;
            //double p2 = (c[0] - b[0]) / cb;
            int decimals = 10;
            if (precision >= 0 && precision < 4)
            {
                decimals = (int)Math.Pow(10, precision);
            }
            //p1 = Math.Round(p1 * decimals) / decimals;
            //p2 = Math.Round(p2 * decimals) / decimals;
            return colineales;
        }

        static double bB;

        public static bool calcularPendiente(double[] a, double[] b, double[] c)
        {
            //Point p1 = centro;
            //Point p2 = centro;

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

        public static bool alineacionSol()
        {
            return alineadosSol == 0;
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


            //(A.x - 0) * (B.y - 0) - (A.y - 0) * (B.x - 0)

            //(A.x - C.x) * (0 - C.y) - (A.y - C.y) * (0 - C.x)

            //(0 - C.x) * (B.y - C.y) - (0 - C.y) * (B.x - C.x)


            return orientacion;

            //(A.x - C.x) * (B.y - C.y) - (A.y - C.y) * (B.x - C.x)

            //(A.x - 0) * (B.y - 0) - (A.y - 0) * (B.x - 0)

            //(A.x - C.x) * (0 - C.y) - (A.y - C.y) * (0 - C.x)

            //(0 - C.x) * (B.y - C.y) - (0 - C.y) * (B.x - C.x)
        }
    }
}
