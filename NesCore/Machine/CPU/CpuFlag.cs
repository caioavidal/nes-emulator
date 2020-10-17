using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NesCore.Machine.CPU
{
    public struct CpuFlag
    {
        public byte Status { get; private set; }

        public void SetNegativeFlagIf7ThBitIsSet(byte value)
        {
            if ((value >> 7 & 1) == 1)
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

        public void SetNewStatus(byte status) => Status = status;

        public void SetFlag(CpuStatusFlag flag, byte value)
        {
            if (value == 1)
                SetFlag(flag);
            else
                ClearFlag(flag);
        }

        public byte GetFlag(CpuStatusFlag flag)
        {
            return (byte)(Status & (byte)flag);
        }

        public byte this[CpuStatusFlag flag]
        {
            get
            {
                return GetFlag(flag);
            }
        }
    }
}
