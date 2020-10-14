using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NesCore.Machine.CPU
{
    public struct RAM
    {
        private byte[] _ram;

        public void Reset()
        {
            _ram = new byte[1024 * 2];
        }

        public void Init(byte[] program)
        {
            Buffer.BlockCopy(program, 0, _ram, 0, program.Length);
        }

        public byte this[int index]
        {
            get
            {
                return _ram[index];
            }
            set
            {
                _ram[index] = value;
            }
        }

        public byte[] this[int index, int end]
        {
            get
            {
                return _ram[index..end];
            }
        }

        public void Store(byte value, int start, int end = 0)
        {
            end = end == 0 ? start + 1 : end;

            for (int i = start; i < end; i++)
            {
                _ram[i] = value;
            }
        }
    }
}
