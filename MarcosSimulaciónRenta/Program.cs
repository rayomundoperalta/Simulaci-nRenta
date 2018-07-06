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
            double TazaFalloEquipo = 0;
            double TazaCancelación = 0;
            double TazaNoPago = 0;

            Console.Write("Costo al público          : ");
            costoPublico = Convert.ToDouble(Console.ReadLine());
            Console.Write("Costo al Distribuidor     : ");
            costoDistribuidor = Convert.ToDouble(Console.ReadLine());
            Console.Write("Periodos por año          : ");
            periodosAnuales = int.Parse(Console.ReadLine());
            Console.Write("Periodos del Plazo        : ");
            periodosDelPlazo = int.Parse(Console.ReadLine());
            Console.Write("Taza de Interés Anual     : ");
            tazaInteresAnual = Convert.ToDouble(Console.ReadLine());
            Console.Write("Numero de Rentas a Simular: ");
            NumeroDeRentas = int.Parse(Console.ReadLine());
            Console.Write("Taza Fallo Equipo         : ");
            TazaFalloEquipo = Convert.ToDouble(Console.ReadLine());
            Console.Write("Taza Cancelación          : ");
            TazaCancelación = Convert.ToDouble(Console.ReadLine());
            Console.Write("Taza No Pago              : ");
            TazaNoPago = Convert.ToDouble(Console.ReadLine());

            TablaAmortización TA = new TablaAmortización(tazaInteresAnual / periodosAnuales, periodosDelPlazo, costoPublico);
            Simulación sim = new Simulación(TA, NumeroDeRentas, costoDistribuidor, periodosDelPlazo,
                TazaFalloEquipo, TazaCancelación, TazaNoPago);

            Console.WriteLine("Pago Mensual de agua caliente : {0,-12:N2}", sim.Result.PagoMensual);
            Console.WriteLine("Costo Inicial de los equipos  : {0,-12:N2}", sim.Result.CostoInicialEquipo);
            Console.WriteLine("Ingresos Totales              : {0,-12:N2}", sim.Result.IngresosTotales);
            Console.WriteLine("Costos por falla de equipos   : {0,-12:N2}", sim.Result.CostosFallasEquipo);
            Console.WriteLine("Numero de Fallas de equipos   : {0,6}", sim.Result.FallasTotales);
            Console.WriteLine("Numero de Cancelaciones       : {0,6}", sim.Result.CuantasCancelaciones);
            Console.WriteLine("Numero de NoPagos             : {0,6}", sim.Result.CuantosNoPagos);

            Console.ReadKey();

        }
    }
}
