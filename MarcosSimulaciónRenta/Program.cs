using System;
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
            Console.Write("Costo al público: ");
            costoPublico = Convert.ToDouble(Console.ReadLine());
            Console.Write("Costo al Distribuidor: ");
            costoDistribuidor = Convert.ToDouble(Console.ReadLine());
            Console.Write("Periodos del Plazo: ");
            periodosDelPlazo = int.Parse(Console.ReadLine());
            Console.Write("Periodos por año: ");
            periodosAnuales = int.Parse(Console.ReadLine());
            Console.Write("Taza de Interés Anual: ");
            tazaInteresAnual = Convert.ToDouble(Console.ReadLine());
            Console.Write("Numero de Rentas a Simular: ");
            NumeroDeRentas = int.Parse(Console.ReadLine());
            //double pagoMensual = Financial.Pmt(tazaInteresAnual / periodosAnuales, periodosDelPlazo, -costoPublico, 0);
            //Console.WriteLine("Pago Mensual: " + pagoMensual);
            TablaAmortización TA = new TablaAmortización(tazaInteresAnual / periodosAnuales, periodosDelPlazo, costoPublico);
            /*
            for (int t = 0; t <= periodosDelPlazo; t++)
            {
                Console.WriteLine("{0,4}: {1,-6:N2}  {2,-6:N2}  {3,-6:N2}", TA[t].periodo, TA[t].capitalInsoluto, TA[t].amortización, TA[t].interes);
            }
            */

            int CuantasFallas = 0;
            int CuantasCancelaciones = 0;
            int CuantosNoPagos = 0;

            double CostoInicialEquipo = 0;
            double CostosFallasEquipo = 0;
            double IngresosTotales = 0;
            Renta[] rentas = new Renta[NumeroDeRentas];

            CostoInicialEquipo = NumeroDeRentas * costoDistribuidor; 
            for (int i = 0; i < NumeroDeRentas; i++)
            {
                rentas[i] = new Renta(periodosDelPlazo, 0.03, 0.02, 0.05);
                if (rentas[i]._FalloEquipo) CuantasFallas++;
                if (rentas[i]._CancelaciónContrato)
                {
                    CuantasCancelaciones++;
                    IngresosTotales += rentas[i]._PeriodoCancelación * TA.PagoPeriodico();
                }
                else
                {
                    if (rentas[i]._NoPago)
                    {
                        CuantosNoPagos++;
                        IngresosTotales += (rentas[i]._PeriodoNoPago - 1) * TA.PagoPeriodico();
                    }
                    else
                    {
                        IngresosTotales += periodosDelPlazo * TA.PagoPeriodico();
                    }
                }
                
            }

            CostosFallasEquipo = CuantasFallas * costoDistribuidor;

            Console.WriteLine("Pago Mensual de agua caliente : {0,-12:N2}", TA.PagoPeriodico());
            Console.WriteLine("Costo Inicial de los equipos  : {0,-12:N2}", CostoInicialEquipo);
            Console.WriteLine("Ingresos Totales              : {0,-12:N2}", IngresosTotales);
            Console.WriteLine("Costos por falla de equipos   : {0,-12:N2}", CostosFallasEquipo);
            Console.WriteLine("Numero de Fallas de equipos   : {0,6}", CuantasFallas);
            Console.WriteLine("Numero de Cancelaciones       : {0,6}", CuantasCancelaciones);
            Console.WriteLine("Numero de NoPagos             : {0,6}", CuantosNoPagos);

            /*
            Console.WriteLine("Fallo Cancelación Nopago");
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("{0,5} {1,11} {2,6}", rentas[i]._PeriodoFallo, rentas[i]._PeriodoCancelación, rentas[i]._PeriodoNoPago);
            }
            */

            Console.ReadKey();

        }
    }
}
