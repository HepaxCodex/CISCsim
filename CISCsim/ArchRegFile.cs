using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISCsim
{
    /// <summary>
    /// Represents the Architecture Register File
    /// </summary>
    class ArchRegFile
    {

        /// <summary>
        /// 0..31 == r0..r31
        /// 32..62 == f0..f30
        /// 63 == HiLo
        /// 64 == FCC
        /// </summary>
        public ArfEntry[] regFile;

        public ArchRegFile()
        {
        }

    }
}
