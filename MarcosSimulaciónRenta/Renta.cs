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
        
        public Renta(int periodos, double tazaDeFalloEquipo, double tazaCancelación, double tazaNoPago)
        {
            if (RandomSingleton.NextDouble() < tazaDeFalloEquipo)
            {
                _FalloEquipo = true;
                _PeriodoFallo = RandomSingleton.Next(1, periodos);
            }
            /* Esto evidentemente no es cierto, es solo para empezar */
            if (RandomSingleton.NextDouble() < tazaCancelación)
            {
                _CancelaciónContrato = true;
                _PeriodoCancelación = RandomSingleton.Next(1, periodos);
            }
            else
            {
                if (RandomSingleton.NextDouble() < tazaNoPago)
                {
                    _NoPago = true;
                    _PeriodoNoPago = RandomSingleton.Next(1, periodos);
                }
            }
            if (_FalloEquipo)
            {
                if (_CancelaciónContrato)
                {
                    while (_PeriodoFallo >= _PeriodoCancelación)
                    {
                        _PeriodoFallo = RandomSingleton.Next(1, periodos);
                    }
                }
                if (_NoPago)
                {
                    while (_PeriodoFallo >= _PeriodoNoPago)
                    {
                        _PeriodoFallo = RandomSingleton.Next(1, periodos);
                    }
                }
            }
        }
    }
}
