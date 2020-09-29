using NesCore.Machine;
using System;
using System.Collections.Generic;
using System.Text;

namespace NesCore.Machine
{
    public class Bus
    {
        private Cpu _cpu;

        private byte[] Addresses = new byte[64*1024];

        public void AttachCpu(Cpu cpu)
        {
            _cpu = cpu;
        }

        
        //public void WriteAddress(ushort address, byte data)
        //{
        //    Addresses[address] = data;
        //}
        public byte ReadData(ushort address) => Addresses[address];
        
        public void WriteData(ushort address, byte data)
        {
            Addresses[address] = data;
        }
    }
}
