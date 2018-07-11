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
        
        public Simulación[] simulaciones;
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
            simulaciones = new Simulación[NumeroDeSimulaciones];
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
                simulaciones[s] = new Simulación(TA, NumeroDeRentas, CostoDistribuidor, periodosDelPlazo,
                    TazaFalloEquipo, TazaCancelación, TazaNoPago);
                MeanIngresosTotales += simulaciones[s].Result.IngresosTotales;
                MeanCostosFallasEquipo += simulaciones[s].Result.CostosFallasEquipo;
                MeanFallasTotales += simulaciones[s].Result.FallasTotales;
                MeanCancelaciones += simulaciones[s].Result.CuantasCancelaciones;
                MeanNoPagos += simulaciones[s].Result.CuantosNoPagos;
            }
        }
    }
}

