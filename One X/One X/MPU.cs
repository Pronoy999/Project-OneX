using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections;
using System.Threading.Tasks;

namespace One_X {
    public static class MPU {
        private static string memName = "";

        private static bool running = false;
        private static bool interpt = true;

        public static Memory memory;

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

        public class MPUEventArgs: EventArgs {
            public string VarName;
            public object NewValue;

            public MPUEventArgs(string varName, object newValue) {
                VarName = varName;
                NewValue = newValue;
            }
        }

        public static event EventHandler<MPUEventArgs> ValueChanged;

        // todo events for all registers/flags.
        internal static byte regA, regB, regC, regD, regE, regH, regL; //and M

        internal static BitArray flags = new BitArray(8, false);

        internal static void Set(this Flag flag) => flag.Set(true);
        internal static void Reset(this Flag flag) => flag.Set(false);
        internal static void Set(this Flag flag, bool set) {
            flags.Set((byte)flag, set);
            if (ValueChanged != null) {
                ValueChanged.Invoke(null, new MPUEventArgs(flag.ToString(), set));
            }
        }
        internal static void Toggle(this Flag flag) => flags.Set((byte)flag, !flag.IsSet());
        internal static bool IsSet(this Flag flag) => flags.Get((byte)flag);

        internal static ushort progCntr, stackPtr;

