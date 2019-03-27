using Job.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Repository
{
   public class PlanetRepository
    {
       public static List<Planet> GetPlanetRepository()
       {
       
           Planet planeta1 = new Planet();
            planeta1.Name = "Ferengi";
            planeta1.Radio = 500;
            planeta1.Speed = 1;
            planeta1.DirectionSense = -1;


            Planet planeta2 = new Planet();
            planeta2.Name = "Betasoide";
            planeta2.Radio = 2000;
            planeta2.Speed = 3;
            planeta2.DirectionSense = -1;


            Planet planeta3 = new Planet();
            planeta3.Name = "Vulcano";
            planeta3.Radio = 1000;
            planeta3.Speed = 5;
            planeta3.DirectionSense = 1;

            int dias = 360;

            List<Planet> planet = new List<Planet>();
            planet.Add(planeta1);
            planet.Add(planeta2);
            planet.Add(planeta3);

            return planet;
       }

       
    }
}
