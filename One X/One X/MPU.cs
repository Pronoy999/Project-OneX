using System;
using System.Collections;

namespace One_X {
    public static class MPU {

        internal enum Flag : byte {
            Sign = 7,
            Zero = 6,
            AuxiliaryCarry = 4,
            Parity = 2,
            Carry = 0,
            // common names
            S = Sign,
            Z = Zero,
            AC = AuxiliaryCarry,
            P = Parity,
            CY = Carry
        }

        internal static byte acc;
        internal static Memory memory;
        internal static byte regB, regC, regD, regE, regH, regL;
        internal static byte regM { get; set; } // TODO Direct to Memory
        internal static byte regA {
            get {
                return acc;
            }
            set {
                acc = value;
                Flag.Sign.Set(acc.IsNegative());
                Flag.Zero.Set(acc == 0);
                Flag.Parity.Set(acc.Parity());
            }
        }

        internal static BitArray flags = new BitArray(8, false);

        internal static void Set(this MPU.Flag flag) => flag.Set(true);
        internal static void Reset(this MPU.Flag flag) => flag.Set(false);
        internal static void Set(this MPU.Flag flag, bool set) => MPU.flags.Set((byte)flag, set);
        internal static void Toggle(this MPU.Flag flag) => MPU.flags.Set((byte)flag, !flag.IsSet());
        internal static bool IsSet(this MPU.Flag flag) => MPU.flags.Get((byte)flag);

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
        public static void AddB() => Adi(regB);
        public static void AddC() => Adi(regC);
        public static void AddD() => Adi(regD);
        public static void AddE() => Adi(regE);
        public static void AddH() => Adi(regH);
        public static void AddL() => Adi(regL);
        public static void AddM() => Adi(regM);
        public static void AddA() => Adi(regA);

        public static void Adi(byte data) {
            int res = regA + data;
            Flag.Carry.Set(res > byte.MaxValue);
            // TODO Set AuxiliaryCarry
            regA = (byte)res;
        }
        #endregion

        #region DAD
        public static void DadB() => DadI(BRp);
        public static void DadD() => DadI(DRp);
        public static void DadH() => DadI(HRp);

        private static void DadI(ushort data) {
            int res = HRp + data;
            Flag.Carry.Set(res > ushort.MaxValue);
            HRp = (ushort)res;
        }
        #endregion

        #region SUB
        public static void SubB() => Sbi(regB);
        public static void SubC() => Sbi(regC);
        public static void SubD() => Sbi(regD);
        public static void SubE() => Sbi(regE);
        public static void SubH() => Sbi(regH);
        public static void SubL() => Sbi(regL);
        public static void SubM() => Sbi(regM);
        public static void SubA() => Sbi(regA);

        public static void Sbi(byte data) {
            int res = regA + regH.TwosComplement();
            Flag.Carry.Set(res <= byte.MaxValue);
            regA = (byte)res;
            // TODO Auxiliary Carry
        }
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
        public static void MoveBB() => LoadB(regB);
        public static void MoveBC() => LoadB(regC);
        public static void MoveBD() => LoadB(regD);
        public static void MoveBE() => LoadB(regE);
        public static void MoveBH() => LoadB(regH);
        public static void MoveBL() => LoadB(regL);
        public static void MoveBM() => LoadB(regM);
        public static void MoveBA() => LoadB(regA);
        #endregion

        #region MOV C
        public static void MoveCB() => LoadC(regB);
        public static void MoveCC() => LoadC(regC);
        public static void MoveCD() => LoadC(regD);
        public static void MoveCE() => LoadC(regE);
        public static void MoveCH() => LoadC(regH);
        public static void MoveCL() => LoadC(regL);
        public static void MoveCM() => LoadC(regM);
        public static void MoveCA() => LoadC(regA);
        #endregion

        #region MOV D
        public static void MoveDB() => LoadD(regB);
        public static void MoveDC() => LoadD(regC);
        public static void MoveDD() => LoadD(regD);
        public static void MoveDE() => LoadD(regE);
        public static void MoveDH() => LoadD(regH);
        public static void MoveDL() => LoadD(regL);
        public static void MoveDM() => LoadD(regM);
        public static void MoveDA() => LoadD(regA);
        #endregion

        #region MOV E
        public static void MoveEB() => LoadE(regB);
        public static void MoveEC() => LoadE(regC);
        public static void MoveED() => LoadE(regD);
        public static void MoveEE() => LoadE(regE);
        public static void MoveEH() => LoadE(regH);
        public static void MoveEL() => LoadE(regL);
        public static void MoveEM() => LoadE(regM);
        public static void MoveEA() => LoadE(regA);
        #endregion

