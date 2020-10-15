using System;
using System.Collections.Generic;
using System.Text;

namespace NesCore.Machine.CPU.Instructions
{
    public readonly struct Operand
    {
        public Operand(byte address, byte value)
        {
            Address16 = null;
            Address8 = address;
            Value = value;
            Offset = null;
            Values = null;
        }
        public Operand(ushort address, byte value)
        {
            Address16 = address;
            Address8 = null;
            Value = value;
            Offset = null;
            Values = null;

        }
        public Operand(byte address, byte offset, byte[] values)
        {
            Address16 = null;
            Address8 = address;
            Value = null;
            Offset = offset;
            Values = values;

        }
        public Operand(ushort address, byte offset, byte[] values)
        {
            Address16 = address;
            Address8 = null;
            Value = null;
            Offset = offset;
            Values = values;

        }
        public Operand(byte value)
        {
            Address16 = null;
            Address8 = null;
            Value = value;
            Offset = null;
            Values = null;
        }

        private ushort? Address16 { get; }
        private byte? Address8 { get; }
        public ushort Address => Address8 ?? Address16 ?? throw new Exception("No value found in operand");
        public byte? Value { get; }
        public byte[]? Values { get; }
        public byte? Offset { get; }
        public bool UseAddress => Address8 != null;
        public bool HasOffset => Offset != null;

    }
}
