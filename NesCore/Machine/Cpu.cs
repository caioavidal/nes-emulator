using System;
using System.Collections.Generic;
using System.Text;

namespace NesCore.Machine
{
    /// <summary>
    /// The NES CPU core is based on the 6502 processor and runs at approximately 1.79 MHz (1.66 MHz in a PAL NES).
    /// </summary>
    public class Cpu
    {
        private byte A;
        private byte X;
        private byte Y;

        private ushort PC;
        private byte S;
        private byte P;

        private Bus _bus;

        public void ReadData(ushort address) => _bus.ReadData(address);
        public void WriteData(ushort address, byte data) => _bus.WriteData(address, data);

        public void ConnectToBus(Bus bus)
        {
            _bus = bus;
            //bus.AttachCpu(this);
        }
    }
}
