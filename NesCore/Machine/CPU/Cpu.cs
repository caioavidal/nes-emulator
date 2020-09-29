using NesCore.Machine.CPU;
using System;
using System.Collections.Generic;
using System.Text;

namespace NesCore.Machine
{
    /// <summary>
    /// The NES CPU core is based on the 6502 processor and runs at approximately 1.79 MHz (1.66 MHz in a PAL NES).
    /// </summary>
    public class Cpu
    {
        private byte A;
        private byte X;
        private byte Y;
        private byte P;

        private ushort PC;
        private ushort S = 0x00;

        private byte[] _ram = new byte[1024 * 2];

        private Bus _bus;

        public void ReadData(ushort address) => _bus.ReadData(address);
        public void WriteData(ushort address, byte data) => _bus.WriteData(address, data);

        public void ConnectToBus(Bus bus)
        {
            _bus = bus;
            //bus.AttachCpu(this);
        }

        public void ClearFlag(CpuStatusFlag flag) => P &= (byte)~flag;
        public void SetFlag(CpuStatusFlag flag) => P |= (byte)flag;

        public void ADC() { }
        public void AND() { }
        public void ASL()
        {
            A <<= 1;
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
        public void LDA(byte operand, Func<byte, byte> addressingMode)
        {

            if (operand >= 0x00 && operand <= 0x7f)
                ClearFlag(CpuStatusFlag.N);
            else
                SetFlag(CpuStatusFlag.N);

            A = addressingMode(operand);
        }

        public void LDX() { }
        public void LDY() { }
        public void LSR() { }
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
        public void STA() { }
        public void STX() { }
        public void STY() { }
        public void TAX()
        {
            X = A;
        }
        public void TAY() { }
        public void TSX() { }
        public void TXA() { }
        public void TXS() { }
        public void TYT() { }


        ///addressing modes
        ///
        public byte Immediate(byte value) => value;
        public byte ZeroPage(byte address) => _ram[address];
        public byte Absolute(ushort address) => _ram[address];
       // public byte Indexed(ushort address, byte offset) => Buffer.BlockCopy(new byte {  }

    }



    public class AddressingMode
    {
        private readonly byte[] _ram;
        public AddressingMode(byte[] ram)
        {
            _ram = ram;
        }

   
    }


}
