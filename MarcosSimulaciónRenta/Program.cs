using System;
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

            Console.Write("Costo al público      (6954)  : ");
            costoPublico = Convert.ToDouble(Console.ReadLine());
            Console.Write("Costo al Distribuidor (4635)  : ");
            costoDistribuidor = Convert.ToDouble(Console.ReadLine());
            Console.Write("Periodos por año              : ");
            periodosAnuales = int.Parse(Console.ReadLine());
            Console.Write("Periodos del Contrato         : ");
            periodosDelPlazo = int.Parse(Console.ReadLine());
            Console.Write("Taza de Interés Anual         : ");
            tazaInteresAnual = Convert.ToDouble(Console.ReadLine());
            Console.Write("Numero de Rentas a Simular    : ");
            NumeroDeRentas = int.Parse(Console.ReadLine());
            Console.Write("Taza Fallo Equipo             : ");
            TazaFalloEquipo = Convert.ToDouble(Console.ReadLine());
            Console.Write("Taza Cancelación              : ");
            TazaCancelación = Convert.ToDouble(Console.ReadLine());
            Console.Write("Taza No Pago                  : ");
            TazaNoPago = Convert.ToDouble(Console.ReadLine());
            do
            {
                Console.Write("Numero de Threads (max 8)     : ");
                NumeroThreads = int.Parse(Console.ReadLine());
            }
            while (NumeroThreads > 8);
            
            int repeticiones = 1000;   // veces que se simula una renta
            NumeroSimulaciones = NumeroDeRentas * repeticiones;
            int cociente = NumeroSimulaciones / NumeroThreads;
            int residuo = NumeroSimulaciones % NumeroThreads;
            Console.WriteLine("cociente: " + cociente + " residuo: " + residuo + " prueba " + (NumeroThreads * cociente + residuo - NumeroSimulaciones));
            
            for (int i = 0; i < (NumeroThreads - 1); i++)
            {
                HilosDeSimulacion[i] = new HiloDeSimulacion(cociente, NumeroDeRentas, costoDistribuidor, periodosDelPlazo, TazaFalloEquipo, TazaCancelación, TazaNoPago,
                    tazaInteresAnual, periodosAnuales, costoPublico);
            }
            HilosDeSimulacion[NumeroThreads - 1] = new HiloDeSimulacion(cociente + residuo, NumeroDeRentas, costoDistribuidor, periodosDelPlazo, TazaFalloEquipo, TazaCancelación, TazaNoPago,
                tazaInteresAnual, periodosAnuales, costoPublico);

            var empiezo = DateAndTime.TimeOfDay;
            
            for (int i = 0; i < NumeroThreads; i++)
            {
                threads[i] = new Thread(new ThreadStart(HilosDeSimulacion[i].EjecutaHilo));
            }
            
            for (int i = 0; i < NumeroThreads; i ++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < NumeroThreads; i++)
            {
                threads[i].Join();
            }
            var fin = DateAndTime.TimeOfDay;
            
            Console.WriteLine("Simulation time: " + (fin - empiezo));

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
            Console.WriteLine("Pago Mensual de agua caliente : {0,-12:N2}", TA.PagoPeriodico());
            Console.WriteLine("Costo Inicial de los equipos  : {0,-12:N2}", NumeroDeRentas * costoDistribuidor);
            
            Console.WriteLine("Ingresos Totales              : {0,-12:N2}", MIT / NumeroSimulaciones);
            Console.WriteLine("Costos por falla de equipos   : {0,-12:N2}", MCFE / NumeroSimulaciones);
            Console.WriteLine("Numero de Fallas de equipos   : {0,-12:N2}", MFT / NumeroSimulaciones);
            Console.WriteLine("Numero de Cancelaciones       : {0,-12:N2}", MC / NumeroSimulaciones);
            Console.WriteLine("Numero de NoPagos             : {0,-12:N2}", MNP / NumeroSimulaciones);

            Console.ReadKey();

        }
    }
}
