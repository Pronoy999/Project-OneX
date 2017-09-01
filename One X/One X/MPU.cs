using System;
using System.Collections;

namespace One_X {
    public static class MPU {
        internal enum Flag : byte {
            Sign = 7,
            Zero = 6,
            AuxCarry = 4,
            Parity = 2,
            Carry = 0,
            // common names
            S = Sign,
            Z = Zero,
            AC = AuxCarry,
            P = Parity,
            CY = Carry
        }

        internal static byte regA, regB, regC, regD, regE, regH, regL;

        internal static byte regM { get; set; } // TODO Direct to Memory

        internal static BitArray flags = new BitArray(8, false);

        internal static void Set(this MPU.Flag flag) => flag.Set(true);
        internal static void Reset(this MPU.Flag flag) => flag.Set(false);
        internal static void Set(this MPU.Flag flag, bool set) => MPU.flags.Set((byte)flag, set);
        internal static void Toggle(this MPU.Flag flag) => MPU.flags.Set((byte)flag, !flag.IsSet());
        internal static bool IsSet(this MPU.Flag flag) => MPU.flags.Get((byte)flag);

        internal static byte SetFlags(this int data) {
            Flag.Carry.Set(data > byte.MaxValue);
            byte ret = (byte)data;
            Flag.Zero.Set(ret == 0);
            Flag.Sign.Set(ret.IsNegative());
            return ret;
        }

        internal static ushort progCntr, stackPtr;

        #region Properties
        public static byte A {
            get => regA;
            internal set => regA = value;
        }
        public static byte B {
            get => regB;
            internal set => regB = value;
        }
        public static byte C {
            get => regC;
            internal set => regC = value;
        }
        public static byte D {
            get => regD;
            internal set => regD = value;
        }
        public static byte E {
            get => regE;
            internal set => regE = value;
        }
        public static byte H {
            get => regH;
            internal set => regH = value;
        }
        public static byte L {
            get => regL;
            internal set => regL = value;
        }
        public static byte M {
            get => regM;
            internal set => regM = value;
        }
        public static ushort HRp {
            get => (H, L).ToShort();
            internal set => LoadHRp(value);
        }
        public static ushort BRp {
            get => (B, C).ToShort();
            internal set => LoadBRp(value);
        }
        public static ushort DRp {
            get => (D, E).ToShort();
            internal set => LoadDRp(value);
        }
        public static ushort PC {
            get => progCntr;
            internal set => progCntr = value;
        }
        public static ushort SP {
            get => stackPtr;
            internal set => stackPtr = value;
        }
        #endregion

        #region MVI
        public static void LoadB(byte data) => regB = data;
        public static void LoadC(byte data) => regC = data;
        public static void LoadD(byte data) => regD = data;
        public static void LoadE(byte data) => regE = data;
        public static void LoadH(byte data) => regH = data;
        public static void LoadL(byte data) => regL = data;
        public static void LoadM(byte data) => regM = data;
        public static void LoadA(byte data) => regA = data;
        #endregion

        #region LXI
        public static void LoadBRp(ushort data) {
            var d = data.ToBytes();
            regB = d.HO;
            regC = d.LO;
        }
        public static void LoadDRp(ushort data) {
            var d = data.ToBytes();
            regD = d.HO;
            regE = d.LO;
        }
        public static void LoadHRp(ushort data) {
            var d = data.ToBytes();
            regH = d.HO;
            regL = d.LO;
        }
        #endregion

        #region ADD
        public static void AddB() => regA = (regA + regB).SetFlags();
        public static void AddC() => regA = (regA + regB).SetFlags();
        public static void AddD() => regA = (regA + regB).SetFlags();
        public static void AddE() => regA = (regA + regB).SetFlags();
        public static void AddH() => regA = (regA + regB).SetFlags();
        public static void AddL() => regA = (regA + regB).SetFlags();
        public static void AddM() => regA = (regA + regB).SetFlags();
        public static void AddA() => regA = (regA + regB).SetFlags();
        #endregion

        #region DAD
        public static void DadB() => HRp += BRp;
        public static void DadD() => HRp += DRp;
        public static void DadH() => HRp += HRp;
        #endregion

        #region SUB
        public static void SubB() => regA -= regB;
        public static void SubC() => regA -= regC;
        public static void SubD() => regA -= regD;
        public static void SubE() => regA -= regE;
        public static void SubH() => regA -= regE;
        public static void SubL() => regA -= regL;
        public static void SubM() => regA -= regM;
        public static void SubA() => regA -= regA;
        #endregion

