using NesCore.Machine.CPU;
using NesCore.Machine.CPU.Instructions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;
using System.Text;

namespace NesCore.Machine
{
    /// <summary>
    /// The NES CPU core is based on the 6502 processor and runs at approximately 1.79 MHz (1.66 MHz in a PAL NES).
    /// </summary>
    public class Cpu
    {

        private Registers _registers = new Registers();

        private RAM _ram = new RAM();

        private Bus _bus;

        public Cpu()
        {
            _ram.Reset();
            _registers.Reset();
        }

        public void ReadData(ushort address) => _bus.ReadData(address);
        public void WriteData(ushort address, byte data) => _bus.WriteData(address, data);

        public void Run(byte[] program)
        {
            _ram.Init(program);

            while (true)
            {
                Decode();
            }
        }

        public void ConnectToBus(Bus bus)
        {
            _bus = bus;
            //bus.AttachCpu(this);
        }

        private void Fetch()
        {
        }

        private void Decode()
        {
            var code = _ram[_registers.PC];
            var opcode = OpcodeMap.GetOpcode(code);

            var disassembler = new Disassembler(this);
            var instruction = disassembler.Decode(opcode, _ram[_registers.PC, _registers.PC + opcode.Bytes]);

            instruction.Item1.Invoke(this, instruction.Item2);
            _registers.PC += opcode.Bytes;

        }


        /// <summary>
        /// This instruction adds the contents of a memory location to the accumulator together with the carry bit. If overflow occurs the carry bit is set, this enables multiple byte addition to be performed.

        /// </summary>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        public void ADC(ushort address, byte offset = 0)
        {
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Accumulator);
            _registers.P.SetNegativeFlagToValueOf7ThBit((byte)address);
            //_registers.Accumulator += address
        }
        public void AND(ushort operand, byte offset)
        {
            operand &= _registers.Accumulator;

            _registers.P.SetNegativeFlagToValueOf7ThBit((byte)operand);
            _registers.P.SetZeroFlagIfEqualsToZero((byte)operand);

            //_registers.Accumulator = operand;
        }
        public void ASL()
        {
            _registers.BitShiftLeftAccumulator();
        }
        public void BCC() { }
        public void BCS() { }
        public void BEQ() { }
        public void BIT() { }
        public void BMI() { }
        public void BNE() { }
        public void BPL() { }
        public void BRK() { }
        public void BVC() { }
        public void BVS() { }
        public void CLC() { }
        public void CLD() { }
        public void CLI() { }
        public void CLV() { }
        public void CMP() { }
        public void CPX() { }
        public void CPY() { }
        public void DEC() { }
        public void DEX() { }
        public void DEY() { }
        public void EOR() { }
        public void INC() { }
        public void INX() { }
        public void INY() { }
        public void JMP() { }
        public void JSR() { }

        /// <summary>
        /// LDA (Load Accumulator With Memory) loads the accumulator with specified memory. It is probably the most-used opcode in 6502 assembly as it loads the most-used register. It is similar in function to LDX and LDY.
        /// </summary>
        public void LDA(byte operand, Func<byte, byte> addressingMode = null)
        {
            //addressingMode = addressingMode ?? Immediate;

            if (operand >= 0x00 && operand <= 0x7f)
                _registers.P.ClearFlag(CpuStatusFlag.N);
            else
                _registers.P.SetFlag(CpuStatusFlag.N);

            _registers.P.SetZeroFlagIfEqualsToZero(operand);


            //_registers.Accumulator = addressingMode(operand);
        }

        public void LDX(byte operand, Func<byte, byte> addressingMode = null)
        {

            //addressingMode = addressingMode ?? Immediate;

            _registers.P.SetNegativeFlagToValueOf7ThBit(operand);
            _registers.P.SetZeroFlagIfEqualsToZero(operand);


            // _registers.X = addressingMode(operand);

        }
        public void LDY(byte operand, Func<byte, byte> addressingMode = null)
        {

            //   addressingMode = addressingMode ?? Immediate;

            if (operand >= 0x00 && operand <= 0x7f)
                _registers.P.ClearFlag(CpuStatusFlag.N);
            else
                _registers.P.SetFlag(CpuStatusFlag.N);

            _registers.P.SetZeroFlagIfEqualsToZero(operand);



            // _registers.Y = addressingMode(operand);

        }
        public void LSR()
        {
        }
        public void NOP() { }
        public void ORO() { }
        public void PHA() { }
        public void PHP() { }
        public void PLA() { }
        public void PLP() { }
        public void ROL() { }
        public void ROR() { }
        public void RTI() { }
        public void RTS() { }
        public void SBC() { }
        public void SEC() { }
        public void SED() { }
        public void SEI() { }

        /// <summary>
        /// Stores the contents of the accumulator into memory.
        /// </summary>
        /// <param name="operand"></param>
        public void STA(Operand operand) => _ram.Store(_registers.Accumulator, operand.Address, operand.Offset ?? 0);

        public void STX(byte operand)
        {
            _ram[operand] = _registers.X;
        }
        public void STY(byte operand)
        {
            _ram[operand] = _registers.Y;
        }

        ///addressing modes
        ///
        public Operand Immediate(byte value) => new Operand(value: value);
        public Operand ZeroPage(byte address) => new Operand(value: _ram[address], address: address);
        public Operand Absolute(ushort address) => new Operand(value: _ram[address], address: address);

        public Operand ZeroPageX(byte address) => new Operand(values: _ram[address, _registers.X], address: address, offset: _registers.X);

        public Operand ZeroPageY(byte address) => new Operand(values: _ram[address, _registers.Y], address: address, offset: _registers.Y);

        public Operand AbsoluteX(ushort address) => new Operand(values: _ram[address, _registers.X], address: address, offset: _registers.X);
        public Operand AbsoluteY(ushort address) => new Operand(values: _ram[address, _registers.Y], address: address, offset: _registers.Y);

        //public byte Indirect(byte address) => _ram[_ram[address + X]];

        public Operand IndexedIndirectX(byte address) => new Operand(value: _ram[_ram[address + _registers.X]], address: _ram[address + _registers.X]);
        public Operand IndexedIndirectY(byte address) => new Operand(value: _ram[_ram[address + _registers.Y]], address: _ram[address + _registers.Y]);


    }

    public enum AddressingModeName
    {
        Immediate,
        ZeroPage,
        Absolute,
        ZeroPageX,
        ZeroPageY,
        IndexedIndirectX,
        AbsoluteX,
        AbsoluteY,
        IndexedIndirectY,
        Implied,
        Accumulator
    }


}
