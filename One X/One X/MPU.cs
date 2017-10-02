using System;
using System.Collections;

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

        internal static byte acc;
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

        internal static void Set(this Flag flag) => flag.Set(true);
        internal static void Reset(this Flag flag) => flag.Set(false);
        internal static void Set(this Flag flag, bool set) => flags.Set((byte)flag, set);
        internal static void Toggle(this Flag flag) => flags.Set((byte)flag, !flag.IsSet());
        internal static bool IsSet(this Flag flag) => flags.Get((byte)flag);

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
            get => (H, L).ToUShort();
            internal set => LoadHRp(value);
        }
        public static ushort BRp {
            get => (B, C).ToUShort();
            internal set => LoadBRp(value);
        }
        public static ushort DRp {
            get => (D, E).ToUShort();
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
        public static void LoadSP(ushort data) => stackPtr = data;
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
        public static void DadSP() => DadI(SP);

        private static void DadI(ushort data) {
            int res = HRp + data;
            Flag.Carry.Set(res > ushort.MaxValue);
            HRp = (ushort)res;
        }
        #endregion

        #region SUB
        public static void SubB() => Sui(regB);
        public static void SubC() => Sui(regC);
        public static void SubD() => Sui(regD);
        public static void SubE() => Sui(regE);
        public static void SubH() => Sui(regH);
        public static void SubL() => Sui(regL);
        public static void SubM() => Sui(regM);
        public static void SubA() => Sui(regA);

        public static void Sui(byte data) {
            int res = regA + regH.TwosComplement();
            Flag.Carry.Set(res <= byte.MaxValue);
            regA = (byte)res;
            // TODO Auxiliary Carry
        }
        #endregion

        #region ADC
        public static void AdcB() => Aci(regB);
        public static void AdcC() => Aci(regC);
        public static void AdcD() => Aci(regD);
        public static void AdcE() => Aci(regE);
        public static void AdcH() => Aci(regH);
        public static void AdcL() => Aci(regL);
        public static void AdcM() => Aci(regM);
        public static void AdcA() => Aci(regA);
        public static void Aci(byte data) {
            Adi(data);
            Adi((byte)Flag.Carry.IsSet().ToBitInt());
        }
        #endregion

        #region SBB
        public static void SbbB() => Sbi(regB);
        public static void SbbC() => Sbi(regC);
        public static void SbbD() => Sbi(regD);
        public static void SbbE() => Sbi(regE);
        public static void SbbH() => Sbi(regH);
        public static void SbbL() => Sbi(regL);
        public static void SbbM() => Sbi(regM);
        public static void SbbA() => Sbi(regA);
        public static void Sbi(byte data) => Sui((byte)(data + Flag.Carry.IsSet().ToBitInt()));
        #endregion

        #region INR
        public static void InrA() => regA++;
        public static void InrB() {
            regB++;
            Flag.Sign.Set(regB.IsNegative());
            Flag.Zero.Set(regB == 0);
            Flag.Parity.Set(regB.Parity());
        }
        public static void InrC() {
            regC++;
            Flag.Sign.Set(regC.IsNegative());
            Flag.Zero.Set(regC == 0);
            Flag.Parity.Set(regC.Parity());
        }
        public static void InrD() {
            regD++;
            Flag.Sign.Set(regD.IsNegative());
            Flag.Zero.Set(regD == 0);
            Flag.Parity.Set(regD.Parity());
        }
        public static void InrE() {
            regE++;
            Flag.Sign.Set(regE.IsNegative());
            Flag.Zero.Set(regE == 0);
            Flag.Parity.Set(regE.Parity());
        }
        public static void InrH() {
            regH++;
            Flag.Sign.Set(regH.IsNegative());
            Flag.Zero.Set(regH == 0);
            Flag.Parity.Set(regH.Parity());

        }
        public static void InrL() {
            regL++;
            Flag.Sign.Set(regL.IsNegative());
            Flag.Zero.Set(regL == 0);
            Flag.Parity.Set(regL.Parity());
        }
        #endregion

        #region DCR
        public static void DcrA() => regA--;
        public static void DcrB() {
            regB--;
            Flag.Sign.Set(regB.IsNegative());
            Flag.Zero.Set(regB == 0);
            Flag.Parity.Set(regB.Parity());
        }
        public static void DcrC() {
            regC--;
            Flag.Sign.Set(regC.IsNegative());
            Flag.Zero.Set(regC == 0);
            Flag.Parity.Set(regC.Parity());
        }
        public static void DcrD() {
            regD--;
            Flag.Sign.Set(regD.IsNegative());
            Flag.Zero.Set(regD == 0);
            Flag.Parity.Set(regD.Parity());
        }
        public static void DcrE() {
            regE--;
            Flag.Sign.Set(regE.IsNegative());
            Flag.Zero.Set(regE == 0);
            Flag.Parity.Set(regE.Parity());
        }
        public static void DcrH() {
            regH--;
            Flag.Sign.Set(regH.IsNegative());
            Flag.Zero.Set(regH == 0);
            Flag.Parity.Set(regH.Parity());
        }
        public static void DcrL() {
            regL--;
            Flag.Sign.Set(regL.IsNegative());
            Flag.Zero.Set(regL == 0);
            Flag.Parity.Set(regL.Parity());
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
                Flag.Carry.Set();
                Flag.Zero.Reset();
            } else if (data == regA) {
                Flag.Carry.Reset();
                Flag.Zero.Set();
            } else { // data < regA
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
            memory.WriteUShort((regA, flags.ToByte()).ToUShort(), SP);
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
            regA = data.HO;
            flags = data.LO.ToBitArray();
            SP += 2;
        }
        #endregion

        #region ROTATE
        public static void RRC() {
            regA = (byte)((regA >> 1) + (regA << 7));
            Flag.Carry.Set((regA >> 7).ToBitBool());
        }

        public static void RLC() {
            Flag.Carry.Set((regA >> 7).ToBitBool());
            regA = (byte)((regA << 1) + (regA >> 7));
        }

        public static void RAL() {
            bool d7 = (regA >> 7).ToBitBool();
            regA = (byte)(Flag.Carry.IsSet().ToBitInt() + (regA << 1));
            Flag.Carry.Set(d7);
        }

        public static void RAR() {
            bool d0 = (regA & 1).ToBitBool();
            regA = (byte)((Flag.Carry.IsSet().ToBitInt() << 7) + (regA >> 1));
            Flag.Carry.Set(d0);
        }
        #endregion

        #region MISC
        public static void Exchange() {
            HRp += BRp;
            BRp = (ushort)(HRp - BRp);
            HRp -= BRp;
        }

        public static void ComplA() => regA = (byte)~regA;

        public static void Nop() { /*sleep for 1000 ms */ }

        public static void Halt() {
            running = false;
            PC = 0x0000;
        } //TODO: HALT SIGNAL TO EXECUTOR
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

        //TODO:RIM,SIM,DAA,STC,CMC,IN,OUT,XTHL,PCHL,DI,SPHL,EI
    }
}

