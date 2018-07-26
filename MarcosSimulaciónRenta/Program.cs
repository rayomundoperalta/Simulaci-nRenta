using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.VisualBasic;

namespace MarcosSimulaciónRenta
{
    class Program
    {
        static void Main(string[] args)
        {
            double costoPublico = 0;
            double costoDistribuidor = 0;
            int periodosDelPlazo = 0;
            int periodosAnuales = 0;
            double tazaInteresAnual = 0;
            int NumeroDeRentas = 0;
            double TazaFalloEquipo = 0;
            double TazaCancelación = 0;
            double TazaNoPago = 0;

            int NumeroSimulaciones = 0;
            
            int NumeroThreads;
            /* El 8 es el numero de cores del procesador */
            HiloDeSimulacion[] HilosDeSimulacion = new HiloDeSimulacion[8];
            Thread[] threads = new Thread[8];
            /*
            
            do
            {
                Console.Write("Numero de Threads (max 8)     : ");
                NumeroThreads = int.Parse(Console.ReadLine());
            }
            while (NumeroThreads > 8);
            */
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\SimulacionesRentas\ResultadosSimulaciones.txt"))
            {
                file.WriteLine("CostoPublico, CostoDistribuidor, PeriodosPorAño, PeriodosDelContrato, TazaIntAnual, NumeroDeRentas, TazaDeFallo, TazaDeCancelación, TazaNoPago, NumeroDeThreads, " +
                "PagoMensualDeAguaCaliente, CostoInicialDeLosEquipos, IngresosTotales, CostosPorFallaDeEquipos, NumeroDeFallasDeEquipos, NumeroDeCancelaciones, NumeroDeNoPagos, SimulationTime");

                var lineas = File.ReadLines(@"D:\SimulacionesRentas\Simulaciones.txt");
                
                int linea = 1;
                foreach (string line in lineas)
                {
                    Console.WriteLine(line);
                    //Console.ReadKey();
                    string[] numbers = line.Split(',');
                    costoPublico = Convert.ToDouble(numbers[0]);
                    costoDistribuidor = Convert.ToDouble(numbers[1]);
                    periodosAnuales = int.Parse(numbers[2]);
                    periodosDelPlazo = int.Parse(numbers[3]);
                    tazaInteresAnual = Convert.ToDouble(numbers[4]);
                    NumeroDeRentas = int.Parse(numbers[5]);
                    TazaFalloEquipo = Convert.ToDouble(numbers[6]);
                    TazaCancelación = Convert.ToDouble(numbers[7]);
                    TazaNoPago = Convert.ToDouble(numbers[8]);
                    NumeroThreads = int.Parse(numbers[9]);
                    if (NumeroThreads < 9)
                    {
                        file.Write(costoPublico + ", " + costoDistribuidor + ", " + periodosAnuales + ", " + periodosDelPlazo + ", " + tazaInteresAnual + ", " + NumeroDeRentas + ", " + TazaFalloEquipo + ", " + TazaCancelación + ", " + TazaNoPago + ", " + NumeroThreads + ", ");
                        int repeticiones = 100;   // veces que se simula una renta
                        NumeroSimulaciones = NumeroDeRentas * repeticiones;
                        int cociente = NumeroSimulaciones / NumeroThreads;
                        int residuo = NumeroSimulaciones % NumeroThreads;
                        Console.WriteLine("Numero de Rentas: " + NumeroDeRentas);
                        Console.WriteLine("cociente: " + cociente + " residuo: " + residuo + " prueba " + (NumeroThreads * cociente + residuo - NumeroSimulaciones));

                        string ArchivoSalida = "AS_" + linea++ + "_";
                        for (int i = 0; i < (NumeroThreads - 1); i++)
                        {
                            HilosDeSimulacion[i] = new HiloDeSimulacion(ArchivoSalida + i, cociente, NumeroDeRentas, costoDistribuidor, periodosDelPlazo, TazaFalloEquipo, TazaCancelación, TazaNoPago,
                                tazaInteresAnual, periodosAnuales, costoPublico);
                        }
                        HilosDeSimulacion[NumeroThreads - 1] = new HiloDeSimulacion(ArchivoSalida + (NumeroThreads - 1), cociente + residuo, NumeroDeRentas, costoDistribuidor, periodosDelPlazo, TazaFalloEquipo, TazaCancelación, TazaNoPago,
                            tazaInteresAnual, periodosAnuales, costoPublico);

                        var empiezo = DateAndTime.TimeOfDay;
                        int epochInicio = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

                        for (int i = 0; i < NumeroThreads; i++)
                        {
                            threads[i] = new Thread(new ThreadStart(HilosDeSimulacion[i].EjecutaHilo));
                        }

                        for (int i = 0; i < NumeroThreads; i++)
                        {
                            threads[i].Start();
                        }

                        for (int i = 0; i < NumeroThreads; i++)
                        {
                            threads[i].Join();
                        }
                        var fin = DateAndTime.TimeOfDay;
                        int epochFin = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

                        // int epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                        Console.WriteLine("Simulation time: " + (fin - empiezo));
                        Console.WriteLine("Epoch " + epochFin + " " + epochInicio);

                        double MIT = 0;
                        for (int i = 0; i < NumeroThreads; i++) MIT += HilosDeSimulacion[i].MeanIngresosTotales;
                        double MCFE = 0;
                        for (int i = 0; i < NumeroThreads; i++) MCFE += HilosDeSimulacion[i].MeanCostosFallasEquipo;
                        double MFT = 0;
                        for (int i = 0; i < NumeroThreads; i++) MFT += HilosDeSimulacion[i].MeanFallasTotales;
                        double MC = 0;
                        for (int i = 0; i < NumeroThreads; i++) MC += HilosDeSimulacion[i].MeanCancelaciones;
                        double MNP = 0;
                        for (int i = 0; i < NumeroThreads; i++) MNP += HilosDeSimulacion[i].MeanNoPagos;

                        TablaAmortización TA = new TablaAmortización(tazaInteresAnual / periodosAnuales, periodosDelPlazo, costoPublico);
                        file.WriteLine(TA.PagoPeriodico() + ", " + NumeroDeRentas * costoDistribuidor + ", " + MIT / NumeroSimulaciones + ", " + MCFE / NumeroSimulaciones + ", " + MFT / NumeroSimulaciones + ", " + MC / NumeroSimulaciones + ", " +  MNP / NumeroSimulaciones + ", " + (fin - empiezo));
                    }
                    else
                    {
                        Console.WriteLine("Demasiados Threads");
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