        #region MOV H
        public static void MoveHB() => LoadH(regB);
        public static void MoveHC() => LoadH(regC);
        public static void MoveHD() => LoadH(regD);
        public static void MoveHE() => LoadH(regE);
        public static void MoveHH() => LoadH(regH);
        public static void MoveHL() => LoadH(regL);
        public static void MoveHM() => LoadH(regM);
        public static void MoveHA() => LoadH(regA);
        #endregion

        #region MOV L
        public static void MoveLB() => LoadL(regB);
        public static void MoveLC() => LoadL(regC);
        public static void MoveLD() => LoadL(regD);
        public static void MoveLE() => LoadL(regE);
        public static void MoveLH() => LoadL(regH);
        public static void MoveLL() => LoadL(regL);
        public static void MoveLM() => LoadL(regM);
        public static void MoveLA() => LoadL(regA);
        #endregion

        #region MOV M
        public static void MoveMB() => LoadM(regB);
        public static void MoveMC() => LoadM(regC);
        public static void MoveMD() => LoadM(regD);
        public static void MoveME() => LoadM(regE);
        public static void MoveMH() => LoadM(regH);
        public static void MoveML() => LoadM(regL);
        public static void MoveMM() => LoadM(regM);
        public static void MoveMA() => LoadM(regA);
        #endregion

        #region MOV A
        public static void MoveAB() => LoadA(regB);
        public static void MoveAC() => LoadA(regC);
        public static void MoveAD() => LoadA(regD);
        public static void MoveAE() => LoadA(regE);
        public static void MoveAH() => LoadA(regH);
        public static void MoveAL() => LoadA(regL);
        public static void MoveAM() => LoadA(regM);
        public static void MoveAA() => LoadA(regA);
        #endregion

        #region AND
        public static void AndB() => Ani(regB);
        public static void AndC() => Ani(regC);
        public static void AndD() => Ani(regD);
        public static void AndE() => Ani(regE);
        public static void AndH() => Ani(regH);
        public static void AndL() => Ani(regL);
        public static void AndM() => Ani(regM);
        public static void AndA() => Ani(regA);

        public static void Ani(byte data) {
            //TODO:reset CY and set AC
            regA &= data;
        }
        #endregion

        #region OR
        public static void OrB() => Ori(regB);
        public static void OrC() => Ori(regC);
        public static void OrD() => Ori(regD);
        public static void OrE() => Ori(regE);
        public static void OrH() => Ori(regH);
        public static void OrL() => Ori(regL);
        public static void OrM() => Ori(regM);
        public static void OrA() => Ori(regA);
        public static void Ori(byte data) {
            //TODO:Z,S,P are modified and AC AND CY are reset
            regA |= data;
        }
        #endregion

        #region XOR
        public static void XorB() => Xri(regB);
        public static void XorC() => Xri(regC);
        public static void XorD() => Xri(regD);
        public static void XorE() => Xri(regE);
        public static void XorH() => Xri(regH);
        public static void XorL() => Xri(regL);
        public static void XorM() => Xri(regM);
        public static void XorA() => Xri(regA);
        public static void Xri(byte data) {
            //TODO:Z,S,P are modified and AC AND CY are reset
            regA ^= data;
        }

        #endregion

        #region STORE
        public static void StoreA(ushort address) => memory.WriteByte(regA, address);
        public static void StoreAtBC() => memory.WriteByte(regA, BRp);
        public static void StoreAtDE() => memory.WriteByte(regA, DRp);
        public static void StoreHL(ushort address) => memory.WriteUShort(HRp, address);
        #endregion

        #region LOAD
        public static void LoadA(ushort address) => regA = memory.ReadByte(address);
        public static void LoadFromBC() => regA = memory.ReadByte(BRp);
        public static void LoadFromDE() => regA = memory.ReadByte(DRp);
        public static void LoadHL(ushort address) => HRp = memory.ReadUShort(address);
        #endregion

        #region CMP
        public static void CmpB() => Cpi(regB);
        public static void CmpC() => Cpi(regC);
        public static void CmpD() => Cpi(regD);
        public static void CmpE() => Cpi(regE);
        public static void CmpH() => Cpi(regH);
        public static void CmpL() => Cpi(regL);
        public static void CmpM() => Cpi(regM);
        public static void CmpA() => Cpi(regA);
        public static void Cpi(byte data) {
            if (data > regA) {
                // carry set, zero reset
            } else if (data == regA) {
                // carry reset, zero set
            } else { // data < regA
                // carry reset, zero reset
            }
        }
        #endregion

        public static void ComplA() => regA = (byte) ~regA;

        public static void Halt() { } //TODO: HALT SIGNAL TO EXECUTOR
        // TODO: COMPARE INSTRUCTIONS (Call CPI from any CMP like ADI and ADD above, dont write body for CPI till we have clear idea about the flags)
    }
}

