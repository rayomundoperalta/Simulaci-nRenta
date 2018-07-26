using System;
using System.Collections.Generic;
using System.IO;
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
        string ArchivoSalida;
        

        public HiloDeSimulacion(string ArchivoSalida, int NumeroDeSimulaciones, int NumeroDeRentas, double CostoDistribuidor, int periodosDelPlazo,
            double TazaFalloEquipo, double TazaCancelación, double TazaNoPago, double tazaInteresAnual, int periodosAnuales, double costoPublico)
        {
            this.NumeroDeSimulaciones = NumeroDeSimulaciones;
            this.NumeroDeRentas = NumeroDeRentas;
            this.CostoDistribuidor = CostoDistribuidor;
            this.periodosDelPlazo = periodosDelPlazo;
            this.TazaFalloEquipo = TazaFalloEquipo;
            this.TazaCancelación = TazaCancelación;
            this.TazaNoPago = TazaNoPago;
            this.ArchivoSalida = ArchivoSalida;
            TA = TA = new TablaAmortización(tazaInteresAnual / periodosAnuales, periodosDelPlazo, costoPublico);
        }

        public void EjecutaHilo()
        {
            using (System.IO.Stream fileStream = new FileStream(@"C:\SimulacionesRentas\" + ArchivoSalida, FileMode.Create),
                bs = new BufferedStream(fileStream))
            {
                Console.WriteLine(ArchivoSalida + " INI");
                using (var file = new BinaryWriter(bs))
                {
                    for (int s = 0; s < (NumeroDeSimulaciones); s++)
                    {
                        simulacion.EjecutaSimulación(file, TA, NumeroDeRentas, CostoDistribuidor, periodosDelPlazo,
                            TazaFalloEquipo, TazaCancelación, TazaNoPago);
                        MeanIngresosTotales += simulacion.Result.IngresosTotales;
                        MeanCostosFallasEquipo += simulacion.Result.CostosFallasEquipo;
                        MeanFallasTotales += simulacion.Result.FallasTotales;
                        MeanCancelaciones += simulacion.Result.CuantasCancelaciones;
                        MeanNoPagos += simulacion.Result.CuantosNoPagos;
                    }
                }
                Console.WriteLine(ArchivoSalida + " FIN");
            }

            RentasData datos = new RentasData();
            byte[] arrBytes;

            using (System.IO.Stream fileStream = new FileStream(@"C:\SimulacionesRentas\" + ArchivoSalida, FileMode.Open),
                bs = new BufferedStream(fileStream))
            {
                using (var file = new BinaryReader(bs))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Int32 size = file.ReadInt32();
                        arrBytes = file.ReadBytes(size);
                        datos.Deserialize(ref arrBytes);
                        datos.PrintConsole(ArchivoSalida);
                    }
                   
                }
            }
            
        }
    }
}

