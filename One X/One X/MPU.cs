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
        public static void LoadB(byte data) => B = data;
        public static void LoadC(byte data) => C = data;
        public static void LoadD(byte data) => D = data;
        public static void LoadE(byte data) => E = data;
        public static void LoadH(byte data) => H = data;
        public static void LoadL(byte data) => L = data;
        public static void LoadM(byte data) => M = data;
        public static void LoadA(byte data) => A = data;
        #endregion

        #region LXI
        public static void LoadBRp(ushort data) {
            var d = data.ToBytes();
            B = d.HO;
            C = d.LO;
        }
        public static void LoadDRp(ushort data) {
            var d = data.ToBytes();
            D = d.HO;
            E = d.LO;
        }
        public static void LoadHRp(ushort data) {
            var d = data.ToBytes();
            H = d.HO;
            L = d.LO;
        }
        public static void LoadSP(ushort data) => stackPtr = data;
        #endregion

        #region ADD
        public static void AddB() => Adi(B);
        public static void AddC() => Adi(C);
        public static void AddD() => Adi(D);
        public static void AddE() => Adi(E);
        public static void AddH() => Adi(H);
        public static void AddL() => Adi(L);
        public static void AddM() => Adi(M);
        public static void AddA() => Adi(A);

        public static void Adi(byte data) {
            int res = A + data;
            Flag.Carry.Set(res > byte.MaxValue);
            // TODO Set AuxiliaryCarry
            A = (byte)res;
        }
        #endregion

        #region DAD
        public static void DadB() => DadI(BRp);
        public static void DadD() => DadI(DRp);
        public static void DadH() => DadI(HRp);
        public static void DadSP() => DadI(SP);

        private static void DadI(ushort data) {
            int res = HRp + data;
            Flag.Carry.Set(res > ushort.MaxValue);
            HRp = (ushort)res;
        }
        #endregion

        #region SUB
        public static void SubB() => Sui(B);
        public static void SubC() => Sui(C);
        public static void SubD() => Sui(D);
        public static void SubE() => Sui(E);
        public static void SubH() => Sui(H);
        public static void SubL() => Sui(L);
        public static void SubM() => Sui(M);
        public static void SubA() => Sui(A);

        public static void Sui(byte data) {
            int res = A + H.TwosComplement();
            Flag.Carry.Set(res <= byte.MaxValue);
            A = (byte)res;
            // TODO Auxiliary Carry
        }
        #endregion

        #region ADC
        public static void AdcB() => Aci(B);
        public static void AdcC() => Aci(C);
        public static void AdcD() => Aci(D);
        public static void AdcE() => Aci(E);
        public static void AdcH() => Aci(H);
        public static void AdcL() => Aci(L);
        public static void AdcM() => Aci(M);
        public static void AdcA() => Aci(A);
        public static void Aci(byte data) {
            Adi(data);
            Adi((byte)Flag.Carry.IsSet().ToBitInt());
        }
        #endregion

        #region SBB
        public static void SbbB() => Sbi(B);
        public static void SbbC() => Sbi(C);
        public static void SbbD() => Sbi(D);
        public static void SbbE() => Sbi(E);
        public static void SbbH() => Sbi(H);
        public static void SbbL() => Sbi(L);
        public static void SbbM() => Sbi(M);
        public static void SbbA() => Sbi(A);
        public static void Sbi(byte data) => Sui((byte)(data + Flag.Carry.IsSet().ToBitInt()));
        #endregion

        #region INR
        public static void InrA() => A++;
        public static void InrB() {
            B++;
            Flag.Sign.Set(B.IsNegative());
            Flag.Zero.Set(B == 0);
            Flag.Parity.Set(B.Parity());
        }
        public static void InrC() {
            C++;
            Flag.Sign.Set(C.IsNegative());
            Flag.Zero.Set(C == 0);
            Flag.Parity.Set(C.Parity());
        }
        public static void InrD() {
            D++;
            Flag.Sign.Set(D.IsNegative());
            Flag.Zero.Set(D == 0);
            Flag.Parity.Set(D.Parity());
        }
        public static void InrE() {
            E++;
            Flag.Sign.Set(E.IsNegative());
            Flag.Zero.Set(E == 0);
            Flag.Parity.Set(E.Parity());
        }
        public static void InrH() {
            H++;
            Flag.Sign.Set(H.IsNegative());
            Flag.Zero.Set(H == 0);
            Flag.Parity.Set(H.Parity());

        }
        public static void InrL() {
            L++;
            Flag.Sign.Set(L.IsNegative());
            Flag.Zero.Set(L == 0);
            Flag.Parity.Set(L.Parity());
        }
        #endregion

        #region DCR
        public static void DcrA() => A--;
        public static void DcrB() {
            B--;
            Flag.Sign.Set(B.IsNegative());
            Flag.Zero.Set(B == 0);
            Flag.Parity.Set(B.Parity());
        }
        public static void DcrC() {
            C--;
            Flag.Sign.Set(C.IsNegative());
            Flag.Zero.Set(C == 0);
            Flag.Parity.Set(C.Parity());
        }
        public static void DcrD() {
            D--;
            Flag.Sign.Set(D.IsNegative());
            Flag.Zero.Set(D == 0);
            Flag.Parity.Set(D.Parity());
        }
        public static void DcrE() {
            E--;
            Flag.Sign.Set(E.IsNegative());
            Flag.Zero.Set(E == 0);
            Flag.Parity.Set(E.Parity());
        }
        public static void DcrH() {
            H--;
            Flag.Sign.Set(H.IsNegative());
            Flag.Zero.Set(H == 0);
            Flag.Parity.Set(H.Parity());
        }
        public static void DcrL() {
            L--;
            Flag.Sign.Set(L.IsNegative());
            Flag.Zero.Set(L == 0);
            Flag.Parity.Set(L.Parity());
        }
        #endregion

        #region INX
        public static void InxB() => BRp++;
        public static void InxD() => DRp++;
        public static void InxH() => HRp++;
        public static void InxSP() => SP++;
        #endregion

        #region DCX
        public static void DcxB() => BRp--;
        public static void DcxD() => DRp--;
        public static void DcxH() => HRp--;
        public static void DcxSP() => SP--;
        #endregion

        #region MOV B
        public static void MoveBB() => LoadB(B);
        public static void MoveBC() => LoadB(C);
        public static void MoveBD() => LoadB(D);
        public static void MoveBE() => LoadB(E);
        public static void MoveBH() => LoadB(H);
        public static void MoveBL() => LoadB(L);
        public static void MoveBM() => LoadB(M);
        public static void MoveBA() => LoadB(A);
        #endregion

        #region MOV C
        public static void MoveCB() => LoadC(B);
        public static void MoveCC() => LoadC(C);
        public static void MoveCD() => LoadC(D);
        public static void MoveCE() => LoadC(E);
        public static void MoveCH() => LoadC(H);
        public static void MoveCL() => LoadC(L);
        public static void MoveCM() => LoadC(M);
        public static void MoveCA() => LoadC(A);
        #endregion

        #region MOV D
        public static void MoveDB() => LoadD(B);
        public static void MoveDC() => LoadD(C);
        public static void MoveDD() => LoadD(D);
        public static void MoveDE() => LoadD(E);
        public static void MoveDH() => LoadD(H);
        public static void MoveDL() => LoadD(L);
        public static void MoveDM() => LoadD(M);
        public static void MoveDA() => LoadD(A);
        #endregion

        #region MOV E
        public static void MoveEB() => LoadE(B);
        public static void MoveEC() => LoadE(C);
        public static void MoveED() => LoadE(D);
        public static void MoveEE() => LoadE(E);
        public static void MoveEH() => LoadE(H);
        public static void MoveEL() => LoadE(L);
        public static void MoveEM() => LoadE(M);
        public static void MoveEA() => LoadE(A);
        #endregion

        #region MOV H
        public static void MoveHB() => LoadH(B);
        public static void MoveHC() => LoadH(C);
        public static void MoveHD() => LoadH(D);
        public static void MoveHE() => LoadH(E);
        public static void MoveHH() => LoadH(H);
        public static void MoveHL() => LoadH(L);
        public static void MoveHM() => LoadH(M);
        public static void MoveHA() => LoadH(A);
        #endregion

        #region MOV L
        public static void MoveLB() => LoadL(B);
        public static void MoveLC() => LoadL(C);
        public static void MoveLD() => LoadL(D);
        public static void MoveLE() => LoadL(E);
        public static void MoveLH() => LoadL(H);
        public static void MoveLL() => LoadL(L);
        public static void MoveLM() => LoadL(M);
        public static void MoveLA() => LoadL(A);
        #endregion

        #region MOV M
        public static void MoveMB() => LoadM(B);
        public static void MoveMC() => LoadM(C);
        public static void MoveMD() => LoadM(D);
        public static void MoveME() => LoadM(E);
        public static void MoveMH() => LoadM(H);
        public static void MoveML() => LoadM(L);
        public static void MoveMM() => LoadM(M);
        public static void MoveMA() => LoadM(A);
        #endregion

        #region MOV A
        public static void MoveAB() => LoadA(B);
        public static void MoveAC() => LoadA(C);
        public static void MoveAD() => LoadA(D);
        public static void MoveAE() => LoadA(E);
        public static void MoveAH() => LoadA(H);
        public static void MoveAL() => LoadA(L);
        public static void MoveAM() => LoadA(M);
        public static void MoveAA() => LoadA(A);
        #endregion

        #region AND
        public static void AndB() => Ani(B);
        public static void AndC() => Ani(C);
        public static void AndD() => Ani(D);
        public static void AndE() => Ani(E);
        public static void AndH() => Ani(H);
        public static void AndL() => Ani(L);
        public static void AndM() => Ani(M);
        public static void AndA() => Ani(A);

        public static void Ani(byte data) {
            //TODO:reset CY and set AC
            A &= data;
        }
        #endregion

        #region OR
        public static void OrB() => Ori(B);
        public static void OrC() => Ori(C);
        public static void OrD() => Ori(D);
        public static void OrE() => Ori(E);
        public static void OrH() => Ori(H);
        public static void OrL() => Ori(L);
        public static void OrM() => Ori(M);
        public static void OrA() => Ori(A);
        public static void Ori(byte data) {
            //TODO:Z,S,P are modified and AC AND CY are reset
            A |= data;
        }
        #endregion

        #region XOR
        public static void XorB() => Xri(B);
        public static void XorC() => Xri(C);
        public static void XorD() => Xri(D);
        public static void XorE() => Xri(E);
        public static void XorH() => Xri(H);
        public static void XorL() => Xri(L);
        public static void XorM() => Xri(M);
        public static void XorA() => Xri(A);
        public static void Xri(byte data) {
            //TODO:Z,S,P are modified and AC AND CY are reset
            A ^= data;
        }

        #endregion

        #region STORE
        public static void StoreA(ushort address) => memory.WriteByte(A, address);
        public static void StoreAtBC() => memory.WriteByte(A, BRp);
        public static void StoreAtDE() => memory.WriteByte(A, DRp);
        public static void StoreHL(ushort address) => memory.WriteUShort(HRp, address);
        #endregion

        #region LOAD
        public static void LoadAFrom(ushort address) => A = memory.ReadByte(address);
        public static void LoadFromBC() => A = memory.ReadByte(BRp);
        public static void LoadFromDE() => A = memory.ReadByte(DRp);
        public static void LoadHL(ushort address) => HRp = memory.ReadUShort(address);
        #endregion

        #region CMP
        public static void CmpB() => Cpi(B);
        public static void CmpC() => Cpi(C);
        public static void CmpD() => Cpi(D);
        public static void CmpE() => Cpi(E);
        public static void CmpH() => Cpi(H);
        public static void CmpL() => Cpi(L);
        public static void CmpM() => Cpi(M);
        public static void CmpA() => Cpi(A);
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
        public static void Jump(ushort address) => progCntr = address;
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

        public static void ComplA() => A = (byte)~A;

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

        public static void stPCtoHL() => progCntr = HRp;

        public static void stSPtoHL() => stackPtr = HRp;

        public static void setCY() => Flag.Carry.Set();

        public static void compCY() => Flag.Carry.Toggle();

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

