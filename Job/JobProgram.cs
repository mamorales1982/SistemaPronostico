using Job.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Repository;
using System.Data.SqlClient;
using System.Data;

namespace Job
{
   public class JobProgram
    {
       static void Main(string[] args)
       {
           Console.WriteLine("Job - Modelos de Datos");
           Console.WriteLine("-");



           Console.WriteLine("Precione 1 para comenzar el proceso de sicronizacion ");
           Console.WriteLine("Precione 2 para consultar estado del clima del Dia ");
           String texto;
           texto = Console.ReadLine();

           if (texto == "1")
           {
              
               
               
               int dias = 3650;
               Console.WriteLine("Iniciando...... ");
               Console.WriteLine(" ");
               Delete();
               
               List<Data> listPlanets = new List<Data>();

               //   StreamWriter file = new System.IO.StreamWriter(@"C:\tempo\planetas.txt");



               for (int i = 0; i < dias; i++)
               {
                   foreach (var item in PlanetRepository.GetPlanetRepository())
                   {
                       var pointX = Calculations.getCoordX(item, i);
                       var pointY = Calculations.getCoordY(item, i);

                       listPlanets.Add(new Data { PlanetaPosicion = item, X = pointX, Y = pointY, Dia = i });
                   }


               }


               List<Data> listanueva = new List<Data>();
               for (int i = 0; i < dias; i++)
               {
                   var listDay = listPlanets.Where(x => x.Dia == i).ToList();

                   var planetasAlineados = Calculations.PlanetasAlineados(listDay[0].GetCoordenadas(), listDay[1].GetCoordenadas(), listDay[2].GetCoordenadas(), 2);
                   var alineacionSol = Calculations.alineacionSol();
                   //  if (Util.calcularPendiente(listadia[0].GetCoordenadas(), listadia[1].GetCoordenadas(), listadia[2].GetCoordenadas()));
                   var perimetroTriangulo = Calculations.trianguloPerimetro(listDay[0].GetCoordenadas(), listDay[1].GetCoordenadas(), listDay[2].GetCoordenadas());

                   var puntoAdentroTriangulo = Calculations.PuntoAdentroTriangulo(listDay[0].GetCoordenadas(), listDay[1].GetCoordenadas(), listDay[2].GetCoordenadas());
                   var pendiente = Calculations.calcularPendiente(listDay[0].GetCoordenadas(), listDay[1].GetCoordenadas(), listDay[2].GetCoordenadas());

                   if (planetasAlineados == true && pendiente == true)
                   {
                       listDay[0].Clima = Data.WeatherTypeEnum.Sequia.ToString();
                       listDay[1].Clima = Data.WeatherTypeEnum.Sequia.ToString();
                       listDay[2].Clima = Data.WeatherTypeEnum.Sequia.ToString();

                   }
                   else if (planetasAlineados == true && pendiente == false)
                   {
                       listDay[0].Clima = Data.WeatherTypeEnum.Optimo.ToString();
                       listDay[1].Clima = Data.WeatherTypeEnum.Optimo.ToString();
                       listDay[2].Clima = Data.WeatherTypeEnum.Optimo.ToString();

                       //   Console.WriteLine("Dia: " + i + " - " + Data.WeatherTypeEnum.Optimo);
                   }

                   //listanueva.Add(new Data {PlanetaPosicion =  });    

                       //sequia(dia);

                   else if (planetasAlineados == false && puntoAdentroTriangulo == true)
                   {
                       listDay[0].Clima = Data.WeatherTypeEnum.Llueve.ToString();
                       listDay[1].Clima = Data.WeatherTypeEnum.Llueve.ToString();
                       listDay[2].Clima = Data.WeatherTypeEnum.Llueve.ToString();
                       //Console.WriteLine("Dia: " + i + " - " + Data.WeatherTypeEnum.Llueve);
                   }
                   else if (puntoAdentroTriangulo == false)
                   {
                       listDay[0].Clima = Data.WeatherTypeEnum.Nollueve.ToString();
                       listDay[1].Clima = Data.WeatherTypeEnum.Nollueve.ToString();
                       listDay[2].Clima = Data.WeatherTypeEnum.Nollueve.ToString();

                       //Console.WriteLine("Dia: " + i + " - " + Data.WeatherTypeEnum.Nollueve);
                   }
                   else
                   {
                       //Console.WriteLine("Dia: " + i + " - " + Data.WeatherTypeEnum.Optimo);
                       //BuenClima(dia);
                   }

                   //else
                   //{

                   //    if (IncluyeAlSol())
                   //    {
                   //        Lluvia(dia, trianguloPerimetro());
                   //    }
                   //    else
                   //    {
                   //        Normal(dia);
                   //    }
                   //}

                   // List<Planetas>


               }


               for (int i = 0; i < dias; i++)
               {
                   var list = listPlanets.Where(x => x.Dia == i).ToList();

                   Console.WriteLine("Planeta = " + list[0].PlanetaPosicion.Name + " - " + "dia = " + i + " - " + "posicion Coordenadas (" + list[0].X.ToString() + " , " + list[0].Y.ToString() + ")" + " - " + "Clima = " + list[0].Clima.ToString());
                   Console.WriteLine("Planeta = " + list[1].PlanetaPosicion.Name + " - " + "dia = " + i + " - " + "posicion Coordenadas (" + list[1].X.ToString() + " , " + list[1].Y.ToString() + ")" + " - " + "Clima = " + list[1].Clima.ToString());
                   Console.WriteLine("Planeta = " + list[2].PlanetaPosicion.Name + " - " + "dia = " + i + " - " + "posicion Coordenadas (" + list[2].X.ToString() + " , " + list[2].Y.ToString() + ")" + " - " + "Clima = " + list[2].Clima.ToString());

               }

               InsertData (listPlanets);

               Console.WriteLine(" ");

               Console.WriteLine("NUmero de registros = " + listPlanets.Count());

               Console.ReadKey();




           }
           else if (texto == "2")
           {
               Console.WriteLine("ingrese el numero del dia que desea ver el pronostico  ");
               String number;
               number = Console.ReadLine();
               ConsultaDia(Convert.ToInt32(number));
           }
           else
           {
               Console.WriteLine("Por favor , seleccione un valor  ");
           }



       }

