using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcosSimulaciónRenta
{
    public struct ResultadoSimulación
    {
        public double PagoMensual;
        public double CostoInicialEquipo;
        public double IngresosTotales;
        public double CostosFallasEquipo;
        public int FallasTotales;
        public int CuantasCancelaciones;
        public int CuantosNoPagos;

        public ResultadoSimulación(double PM, double CIE, double IT, double CFE, int FT, int CC, int CNP)
        {
            PagoMensual = PM;
            CostoInicialEquipo = CIE;
            IngresosTotales = IT;
            CostosFallasEquipo = CFE;
            FallasTotales = FT;
            CuantasCancelaciones = CC;
            CuantosNoPagos = CNP;
        }
    }

    class Simulación
    {
        public ResultadoSimulación Result;

        public void  EjecutaSimulación (TablaAmortización TA, int NumeroDeRentas, double costoDistribuidor, int periodosDelPlazo, double TazaFalloEquipo, double TazaCancelación, double TazaNoPago)
        {
            int CuantasFallas = 0;
            int CuantasCancelaciones = 0;
            int CuantosNoPagos = 0;

            double CostoInicialEquipo = 0;
            double CostosFallasEquipo = 0;
            double IngresosTotales = 0;
            Renta renta = new Renta();
            Random rand = new Random();

            CostoInicialEquipo = NumeroDeRentas * costoDistribuidor;
            for (int i = 0; i < NumeroDeRentas; i++)
            {
                renta.SimulaRenta(periodosDelPlazo, TazaFalloEquipo, TazaCancelación, TazaNoPago, rand);
                if (renta._FalloEquipo) CuantasFallas++;
                if (renta._CancelaciónContrato)
                {
                    CuantasCancelaciones++;
                    IngresosTotales += renta._PeriodoCancelación * TA.PagoPeriodico();
                }
                else
                {
                    if (renta._NoPago)
                    {
                        CuantosNoPagos++;
                        IngresosTotales += (renta._PeriodoNoPago - 1) * TA.PagoPeriodico();
                    }
                    else
                    {
                        IngresosTotales += periodosDelPlazo * TA.PagoPeriodico();
                    }
                }
            }
            CostosFallasEquipo = CuantasFallas * costoDistribuidor;
            Result.PagoMensual = TA.PagoPeriodico();
            Result.CostoInicialEquipo = CostoInicialEquipo;
            Result.IngresosTotales = IngresosTotales;
            Result.CostosFallasEquipo = CostosFallasEquipo;
            Result.FallasTotales = CuantasFallas;
            Result.CuantasCancelaciones = CuantasCancelaciones;
            Result.CuantosNoPagos = CuantosNoPagos;
        }
    }
}
