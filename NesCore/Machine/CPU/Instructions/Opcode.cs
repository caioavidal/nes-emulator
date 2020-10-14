using System;
using System.Collections.Generic;
using System.Text;

namespace NesCore.Machine.CPU
{
    public readonly struct Opcode
    {
        public Opcode(byte opcodeValue, byte bytes, byte cycles, AddressingMode addressingMode, string instruction)
        {
            OpcodeValue = opcodeValue;
            Bytes = bytes;
            Cycles = cycles;
            AddressingMode = addressingMode;
            Instruction = instruction;
            PageCrossed = 0;
        }
        public Opcode(byte opcodeValue, byte bytes, byte cycles, byte pageCrossed, AddressingMode addressingMode, string instruction)
        {
            OpcodeValue = opcodeValue;
            Bytes = bytes;
            Cycles = cycles;
            AddressingMode = addressingMode;
            Instruction = instruction;
            PageCrossed = pageCrossed;
        }

        public byte PageCrossed { get; }

        public byte OpcodeValue { get; }
        public byte Bytes{ get;  }
        public byte Cycles { get;  }
        public AddressingMode AddressingMode { get; }
        public string Instruction { get;  }
    }
}
