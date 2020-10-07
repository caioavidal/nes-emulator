using System;
using System.Collections.Generic;
using System.Text;

namespace NesCore.Machine.CPU
{
    public enum CpuStatusFlag: byte
    {
        /// <summary>
        /// Carry Flag
        /// </summary>
        C = 1 << 0,
        /// <summary>
        /// Zero Flag
        /// </summary>
        Z = 1 << 1,
        /// <summary>
        /// Interrupt Flag
        /// </summary>
        I = 1 << 2,
        /// <summary>
        /// Decimal Flag
        /// </summary>
        D = 1 << 3,
        /// <summary>
        /// Break Flag
        /// </summary>
        B = 1 << 4,
        /// <summary>
        /// Always 1 Flag
        /// </summary>
        One = 1 << 5,
        /// <summary>
        /// Overflow Flag
        /// </summary>
        V = 1 << 6,
        /// <summary>
        /// Negative Flag
        /// </summary>
        N = 1 << 7

    }
}
