using NesCore.Machine;
using System;

namespace NesCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var cpu = new Cpu();
            var bus = new Bus();

            cpu.ConnectToBus(bus);

            cpu.Run(new byte[] { 0x8D, 10, 00 });

            Console.Read();
        }
    }
}