        public static void InsertData(List<Data> listPlanets)
        {
            string connectionString = "data source=servermario.database.windows.net;initial catalog=Clima;user id=mamorales;password=Bauti2020;MultipleActiveResultSets=True;App=EntityFramework";


            // define INSERT query with parameters
            string query = "INSERT INTO [Table] (Id, Planeta, Dia, CoordenadaX, CoordenadaY, Clima) " +
                       "VALUES (@Id, @Planeta, @Dia, @CoordenadaX, @CoordenadaY, @Clima ) ";

            // create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                int i = 1;
                foreach (var item in listPlanets)
                {
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int, 50).Value = i;
                        cmd.Parameters.Add("@Planeta", SqlDbType.VarChar, 50).Value = item.PlanetaPosicion.Name;
                        cmd.Parameters.Add("@Dia", SqlDbType.Int, 50).Value = item.Dia;
                        cmd.Parameters.Add("@CoordenadaX", SqlDbType.Decimal, 50).Value = item.X;
                        cmd.Parameters.Add("@CoordenadaY", SqlDbType.Decimal, 50).Value = item.Y;
                        cmd.Parameters.Add("@Clima", SqlDbType.VarChar).Value = item.Clima;

                        cmd.ExecuteNonQuery();
                        i++;
                    }
                }

                cn.Close();
            }
        }


        public static string ConsultaDia(int dia)
        {
            string connectionString = "data source=servermario.database.windows.net;initial catalog=Clima;user id=mamorales;password=Bauti2020;MultipleActiveResultSets=True;App=EntityFramework";


            // Provide the query string with a parameter placeholder.
            string queryString = "SELECT * from [Table] "
                    + "WHERE Dia = @Dia";

            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                // Create the Command and Parameter objects.

                command.Parameters.AddWithValue("Dia", dia);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(); ;
                try
                {

                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}",
                            reader[2], reader[1], reader[5]);
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadLine();
            }

            return "";
        }


        public static void Delete() 
        {
            try
            {
                string connectionString = "data source=servermario.database.windows.net;initial catalog=Clima;user id=mamorales;password=Bauti2020;MultipleActiveResultSets=True;App=EntityFramework";

                using (var sc = new SqlConnection(connectionString))
                using (var cmd = sc.CreateCommand())
                {
                    sc.Open();
                    cmd.CommandText = "DELETE FROM [Table]";
                 //   cmd.Parameters.AddWithValue("@word", word);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
               Console.WriteLine("SQL error " + e);
            }
        }

    }
}
