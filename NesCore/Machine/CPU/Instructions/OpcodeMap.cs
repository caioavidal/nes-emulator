using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace NesCore.Machine.CPU.Instructions
{
    public class OpcodeMap
    {
        private static IDictionary<byte, Opcode> _opcodes = new Dictionary<byte, Opcode>()
        {
            { 0x69, new Opcode(0x69, 2, 2, AddressingModes.All[AddressingModeName.Immediate], "ADC") },

            { 0xA9, new Opcode(0xA9, 2, 2, AddressingModes.All[AddressingModeName.Immediate], "LDA") },
            { 0xA9, new Opcode(0xA5, 2, 3, AddressingModes.All[AddressingModeName.ZeroPage], "LDA") },
            { 0xA9, new Opcode(0xB5, 2, 4, AddressingModes.All[AddressingModeName.ZeroPageX], "LDA") },
            { 0xA9, new Opcode(0xAD, 3, 4, AddressingModes.All[AddressingModeName.Absolute], "LDA") },
            { 0xA9, new Opcode(0xBD, 3, 4, 1, AddressingModes.All[AddressingModeName.AbsoluteX], "LDA") },
            { 0xA9, new Opcode(0xB9, 3, 4, 1, AddressingModes.All[AddressingModeName.AbsoluteY], "LDA") },
            { 0xA9, new Opcode(0xA1, 2, 6, AddressingModes.All[AddressingModeName.IndexedIndirectX], "LDA") },
            { 0xA9, new Opcode(0xB1, 2, 5, 1, AddressingModes.All[AddressingModeName.IndexedIndirectY], "LDA") },

            { 0xA9, new Opcode(0xA2, 2, 2, AddressingModes.All[AddressingModeName.Immediate], "LDX") },
            { 0xA9, new Opcode(0xA6, 2, 3, AddressingModes.All[AddressingModeName.ZeroPage], "LDX") },
            { 0xA9, new Opcode(0xB6, 2, 4, AddressingModes.All[AddressingModeName.ZeroPageY], "LDX") },
            { 0xA9, new Opcode(0xAE, 3, 4, AddressingModes.All[AddressingModeName.Absolute], "LDX") },
            { 0xA9, new Opcode(0xBE, 3, 4, 1, AddressingModes.All[AddressingModeName.AbsoluteY], "LDX") },

            { 0xA9, new Opcode(0xA0, 2, 2, AddressingModes.All[AddressingModeName.Immediate], "LDY") },
            { 0xA9, new Opcode(0xA4, 2, 3, AddressingModes.All[AddressingModeName.ZeroPage], "LDY") },
            { 0xA9, new Opcode(0xB4, 2, 4, AddressingModes.All[AddressingModeName.ZeroPageX], "LDY") },
            { 0xA9, new Opcode(0xAC, 3, 4, AddressingModes.All[AddressingModeName.Absolute], "LDY") },
            { 0xA9, new Opcode(0xBC, 3, 4, 1, AddressingModes.All[AddressingModeName.AbsoluteX], "LDY") },

            { 0xA9, new Opcode(0x4A, 1, 2, AddressingModes.All[AddressingModeName.Accumulator], "LSR") },
            { 0xA9, new Opcode(0x46, 2, 5, AddressingModes.All[AddressingModeName.ZeroPage], "LSR") },
            { 0xA9, new Opcode(0x56, 2, 6, AddressingModes.All[AddressingModeName.ZeroPageX], "LSR") },
            { 0xA9, new Opcode(0x4E, 3, 6, AddressingModes.All[AddressingModeName.Absolute], "LSR") },
            { 0xA9, new Opcode(0x5E, 3, 7, AddressingModes.All[AddressingModeName.AbsoluteX], "LSR") },

            { 0xA9, new Opcode(0xEA, 1, 2, AddressingModes.All[AddressingModeName.Implied], "NOP") },

            { 0xA9, new Opcode(0x09, 2, 2, AddressingModes.All[AddressingModeName.Immediate], "ORA") },
            { 0xA9, new Opcode(0x05, 2, 3, AddressingModes.All[AddressingModeName.ZeroPage], "ORA") },
            { 0xA9, new Opcode(0x15, 2, 4, AddressingModes.All[AddressingModeName.ZeroPageX], "ORA") },
            { 0xA9, new Opcode(0x0D, 3, 4, AddressingModes.All[AddressingModeName.Absolute], "ORA") },
            { 0xA9, new Opcode(0x1D, 3, 4, 1, AddressingModes.All[AddressingModeName.AbsoluteX], "ORA") },
            { 0xA9, new Opcode(0x19, 3, 4, 1, AddressingModes.All[AddressingModeName.AbsoluteY], "ORA") },
            { 0xA9, new Opcode(0x01, 2, 6, AddressingModes.All[AddressingModeName.IndexedIndirectX], "ORA") },
            { 0xA9, new Opcode(0x11, 2, 5, 1, AddressingModes.All[AddressingModeName.IndexedIndirectY], "ORA") },

            { 0xA9, new Opcode(0x48, 1, 3, AddressingModes.All[AddressingModeName.Implied], "PHA") },
            { 0xA9, new Opcode(0x08, 1, 3, AddressingModes.All[AddressingModeName.Implied], "PHP") },
            { 0xA9, new Opcode(0x68, 1, 4, AddressingModes.All[AddressingModeName.Implied], "PLA") },
            { 0xA9, new Opcode(0x28, 1, 4, AddressingModes.All[AddressingModeName.Implied], "PLP") },

            { 0xA9, new Opcode(0x2A, 1, 2, AddressingModes.All[AddressingModeName.Accumulator], "ROL") },
            { 0xA9, new Opcode(0x26, 2, 5, AddressingModes.All[AddressingModeName.ZeroPage], "ROL") },
            { 0xA9, new Opcode(0x36, 2, 6, AddressingModes.All[AddressingModeName.ZeroPageX], "ROL") },
            { 0xA9, new Opcode(0x2E, 3, 6, AddressingModes.All[AddressingModeName.Absolute], "ROL") },
            { 0xA9, new Opcode(0x3E, 3, 7, AddressingModes.All[AddressingModeName.AbsoluteX], "ROL") },

            { 0xA9, new Opcode(0x6A, 1, 2, AddressingModes.All[AddressingModeName.Accumulator], "ROR") },
            { 0xA9, new Opcode(0x66, 2, 5, AddressingModes.All[AddressingModeName.ZeroPage], "ROR") },
            { 0xA9, new Opcode(0x76, 2, 6, AddressingModes.All[AddressingModeName.ZeroPageX], "ROR") },
            { 0xA9, new Opcode(0x6E, 3, 6, AddressingModes.All[AddressingModeName.Absolute], "ROR") },
            { 0xA9, new Opcode(0x7E, 3, 7, AddressingModes.All[AddressingModeName.AbsoluteX], "ROR") },

            { 0xA9, new Opcode(0x40, 1, 6, AddressingModes.All[AddressingModeName.Implied], "RTI") },
            { 0xA9, new Opcode(0x60, 1, 6, AddressingModes.All[AddressingModeName.Implied], "RTS") },


            { 0xA9, new Opcode(0xE9, 2, 2, AddressingModes.All[AddressingModeName.Immediate], "SBC") },
            { 0xA9, new Opcode(0xE5, 2, 3, AddressingModes.All[AddressingModeName.ZeroPage], "SBC") },
            { 0xA9, new Opcode(0xF5, 2, 4, AddressingModes.All[AddressingModeName.ZeroPageX], "SBC") },
            { 0xA9, new Opcode(0xED, 3, 4, AddressingModes.All[AddressingModeName.Absolute], "SBC") },
            { 0xA9, new Opcode(0xFD, 3, 4, 1, AddressingModes.All[AddressingModeName.AbsoluteX], "SBC") },
            { 0xA9, new Opcode(0xF9, 3, 4, 1, AddressingModes.All[AddressingModeName.AbsoluteY], "SBC") },
            { 0xA9, new Opcode(0xE1, 2, 6, AddressingModes.All[AddressingModeName.IndexedIndirectX], "SBC") },
            { 0xA9, new Opcode(0xF1, 2, 5, 1, AddressingModes.All[AddressingModeName.IndexedIndirectY], "SBC") },

            { 0xA9, new Opcode(0x38, 1, 2, AddressingModes.All[AddressingModeName.Implied], "SEC") },
            { 0xA9, new Opcode(0xF8, 1, 2, AddressingModes.All[AddressingModeName.Implied], "SED") },
            { 0xA9, new Opcode(0x78, 1, 2, AddressingModes.All[AddressingModeName.Implied], "SEI") },

            { 0x85, new Opcode(0x85, 2, 3, AddressingModes.All[AddressingModeName.ZeroPage], "STA") },
            { 0x95, new Opcode(0x95, 2, 4, AddressingModes.All[AddressingModeName.ZeroPageX], "STA") },
            { 0x8D, new Opcode(0x8D, 3, 4, AddressingModes.All[AddressingModeName.Absolute], "STA") },
            { 0x9D, new Opcode(0x9D, 3, 5, AddressingModes.All[AddressingModeName.AbsoluteX], "STA") },
            { 0x99, new Opcode(0x99, 3, 5, AddressingModes.All[AddressingModeName.AbsoluteY], "STA") },
            { 0x81, new Opcode(0x81, 3, 6, AddressingModes.All[AddressingModeName.IndexedIndirectX], "STA") },
            { 0x91, new Opcode(0x91, 3, 6, AddressingModes.All[AddressingModeName.IndexedIndirectY], "STA") },

            { 0x85, new Opcode(0x86, 2, 3, AddressingModes.All[AddressingModeName.ZeroPage], "STX") },
            { 0x95, new Opcode(0x96, 2, 4, AddressingModes.All[AddressingModeName.ZeroPageY], "STX") },
            { 0x8D, new Opcode(0x8E, 3, 4, AddressingModes.All[AddressingModeName.Absolute], "STX") },

            { 0x85, new Opcode(0x84, 2, 3, AddressingModes.All[AddressingModeName.ZeroPage], "STY") },
            { 0x95, new Opcode(0x94, 2, 4, AddressingModes.All[AddressingModeName.ZeroPageX], "STY") },
            { 0x8D, new Opcode(0x8C, 3, 4, AddressingModes.All[AddressingModeName.Absolute], "STY") },

            { 0x8D, new Opcode(0xAA, 1, 2, AddressingModes.All[AddressingModeName.Implied], "TAX") },
            { 0x8D, new Opcode(0xA8, 1, 2, AddressingModes.All[AddressingModeName.Implied], "TAY") },
            { 0x8D, new Opcode(0xBA, 1, 2, AddressingModes.All[AddressingModeName.Implied], "TSX") },
            { 0x8D, new Opcode(0x8A, 1, 2, AddressingModes.All[AddressingModeName.Implied], "TXA") },
            { 0x8D, new Opcode(0x9A, 1, 2, AddressingModes.All[AddressingModeName.Implied], "TXS") },
            { 0x8D, new Opcode(0x98, 1, 2, AddressingModes.All[AddressingModeName.Implied], "TYA") },



        };

        public static Opcode GetOpcode(byte opcode)
        {
            return _opcodes[opcode];
        }

    }

    public class Disassembler
    {
        private readonly Cpu _cpu;

        public Disassembler(Cpu cpu)
        {
            _cpu = cpu;
        }
        public Tuple<MethodInfo, object[]> Decode(byte[] bytes)
        {
            var opcode = OpcodeMap.GetOpcode(bytes.FirstOrDefault());

            object operand = 0;


            if (bytes.Length == 2) //zero page
            {

                operand = bytes.LastOrDefault();
            }
            if (bytes.Length == 3)
            {
                operand = BitConverter.ToUInt16(bytes[1..3]);
            }


            var addresses = new object[2];
            switch (opcode.AddressingMode.Type)
            {
                case AddressingModeType.NonIndexed:

                    var address = _cpu.GetType().GetMethod(opcode.AddressingMode.Name.ToString()).Invoke(_cpu, new object[] { operand });
                    addresses[0] = address;
                    break;

                case AddressingModeType.Indexed:
                    address = _cpu.GetType().GetMethod(opcode.AddressingMode.Name.ToString()).Invoke(_cpu, new object[] { operand });
                    addresses[0] = address;
                    break;

                default:
                    break;
            }

            var instruction = _cpu.GetType().GetMethod(opcode.Instruction);

            return new Tuple<MethodInfo, object[]>(instruction, addresses);

            //opcode.Instruction
        }
    }
}
