using System;
using System.Collections.Generic;
using System.Text;

namespace NesCore.Machine
{
    /// <summary>
    /// The NES PPU, or Picture Processing Unit, generates a composite video signal with 240 lines of pixels, designed to be received by a television
    /// </summary>
    public class PPU
    {
        private byte[] Ram = new byte[1024 * 2];
    }
}
