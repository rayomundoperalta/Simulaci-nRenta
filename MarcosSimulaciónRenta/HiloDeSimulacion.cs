using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcosSimulaciónRenta
{
    class HiloDeSimulacion
    {
        public double MeanIngresosTotales;
        public double MeanCostosFallasEquipo;
        public double MeanFallasTotales;
        public double MeanCancelaciones;
        public double MeanNoPagos;
        
        Simulación simulacion = new Simulación();
        TablaAmortización TA;
        int NumeroDeRentas;
        double CostoDistribuidor;
        int periodosDelPlazo;
        double TazaFalloEquipo;
        double TazaCancelación;
        double TazaNoPago;
        public int NumeroDeSimulaciones;
        

        public HiloDeSimulacion(int NumeroDeSimulaciones, int NumeroDeRentas, double CostoDistribuidor, int periodosDelPlazo,
            double TazaFalloEquipo, double TazaCancelación, double TazaNoPago, double tazaInteresAnual, int periodosAnuales, double costoPublico)
        {
            this.NumeroDeSimulaciones = NumeroDeSimulaciones;
            this.NumeroDeRentas = NumeroDeRentas;
            this.CostoDistribuidor = CostoDistribuidor;
            this.periodosDelPlazo = periodosDelPlazo;
            this.TazaFalloEquipo = TazaFalloEquipo;
            this.TazaCancelación = TazaCancelación;
            this.TazaNoPago = TazaNoPago;
            TA = TA = new TablaAmortización(tazaInteresAnual / periodosAnuales, periodosDelPlazo, costoPublico);
        }

        public void EjecutaHilo()
        {
            for (int s = 0; s < (NumeroDeSimulaciones); s++)
            {
                simulacion.EjecutaSimulación(TA, NumeroDeRentas, CostoDistribuidor, periodosDelPlazo,
                    TazaFalloEquipo, TazaCancelación, TazaNoPago);
                MeanIngresosTotales += simulacion.Result.IngresosTotales;
                MeanCostosFallasEquipo += simulacion.Result.CostosFallasEquipo;
                MeanFallasTotales += simulacion.Result.FallasTotales;
                MeanCancelaciones += simulacion.Result.CuantasCancelaciones;
                MeanNoPagos += simulacion.Result.CuantosNoPagos;
            }
        }
    }
}