        #region INR
        public static void InrA() => regA++;
        public static void InrB() => regB++;
        public static void InrC() => regC++;
        public static void InrD() => regD++;
        public static void InrE() => regE++;
        public static void InrH() => regH++;
        public static void InrL() => regL++;
        #endregion

        #region DCR
        public static void DcrA() => regA--;
        public static void DcrB() => regB--;
        public static void DcrC() => regC--;
        public static void DcrD() => regD--;
        public static void DcrE() => regE--;
        public static void DcrH() => regH--;
        public static void DcrL() => regL--;
        #endregion

        #region INX
        public static void InxB() => BRp++;
        public static void InxD() => DRp++;
        public static void InxH() => HRp++;
        #endregion

        #region DCX
        public static void DcxB() => BRp--;
        public static void DcxD() => DRp--;
        public static void DcxH() => HRp--;
        #endregion

        #region MOV B
        public static void MoveBB() => regB = regB;
        public static void MoveBC() => regB = regC;
        public static void MoveBD() => regB = regD;
        public static void MoveBE() => regB = regE;
        public static void MoveBH() => regB = regH;
        public static void MoveBL() => regB = regL;
        public static void MoveBM() => regB = regM;
        public static void MoveBA() => regB = regA;
        #endregion

        #region MOV C
        public static void MoveCB() => regC = regB;
        public static void MoveCC() => regC = regC;
        public static void MoveCD() => regC = regD;
        public static void MoveCE() => regC = regE;
        public static void MoveCH() => regC = regH;
        public static void MoveCL() => regC = regL;
        public static void MoveCM() => regC = regM;
        public static void MoveCA() => regC = regA;
        #endregion

        #region MOV D
        public static void MoveDB() => regD = regB;
        public static void MoveDC() => regD = regC;
        public static void MoveDD() => regD = regD;
        public static void MoveDE() => regD = regE;
        public static void MoveDH() => regD = regH;
        public static void MoveDL() => regD = regL;
        public static void MoveDM() => regD = regM;
        public static void MoveDA() => regD = regA;
        #endregion

        #region MOV E
        public static void MoveEB() => regE = regB;
        public static void MoveEC() => regE = regC;
        public static void MoveED() => regE = regD;
        public static void MoveEE() => regE = regE;
        public static void MoveEH() => regE = regH;
        public static void MoveEL() => regE = regL;
        public static void MoveEM() => regE = regM;
        public static void MoveEA() => regE = regA;
        #endregion

        #region MOV H
        public static void MoveHB() => regH = regB;
        public static void MoveHC() => regH = regC;
        public static void MoveHD() => regH = regD;
        public static void MoveHE() => regH = regE;
        public static void MoveHH() => regH = regH;
        public static void MoveHL() => regH = regL;
        public static void MoveHM() => regH = regM;
        public static void MoveHA() => regH = regA;
        #endregion

        #region MOV L
        public static void MoveLB() => regL = regB;
        public static void MoveLC() => regL = regC;
        public static void MoveLD() => regL = regD;
        public static void MoveLE() => regL = regE;
        public static void MoveLH() => regL = regH;
        public static void MoveLL() => regL = regL;
        public static void MoveLM() => regL = regM;
        public static void MoveLA() => regL = regA;
        #endregion

        #region MOV M
        public static void MoveMB() => regM = regB;
        public static void MoveMC() => regM = regC;
        public static void MoveMD() => regM = regD;
        public static void MoveME() => regM = regE;
        public static void MoveMH() => regM = regH;
        public static void MoveML() => regM = regL;
        public static void MoveMM() => regM = regM;
        public static void MoveMA() => regM = regA;
        #endregion

        #region MOV A
        public static void MoveAB() => regA = regB;
        public static void MoveAC() => regA = regC;
        public static void MoveAD() => regA = regD;
        public static void MoveAE() => regA = regE;
        public static void MoveAH() => regA = regH;
        public static void MoveAL() => regA = regL;
        public static void MoveAM() => regA = regM;
        public static void MoveAA() => regA = regA;
        #endregion

        public static void Halt() { } //TODO: HALT SIGNAL TO EXECUTOR

        // TODO: TO BE EDITED
        public static void cmpB() {
            int a = regB.CompareTo(regA);
            if (a == 0) { //carry flag 
            } else if (a > 0) { //carry flag
            } else {  //carry flag
            }
        }
        public static void cpi(byte xx) {
            int a = xx.CompareTo(regA);
            if (a == 0) { //carry flag 
            } else if (a > 0) { //carry flag
            } else {  //carry flag
            }
        }
        public static void adi(byte xx) { regA = (byte)(regA + xx); }
    }
}

