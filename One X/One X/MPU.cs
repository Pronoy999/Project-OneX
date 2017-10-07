using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections;
using System.Threading.Tasks;

namespace One_X {
    public static class MPU {
        private static bool running = false;

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
            int res = A + H.TwosComplement();
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
        public static void MoveAB() {
            LoadA(B);
        }

        public static void MoveAC() {
            LoadA(C);
        }

        public static void MoveAD() {
            LoadA(D);
        }

        public static void MoveAE() {
            LoadA(E);
        }

        public static void MoveAH() {
            LoadA(H);
        }

        public static void MoveAL() {
            LoadA(L);
        }

        public static void MoveAM() {
            LoadA(M);
        }

        public static void MoveAA() {
            LoadA(A);
        }
        #endregion

        #region AND
        public static void AndB() {
            Ani(B);
        }

        public static void AndC() {
            Ani(C);
        }

        public static void AndD() {
            Ani(D);
        }

        public static void AndE() {
            Ani(E);
        }

        public static void AndH() {
            Ani(H);
        }

        public static void AndL() {
            Ani(L);
        }

        public static void AndM() {
            Ani(M);
        }

        public static void AndA() {
            Ani(A);
        }

        public static void Ani(byte data) {
            //TODO:reset CY and set AC
            A &= data;
        }
        #endregion

        #region OR
        public static void OrB() {
            Ori(B);
        }

        public static void OrC() {
            Ori(C);
        }

        public static void OrD() {
            Ori(D);
        }

        public static void OrE() {
            Ori(E);
        }

        public static void OrH() {
            Ori(H);
        }

        public static void OrL() {
            Ori(L);
        }

        public static void OrM() {
            Ori(M);
        }

        public static void OrA() {
            Ori(A);
        }

        public static void Ori(byte data) {
            //TODO:Z,S,P are modified and AC AND CY are reset
            A |= data;
        }
        #endregion

        #region XOR
        public static void XorB() {
            Xri(B);
        }

        public static void XorC() {
            Xri(C);
        }

        public static void XorD() {
            Xri(D);
        }

        public static void XorE() {
            Xri(E);
        }

        public static void XorH() {
            Xri(H);
        }

        public static void XorL() {
            Xri(L);
        }

        public static void XorM() {
            Xri(M);
        }

        public static void XorA() {
            Xri(A);
        }

        public static void Xri(byte data) {
            //TODO:Z,S,P are modified and AC AND CY are reset
            A ^= data;
        }

        #endregion

        #region STORE
        public static void StoreA(ushort address) {
            memory.WriteByte(A, address);
        }

        public static void StoreAtBC() {
            memory.WriteByte(A, BRp);
        }

        public static void StoreAtDE() {
            memory.WriteByte(A, DRp);
        }

        public static void StoreHL(ushort address) {
            memory.WriteUShort(HRp, address);
        }
        #endregion

        #region LOAD
        public static void LoadAFrom(ushort address) {
            A = memory.ReadByte(address);
        }

        public static void LoadFromBC() {
            A = memory.ReadByte(BRp);
        }

        public static void LoadFromDE() {
            A = memory.ReadByte(DRp);
        }

        public static void LoadHL(ushort address) {
            HRp = memory.ReadUShort(address);
        }
        #endregion

        #region CMP
        public static void CmpB() {
            Cpi(B);
        }

        public static void CmpC() {
            Cpi(C);
        }

        public static void CmpD() {
            Cpi(D);
        }

        public static void CmpE() {
            Cpi(E);
        }

        public static void CmpH() {
            Cpi(H);
        }

        public static void CmpL() {
            Cpi(L);
        }

        public static void CmpM() {
            Cpi(M);
        }

        public static void CmpA() {
            Cpi(A);
        }

        public static void Cpi(byte data) {
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
        }
        #endregion

        #region JUMP
        public static void Jump(ushort address) {
            progCntr = address;
        }

        public static void JumpNZ(ushort address) {
            if (!Flag.Z.IsSet()) Jump(address);
        }
        public static void JumpZ(ushort address) {
            if (Flag.Z.IsSet()) Jump(address);
        }
        public static void JumpC(ushort address) {
            if (Flag.CY.IsSet()) Jump(address);
        }
        public static void JumpNC(ushort address) {
            if (!Flag.CY.IsSet()) Jump(address);
        }
        public static void JumpP(ushort address) {
            if (!Flag.S.IsSet()) Jump(address);
        }
        public static void JumpM(ushort address) {
            if (Flag.S.IsSet()) Jump(address);
        }
        public static void JumpPE(ushort address) {
            if (Flag.AC.IsSet()) Jump(address);
        }
        public static void JumpPO(ushort address) {
            if (!Flag.AC.IsSet()) Jump(address);
        }
        #endregion

        #region [STACK] PUSH
        public static void PushBRp() {
            SP -= 2;
            memory.WriteUShort(BRp, SP);
        }
        public static void PushDRp() {
            SP -= 2;
            memory.WriteUShort(DRp, SP);
        }
        public static void PushHRp() {
            SP -= 2;
            memory.WriteUShort(HRp, SP);
        }
        public static void PushPSW() {
            SP -= 2;
            memory.WriteUShort((A, flags.ToByte()).ToUShort(), SP);
        }
        #endregion

