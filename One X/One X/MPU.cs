using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_X {
    public static class MPU {
        public static byte A {
            get {
                return regA;
            }
            set {
                regA = value;
            }
        }

        private static byte regA, regB, regC, regD, regE, regH, regL;

        static void loadB(byte x) {
            regB = x;
        }
    }
}
