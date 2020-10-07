using System;
using System.Collections.Generic;
using System.Text;

namespace NesCore.Machine.CPU
{
    public struct Registers
    {
        public byte Accumulator;
        public byte X; // index register
        public byte Y; // index register

        /// <summary>
        /// Status Register
        /// </summary>
        public CpuFlag P; 
        
        /// <summary>
        /// Program Counter
        /// </summary>
        public ushort PC; 
        /// <summary>
        /// Stack Pointer
        /// </summary>
        public ushort SP;

        public void Reset()
        {
            SP = 0x00;
        }

        public void BitShiftLeftAccumulator() => Accumulator <<= 1;

    }
}
