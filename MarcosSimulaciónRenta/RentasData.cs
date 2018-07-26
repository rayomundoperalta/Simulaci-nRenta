using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace MarcosSimulaciónRenta
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct RentasData
    {
        public double PagoPeriodico;
        public double CostoInicialEquipo;
        public bool FalloEquipo;
        public bool CancelaciónContrato;
        public Int32 PeriodoCancelación;
        public bool NoPago;
        public Int32 PeriodoNoPago;
        public Int32 Plazos;
        public double PagoTotal;

        public Byte[] Serialize()
        {
            Int32 iSizeOfMyDataStruct = Marshal.SizeOf(typeof(RentasData));
            byte[] byteArrayRentasData = new byte[iSizeOfMyDataStruct];
            GCHandle gch = GCHandle.Alloc(byteArrayRentasData, GCHandleType.Pinned);
            IntPtr pbyteArrayRentasData = gch.AddrOfPinnedObject();
            Marshal.StructureToPtr(this, pbyteArrayRentasData, false);
            gch.Free();
            return byteArrayRentasData;
        }

        public void Deserialize(ref byte[] byteSerializedData)
        {
            GCHandle gch = GCHandle.Alloc(byteSerializedData, GCHandleType.Pinned);
            IntPtr pbyteSerializedData = gch.AddrOfPinnedObject();
            this = (RentasData)Marshal.PtrToStructure(pbyteSerializedData, typeof(RentasData));
            gch.Free();
        }

        public void PrintConsole(string ArchivoSalida)
        {
            Console.WriteLine(ArchivoSalida + "  " + PagoPeriodico + ", " + CostoInicialEquipo + ", " + FalloEquipo + ", " +
                CancelaciónContrato + ", " + PeriodoCancelación + ", " + NoPago + ", " +
                PeriodoNoPago + ", " + Plazos + ", " + PagoTotal);
        }
    }

    /*
    [Serializable]
    class ProcRentasData
    {
        static BinaryFormatter bf = new BinaryFormatter();
        static MemoryStream memStream = new MemoryStream();
        static BinaryFormatter binForm = new BinaryFormatter();

        public byte[] RentasDataToByteArray(MemoryStream ms, RentasData renta)
        {
            bf.Serialize(ms, renta);
            return ms.ToArray();
        }

        public RentasData ByteArrayToRentasData(byte[] arrBytes)
        {
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            return (RentasData)binForm.Deserialize(memStream);
        }

        public void PrintConsole(string ArchivoSalida, RentasData rentas)
        {
            Console.WriteLine(ArchivoSalida + "  " + rentas.PagoPeriodico + ", " + rentas.CostoInicialEquipo + ", " + rentas.FalloEquipo + ", " +
                rentas.CancelaciónContrato + ", " + rentas.PeriodoCancelación + ", " + rentas.NoPago + ", " +
                rentas.PeriodoNoPago + ", " + rentas.Plazos + ", " + rentas.PagoTotal);
        }
    }
    */

}
