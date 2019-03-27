using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Entities
{
    public class Data
    {
        public Planet PlanetaPosicion { get; set; }

        public int Dia { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double[] GetCoordenadas()
        {
            return new double[2] { X, Y };
        }

        public string Clima { get; set; }

        public enum WeatherTypeEnum
        {
            Sequia, Llueve, Nollueve, Optimo
        }
    }
}
