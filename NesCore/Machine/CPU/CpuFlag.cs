using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NesCore.Machine.CPU
{
    public struct CpuFlag
    {
        public byte Status { get; private set; }

        public void SetNegativeFlagToValueOf7ThBit(byte value)
        {
            if ((value >> 7 & 1) == 0)
                ClearFlag(CpuStatusFlag.N);
            else
                SetFlag(CpuStatusFlag.N);
        }

        public void SetZeroFlagIfEqualsToZero(byte value)
        {
            if (value == 0)
                SetFlag(CpuStatusFlag.Z);
            else
                ClearFlag(CpuStatusFlag.Z);
        }

        public void ClearFlag(CpuStatusFlag flag) => Status &= (byte)~flag;
        public void SetFlag(CpuStatusFlag flag) => Status |= (byte)flag;
    }
}
