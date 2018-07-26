using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        public void  EjecutaSimulación (BinaryWriter file, TablaAmortización TA, int NumeroDeRentas, double costoDistribuidor, int periodosDelPlazo, double TazaFalloEquipo, double TazaCancelación, double TazaNoPago)
        {
            int CuantasFallas = 0;
            int CuantasCancelaciones = 0;
            int CuantosNoPagos = 0;

            double CostoInicialEquipo = 0;
            double CostosFallasEquipo = 0;
            double IngresosTotales = 0;
            Renta renta = new Renta();
            Random rand = new Random();

            CostoInicialEquipo = costoDistribuidor;
            RentasData datos = new RentasData();

            for (int i = 0; i < NumeroDeRentas; i++)
            {
                renta.SimulaRenta(periodosDelPlazo, TazaFalloEquipo, TazaCancelación, TazaNoPago, rand);
                datos.PagoPeriodico = TA.PagoPeriodico();
                datos.CostoInicialEquipo = CostoInicialEquipo;
                datos.FalloEquipo = renta._FalloEquipo;
                datos.CancelaciónContrato = renta._CancelaciónContrato;
                datos.PeriodoCancelación = renta._PeriodoCancelación;
                datos.NoPago = renta._NoPago;
                datos.PeriodoNoPago = renta._PeriodoNoPago;
                datos.Plazos = periodosDelPlazo;
                if (renta._FalloEquipo) CuantasFallas++;
                double PagoTotal = 0;
                if (renta._CancelaciónContrato)
                {
                    CuantasCancelaciones++;
                    PagoTotal = renta._PeriodoCancelación * TA.PagoPeriodico();
                    IngresosTotales += PagoTotal;
                    datos.PagoTotal = PagoTotal;
                    //Console.WriteLine(PagoTotal);
                }
                else
                {
                    if (renta._NoPago)
                    {
                        CuantosNoPagos++;
                        PagoTotal = (renta._PeriodoNoPago - 1) * TA.PagoPeriodico();
                        IngresosTotales += PagoTotal;
                        datos.PagoTotal = PagoTotal;
                        //Console.WriteLine(PagoTotal);
                    }
                    else
                    {
                        PagoTotal = periodosDelPlazo * TA.PagoPeriodico();
                        IngresosTotales += PagoTotal;
                        datos.PagoTotal = PagoTotal;
                        //Console.WriteLine(PagoTotal);
                    }
                }
                byte[] byteArr = datos.Serialize();
                Int32 sizeOfarrByte = byteArr.Length;
                file.Write(sizeOfarrByte);
                file.Write(byteArr);
            }
            CostosFallasEquipo = CuantasFallas * costoDistribuidor;
            Result.PagoMensual = TA.PagoPeriodico();
            Result.CostoInicialEquipo = costoDistribuidor * NumeroDeRentas;
            Result.IngresosTotales = IngresosTotales;
            Result.CostosFallasEquipo = CostosFallasEquipo;
            Result.FallasTotales = CuantasFallas;
            Result.CuantasCancelaciones = CuantasCancelaciones;
            Result.CuantosNoPagos = CuantosNoPagos;
            
        }
    }
}
