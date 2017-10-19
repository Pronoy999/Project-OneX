using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace One_X {
    public class Instruction : Attribute {
        public static IEnumerable<OPCODE> opcodes = ((OPCODE[])Enum.GetValues(typeof(OPCODE)));
        public static IEnumerable<Instruction> list = opcodes.Select(x => x.GetAttributeOfType<Instruction>()).Where(x => !string.IsNullOrWhiteSpace(x.Name));

        public string Name;
        public byte Bytes;
        public byte MCycles;
        public byte TStates;
        
        public (byte HO, byte LO) Arguments;
        public MethodInfo method;

        public Instruction(string Name, byte Bytes, byte MCycles, byte TStates, MethodInfo method, (byte HO, byte LO) Arguments) {
            this.Name = Name;
            this.Bytes = Bytes;
            this.MCycles = MCycles;
            this.TStates = TStates;
            this.method = method;
            this.Arguments = Arguments;
        }

        public Instruction(string Name, byte Bytes, byte MCycles, byte TStates, string method) {
            this.Name = Name;
            this.Bytes = Bytes;
            this.MCycles = MCycles;
            this.TStates = TStates;

            if (!string.IsNullOrWhiteSpace(method)) {
                this.method = typeof(MPU).GetMethod(method);
            }
        }

        public OPCODE GetOPCODE() {
            return opcodes.First(x => x.GetAttributeOfType<Instruction>().Name == Name);
        }

        public ushort Execute() {
            object[] param = null;
            if (Bytes == 2) {
                param = new object[] { Arguments.LO };
            } else if (Bytes == 3) {
                param = new object[] { Arguments.ToUShort() };
            }
            return (ushort) method.Invoke(null, param);
        }

        public void WriteToMemory(Memory mem, ushort loc) {
            mem.WriteByte((byte)GetOPCODE(), loc++);
            if (Bytes == 2) {
                mem.WriteByte(Arguments.LO, loc++);
            } else if (Bytes == 3) {
                mem.WriteUShort(Arguments.ToUShort(), loc++);
            }
        }

        public override string ToString() {
            return "[" + Name + ", " + Bytes + ", " + MCycles + ", " + TStates + "]"; // include arguments
        }

        public enum OPCODE : byte {
            [Instruction("NOP", 1, 1, 4, "Nop")] NOP = 0x00,
            [Instruction("LXI B", 3, 3, 10, "LoadBRp")] LXI_B = 0x01, // comma
            [Instruction("STAX B", 1, 2, 7, "StoreAtBC")] STAX_B = 0x02,
            [Instruction("INX B", 1, 1, 6, "InxB")] INX_B = 0x03,
            [Instruction("INR B", 1, 1, 4, "InrB")] INR_B = 0x04,
            [Instruction("DCR B", 1, 1, 4, "DcrB")] DCR_B = 0x05,
            [Instruction("MVI B", 2, 2, 7, "LoadB")] MVI_B = 0x06, // comma
            [Instruction("RLC", 1, 1, 4, "RLC")] RLC = 0x07,
            [Instruction("", 0, 0, 0, "")] UNKN_08 = 0x08,
            [Instruction("DAD B", 1, 3, 10, "DadB")] DAD_B = 0x09,
            [Instruction("LDAX B", 1, 2, 7, "LoadFromBC")] LDAX_B = 0x0A,
            [Instruction("DCX B", 1, 1, 6, "DcxB")] DCX_B = 0x0B,
            [Instruction("INR C", 1, 1, 4, "InrC")] INR_C = 0x0C,
            [Instruction("DCR C", 1, 1, 4, "DcrC")] DCR_C = 0x0D,
            [Instruction("MVI C", 2, 2, 7, "LoadC")] MVI_C = 0x0E, // comma
            [Instruction("RRC", 1, 1, 4, "RRC")] RRC = 0x0F,
            [Instruction("", 0, 0, 0, "")] UNKN_10 = 0x10,
            [Instruction("LXI D", 3, 3, 10, "LoadDRp")] LXI_D = 0x11, // comma
            [Instruction("STAX D", 1, 2, 7, "StoreAtDE")] STAX_D = 0x12,
            [Instruction("INX D", 1, 1, 6, "InxD")] INX_D = 0x13,
            [Instruction("INR D", 1, 1, 4, "InrD")] INR_D = 0x14,
            [Instruction("DCR D", 1, 1, 4, "DcrD")] DCR_D = 0x15,
            [Instruction("MVI D", 2, 2, 7, "LoadD")] MVI_D = 0x16, // comma
            [Instruction("RAL", 1, 1, 4, "RAL")] RAL = 0x17,
            [Instruction("", 0, 0, 0, "")] UNKN_24 = 0x18,
            [Instruction("DAD D", 1, 3, 10, "DadD")] DAD_D = 0x19,
            [Instruction("LDAX D", 1, 2, 7, "LoadFromDE")] LDAX_D = 0x1A,
            [Instruction("DCX D", 1, 1, 6, "DcxD")] DCX_D = 0x1B,
            [Instruction("INR E", 1, 1, 4, "InrE")] INR_E = 0x1C,
            [Instruction("DCR E", 1, 1, 4, "DcrE")] DCR_E = 0x1D,
            [Instruction("MVI E", 2, 2, 7, "LoadE")] MVI_E = 0x1E, // comma
            [Instruction("RAR", 1, 1, 4, "RAR")] RAR = 0x1F,
            [Instruction("RIM", 1, 1, 4, "")] RIM = 0x20,
            [Instruction("LXI H", 3, 3, 10, "LoadHRp")] LXI_H = 0x21, // comma
            [Instruction("SHLD", 3, 5, 16, "StoreHL")] SHLD = 0x22,
            [Instruction("INX H", 1, 1, 6, "InxH")] INX_H = 0x23,
            [Instruction("INR H", 1, 1, 4, "InrH")] INR_H = 0x24,
            [Instruction("DCR H", 1, 1, 4, "DcrH")] DCR_H = 0x25,
            [Instruction("MVI H", 2, 2, 7, "LoadH")] MVI_H = 0x26, // comma
            [Instruction("DAA", 1, 1, 4, "")] DAA = 0x27,
            [Instruction("", 0, 0, 0, "")] UNKN_28 = 0x28,
            [Instruction("DAD H", 1, 3, 10, "DadH")] DAD_H = 0x29,
            [Instruction("LHLD", 3, 5, 16, "LoadHL")] LHLD = 0x2A,//Plese recheck
            [Instruction("DCX H", 1, 1, 6, "DcxH")] DCX_H = 0x2B,
            [Instruction("INR L", 1, 1, 4, "InrL")] INR_L = 0x2C,
            [Instruction("DCR L", 1, 1, 4, "DcrL")] DCR_L = 0x2D,
            [Instruction("MVI L", 2, 2, 7, "LoadL")] MVI_L = 0x2E, // comma
            [Instruction("CMA", 1, 1, 4, "ComplA")] CMA = 0x2F,//recheck
            [Instruction("SIM", 1, 1, 4, "")] SIM = 0x30,
            [Instruction("LXI SP", 3, 3, 10, "LoadSP")] LXI_SP = 0x31, // comma
            [Instruction("STA", 3, 4, 13, "StoreA")] STA = 0x32,
            [Instruction("INX SP", 1, 1, 6, "LoadSP")] INX_SP = 0x33,
            [Instruction("INR M", 1, 3, 10, "InrM")] INR_M = 0x34,
            [Instruction("DCR M", 1, 3, 10, "DcrM")] DCR_M = 0x35,
            [Instruction("MVI M", 2, 3, 10, "LoadM")] MVI_M = 0x36, // comma
            [Instruction("STC", 1, 1, 4, "SetCarry")] STC = 0x37,
            [Instruction("", 0, 0, 0, "")] UNKN_38 = 0x38,
            [Instruction("DAD SP", 1, 3, 10, "DadSP")] DAD_SP = 0x39,
            [Instruction("LDA", 3, 4, 13, "LoadAFrom")] LDA = 0x3A,
            [Instruction("DCX SP", 1, 1, 6, "DcxSP")] DCX_SP = 0x3B,
            [Instruction("INR A", 1, 1, 4, "InrA")] INR_A = 0x3C,
            [Instruction("DCR A", 1, 1, 4, "DcrA")] DCR_A = 0x3D,
            [Instruction("MVI A", 2, 2, 7, "LoadA")] MVI_A = 0x3E, // comma
            [Instruction("CMC", 1, 1, 4, "ComplCarry")] CMC = 0x3F,
            [Instruction("MOV B,B", 1, 1, 4, "MoveBB")] MOV_BB = 0x40,
            [Instruction("MOV B,C", 1, 1, 4, "MoveBC")] MOV_BC = 0x41,
            [Instruction("MOV B,D", 1, 1, 4, "MoveBD")] MOV_BD = 0x42,
            [Instruction("MOV B,E", 1, 1, 4, "MoveBE")] MOV_BE = 0x43,
            [Instruction("MOV B,H", 1, 1, 4, "MoveBH")] MOV_BH = 0x44,
            [Instruction("MOV B,L", 1, 1, 4, "MoveBL")] MOV_BL = 0x45,
            [Instruction("MOV B,M", 1, 2, 7, "MoveBM")] MOV_BM = 0x46,
            [Instruction("MOV B,A", 1, 1, 4, "MoveBA")] MOV_BA = 0x47,
            [Instruction("MOV C,B", 1, 1, 4, "MoveCB")] MOV_CB = 0x48,
            [Instruction("MOV C,C", 1, 1, 4, "MoveCC")] MOV_CC = 0x49,
            [Instruction("MOV C,D", 1, 1, 4, "MoveCD")] MOV_CD = 0x4A,
            [Instruction("MOV C,E", 1, 1, 4, "MoveCE")] MOV_CE = 0x4B,
            [Instruction("MOV C,H", 1, 1, 4, "MoveCH")] MOV_CH = 0x4C,
            [Instruction("MOV C,L", 1, 1, 4, "MoveCL")] MOV_CL = 0x4D,
            [Instruction("MOV C,M", 1, 2, 7, "MoveCM")] MOV_CM = 0x4E,
            [Instruction("MOV C,A", 1, 1, 4, "MoveCA")] MOV_CA = 0x4F,
            [Instruction("MOV D,B", 1, 1, 4, "MoveDB")] MOV_DB = 0x50,
            [Instruction("MOV D,C", 1, 1, 4, "MoveDC")] MOV_DC = 0x51,
            [Instruction("MOV D,D", 1, 1, 4, "MoveDD")] MOV_DD = 0x52,
            [Instruction("MOV D,E", 1, 1, 4, "MoveDE")] MOV_DE = 0x53,
            [Instruction("MOV D,H", 1, 1, 4, "MoveDH")] MOV_DH = 0x54,
            [Instruction("MOV D,L", 1, 1, 4, "MoveDL")] MOV_DL = 0x55,
            [Instruction("MOV D,M", 1, 2, 7, "MoveDM")] MOV_DM = 0x56,
            [Instruction("MOV D,A", 1, 1, 4, "MoveDA")] MOV_DA = 0x57,
            [Instruction("MOV E,B", 1, 1, 4, "MoveEB")] MOV_EB = 0x58,
            [Instruction("MOV E,C", 1, 1, 4, "MoveEC")] MOV_EC = 0x59,
            [Instruction("MOV E,D", 1, 1, 4, "MoveED")] MOV_ED = 0x5A,
            [Instruction("MOV E,E", 1, 1, 4, "MoveEE")] MOV_EE = 0x5B,
            [Instruction("MOV E,H", 1, 1, 4, "MoveEH")] MOV_EH = 0x5C,
            [Instruction("MOV E,L", 1, 1, 4, "MoveEL")] MOV_EL = 0x5D,
            [Instruction("MOV E,M", 1, 2, 7, "MoveEM")] MOV_EM = 0x5E,
            [Instruction("MOV E,A", 1, 1, 4, "MoveEA")] MOV_EA = 0x5F,
            [Instruction("MOV H,B", 1, 1, 4, "MoveHB")] MOV_HB = 0x60,
            [Instruction("MOV H,C", 1, 1, 4, "MoveHC")] MOV_HC = 0x61,
            [Instruction("MOV H,D", 1, 1, 4, "MoveHD")] MOV_HD = 0x62,
            [Instruction("MOV H,E", 1, 1, 4, "MoveHE")] MOV_HE = 0x63,
            [Instruction("MOV H,H", 1, 1, 4, "MoveHH")] MOV_HH = 0x64,
            [Instruction("MOV H,L", 1, 1, 4, "MoveHL")] MOV_HL = 0x65,
            [Instruction("MOV H,M", 1, 2, 7, "MoveHM")] MOV_HM = 0x66,
            [Instruction("MOV H,A", 1, 1, 4, "MoveHA")] MOV_HA = 0x67,
            [Instruction("MOV L,B", 1, 1, 4, "MoveLB")] MOV_LB = 0x68,
            [Instruction("MOV L,C", 1, 1, 4, "MoveLC")] MOV_LC = 0x69,
            [Instruction("MOV L,D", 1, 1, 4, "MoveLD")] MOV_LD = 0x6A,
            [Instruction("MOV L,E", 1, 1, 4, "MoveLE")] MOV_LE = 0x6B,
            [Instruction("MOV L,H", 1, 1, 4, "MoveLH")] MOV_LH = 0x6C,
            [Instruction("MOV L,L", 1, 1, 4, "MoveLL")] MOV_LL = 0x6D,
            [Instruction("MOV L,M", 1, 2, 7, "MoveLM")] MOV_LM = 0x6E,
            [Instruction("MOV L,A", 1, 1, 4, "MoveLA")] MOV_LA = 0x6F,
            [Instruction("MOV M,B", 1, 2, 7, "MoveMB")] MOV_MB = 0x70,
            [Instruction("MOV M,C", 1, 2, 7, "MoveMC")] MOV_MC = 0x71,
            [Instruction("MOV M,D", 1, 2, 7, "MoveMD")] MOV_MD = 0x72,
            [Instruction("MOV M,E", 1, 2, 7, "MoveME")] MOV_ME = 0x73,
            [Instruction("MOV M,H", 1, 2, 7, "MoveMH")] MOV_MH = 0x74,
            [Instruction("MOV M,L", 1, 2, 7, "MoveML")] MOV_ML = 0x75,
            [Instruction("HLT", 1, 2, 7, "Halt")] HLT = 0x76,
            [Instruction("MOV M,A", 1, 2, 7, "MoveMA")] MOV_MA = 0x77,
            [Instruction("MOV A,B", 1, 1, 4, "MoveAB")] MOV_AB = 0x78,
            [Instruction("MOV A,C", 1, 1, 4, "MoveAC")] MOV_AC = 0x79,
            [Instruction("MOV A,D", 1, 1, 4, "MoveAD")] MOV_AD = 0x7A,
            [Instruction("MOV A,E", 1, 1, 4, "MoveAE")] MOV_AE = 0x7B,
            [Instruction("MOV A,H", 1, 1, 4, "MoveAH")] MOV_AH = 0x7C,
            [Instruction("MOV A,L", 1, 1, 4, "MoveAL")] MOV_AL = 0x7D,
            [Instruction("MOV A,M", 1, 2, 7, "MoveAM")] MOV_AM = 0x7E,
            [Instruction("MOV A,A", 1, 1, 4, "MoveAA")] MOV_AA = 0x7F,
            [Instruction("ADD B", 1, 1, 4, "AddB")] ADD_B = 0x80,
            [Instruction("ADD C", 1, 1, 4, "AddC")] ADD_C = 0x81,
            [Instruction("ADD D", 1, 1, 4, "AddD")] ADD_D = 0x82,
            [Instruction("ADD E", 1, 1, 4, "AddE")] ADD_E = 0x83,
            [Instruction("ADD H", 1, 1, 4, "AddH")] ADD_H = 0x84,
            [Instruction("ADD L", 1, 1, 4, "AddL")] ADD_L = 0x85,
            [Instruction("ADD M", 1, 2, 7, "AddM")] ADD_M = 0x86,
            [Instruction("ADD A", 1, 1, 4, "AddA")] ADD_A = 0x87,
            [Instruction("ADC B", 1, 1, 4, "AdcB")] ADC_B = 0x88,
            [Instruction("ADC C", 1, 1, 4, "AdcC")] ADC_C = 0x89,
            [Instruction("ADC D", 1, 1, 4, "AdcD")] ADC_D = 0x8A,
            [Instruction("ADC E", 1, 1, 4, "AdcE")] ADC_E = 0x8B,
            [Instruction("ADC H", 1, 1, 4, "AdcH")] ADC_H = 0x8C,
            [Instruction("ADC L", 1, 1, 4, "AdcL")] ADC_L = 0x8D,
            [Instruction("ADC M", 1, 2, 7, "AdcM")] ADC_M = 0x8E,
            [Instruction("ADC A", 1, 1, 4, "AdcA")] ADC_A = 0x8F,
            [Instruction("SUB B", 1, 1, 4, "SubB")] SUB_B = 0x90,
            [Instruction("SUB C", 1, 1, 4, "SubC")] SUB_C = 0x91,
            [Instruction("SUB D", 1, 1, 4, "SubD")] SUB_D = 0x92,
            [Instruction("SUB E", 1, 1, 4, "SubE")] SUB_E = 0x93,
            [Instruction("SUB H", 1, 1, 4, "SubH")] SUB_H = 0x94,
            [Instruction("SUB L", 1, 1, 4, "SubL")] SUB_L = 0x95,
            [Instruction("SUB M", 1, 2, 7, "SubM")] SUB_M = 0x96,
            [Instruction("SUB A", 1, 1, 4, "SubA")] SUB_A = 0x97,
            [Instruction("SBB B", 1, 1, 4, "SbbB")] SBB_B = 0x98,
            [Instruction("SBB C", 1, 1, 4, "SbbC")] SBB_C = 0x99,
            [Instruction("SBB D", 1, 1, 4, "SbbD")] SBB_D = 0x9A,
            [Instruction("SBB E", 1, 1, 4, "SbbE")] SBB_E = 0x9B,
            [Instruction("SBB H", 1, 1, 4, "SbbH")] SBB_H = 0x9C,
            [Instruction("SBB L", 1, 1, 4, "SbbL")] SBB_L = 0x9D,
            [Instruction("SBB M", 1, 2, 7, "SbbM")] SBB_M = 0x9E,
            [Instruction("SBB A", 1, 1, 4, "SbbA")] SBB_A = 0x9F,
            [Instruction("ANA B", 1, 1, 4, "AndB")] ANA_B = 0xA0,
            [Instruction("ANA C", 1, 1, 4, "AndC")] ANA_C = 0xA1,
            [Instruction("ANA D", 1, 1, 4, "AndD")] ANA_D = 0xA2,
            [Instruction("ANA E", 1, 1, 4, "AndE")] ANA_E = 0xA3,
            [Instruction("ANA H", 1, 1, 4, "AndH")] ANA_H = 0xA4,
            [Instruction("ANA L", 1, 1, 4, "AndL")] ANA_L = 0xA5,
            [Instruction("ANA M", 1, 2, 7, "AndM")] ANA_M = 0xA6,
            [Instruction("ANA A", 1, 1, 4, "AndA")] ANA_A = 0xA7,
            [Instruction("XRA B", 1, 1, 4, "XorB")] XRA_B = 0xA8,
            [Instruction("XRA C", 1, 1, 4, "XorC")] XRA_C = 0xA9,
            [Instruction("XRA D", 1, 1, 4, "XorD")] XRA_D = 0xAA,
            [Instruction("XRA E", 1, 1, 4, "XorE")] XRA_E = 0xAB,
            [Instruction("XRA H", 1, 1, 4, "XorH")] XRA_H = 0xAC,
            [Instruction("XRA L", 1, 1, 4, "XorL")] XRA_L = 0xAD,
            [Instruction("XRA M", 1, 2, 7, "XorM")] XRA_M = 0xAE,
            [Instruction("XRA A", 1, 1, 4, "XorA")] XRA_A = 0xAF,
            [Instruction("ORA B", 1, 1, 4, "OrB")] ORA_B = 0xB0,
            [Instruction("ORA C", 1, 1, 4, "OrC")] ORA_C = 0xB1,
            [Instruction("ORA D", 1, 1, 4, "OrD")] ORA_D = 0xB2,
            [Instruction("ORA E", 1, 1, 4, "OrE")] ORA_E = 0xB3,
            [Instruction("ORA H", 1, 1, 4, "OrH")] ORA_H = 0xB4,
            [Instruction("ORA L", 1, 1, 4, "OrL")] ORA_L = 0xB5,
            [Instruction("ORA M", 1, 2, 7, "OrM")] ORA_M = 0xB6,
            [Instruction("ORA A", 1, 1, 4, "OrA")] ORA_A = 0xB7,
            [Instruction("CMP B", 1, 1, 4, "CmpB")] CMP_B = 0xB8,
            [Instruction("CMP C", 1, 1, 4, "CmpC")] CMP_C = 0xB9,
            [Instruction("CMP D", 1, 1, 4, "CmpD")] CMP_D = 0xBA,
            [Instruction("CMP E", 1, 1, 4, "CmpE")] CMP_E = 0xBB,
            [Instruction("CMP H", 1, 1, 4, "CmpH")] CMP_H = 0xBC,
            [Instruction("CMP L", 1, 1, 4, "CmpL")] CMP_L = 0xBD,
            [Instruction("CMP M", 1, 2, 7, "CmpM")] CMP_M = 0xBE,
            [Instruction("CMP A", 1, 1, 4, "CmpA")] CMP_A = 0xBF,
            [Instruction("RNZ", 1, 3, 10, "ReturnNZ")] RNZ = 0xC0,
            [Instruction("POP B", 1, 3, 10, "PopBRp")] POP_B = 0xC1,
            [Instruction("JNZ", 3, 3, 10, "JumpNZ")] JNZ = 0xC2,
            [Instruction("JMP", 3, 3, 10, "Jump")] JMP = 0xC3,
            [Instruction("CNZ", 3, 5, 18, "CallNZ")] CNZ = 0xC4,
            [Instruction("PUSH B", 1, 3, 12, "PushBRp")] PUSH_B = 0xC5,
            [Instruction("ADI", 2, 2, 7, "Adi")] ADI = 0xC6, // space
            [Instruction("RST 0", 1, 3, 12, "Reset0")] RST_0 = 0xC7,
            [Instruction("RZ", 1, 3, 10, "ReturnZ")] RZ = 0xC8,
            [Instruction("RET", 1, 3, 10, "Return")] RET = 0xC9,
            [Instruction("JZ", 3, 3, 10, "JumpZ")] JZ = 0xCA,
            [Instruction("", 0, 0, 0, "")] UNKN_CB = 0xCB,
            [Instruction("CZ", 3, 5, 18, "CallZ")] CZ = 0xCC,
            [Instruction("CALL", 3, 5, 18, "Call")] CALL = 0xCD,
            [Instruction("ACI", 2, 2, 7, "Aci")] ACI = 0xCE, // space
            [Instruction("RST 1", 1, 3, 12, "Reset1")] RST_1 = 0xCF,
            [Instruction("RNC", 1, 3, 10, "ReturnNC")] RNC = 0xD0,
            [Instruction("POP D", 1, 3, 10, "PopDRp")] POP_D = 0xD1,
            [Instruction("JNC", 3, 3, 10, "JumpNC")] JNC = 0xD2,
            [Instruction("OUT", 2, 3, 10, "Output")] OUT = 0xD3, // space
            [Instruction("CNC", 3, 5, 18, "CallNC")] CNC = 0xD4,
            [Instruction("PUSH D", 1, 3, 12, "PushDRp")] PUSH_D = 0xD5,
            [Instruction("SUI", 2, 2, 7, "Sui")] SUI = 0xD6, // space
            [Instruction("RST 2", 1, 3, 12, "Reset2")] RST_2 = 0xD7,
            [Instruction("RC", 1, 3, 10, "ReturnC")] RC = 0xD8,
            [Instruction("", 0, 0, 0, "")] UNKN_D9 = 0xD9,
            [Instruction("JC", 3, 3, 10, "JumpC")] JC = 0xDA,
            [Instruction("IN", 2, 3, 10, "Input")] IN = 0xDB, // space
            [Instruction("CC", 3, 5, 18, "CallC")] CC = 0xDC,
            [Instruction("", 0, 0, 0, "")] UNKN_0D = 0xDD,
            [Instruction("SBI", 2, 2, 7, "Sbi")] SBI = 0xDE, // space
            [Instruction("RST 3", 1, 3, 12, "Reset3")] RST_3 = 0xDF,
            [Instruction("RPO", 1, 3, 10, "ReturnPO")] RPO = 0xE0,
            [Instruction("POP H", 1, 3, 10, "PopHRp")] POP_H = 0xE1,
            [Instruction("JPO", 1, 3, 10, "JumpPO")] JPO = 0xE2,
            [Instruction("XTHL", 1, 5, 16, "ExSTwHL")] XTHL = 0xE3,
            [Instruction("CPO", 3, 5, 18, "CallPO")] CPO = 0xE4,
            [Instruction("PUSH H", 1, 3, 12, "PushHRp")] PUSH_H = 0xE5,
            [Instruction("ANI", 2, 2, 7, "Ani")] ANI = 0xE6, // space
            [Instruction("RST 4", 1, 3, 12, "Reset4")] RST_4 = 0xE7,
            [Instruction("RPE", 1, 3, 10, "ReturnPE")] RPE = 0xE8,
            [Instruction("PCHL", 1, 1, 6, "PCtoHL")] PCHL = 0xE9,
            [Instruction("JPE", 3, 3, 10, "JumpPE")] JPE = 0xEA, // space
            [Instruction("XCHG", 1, 1, 4, "Exchange")] XCHG = 0xEB,
            [Instruction("CPE", 3, 5, 18, "CallPE")] CPE = 0xEC, // space
            [Instruction("", 0, 0, 0, "")] UNKN_ED = 0xED,
            [Instruction("XRI", 2, 2, 7, "Xri")] XRI = 0xEE, // space
            [Instruction("RST 5", 1, 3, 12, "Reset5")] RST_5 = 0xEF,
            [Instruction("RP", 1, 3, 10, "ReturnP")] RP = 0xF0,
            [Instruction("POP PSW", 1, 3, 10, "PopPSW")] POP_PSW = 0xF1,
            [Instruction("JP", 3, 3, 10, "JumpP")] JP = 0xF2, // space
            [Instruction("DI", 1, 1, 4, "DisInt")] DI = 0xF3,
            [Instruction("CP", 3, 5, 18, "CallP")] CP = 0xF4, // space
            [Instruction("PUSH PSW", 1, 3, 12, "PushPSW")] PUSH_PSW = 0xF5,
            [Instruction("ORI", 2, 2, 7, "Ori")] ORI = 0xF6, // space
            [Instruction("RST 6", 1, 3, 12, "Reset6")] RST_6 = 0xF7,
            [Instruction("RM", 1, 3, 10, "ReturnM")] RM = 0xF8,
            [Instruction("SPHL", 1, 1, 6, "SPtoHL")] SPHL = 0xF9,
            [Instruction("JM", 3, 3, 10, "JumpM")] JM = 0xFA, // space
            [Instruction("EI", 1, 1, 4, "EnInt")] EI = 0xFB,
            [Instruction("CM", 3, 5, 18, "CallM")] CM = 0xFC, // space
            [Instruction("", 0, 0, 0, "")] UNKN_FD = 0xFD,
            [Instruction("CPI", 2, 2, 7, "Cpi")] CPI = 0xFE, // space
            [Instruction("RST 7", 1, 3, 12, "Reset7")] RST_7 = 0xFF
        }
    }
}
