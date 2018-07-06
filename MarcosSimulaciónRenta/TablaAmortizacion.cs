using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace MarcosSimulaciónRenta
{
    public struct PeriodoAmortización
    {
        public int periodo;
        public double capitalInsoluto;
        public double amortización;
        public double interes;

        public PeriodoAmortización(int t, double C, double A, double I)
        {
            periodo = t;
            capitalInsoluto = C;
            amortización = A;
            interes = I;
        }
    }

    class TablaAmortización
    {
        PeriodoAmortización[] Tabla;
        double _TazaPeriodo;
        int _NumeroPeriodosDelPlazo;
        double _CapitalInicial;
        double _PagoPorPeriodo;

        public TablaAmortización(double TazaPeriodo, int NumeroPeriodosDelPlazo, double CapitalInicial)
        {
            _TazaPeriodo = TazaPeriodo;
            _NumeroPeriodosDelPlazo = NumeroPeriodosDelPlazo;
            _CapitalInicial = CapitalInicial;
            _PagoPorPeriodo = Financial.Pmt(_TazaPeriodo, _NumeroPeriodosDelPlazo, -_CapitalInicial, 0);
            Tabla = new PeriodoAmortización[NumeroPeriodosDelPlazo + 2];
            Tabla[0] = new PeriodoAmortización(0, _CapitalInicial, 0.0, 0.0);
            for (int t = 1; t <= _NumeroPeriodosDelPlazo + 1; t++)
            {
                double interes = Tabla[t - 1].capitalInsoluto * _TazaPeriodo;  // interés del periodo
                double amortización = _PagoPorPeriodo - interes;
                double nuevoCapitalInsoluto = Tabla[t - 1].capitalInsoluto - amortización;
                Tabla[t] = new PeriodoAmortización(t, nuevoCapitalInsoluto, amortización, interes);
            }
        }

        public double TazaPeriodo()
        {
            return _TazaPeriodo;
        }

        public int PeriodosDelPlazo()
        {
            return _NumeroPeriodosDelPlazo;
        }

        public double CapitalInicial()
        {
            return _CapitalInicial;
        }

        public double PagoPeriodico()
        {
            return _PagoPorPeriodo;
        }

        public PeriodoAmortización this[int indice]
        {
            get
            {
                return Tabla[indice];
            }

            set
            {
                Tabla[indice] = value;
            }
        }
    }
}
