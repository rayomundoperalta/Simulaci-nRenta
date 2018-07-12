using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcosSimulaciónRenta
{
    class Renta
    {
        /* Vamos a suponer que a una renta solo le ocurre cuando mucho un evento cada vez, y que  pueden
         * ocurrir todos los eventos en una sola renta
         */

        public bool _FalloEquipo = false;
        public int _PeriodoFallo = 0;
        public bool _CancelaciónContrato = false;
        public int _PeriodoCancelación = 0;
        public bool _NoPago = false;
        public int _PeriodoNoPago = 0;
        
        public void SimulaRenta(int periodos, double tazaDeFalloEquipo, double tazaCancelación, double tazaNoPago, Random rand)
        {
            if (rand.NextDouble() < tazaDeFalloEquipo)
            {
                _FalloEquipo = true;
                _PeriodoFallo = rand.Next(1, periodos);
            }
            /* Esto evidentemente no es cierto, es solo para empezar */
            if (rand.NextDouble() < tazaCancelación)
            {
                _CancelaciónContrato = true;
                _PeriodoCancelación = rand.Next(1, periodos);
            }
            else
            {
                if (rand.NextDouble() < tazaNoPago)
                {
                    _NoPago = true;
                    _PeriodoNoPago = rand.Next(1, periodos);
                }
            }
            if (_FalloEquipo)
            {
                if (_CancelaciónContrato)
                {
                    if (_PeriodoCancelación == 1)
                    {
                        _PeriodoFallo = 1;
                        _PeriodoCancelación = 2;
                    }
                    else
                    {
                        _PeriodoFallo = _PeriodoCancelación - 1;
                    }
                }
                if (_NoPago)
                {
                    if (_PeriodoNoPago == 1)
                    {
                        _PeriodoFallo = 1;
                        _PeriodoNoPago = 2;
                    }
                    else
                    {
                        _PeriodoFallo = _PeriodoNoPago - 1;
                    }
                }
            }
        }
    }
}
