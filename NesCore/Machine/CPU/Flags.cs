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
        /// 
        /// </summary>
        I = 1 << 2,
        D = 1 << 3,
        /// <summary>
        /// Break Command
        /// </summary>
        B = 1 << 4,
        One = 1 << 5,
        V = 1 << 6,
        N = 1 << 7

    }
}
