//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NesCore.Machine.CPU.Instructions.Storage
//{
//    /// <summary>
//    /// Transfer instructions
//    /// </summary>
//    public partial class Cpu
//    {
//       // private Registers _registers;
//        //public Cpu(ref Registers registers)
//        //{
//        //    _registers = registers;
//        //}
//        private void DoStatusOperation()
//        {
//            _registers.P.SetNegativeFlagToValueOf7ThBit(_registers.Accumulator);
//            _registers.P.SetZeroFlagIfEqualsToZero(_registers.Accumulator);
//        }
//        /// <summary>
//        /// Transfer A to X
//        /// </summary>
//        public void TAX()
//        {
//            DoStatusOperation();
//            _registers.X = _registers.Accumulator;
//        }

//        /// <summary>
//        /// Transfer A to Y
//        /// </summary>
//        public void TAY()
//        {
//            DoStatusOperation();
//            _registers.Y = _registers.Accumulator;
//        }

//        /// <summary>
//        /// Transfer Stack Pointer to X
//        /// </summary>
//        public void TSX()
//        {
//            DoStatusOperation();
//            _registers.X = (byte)_registers.SP;
//        }

//        /// <summary>
//        /// Transfer X to A
//        /// </summary>
//        public void TXA()
//        {
//            DoStatusOperation();
//            _registers.Accumulator = _registers.X;
//        }

//        /// <summary>
//        /// Transfer X to Stack Pointer
//        /// </summary>
//        public void TXS()
//        {
//            DoStatusOperation();
//            _registers.SP = _registers.X;

//        }

//        /// <summary>
//        /// Transfer Y to A
//        /// </summary>
//        public void TYT()
//        {
//            DoStatusOperation();
//            _registers.Accumulator = _registers.Y;

//        }
//    }
//}
