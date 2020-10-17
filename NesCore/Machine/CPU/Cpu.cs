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
        //todo
        public void ADC(Operand operand)
        {
            _registers.Accumulator += operand.Value.Value;
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Accumulator);
            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Accumulator);
            //_registers.Accumulator += address
        }
        public void AND(Operand operand)
        {
            _registers.Accumulator &= operand.Value.Value;

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Accumulator);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Accumulator);
        }
        public void ASL()
        {
            _registers.BitShiftLeftAccumulator();
        }

        /// <summary>
        /// If the carry flag is clear then add the relative displacement to the program counter to cause a branch to a new location.
        /// </summary>
        public void BCC() {
            
        }
        public void BCS() { }
        public void BEQ() { }
        public void BIT()
        {

        }
        public void BMI(Operand operand) { }
        public void BNE(Operand operand) { }
        public void BPL(Operand operand) { }
        public void BRK(Operand operand) { }
        public void BVC(Operand operand) { }
        public void BVS(Operand operand) { }
        public void CLC() => _registers.P.ClearFlag(CpuStatusFlag.C);
        public void CLD() => _registers.P.ClearFlag(CpuStatusFlag.D);
        public void CLI() => _registers.P.ClearFlag(CpuStatusFlag.I);
        public void CLV() => _registers.P.ClearFlag(CpuStatusFlag.V);

        public void CMP(Operand operand)
        {
            if (_registers.Accumulator >= operand.Value.Value)
                _registers.P.SetFlag(CpuStatusFlag.C);

            if (_registers.Accumulator == operand.Value.Value)
                _registers.P.SetFlag(CpuStatusFlag.Z);

            var result = (byte)(_registers.Accumulator - operand.Value.Value);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(result);

        }
        public void CPX(Operand operand)
        {
            if (_registers.X >= operand.Value.Value)
                _registers.P.SetFlag(CpuStatusFlag.C);

            if (_registers.X == operand.Value.Value)
                _registers.P.SetFlag(CpuStatusFlag.Z);

            var result = (byte)(_registers.X - operand.Value.Value);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(result);
        }
        public void CPY(Operand operand)
        {
            if (_registers.Y >= operand.Value.Value)
                _registers.P.SetFlag(CpuStatusFlag.C);

            if (_registers.Y == operand.Value.Value)
                _registers.P.SetFlag(CpuStatusFlag.Z);

            var result = (byte)(_registers.Y - operand.Value.Value);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(result);
        }

        /// <summary>
        /// Subtracts one from the value held at a specified memory location setting the zero and negative flags as appropriate.
        /// </summary>
        /// <param name="operand"></param>
        public void DEC(Operand operand)
        {

            _ram[operand.Address] = (byte)(operand.Value.Value - 1);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_ram[operand.Address]);
            _registers.P.SetZeroFlagIfEqualsToZero(_ram[operand.Address]);
        }
        /// <summary>
        /// Subtracts one from the X register setting the zero and negative flags as appropriate.
        /// </summary>
        /// <param name="operand"></param>
        public void DEX(Operand operand)
        {

            _registers.X = (byte)(operand.Value.Value - 1);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.X);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.X);
        }

        /// <summary>
        /// Subtracts one from the Y register setting the zero and negative flags as appropriate.
        /// </summary>
        /// <param name="operand"></param>
        public void DEY(Operand operand)
        {
            _registers.Y = (byte)(operand.Value.Value - 1);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Y);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Y);
        }
        public void EOR(Operand operand)
        {
            _registers.Accumulator ^= operand.Value.Value;

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Accumulator);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Accumulator);
        }

        /// <summary>
        /// Adds one to the value held at a specified memory location setting the zero and negative flags as appropriate.
        /// </summary>
        public void INC(Operand operand)
        {

            _ram[operand.Address] = (byte)(operand.Value.Value + 1);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_ram[operand.Address]);
            _registers.P.SetZeroFlagIfEqualsToZero(_ram[operand.Address]);
        }

        /// <summary>
        /// Adds one to the X register setting the zero and negative flags as appropriate.
        /// </summary>
        /// <param name="operand"></param>
        public void INX(Operand operand)
        {
            _registers.X = (byte)(operand.Value.Value + 1);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.X);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.X);
        }

        /// <summary>
        /// Adds one to the Y register setting the zero and negative flags as appropriate.
        /// </summary>
        /// <param name="operand"></param>
        public void INY(Operand operand)
        {
            _registers.Y = (byte)(operand.Value.Value + 1);

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Y);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Y);
        }

        /// <summary>
        /// Sets the program counter to the address specified by the operand.
        /// An original 6502 has does not correctly fetch the target address if the indirect vector falls on a page boundary (e.g. $xxFF where xx is any value from $00 to $FF). 
        /// In this case fetches the LSB from $xxFF as expected but takes the MSB from $xx00. This is fixed in some later chips like the 65SC02 so for compatibility always ensure the indirect vector is not at the end of the page.
        /// </summary>
        /// <param name="operand"></param>
        public void JMP(Operand operand) => _registers.PC = operand.Address;

        /// <summary>
        /// The JSR instruction pushes the address (minus one) of the return point on to the stack and then sets the program counter to the target memory address.
        /// </summary>
        public void JSR(Operand operand) 
        {
            _ram.Stack.Push((byte)(_registers.PC - 1));
            _registers.PC = operand.Address;
        }

        /// <summary>
        /// LDA (Load Accumulator With Memory) loads the accumulator with specified memory. It is probably the most-used opcode in 6502 assembly as it loads the most-used register. It is similar in function to LDX and LDY.
        /// </summary>
        public void LDA(Operand operand)
        {
            _registers.Accumulator = operand.Value.Value;

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Accumulator);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Accumulator);
        }

        /// <summary>
        /// Loads a byte of memory into the X register setting the zero and negative flags as appropriate.
        /// </summary>
        /// <param name="operand"></param>
        public void LDX(Operand operand)
        {
            _registers.X = operand.Value.Value;

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.X);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.X);

        }

        /// <summary>
        /// Loads a byte of memory into the Y register setting the zero and negative flags as appropriate.
        /// </summary>
        /// <param name="operand"></param>
        /// <param name="addressingMode"></param>
        public void LDY(Operand operand)
        {
            _registers.Y = operand.Value.Value;

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Y);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Y);
        }
        public void LSR()
        {
        }

        /// <summary>
        /// The NOP instruction causes no changes to the processor other than the normal incrementing of the program counter to the next instruction.
        /// </summary>
        public void NOP() { }
        public void ORA(Operand operand)
        {
            _registers.Accumulator |= operand.Value.Value;

            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Accumulator);
            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Accumulator);
        }

        #region Stack Operations
        public void PHA()
        {
            _ram.Stack.Push(_registers.Accumulator);
            _registers.IncrementStackPointer();
        }
        public void PHP()
        {
            _ram.Stack.Push(_registers.P.Status);
            _registers.IncrementStackPointer();
        }
        public void PLA()
        {
            _registers.Accumulator = _ram.Stack.Pop();
            _registers.DecrementStackPointer();

            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Accumulator);
            _registers.P.SetNegativeFlagIf7ThBitIsSet(_registers.Accumulator);
        }
        public void PLP()
        {
            _registers.P.SetNewStatus(_ram.Stack.Pop());
            _registers.DecrementStackPointer();
        }

        #endregion

        /// <summary>
        /// Move each of the bits in either A or M one place to the left. Bit 0 is filled with the current value of the carry flag whilst the old bit 7 becomes the new carry flag value.
        /// </summary>
        /// <param name="operand"></param>
        public void ROL(Operand operand)
        {
            var result = (byte)(operand.Value.Value << 1);

            if (_registers.P[CpuStatusFlag.C] == 1)
            {
                result |= _registers.P[CpuStatusFlag.C];
            }
            else
            {
                result &= (byte)~(_registers.P[CpuStatusFlag.C]);

            }

            if (operand.UseAccumulator)
            {
                _registers.Accumulator = result;
            }
            else
            {
                _ram[operand.Address] = result;
            }

            _registers.P.SetFlag(CpuStatusFlag.C, (byte)(operand.Value.Value >> 7 & 1));
            _registers.P.SetZeroFlagIfEqualsToZero(result);
            _registers.P.SetNegativeFlagIf7ThBitIsSet(result);
        }

        /// <summary>
        /// Move each of the bits in either A or M one place to the right. Bit 7 is filled with the current value of the carry flag whilst the old bit 0 becomes the new carry flag value.
        /// </summary>
        /// <param name="operand"></param>
        public void ROR(Operand operand) {

            var result = (byte)(operand.Value.Value >> 1);

            if (_registers.P[CpuStatusFlag.C] == 1)
            {
                result |= (byte)(_registers.P[CpuStatusFlag.C] << 7);
            }
            else
            {
                result &= (byte)~(_registers.P[CpuStatusFlag.C] << 7);

            }

            if (operand.UseAccumulator)
            {
                _registers.Accumulator = result;
            }
            else
            {
                _ram[operand.Address] = result;
            }

            _registers.P.SetFlag(CpuStatusFlag.C, (byte)(operand.Value.Value & 1));
            _registers.P.SetZeroFlagIfEqualsToZero(result);
            _registers.P.SetNegativeFlagIf7ThBitIsSet(result);
        }
        public void RTI(Operand operand) { }

        /// <summary>
        /// The RTS instruction is used at the end of a subroutine to return to the calling routine. It pulls the program counter (minus one) from the stack.
        /// </summary>
        /// <param name="operand"></param>
        public void RTS(Operand operand) 
        {
            _registers.PC = (ushort)(_ram.Stack.Pop() - 1);
        }
        public void SBC(Operand operand) { }
        public void SEC(Operand operand) { }
        public void SED(Operand operand) { }
        public void SEI(Operand operand) { }

        /// <summary>
        /// Stores the contents of the accumulator into memory.
        /// </summary>
        /// <param name="operand"></param>
        public void STA(Operand operand) => _ram[operand.Address] = _registers.Accumulator;

        /// <summary>
        /// Stores the contents of the X register into memory.
        /// </summary>
        /// <param name="operand"></param>
        public void STX(Operand operand) => _ram[operand.Address] = _registers.X;

        /// <summary>
        /// Stores the contents of the Y register into memory.
        /// </summary>
        /// <param name="operand"></param>
        public void STY(Operand operand) => _ram[operand.Address] = _registers.Y;


        ///addressing modes
        ///
        public Operand Immediate(byte value) => new Operand(value: value);
        public Operand ZeroPage(byte address) => new Operand(value: _ram[address], address: address);
        public Operand Absolute(ushort address) => new Operand(value: _ram[address], address: address);

        public Operand ZeroPageX(byte address) => new Operand(value: _ram[address + _registers.X], address: (byte)(address + _registers.X));

        public Operand ZeroPageY(byte address) => new Operand(value: _ram[address + _registers.Y], address: (byte)(address + _registers.Y));

        public Operand AbsoluteX(ushort address) => new Operand(value: _ram[address + _registers.X], address: (byte)(address + _registers.X));
        public Operand AbsoluteY(ushort address) => new Operand(value: _ram[address + _registers.Y], address: (byte)(address + _registers.Y));

        public Operand Indirect(ushort address) => new Operand(value: _ram[(ushort)(_ram[address] << 8 | _ram[address + 1])], address: address);

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
