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

            double[] MeanIngresosTotales = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] MeanCostosFallasEquipo = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] MeanFallasTotales = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] MeanCancelaciones = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] MeanNoPagos = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int NumeroSimulaciones = 0;
            Simulación[] simulaciones;
            TablaAmortización TA;

            void grupo1()
            {
                for (int s = 0; s < NumeroSimulaciones / 10; s++)
                {
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[0] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[0] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[0] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[0] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[0] += simulaciones[s].Result.CuantosNoPagos;
                }
            }

            void grupo2()
            {
                //Console.WriteLine("grupo 2 ini");
                for (int s = NumeroSimulaciones / 10; s < 2 * NumeroSimulaciones / 10; s++)
                {
                    //Console.WriteLine("sim  2 - " + s);
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[1] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[1] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[1] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[1] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[1] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 2 fin");
            }

            void grupo3()
            {
                //Console.WriteLine("grupo 3 ini");
                for (int s = 2 * NumeroSimulaciones / 10; s < 3 * NumeroSimulaciones / 10; s++)
                {
                    //Console.WriteLine("sim  3 - " + s);
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[2] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[2] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[2] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[2] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[2] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 3 fin");
            }

            void grupo4()
            {
                //Console.WriteLine("grupo 4 ini");
                for (int s = 3 * NumeroSimulaciones / 10; s < 4 * NumeroSimulaciones / 10; s++)
                {
                    //Console.WriteLine("sim  4 - " + s);
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[3] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[3] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[3] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[3] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[3] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 4 fin");
            }

            void grupo5()
            {
                //Console.WriteLine("grupo 5 ini");
                for (int s = 4 * NumeroSimulaciones / 10; s < 5 * NumeroSimulaciones / 10; s++)
                {
                    //Console.WriteLine("sim  5 - " + s);
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[4] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[4] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[4] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[4] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[4] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 5 fin");
            }

            void grupo6()
            {
                //Console.WriteLine("grupo 6 ini");
                for (int s = 5 * NumeroSimulaciones / 10; s < 6 * NumeroSimulaciones / 10; s++)
                {
                    //Console.WriteLine("sim  6 - " + s);
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[5] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[5] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[5] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[5] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[5] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 6 fin");
            }

            void grupo7()
            {
                //Console.WriteLine("grupo 7 ini");
                for (int s = 6 * NumeroSimulaciones / 10; s < 7 * NumeroSimulaciones / 10; s++)
                {
                    //Console.WriteLine("sim  7 - " + s);
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[6] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[6] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[6] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[6] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[6] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 7 fin");
            }

            void grupo8()
            {
                //Console.WriteLine("grupo 8 ini");
                for (int s = 7 * NumeroSimulaciones / 10; s < 8 * NumeroSimulaciones / 10; s++)
                {
                    //Console.WriteLine("sim  8 - " + s);
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[7] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[7] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[7] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[7] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[7] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 8 fin");
            }

            void grupo9()
            {
                //Console.WriteLine("grupo 9 ini");
                for (int s = 8 * NumeroSimulaciones / 10; s < 9 * NumeroSimulaciones / 10; s++)
                {
                    //Console.WriteLine("sim  9 - " + s);
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[8] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[8] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[8] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[8] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[8] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 9 fin");
            }

            void grupo10()
            {
                //Console.WriteLine("grupo 10 ini");
                for (int s = 9 * NumeroSimulaciones / 10; s < NumeroSimulaciones; s++)
                {
                    simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                        TazaFalloEquipo, TazaCancelación, TazaNoPago);
                    MeanIngresosTotales[9] += simulaciones[s].Result.IngresosTotales;
                    MeanCostosFallasEquipo[9] += simulaciones[s].Result.CostosFallasEquipo;
                    MeanFallasTotales[9] += simulaciones[s].Result.FallasTotales;
                    MeanCancelaciones[9] += simulaciones[s].Result.CuantasCancelaciones;
                    MeanNoPagos[9] += simulaciones[s].Result.CuantosNoPagos;
                }
                //Console.WriteLine("grupo 10 fin");
            }

            Console.Write("Costo al público      (6954): ");
            costoPublico = Convert.ToDouble(Console.ReadLine());
            Console.Write("Costo al Distribuidor (4635): ");
            costoDistribuidor = Convert.ToDouble(Console.ReadLine());
            Console.Write("Periodos por año            : ");
            periodosAnuales = int.Parse(Console.ReadLine());
            Console.Write("Periodos del Contrato       : ");
            periodosDelPlazo = int.Parse(Console.ReadLine());
            Console.Write("Taza de Interés Anual       : ");
            tazaInteresAnual = Convert.ToDouble(Console.ReadLine());
            Console.Write("Numero de Rentas a Simular  : ");
            NumeroDeRentas = int.Parse(Console.ReadLine());
            Console.Write("Taza Fallo Equipo           : ");
            TazaFalloEquipo = Convert.ToDouble(Console.ReadLine());
            Console.Write("Taza Cancelación            : ");
            TazaCancelación = Convert.ToDouble(Console.ReadLine());
            Console.Write("Taza No Pago                : ");
            TazaNoPago = Convert.ToDouble(Console.ReadLine());

            TA = new TablaAmortización(tazaInteresAnual / periodosAnuales, periodosDelPlazo, costoPublico);
            int repeticiones = 1000;   // veces que se simula una renta
            NumeroSimulaciones = NumeroDeRentas * repeticiones;
            simulaciones = new Simulación[NumeroSimulaciones];

            /*
            Thread s0 = new Thread(new ThreadStart(grupo1));
            Thread s1 = new Thread(new ThreadStart(grupo2));
            Thread s2 = new Thread(new ThreadStart(grupo3));
            Thread s3 = new Thread(new ThreadStart(grupo4));
            Thread s4 = new Thread(new ThreadStart(grupo5));
            Thread s5 = new Thread(new ThreadStart(grupo6));
            Thread s6 = new Thread(new ThreadStart(grupo7));
            Thread s7 = new Thread(new ThreadStart(grupo8));
            Thread s8 = new Thread(new ThreadStart(grupo9));
            Thread s9 = new Thread(new ThreadStart(grupo10));

            s0.Start();
            s1.Start();
            s2.Start();
            s3.Start();
            s4.Start();
            s5.Start();
            s6.Start();
            s7.Start();
            s8.Start();
            s9.Start();

            s0.Join();
            s1.Join();
            s2.Join();
            s3.Join();
            s4.Join();
            s5.Join();
            s6.Join();
            s7.Join();
            s8.Join();
            s9.Join();
            */
            
            for (int s = 0; s < NumeroSimulaciones; s++)
            {
                simulaciones[s] = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                    TazaFalloEquipo, TazaCancelación, TazaNoPago);
                MeanIngresosTotales[0] += simulaciones[s].Result.IngresosTotales;
                MeanCostosFallasEquipo[0] += simulaciones[s].Result.CostosFallasEquipo;
                MeanFallasTotales[0] += simulaciones[s].Result.FallasTotales;
                MeanCancelaciones[0] += simulaciones[s].Result.CuantasCancelaciones;
                MeanNoPagos[0] += simulaciones[s].Result.CuantosNoPagos;
            }
            
            double MIT = 0;
            for (int i = 0; i < 10; i++) MIT += MeanIngresosTotales[i];
            double MCFE = 0;
            for (int i = 0; i < 10; i++) MCFE += MeanCostosFallasEquipo[i];
            double MFT = 0;
            for (int i = 0; i < 10; i++) MFT += MeanFallasTotales[i];
            double MC = 0;
            for (int i = 0; i < 10; i++) MC += MeanCancelaciones[i];
            double MNP = 0;
            for (int i = 0; i < 10; i++) MNP += MeanNoPagos[i];

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
