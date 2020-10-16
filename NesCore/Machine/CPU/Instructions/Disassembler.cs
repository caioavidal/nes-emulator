using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NesCore.Machine.CPU.Instructions
{
    public class Disassembler
    {
        private readonly Cpu _cpu;

        public Disassembler(Cpu cpu)
        {
            _cpu = cpu;
        }
        public Tuple<MethodInfo, object[]> Decode(Opcode opcode, byte[] bytes)
        {
            var addressingModeMethod = _cpu.GetType().GetMethod(opcode.AddressingMode.ToString());
            var argumentType = addressingModeMethod.GetParameters().First().ParameterType;

            var operand =  addressingModeMethod.Invoke(_cpu, new object[] { argumentType == typeof(byte) ? bytes[1] : BitConverter.ToUInt16(bytes[1..3]) });

            var instruction = _cpu.GetType().GetMethod(opcode.Instruction);

            return new Tuple<MethodInfo, object[]>(instruction, new object[] { operand }); ;
        }
    }
}
