using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace NesCore.Machine.CPU.Instructions
{
    public static class OpcodeMap
    {
        private static ImmutableDictionary<byte, Opcode> _opcodes = new Dictionary<byte, Opcode>()
        {
            { 0x69, new Opcode(0x69, 2, 2, AddressingModeName.Immediate, "ADC") },

            { 0xA9, new Opcode(0xA9, 2, 2, AddressingModeName.Immediate, "LDA") },
            { 0xA5, new Opcode(0xA5, 2, 3, AddressingModeName.ZeroPage, "LDA") },
            { 0xB5, new Opcode(0xB5, 2, 4, AddressingModeName.ZeroPageX, "LDA") },
            { 0xAD, new Opcode(0xAD, 3, 4, AddressingModeName.Absolute, "LDA") },
            { 0xBD, new Opcode(0xBD, 3, 4, 1, AddressingModeName.AbsoluteX, "LDA") },
            { 0xB9, new Opcode(0xB9, 3, 4, 1, AddressingModeName.AbsoluteY, "LDA") },
            { 0xA1, new Opcode(0xA1, 2, 6, AddressingModeName.IndexedIndirectX, "LDA") },
            { 0xB1, new Opcode(0xB1, 2, 5, 1, AddressingModeName.IndexedIndirectY, "LDA") },

            { 0xA2, new Opcode(0xA2, 2, 2, AddressingModeName.Immediate, "LDX") },
            { 0xA6, new Opcode(0xA6, 2, 3, AddressingModeName.ZeroPage, "LDX") },
            { 0xB6, new Opcode(0xB6, 2, 4, AddressingModeName.ZeroPageY, "LDX") },
            { 0xAE, new Opcode(0xAE, 3, 4, AddressingModeName.Absolute, "LDX") },
            { 0xBE, new Opcode(0xBE, 3, 4, 1, AddressingModeName.AbsoluteY, "LDX") },

            { 0xA0, new Opcode(0xA0, 2, 2, AddressingModeName.Immediate, "LDY") },
            { 0xA4, new Opcode(0xA4, 2, 3, AddressingModeName.ZeroPage, "LDY") },
            { 0xB4, new Opcode(0xB4, 2, 4, AddressingModeName.ZeroPageX, "LDY") },
            { 0xAC, new Opcode(0xAC, 3, 4, AddressingModeName.Absolute, "LDY") },
            { 0xBC, new Opcode(0xBC, 3, 4, 1, AddressingModeName.AbsoluteX, "LDY") },

            { 0x4A, new Opcode(0x4A, 1, 2, AddressingModeName.Accumulator, "LSR") },
            { 0x46, new Opcode(0x46, 2, 5, AddressingModeName.ZeroPage, "LSR") },
            { 0x56, new Opcode(0x56, 2, 6, AddressingModeName.ZeroPageX, "LSR") },
            { 0x4E, new Opcode(0x4E, 3, 6, AddressingModeName.Absolute, "LSR") },
            { 0x5E, new Opcode(0x5E, 3, 7, AddressingModeName.AbsoluteX, "LSR") },

            { 0xEA, new Opcode(0xEA, 1, 2, AddressingModeName.Implied, "NOP") },

            { 0x09, new Opcode(0x09, 2, 2, AddressingModeName.Immediate, "ORA") },
            { 0x05, new Opcode(0x05, 2, 3, AddressingModeName.ZeroPage, "ORA") },
            { 0x15, new Opcode(0x15, 2, 4, AddressingModeName.ZeroPageX, "ORA") },
            { 0x0D, new Opcode(0x0D, 3, 4, AddressingModeName.Absolute, "ORA") },
            { 0x1D, new Opcode(0x1D, 3, 4, 1, AddressingModeName.AbsoluteX, "ORA") },
            { 0x19, new Opcode(0x19, 3, 4, 1, AddressingModeName.AbsoluteY, "ORA") },
            { 0x01, new Opcode(0x01, 2, 6, AddressingModeName.IndexedIndirectX, "ORA") },
            { 0x11, new Opcode(0x11, 2, 5, 1, AddressingModeName.IndexedIndirectY, "ORA") },

            { 0x48, new Opcode(0x48, 1, 3, AddressingModeName.Implied, "PHA") },
            { 0x08, new Opcode(0x08, 1, 3, AddressingModeName.Implied, "PHP") },
            { 0x68, new Opcode(0x68, 1, 4, AddressingModeName.Implied, "PLA") },
            { 0x28, new Opcode(0x28, 1, 4, AddressingModeName.Implied, "PLP") },

            { 0x2A, new Opcode(0x2A, 1, 2, AddressingModeName.Accumulator, "ROL") },
            { 0x26, new Opcode(0x26, 2, 5, AddressingModeName.ZeroPage, "ROL") },
            { 0x36, new Opcode(0x36, 2, 6, AddressingModeName.ZeroPageX, "ROL") },
            { 0x2E, new Opcode(0x2E, 3, 6, AddressingModeName.Absolute, "ROL") },
            { 0x3E, new Opcode(0x3E, 3, 7, AddressingModeName.AbsoluteX, "ROL") },

            { 0x6A, new Opcode(0x6A, 1, 2, AddressingModeName.Accumulator, "ROR") },
            { 0x66, new Opcode(0x66, 2, 5, AddressingModeName.ZeroPage, "ROR") },
            { 0x76, new Opcode(0x76, 2, 6, AddressingModeName.ZeroPageX, "ROR") },
            { 0x6E, new Opcode(0x6E, 3, 6, AddressingModeName.Absolute, "ROR") },
            { 0x7E, new Opcode(0x7E, 3, 7, AddressingModeName.AbsoluteX, "ROR") },

            { 0x40, new Opcode(0x40, 1, 6, AddressingModeName.Implied, "RTI") },
            { 0x60, new Opcode(0x60, 1, 6, AddressingModeName.Implied, "RTS") },


            { 0xE9, new Opcode(0xE9, 2, 2, AddressingModeName.Immediate, "SBC") },
            { 0xE5, new Opcode(0xE5, 2, 3, AddressingModeName.ZeroPage, "SBC") },
            { 0xF5, new Opcode(0xF5, 2, 4, AddressingModeName.ZeroPageX, "SBC") },
            { 0xED, new Opcode(0xED, 3, 4, AddressingModeName.Absolute, "SBC") },
            { 0xFD, new Opcode(0xFD, 3, 4, 1, AddressingModeName.AbsoluteX, "SBC") },
            { 0xF9, new Opcode(0xF9, 3, 4, 1, AddressingModeName.AbsoluteY, "SBC") },
            { 0xE1, new Opcode(0xE1, 2, 6, AddressingModeName.IndexedIndirectX, "SBC") },
            { 0xF1, new Opcode(0xF1, 2, 5, 1, AddressingModeName.IndexedIndirectY, "SBC") },

            { 0x38, new Opcode(0x38, 1, 2, AddressingModeName.Implied, "SEC") },
            { 0xF8, new Opcode(0xF8, 1, 2, AddressingModeName.Implied, "SED") },
            { 0x78, new Opcode(0x78, 1, 2, AddressingModeName.Implied, "SEI") },

            { 0x85, new Opcode(0x85, 2, 3, AddressingModeName.ZeroPage, "STA") },
            { 0x95, new Opcode(0x95, 2, 4, AddressingModeName.ZeroPageX, "STA") },
            { 0x8D, new Opcode(0x8D, 3, 4, AddressingModeName.Absolute, "STA") },
            { 0x9D, new Opcode(0x9D, 3, 5, AddressingModeName.AbsoluteX, "STA") },
            { 0x99, new Opcode(0x99, 3, 5, AddressingModeName.AbsoluteY, "STA") },
            { 0x81, new Opcode(0x81, 3, 6, AddressingModeName.IndexedIndirectX, "STA") },
            { 0x91, new Opcode(0x91, 3, 6, AddressingModeName.IndexedIndirectY, "STA") },

            { 0x86, new Opcode(0x86, 2, 3, AddressingModeName.ZeroPage, "STX") },
            { 0x96, new Opcode(0x96, 2, 4, AddressingModeName.ZeroPageY, "STX") },
            { 0x8E, new Opcode(0x8E, 3, 4, AddressingModeName.Absolute, "STX") },

            { 0x84, new Opcode(0x84, 2, 3, AddressingModeName.ZeroPage, "STY") },
            { 0x94, new Opcode(0x94, 2, 4, AddressingModeName.ZeroPageX, "STY") },
            { 0x8C, new Opcode(0x8C, 3, 4, AddressingModeName.Absolute, "STY") },

            { 0xAA, new Opcode(0xAA, 1, 2, AddressingModeName.Implied, "TAX") },
            { 0xA8, new Opcode(0xA8, 1, 2, AddressingModeName.Implied, "TAY") },
            { 0xBA, new Opcode(0xBA, 1, 2, AddressingModeName.Implied, "TSX") },
            { 0x8A, new Opcode(0x8A, 1, 2, AddressingModeName.Implied, "TXA") },
            { 0x9A, new Opcode(0x9A, 1, 2, AddressingModeName.Implied, "TXS") },
            { 0x98, new Opcode(0x98, 1, 2, AddressingModeName.Implied, "TYA") }
        }.ToImmutableDictionary();

        public static Opcode GetOpcode(byte opcode)
        {
            return _opcodes[opcode];
        }

    }


}