        #region Properties
        public static byte A {
            get => regA;
            internal set {
                regA = value;
                Flag.Sign.Set(regA.IsNegative());
                Flag.Zero.Set(regA == 0);
                Flag.Parity.Set(regA.Parity());
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("A", A));
                }
            }
        }
        public static byte B {
            get => regB;
            internal set {
                regB = value;
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("B", B));
                }
            }
        }
        public static byte C {
            get => regC;
            internal set {
                regC = value;
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("C", C));
                }
            }
        }
        public static byte D {
            get => regD;
            internal set {
                regD = value;
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("D", D));
                }
            }
        }
        public static byte E {
            get => regE;
            internal set {
                regE = value;
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("E", E));
                }
            }
        }
        public static byte H {
            get => regH;
            internal set {
                regH = value;
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("H", H));
                }
            }
        }
        public static byte L {
            get => regL;
            internal set {
                regL = value;
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("L", L));
                }
            }
        }
        public static byte M {
            get => memory.ReadByte(HRp);
            set {
                memory.WriteByte(value, HRp);
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("M", M));
                }
            }
        }
        public static ushort HRp {
            get => (H, L).ToUShort();
            internal set {
                LoadHRp(value);
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("HRp", HRp));
                }
            }
        }
        public static ushort BRp {
            get => (B, C).ToUShort();
            internal set {
                LoadBRp(value);
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("BRp", BRp));
                }
            }
        }
        public static ushort DRp {
            get => (D, E).ToUShort();
            internal set {
                LoadDRp(value);
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("DRp", DRp));
                }
            }
        }
        public static ushort PC {
            get => progCntr;
            internal set {
                progCntr = value;
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("PC", PC));
                }
            }
        }
        public static ushort SP {
            get => stackPtr;
            internal set {
                stackPtr = value;
                if (ValueChanged != null) {
                    ValueChanged.Invoke(null, new MPUEventArgs("SP", SP));
                }
            }
        }
        #endregion

        #region MVI
        public static ushort LoadB(byte data) {
            B = data;
            return (ushort) (PC+2);
        }
        public static ushort LoadC(byte data) {
            C = data;
            return (ushort)(PC + 2);
        }

        public static ushort LoadD(byte data) {
            D = data;
            return (ushort)(PC + 2);
        }

        public static ushort LoadE(byte data) {
            E = data;
            return (ushort)(PC + 2);
        }

        public static ushort LoadH(byte data) {
            H = data;
            return (ushort)(PC + 2);
        }

        public static ushort LoadL(byte data) {
            L = data;
            return (ushort)(PC + 2);
        }

        public static ushort LoadM(byte data) {
            M = data;
            return (ushort)(PC + 2);
        }

        public static ushort LoadA(byte data) {
            A = data;
            return (ushort)(PC + 2);
        }
        #endregion

        #region LXI
        public static ushort LoadBRp(ushort data) {
            var d = data.ToBytes();
            B = d.HO;
            C = d.LO;
            return (ushort)(PC + 3);
        }
        public static ushort LoadDRp(ushort data) {
            var d = data.ToBytes();
            D = d.HO;
            E = d.LO;
            return (ushort)(PC + 3);
        }
        public static ushort LoadHRp(ushort data) {
            var d = data.ToBytes();
            H = d.HO;
            L = d.LO;
            return (ushort)(PC + 3);
        }
        public static ushort LoadSP(ushort data) {
            stackPtr = data;
            return (ushort)(PC + 3);
        }
        #endregion

        #region ADD
        public static ushort AddB() {
            Adi(B);
            return (ushort)(PC + 1);
        }

        public static ushort AddC() {
            Adi(C);
            return(ushort)(PC + 1);
        }

        public static ushort AddD() {
            Adi(D);
            return (ushort)(PC + 1);
        }

        public static ushort AddE() {
            Adi(E);
            return (ushort)(PC + 1);
        }

        public static ushort AddH() {
            Adi(H);
            return (ushort)(PC + 1);
        }

        public static ushort AddL() {
            Adi(L);
            return (ushort)(PC + 1);
        }

        public static ushort AddM() {
            Adi(M);
            return (ushort)(PC + 1);
        }

        public static ushort AddA() {
            Adi(A);
            return (ushort)(PC + 1);
        }

        public static ushort Adi(byte data) {
            int res = A + data;
            Flag.Carry.Set(res > byte.MaxValue);
            // TODO Set AuxiliaryCarry
            A = (byte)res;
            return (ushort)(PC + 2);
        }
        #endregion

        #region DAD
        public static ushort DadB() {
            DadI(BRp);
            return (ushort)(PC + 1);
        }

        public static ushort DadD() {
            DadI(DRp);
            return (ushort)(PC + 1);
        }

        public static ushort DadH() {
            DadI(HRp);
            return (ushort)(PC + 1);
        }

        public static ushort DadSP() {
            DadI(SP);
            return (ushort)(PC + 1);
        }

        private static void DadI(ushort data) {
            int res = HRp + data;
            Flag.Carry.Set(res > ushort.MaxValue);
            HRp = (ushort)res;
        }
        #endregion

        #region SUB
        public static ushort SubB() {
            Sui(B);
            return (ushort)(PC + 1);
        }

        public static ushort SubC() {
            Sui(C);
            return (ushort)(PC + 1);
        }

        public static ushort SubD() {
            Sui(D);
            return (ushort)(PC + 1);
        }

        public static ushort SubE() {
            Sui(E);
            return (ushort)(PC + 1);
        }

        public static ushort SubH() {
            Sui(H);
            return (ushort)(PC + 1);
        }

        public static ushort SubL() {
            Sui(L);
            return (ushort)(PC + 1);
        }

        public static ushort SubM() {
            Sui(M);
            return (ushort)(PC + 1);
        }

        public static ushort SubA() {
            Sui(A);
            return (ushort)(PC + 1);
        }

        public static ushort Sui(byte data) {
            int res = A + data.TwosComplement();
            Flag.Carry.Set(res <= byte.MaxValue);
            A = (byte)res;
            // TODO: Auxiliary Carry
            return (ushort)(PC + 2);
        }
        #endregion

        #region ADC
        public static ushort AdcB() {
            Aci(B);
            return (ushort)(PC + 1);
        }

        public static ushort AdcC() {
            Aci(C);
            return (ushort)(PC + 1);
        }

        public static ushort AdcD() {
            Aci(D);
            return (ushort)(PC + 1);
        }

        public static ushort AdcE() {
            Aci(E);
            return (ushort)(PC + 1);
        }

        public static ushort AdcH() {
            Aci(H);
            return (ushort)(PC + 1);
        }

        public static ushort AdcL() {
            Aci(L);
            return (ushort)(PC + 1);
        }

        public static ushort AdcM() {
            Aci(M);
            return (ushort)(PC + 1);
        }

        public static ushort AdcA() {
            Aci(A);
            return (ushort)(PC + 1);
        }

        public static ushort Aci(byte data) {
            Adi(data);
            Adi((byte)Flag.Carry.IsSet().ToBitInt());
            return (ushort)(PC + 2);
        }
        #endregion

        #region SBB
        public static ushort SbbB() {
            Sbi(B);
            return (ushort)(PC + 1);
        }

        public static ushort SbbC() {
            Sbi(C);
            return (ushort)(PC + 1);
        }

        public static ushort SbbD() {
            Sbi(D);
            return (ushort)(PC + 1);
        }

        public static ushort SbbE() {
            Sbi(E);
            return (ushort)(PC + 1);
        }

        public static ushort SbbH() {
            Sbi(H);
            return (ushort)(PC + 1);
        }

        public static ushort SbbL() {
            Sbi(L);
            return (ushort)(PC + 1);
        }

        public static ushort SbbM() {
            Sbi(M);
            return (ushort)(PC + 1);
        }

        public static ushort SbbA() {
            Sbi(A);
            return (ushort)(PC + 1);
        }

        public static ushort Sbi(byte data) {
            Sui((byte)(data + Flag.Carry.IsSet().ToBitInt()));
            return (ushort)(PC + 2);
        }
        #endregion

        #region INR
        public static ushort InrA() {
            A++;
            return (ushort)(PC + 1);
        }

        public static ushort InrB() {
            B++;
            Flag.Sign.Set(B.IsNegative());
            Flag.Zero.Set(B == 0);
            Flag.Parity.Set(B.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort InrC() {
            C++;
            Flag.Sign.Set(C.IsNegative());
            Flag.Zero.Set(C == 0);
            Flag.Parity.Set(C.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort InrD() {
            D++;
            Flag.Sign.Set(D.IsNegative());
            Flag.Zero.Set(D == 0);
            Flag.Parity.Set(D.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort InrE() {
            E++;
            Flag.Sign.Set(E.IsNegative());
            Flag.Zero.Set(E == 0);
            Flag.Parity.Set(E.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort InrH() {
            H++;
            Flag.Sign.Set(H.IsNegative());
            Flag.Zero.Set(H == 0);
            Flag.Parity.Set(H.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort InrL() {
            L++;
            Flag.Sign.Set(L.IsNegative());
            Flag.Zero.Set(L == 0);
            Flag.Parity.Set(L.Parity());
            return (ushort)(PC + 1);
        }
        #endregion

        #region DCR
        public static ushort DcrA() {
            A--;
            return (ushort)(PC + 1);
        }

        public static ushort DcrB() {
            B--;
            Flag.Sign.Set(B.IsNegative());
            Flag.Zero.Set(B == 0);
            Flag.Parity.Set(B.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort DcrC() {
            C--;
            Flag.Sign.Set(C.IsNegative());
            Flag.Zero.Set(C == 0);
            Flag.Parity.Set(C.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort DcrD() {
            D--;
            Flag.Sign.Set(D.IsNegative());
            Flag.Zero.Set(D == 0);
            Flag.Parity.Set(D.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort DcrE() {
            E--;
            Flag.Sign.Set(E.IsNegative());
            Flag.Zero.Set(E == 0);
            Flag.Parity.Set(E.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort DcrH() {
            H--;
            Flag.Sign.Set(H.IsNegative());
            Flag.Zero.Set(H == 0);
            Flag.Parity.Set(H.Parity());
            return (ushort)(PC + 1);
        }
        public static ushort DcrL() {
            L--;
            Flag.Sign.Set(L.IsNegative());
            Flag.Zero.Set(L == 0);
            Flag.Parity.Set(L.Parity());
            return (ushort)(PC + 1);
        }
        #endregion

        #region INX
        public static ushort InxB() {
            BRp++;
            return (ushort)(PC + 1);
        }

        public static ushort InxD() {
            DRp++;
            return (ushort)(PC + 1);
        }

        public static ushort InxH() {
            HRp++;
            return (ushort)(PC + 1);
        }

        public static ushort InxSP() {
            SP++;
            return (ushort)(PC + 1);
        }
        #endregion

        #region DCX
        public static ushort DcxB() {
            BRp--;
            return (ushort)(PC + 1);
        }

        public static ushort DcxD() {
            DRp--;
            return (ushort)(PC + 1);
        }

        public static ushort DcxH() {
            HRp--;
            return (ushort)(PC + 1);
        }

        public static ushort DcxSP() {
            SP--;
            return (ushort)(PC + 1);
        }
        #endregion

        #region MOV B
        public static ushort MoveBB() {
            LoadB(B);
            return (ushort)(PC + 1);
        }

        public static ushort MoveBC() {
            LoadB(C);
            return (ushort)(PC + 1);
        }

        public static ushort MoveBD() {
            LoadB(D);
            return (ushort)(PC + 1);
        }

        public static ushort MoveBE() {
            LoadB(E);
            return (ushort)(PC + 1);
        }

        public static ushort MoveBH() {
            LoadB(H);
            return (ushort)(PC + 1);
        }

        public static ushort MoveBL() {
            LoadB(L);
            return (ushort)(PC + 1);
        }

        public static ushort MoveBM() {
            LoadB(M);
            return (ushort)(PC + 1);
        }

        public static ushort MoveBA() {
            LoadB(A);
            return (ushort)(PC + 1);
        }
        #endregion

        #region MOV C
        public static ushort MoveCB() {
            LoadC(B);
            return (ushort)(PC + 1);
        }

        public static ushort MoveCC() {
            LoadC(C);
            return (ushort)(PC + 1);
        }

        public static ushort MoveCD() {
            LoadC(D);
            return (ushort)(PC + 1);
        }

        public static ushort MoveCE() {
            LoadC(E);
            return (ushort)(PC + 1);
        }

        public static ushort MoveCH() {
            LoadC(H);
            return (ushort)(PC + 1);
        }

        public static ushort MoveCL() {
            LoadC(L);
            return (ushort)(PC + 1);
        }

        public static ushort MoveCM() {
            LoadC(M);
            return (ushort)(PC + 1);
        }

        public static ushort MoveCA() {
            LoadC(A);
            return (ushort)(PC + 1);
        }
        #endregion

        #region MOV D
        public static ushort MoveDB() {
            LoadD(B);
            return (ushort)(PC + 1);
        }

        public static ushort MoveDC() {
            LoadD(C);
            return (ushort)(PC + 1);
        }

        public static ushort MoveDD() {
            LoadD(D);
            return (ushort)(PC + 1);
        }

        public static ushort MoveDE() {
            LoadD(E);
            return (ushort)(PC + 1);
        }

        public static ushort MoveDH() {
            LoadD(H);
            return (ushort)(PC + 1);
        }

        public static ushort MoveDL() {
            LoadD(L);
            return (ushort)(PC + 1);
        }

        public static ushort MoveDM() {
            LoadD(M);
            return (ushort)(PC + 1);
        }

        public static ushort MoveDA() {
            LoadD(A);
            return (ushort)(PC + 1);
        }
        #endregion

        #region MOV E
        public static ushort MoveEB() {
            LoadE(B);
            return (ushort)(PC + 1);
        }

        public static ushort MoveEC() {
            LoadE(C);
            return (ushort)(PC + 1);
        }

        public static ushort MoveED() {
            LoadE(D);
            return (ushort)(PC + 1);
        }

        public static ushort MoveEE() {
            LoadE(E);
            return (ushort)(PC + 1);
        }

        public static ushort MoveEH() {
            LoadE(H);
            return (ushort)(PC + 1);
        }

        public static ushort MoveEL() {
            LoadE(L);
            return (ushort)(PC + 1);
        }

        public static ushort MoveEM() {
            LoadE(M);
            return (ushort)(PC + 1);
        }

        public static ushort MoveEA() {
            LoadE(A);
            return (ushort)(PC + 1);
        }
        #endregion

        #region MOV H
        public static ushort MoveHB() {
            LoadH(B);
            return (ushort)(PC + 1);
        }

        public static ushort MoveHC() {
            LoadH(C);
            return (ushort)(PC + 1);
        }

        public static ushort MoveHD() {
            LoadH(D);
            return (ushort)(PC + 1);
        }

        public static ushort MoveHE() {
            LoadH(E);
            return (ushort)(PC + 1);
        }

        public static ushort MoveHH() {
            LoadH(H);
            return (ushort)(PC + 1);
        }

        public static ushort MoveHL() {
            LoadH(L);
            return (ushort)(PC + 1);
        }

        public static ushort MoveHM() {
            LoadH(M);
            return (ushort)(PC + 1);
        }

        public static ushort MoveHA() {
            LoadH(A);
            return (ushort)(PC + 1);
        }
        #endregion

        #region MOV L
        public static ushort MoveLB() {
            LoadL(B);
            return (ushort)(PC + 1);
        }

        public static ushort MoveLC() {
            LoadL(C);
            return (ushort)(PC + 1);
        }

        public static ushort MoveLD() {
            LoadL(D);
            return (ushort)(PC + 1);
        }

        public static ushort MoveLE() {
            LoadL(E);
            return (ushort)(PC + 1);
        }

        public static ushort MoveLH() {
            LoadL(H);
            return (ushort)(PC + 1);
        }

        public static ushort MoveLL() {
            LoadL(L);
            return (ushort)(PC + 1);
        }

        public static ushort MoveLM() {
            LoadL(M);
            return (ushort)(PC + 1);
        }

        public static ushort MoveLA() {
            LoadL(A);
            return (ushort)(PC + 1);
        }
        #endregion

        #region MOV M
        public static ushort MoveMB() {
            LoadM(B);
            return (ushort)(PC + 1);
        }

        public static ushort MoveMC() {
            LoadM(C);
            return (ushort)(PC + 1);
        }

        public static ushort MoveMD() {
            LoadM(D);
            return (ushort)(PC + 1);
        }

        public static ushort MoveME() {
            LoadM(E);
            return (ushort)(PC + 1);
        }

        public static ushort MoveMH() {
            LoadM(H);
            return (ushort)(PC + 1);
        }

        public static ushort MoveML() {
            LoadM(L);
            return (ushort)(PC + 1);
        }

        public static ushort MoveMM() {
            LoadM(M);
            return (ushort)(PC + 1);
        }

        public static ushort MoveMA() {
            LoadM(A);
            return (ushort)(PC + 1);
        }
        #endregion

        #region MOV A
        public static ushort MoveAB() {
            LoadA(B);
            return (ushort)(PC + 1);
        }

        public static ushort MoveAC() {
            LoadA(C);
            return (ushort)(PC + 1);
        }

        public static ushort MoveAD() {
            LoadA(D);
            return (ushort)(PC + 1);
        }

        public static ushort MoveAE() {
            LoadA(E);
            return (ushort)(PC + 1);
        }

        public static ushort MoveAH() {
            LoadA(H);
            return (ushort)(PC + 1);
        }

        public static ushort MoveAL() {
            LoadA(L);
            return (ushort)(PC + 1);
        }

        public static ushort MoveAM() {
            LoadA(M);
            return (ushort)(PC + 1);
        }

        public static ushort MoveAA() {
            LoadA(A);
            return (ushort)(PC + 1);
        }
        #endregion

        #region AND
        public static ushort AndB() {
            Ani(B);
            return (ushort)(PC + 1);
        }

        public static ushort AndC() {
            Ani(C);
            return (ushort)(PC + 1);
        }

        public static ushort AndD() {
            Ani(D);
            return (ushort)(PC + 1);
        }

        public static ushort AndE() {
            Ani(E);
            return (ushort)(PC + 1);
        }

        public static ushort AndH() {
            Ani(H);
            return (ushort)(PC + 1);
        }

        public static ushort AndL() {
            Ani(L);
            return (ushort)(PC + 1);
        }

        public static ushort AndM() {
            Ani(M);
            return (ushort)(PC + 1);
        }

        public static ushort AndA() {
            Ani(A);
            return (ushort)(PC + 1);
        }

        public static ushort Ani(byte data) {
            //TODO:reset CY and set AC
            A &= data;
            return (ushort)(PC + 2);
        }
        #endregion

        #region OR
        public static ushort OrB() {
            Ori(B);
            return (ushort)(PC + 1);
        }

        public static ushort OrC() {
            Ori(C);
            return (ushort)(PC + 1);
        }

        public static ushort OrD() {
            Ori(D);
            return (ushort)(PC + 1);
        }

        public static ushort OrE() {
            Ori(E);
            return (ushort)(PC + 1);
        }

        public static ushort OrH() {
            Ori(H);
            return (ushort)(PC + 1);
        }

        public static ushort OrL() {
            Ori(L);
            return (ushort)(PC + 1);
        }

        public static ushort OrM() {
            Ori(M);
            return (ushort)(PC + 1);
        }

        public static ushort OrA() {
            Ori(A);
            return (ushort)(PC + 1);
        }

        public static ushort Ori(byte data) {
            //TODO:Z,S,P are modified and AC AND CY are reset
            A |= data;
            return (ushort)(PC + 2);
        }
        #endregion

        #region XOR
        public static ushort XorB() {
            Xri(B);
            return (ushort)(PC + 1);
        }

        public static ushort XorC() {
            Xri(C);
            return (ushort)(PC + 1);
        }

        public static ushort XorD() {
            Xri(D);
            return (ushort)(PC + 1);
        }

        public static ushort XorE() {
            Xri(E);
            return (ushort)(PC + 1);
        }

        public static ushort XorH() {
            Xri(H);
            return (ushort)(PC + 1);
        }

        public static ushort XorL() {
            Xri(L);
            return (ushort)(PC + 1);
        }

        public static ushort XorM() {
            Xri(M);
            return (ushort)(PC + 1);
        }

        public static ushort XorA() {
            Xri(A);
            return (ushort)(PC + 1);
        }

        public static ushort Xri(byte data) {
            //TODO:Z,S,P are modified and AC AND CY are reset
            A ^= data;
            return (ushort)(PC + 2);
        }

        #endregion

        #region STORE
        public static ushort StoreA(ushort address) {
            memory.WriteByte(A, address);
            return (ushort)(PC + 3);
        }

        public static ushort StoreAtBC() {
            memory.WriteByte(A, BRp);
            return (ushort)(PC + 1);
        }

        public static ushort StoreAtDE() {
            memory.WriteByte(A, DRp);
            return (ushort)(PC + 1);
        }

        public static ushort StoreHL(ushort address) {
            memory.WriteUShort(HRp, address);
            return (ushort)(PC + 3);
        }
        #endregion

        #region LOAD
        public static ushort LoadAFrom(ushort address) {
            A = memory.ReadByte(address);
            return (ushort)(PC + 3);
        }

        public static ushort LoadFromBC() {
            A = memory.ReadByte(BRp);
            return (ushort)(PC + 1);
        }

        public static ushort LoadFromDE() {
            A = memory.ReadByte(DRp);
            return (ushort)(PC + 1);
        }

        public static ushort LoadHL(ushort address) {
            HRp = memory.ReadUShort(address);
            return (ushort)(PC + 3);
        }
        #endregion

        #region CMP
        public static ushort CmpB() {
            Cpi(B);
            return (ushort)(PC + 1);
        }

        public static ushort CmpC() {
            Cpi(C);
            return (ushort)(PC + 1);
        }

        public static ushort CmpD() {
            Cpi(D);
            return (ushort)(PC + 1);
        }

        public static ushort CmpE() {
            Cpi(E);
            return (ushort)(PC + 1);
        }

        public static ushort CmpH() {
            Cpi(H);
            return (ushort)(PC + 1);
        }

        public static ushort CmpL() {
            Cpi(L);
            return (ushort)(PC + 1);
        }

        public static ushort CmpM() {
            Cpi(M);
            return (ushort)(PC + 1);
        }

        public static ushort CmpA() {
            Cpi(A);
            return (ushort)(PC + 1);
        }

        public static ushort Cpi(byte data) {
            if (data > A) {
                Flag.Carry.Set();
                Flag.Zero.Reset();
            } else if (data == A) {
                Flag.Carry.Reset();
                Flag.Zero.Set();
            } else { // data < A
                Flag.Carry.Reset();
                Flag.Zero.Reset();
            }
            return (ushort)(PC + 2);
        }
        #endregion

        #region JUMP
        public static ushort Jump(ushort address) {
            return address;
        }

        public static ushort JumpNZ(ushort address) {
            if (!Flag.Z.IsSet()) {
                return Jump(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort JumpZ(ushort address) {
            if (Flag.Z.IsSet()) {
                return Jump(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort JumpC(ushort address) {
            if (Flag.CY.IsSet()) {
               return Jump(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort JumpNC(ushort address) {
            if (!Flag.CY.IsSet()) {
                return Jump(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort JumpP(ushort address) {
            if (!Flag.S.IsSet()) {
                return Jump(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort JumpM(ushort address) {
            if (Flag.S.IsSet()) {
                return Jump(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort JumpPE(ushort address) {
            if (Flag.AC.IsSet()) {
                return Jump(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort JumpPO(ushort address) {
            if (!Flag.AC.IsSet()) {
                return Jump(address);
            }
            return (ushort)(PC + 3);
        }
        #endregion

        #region [STACK] PUSH
        public static ushort PushBRp() {
            SP -= 2;
            memory.WriteUShort(BRp, SP);
            return (ushort)(PC + 1);
        }
        public static ushort PushDRp() {
            SP -= 2;
            memory.WriteUShort(DRp, SP);
            return (ushort)(PC + 1);
        }
        public static ushort PushHRp() {
            SP -= 2;
            memory.WriteUShort(HRp, SP);
            return (ushort)(PC + 1);
        }
        public static ushort PushPSW() {
            SP -= 2;
            memory.WriteUShort((A, flags.ToByte()).ToUShort(), SP);
            return (ushort)(PC + 1);
        }
        #endregion

        #region [STACK] POP
        public static ushort PopBRp() {
            BRp = memory.ReadUShort(SP);
            SP += 2;
            return (ushort)(PC + 1);
        }
        public static ushort PopDRp() {
            DRp = memory.ReadUShort(SP);
            SP += 2;
            return (ushort)(PC + 1);
        }
        public static ushort PopHRp() {
            HRp = memory.ReadUShort(SP);
            SP += 2;
            return (ushort)(PC + 1);
        }
        public static ushort PopPSW() {
            var data = memory.ReadUShort(SP).ToBytes();
            A = data.HO;
            flags = data.LO.ToBitArray();
            SP += 2;
            return (ushort)(PC + 1);
        }
        #endregion

        #region ROTATE
        public static ushort RRC() {
            A = (byte)((A >> 1) + (A << 7));
            Flag.Carry.Set((A >> 7).ToBitBool());
            return (ushort)(PC + 1);
        }

        public static ushort RLC() {
            Flag.Carry.Set((A >> 7).ToBitBool());
            A = (byte)((A << 1) + (A >> 7));
            return (ushort)(PC + 1);
        }

        public static ushort RAL() {
            bool d7 = (A >> 7).ToBitBool();
            A = (byte)(Flag.Carry.IsSet().ToBitInt() + (A << 1));
            Flag.Carry.Set(d7);
            return (ushort)(PC + 1);
        }

        public static ushort RAR() {
            bool d0 = (A & 1).ToBitBool();
            A = (byte)((Flag.Carry.IsSet().ToBitInt() << 7) + (A >> 1));
            Flag.Carry.Set(d0);
            return (ushort)(PC + 1);
        }
        #endregion

        #region MISC
        public static ushort Exchange() {
            HRp += BRp;
            BRp = (ushort)(HRp - BRp);
            HRp -= BRp;
            return (ushort)(PC + 1);
        }

        public static ushort ComplA() {
            A = (byte)~A;
            return (ushort)(PC + 1);
        }

        public static ushort Nop() {
            System.Threading.Thread.Sleep(1000);
            return (ushort)(PC + 1);
        }

        public static ushort Halt() {
            running = false;
            return 0;
        }

        public static ushort ExPCwHL() {
            ushort hrp = HRp;
            HRp = memory.ReadUShort(stackPtr);
            memory.WriteUShort(hrp, stackPtr);
            return (ushort)(PC + 1);
        }

        public static ushort stPCtoHL() {
            return HRp;
        }

        public static ushort stSPtoHL() {
            SP = HRp;
            return (ushort)(PC + 1);
        }

        public static ushort setCY() {
            Flag.Carry.Set();
            return (ushort)(PC + 1);
        }

        public static ushort compCY() {
            Flag.Carry.Toggle();
            return (ushort)(PC + 1);
        }

        public static ushort Input(byte port) {
            string error = "";
            while (true) {
                try {
                    string str = Interaction.InputBox(error + "\nInput for port : " + port.ToString("X2") + "\nEnter hex value within range: [00 - FF]", "IN " + port.ToString("X2"), "00");
                    byte b = byte.Parse(str, System.Globalization.NumberStyles.HexNumber);
                    A = b;
                    break;
                } catch {
                    error = "Invalid Format!";
                }
            }
            return (ushort)(PC + 2);
        }

        public static ushort Output(byte port) {
            MessageBox.Show("Output for port : " + port.ToString("X2") + "\n" + A.ToString("X2"), "OUT " + port.ToString("X2"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return (ushort)(PC + 2);
        }

        #endregion

        #region CALL
        public static ushort Call(ushort data) {
            SP -= 2;
            memory.WriteUShort((ushort)(PC + 3), SP);
            return Jump(data);
        }
        public static ushort CallNZ(ushort address) {
            if (!Flag.Z.IsSet()) {
                return Call(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort CallZ(ushort address) {
            if (Flag.Z.IsSet()) {
                return Call(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort CallC(ushort address) {
            if (Flag.CY.IsSet()) {
                return Call(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort CallNC(ushort address) {
            if (!Flag.CY.IsSet()) {
                return Call(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort CallP(ushort address) {
            if (!Flag.S.IsSet()) {
                return Call(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort CallM(ushort address) {
            if (Flag.S.IsSet()) {
                return Call(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort CallPE(ushort address) {
            if (Flag.AC.IsSet()) {
                return Call(address);
            }
            return (ushort)(PC + 3);
        }
        public static ushort CallPO(ushort address) {
            if (!Flag.AC.IsSet()) {
                return Call(address);
            }
            return (ushort)(PC + 3);
        }
        #endregion

        #region RETURN
        public static ushort Return() {
            ushort data = memory.ReadUShort(SP);
            SP += 2;
            return Jump(data);
        }
        public static ushort ReturnNZ() {
            if (!Flag.Z.IsSet()) return Return();
            return (ushort)(PC + 1);
        }
        public static ushort ReturnZ() {
            if (Flag.Z.IsSet()) return Return();
            return (ushort)(PC + 1);
        }
        public static ushort ReturnC() {
            if (Flag.CY.IsSet()) return Return();
            return (ushort)(PC + 1);
        }
        public static ushort ReturnNC() {
            if (!Flag.CY.IsSet()) return Return();
            return (ushort)(PC + 1);
        }
        public static ushort ReturnP() {
            if (!Flag.S.IsSet()) return Return();
            return (ushort)(PC + 1);
        }
        public static ushort ReturnM() {
            if (Flag.S.IsSet()) return Return();
            return (ushort)(PC + 1);
        }
        public static ushort ReturnPE() {
            if (Flag.AC.IsSet()) return Return();
            return (ushort)(PC + 1);
        }
        public static ushort ReturnPO() {
            if (!Flag.AC.IsSet()) return Return();
            return (ushort)(PC + 1);
        }
        #endregion

        #region RST
        public static ushort Reset0() {
            Halt();
            return 0 * 0x0008;
        }
        public static ushort Reset1() {
            Halt();
            return 1 * 0x0008;
        }
        public static ushort Reset2() {
            Halt();
            return 2 * 0x0008;
        }
        public static ushort Reset3() {
            Halt();
            return 3 * 0x0008;
        }
        public static ushort Reset4() {
            Halt();
            return 4 * 0x0008;
        }
        public static ushort Reset5() {
            Halt();
            return 5 * 0x0008;
        }
        public static ushort Reset6() {
            Halt();
            return 6 * 0x0008;
        }
        public static ushort Reset7() {
            Halt();
            return 7 * 0x0008;
        }
        public static ushort EnInt() {
            interpt = true;
            return (ushort)(PC + 1);
        }
        public static ushort DisInt() {
            interpt = false;
            return (ushort)(PC + 1);
        }
        #endregion

        //TODO:RIM,SIM,DAA,DI,EI

        public static void NextStep() {
            Instruction ins = ((Instruction.OPCODE) memory.ReadByte(PC)).GetAttributeOfType<Instruction>();
            if (ins.Bytes > 1) {
                ins.Arguments.LO = memory.ReadByte((ushort)(PC + 1));
                ins.Arguments.HO = 0x00;
            }
            if (ins.Bytes > 2) {
                ins.Arguments.HO = memory.ReadByte((ushort)(PC + 2));
            }
            PC = ins.Execute();
        }

        public static void ExecuteAllSteps() {
            running = true;
            while (running) NextStep();
        }
        
        public static void Stop() {
            running = false;
        }

        public static void InitMemory(string name) {
            memName = name;
            memory = new Memory(memName);
        }

        public static void CommitMemory() {
            memory.Commit(memName);
        }
    }
}