        #region [STACK] POP
        public static void PopBRp() {
            BRp = memory.ReadUShort(SP);
            SP += 2;
        }
        public static void PopDRp() {
            DRp = memory.ReadUShort(SP);
            SP += 2;
        }
        public static void PopHRp() {
            HRp = memory.ReadUShort(SP);
            SP += 2;
        }
        public static void PopPSW() {
            var data = memory.ReadUShort(SP).ToBytes();
            A = data.HO;
            flags = data.LO.ToBitArray();
            SP += 2;
        }
        #endregion

        #region ROTATE
        public static void RRC() {
            A = (byte)((A >> 1) + (A << 7));
            Flag.Carry.Set((A >> 7).ToBitBool());
        }

        public static void RLC() {
            Flag.Carry.Set((A >> 7).ToBitBool());
            A = (byte)((A << 1) + (A >> 7));
        }

        public static void RAL() {
            bool d7 = (A >> 7).ToBitBool();
            A = (byte)(Flag.Carry.IsSet().ToBitInt() + (A << 1));
            Flag.Carry.Set(d7);
        }

        public static void RAR() {
            bool d0 = (A & 1).ToBitBool();
            A = (byte)((Flag.Carry.IsSet().ToBitInt() << 7) + (A >> 1));
            Flag.Carry.Set(d0);
        }
        #endregion

        #region MISC
        public static void Exchange() {
            HRp += BRp;
            BRp = (ushort)(HRp - BRp);
            HRp -= BRp;
        }

        public static void ComplA() {
            A = (byte)~A;
        }

        public static void Nop() { System.Threading.Thread.Sleep(1000); }

        public static void Halt() {
            running = false;
            PC = 0x0000;
        }

        public static void ExPCwHL() {
            ushort hrp = HRp;
            HRp = memory.ReadUShort(stackPtr);
            memory.WriteUShort(hrp, stackPtr);
        }

        public static void stPCtoHL() {
            progCntr = HRp;
        }

        public static void stSPtoHL() {
            stackPtr = HRp;
        }

        public static void setCY() {
            Flag.Carry.Set();
        }

        public static void compCY() {
            Flag.Carry.Toggle();
        }

        public static void Input(byte port) {
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
        }

        public static void Output(byte port) {
            MessageBox.Show("Output for port : " + port.ToString("X2") + "\n" + A.ToString("X2"), "OUT " + port.ToString("X2"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region CALL

        public static void Call(ushort data) {
            SP -= 2;
            memory.WriteUShort(PC, SP);
            Jump(data);
        }
        public static void CallNZ(ushort address) {
            if (!Flag.Z.IsSet()) Call(address);
        }
        public static void CallZ(ushort address) {
            if (Flag.Z.IsSet()) Call(address);
        }
        public static void CallC(ushort address) {
            if (Flag.CY.IsSet()) Call(address);
        }
        public static void CallNC(ushort address) {
            if (!Flag.CY.IsSet()) Call(address);
        }
        public static void CallP(ushort address) {
            if (!Flag.S.IsSet()) Call(address);
        }
        public static void CallM(ushort address) {
            if (Flag.S.IsSet()) Call(address);
        }
        public static void CallPE(ushort address) {
            if (Flag.AC.IsSet()) Call(address);
        }
        public static void CallPO(ushort address) {
            if (!Flag.AC.IsSet()) Call(address);
        }
        #endregion

        #region RETURN
        public static void Return() {
            ushort data = memory.ReadUShort(SP);
            SP += 2;
            Jump(data);
        }
        public static void ReturnNZ() {
            if (!Flag.Z.IsSet()) Return();
        }
        public static void ReturnZ() {
            if (Flag.Z.IsSet()) Return();
        }
        public static void ReturnC() {
            if (Flag.CY.IsSet()) Return();
        }
        public static void ReturnNC() {
            if (!Flag.CY.IsSet()) Return();
        }
        public static void ReturnP() {
            if (!Flag.S.IsSet()) Return();
        }
        public static void ReturnM() {
            if (Flag.S.IsSet()) Return();
        }
        public static void ReturnPE() {
            if (Flag.AC.IsSet()) Return();
        }
        public static void ReturnPO() {
            if (!Flag.AC.IsSet()) Return();
        }
        #endregion

        #region RST
        public static void Reset0() {
            Halt();
            PC = 0 * 0x0008;
        }
        public static void Reset1() {
            Halt();
            PC = 1 * 0x0008;
        }
        public static void Reset2() {
            Halt();
            PC = 2 * 0x0008;
        }
        public static void Reset3() {
            Halt();
            PC = 3 * 0x0008;
        }
        public static void Reset4() {
            Halt();
            PC = 4 * 0x0008;
        }
        public static void Reset5() {
            Halt();
            PC = 5 * 0x0008;
        }
        public static void Reset6() {
            Halt();
            PC = 6 * 0x0008;
        }
        public static void Reset7() {
            Halt();
            PC = 7 * 0x0008;
        }
        #endregion
        
        //TODO:RIM,SIM,DAA,DI,EI

        public static void NextStep() {
            Instruction ins = ((Instruction.OPCODE) memory.ReadByte(PC++)).GetAttributeOfType<Instruction>();
            if (ins.Bytes > 1) {
                ins.Arguments.LO = memory.ReadByte(PC++);
                ins.Arguments.HO = 0x00;
            }
            if (ins.Bytes > 2) {
                ins.Arguments.HO = memory.ReadByte(PC++);
            }
            ins.Execute();
        }

        public static void ExecuteAllSteps() {
            running = true;
            while (running) NextStep();
        }
        
    }
}