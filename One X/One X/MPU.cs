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
        public static byte B
        {
            get
            {
                return regB;
            }
            set
            {
                regB = value;
            }
        }
        public static byte C
        {
            get
            {
                return regC;
            }
            set
            {
                regC = value;
            }
        }
        public static byte D
        {
            get
            {
                return regD;
            }
            set
            {
                regD = value;
            }
        }
        public static byte E
        {
            get
            {
                return regE;
            }
            set
            {
                regE = value;
            }
        }
        public static byte H
        {
            get
            {
                return regH;
            }
            set
            {
                regH = value;
            }
        }
        public static byte L
        {
            get
            {
                return regL;
            }
            set
            {
                regL = value;
            }
        }

        private static byte regA, regB, regC, regD, regE, regH, regL,regM;
        //lxi methods
        static void loadB(byte msb,byte lsb) {
            regB = msb;
            regC = lsb;
        }
       static void loadD(byte msb,byte lsb)
        {
            regD = msb;
            regE = lsb;
        }
        public static void loadH(byte msb,byte lsb)
        {
            regH = msb;
            regL = lsb;
        }
        //add flags

         static void None()
        {

        }
        //ADD
         static void addB()
        {
            regA = (byte)((int)regA + (int)regB);
        }
         static void addC()
        {
            regA = (byte)((int)regA + (int)regC);
        }
         static void addD()
        {
            regA = (byte)((int)regA + (int)regD);
        }
         static void addE()
        {
            regA = (byte)((int)regA + (int)regE);
        }
        public static void addH()
        {
            regA = (byte)((int)regA + (int)regH);
        }
        public static void addL()
        {
            regA = (byte)((int)regA + (int)regL);
        }
        public static void addM()
        {
            regA = (byte)((int)regA + (int)regM);
        }
        public static void addA()
        {
            regA = (byte)((int)regA + (int)regA);
        }
        //DAD
        public static void dadB()
        {
            regB = (byte)((int)regB + (int)regH);
            regC = (byte)((int)regC + (int)regL);//WHAT ABOUT CARRY?
        }
        public static void dadD()
        {
            regD = (byte)((int)regD + (int)regH);
            regE = (byte)((int)regE + (int)regL);//WHAT ABOUT CARRY?
        }
        public static void dadH()
        {
            regH = (byte)((int)regH + (int)regH);
            regL = (byte)((int)regL + (int)regL);//WHAT ABOUT CARRY?
        }
        //SUBTRACT
        public static void subB()
        {
            regA = (byte)((int)regA - (int)regB);
        }
        public static void subC()
        {
            regA = (byte)((int)regA - (int)regC);
        }
        public static void subD()
        {
            regA = (byte)((int)regA - (int)regD);
        }
        public static void subE()
        {
            regA = (byte)((int)regA - (int)regE);
        }
        public static void subH()
        {
            regA = (byte)((int)regA - (int)regH);
        }
        public static void subL()
        {
            regA = (byte)((int)regA - (int)regL);
        }
        public static void subM()
        {
            regA = (byte)((int)regA - (int)regM);
        }
        public static void subA()
        {
            regA = (byte)((int)regA - (int)regA);
        }
        //INCREMENT 
        public static void inrA()
        {
            regA = (byte)((int)regA + 1);
        }
        public static void inrB()
        {
            regB = (byte)((int)regB + 1);
        }
        public static void inrC()
        {
            regC = (byte)((int)regC + 1);
        }
        public static void inrD()
        {
            regD = (byte)((int)regD + 1);
        }
        public static void inrE()
        {
            regE = (byte)((int)regE + 1);
        }
        public static void inrH()
        {
            regH = (byte)((int)regH + 1);
        }
        public static void inrL()
        {
            regL = (byte)((int)regL + 1);
        }
        public static void inxB()
        {
            regB = (byte)((int)regB + 1);//AS PER INSTRUCTIONS
            regC = (byte)((int)regC + 1);
        }
        //DECREMENT
        public static void dcrA()
        {
            regA = (byte)((int)regA - 1);
        }
        public static void dcrB()
        {
            regB = (byte)((int)regB - 1);
        }
        public static void dcrC()
        {
            regC = (byte)((int)regC - 1);
        }
        public static void dcrD()
        {
            regD = (byte)((int)regD - 1);
        }
        public static void dcrE()
        {
            regE = (byte)((int)regE - 1);
        }
        public static void dcrH()
        {
            regH = (byte)((int)regH - 1);
        }
        public static void dcrL()
        {
            regL = (byte)((int)regL - 1);
        }
        //Move functions for B
        public static void moveBB()
        {
            regB = regB;
        }
        public static void moveBC()
        {
            regB = regC;
        }
        public static void moveBD()
        {
            regB = regD;
        }
        public static void moveBE()
        {
            regB = regE;
        }
        public static void moveBH()
        {
            regB = regH;
        }
        public static void moveBL()
        {
            regB = regL;
        }
        public static void moveBM()
        {
            regB = regM;
        }
        public static void moveBA()
        {
            regB = regA;
        }
        //Move functions for C
        public static void moveCB()
        {
            regC = regB;
        }
        public static void moveCC()
        {
            regC = regC;
        }
        public static void moveCD()
        {
            regC = regD;
        }
        public static void moveCE()
        {
            regC = regE;
        }
        public static void moveCH()
        {
            regC = regH;
        }
        public static void moveCL()
        {
            regC = regL;
        }
        public static void moveCM()
        {
            regC = regM;
        }
        public static void moveCA()
        {
            regC = regA;
        }
        //move functions for D
        public static void moveDB()
        {
            regD = regB;
        }
        public static void moveDC()
        {
            regD = regC;
        }
        public static void moveDD()
        {
            regD = regD;
        }
        public static void moveDE()
        {
            regD = regE;
        }
        public static void moveDH()
        {
            regD = regH;
        }
        public static void moveDL()
        {
            regD = regL;
        }
        public static void moveDM()
        {
            regD = regM;
        }
        public static void moveDA()
        {
            regD = regA;
        }
        //move functions for E
        public static void moveEB()
        {
            regE = regB;
        }
        public static void moveEC()
        {
            regE = regC;
        }
        public static void moveED()
        {
            regE = regD;
        }
        public static void moveEE()
        {
            regE = regE;
        }
        public static void moveEH()
        {
            regE = regH;
        }
        public static void moveEL()
        {
            regE = regL;
        }
        public static void moveEM()
        {
            regE = regM;
        }
        public static void moveEA()
        {
            regE = regA;
        }
        //move function for H
        public static void moveHB()
        {
            regH = regB;
        }
        public static void moveHC()
        {
            regH = regC;
        }
        public static void moveHD()
        {
            regH = regD;
        }
        public static void moveHE()
        {
            regH = regE;
        }
        public static void moveHH()
        {
            regH = regH;
        }
        public static void moveHL()
        {
            regH = regL;
        }
        public static void moveHM()
        {
            regH = regM;
        }
        public static void moveHA()
        {
            regH = regA;
        }
        //move function for M
        public static void moveMB()
        {
            regM = regB;
        }
        public static void moveMC()
        {
            regM = regC;
        }
        public static void moveMD()
        {
            regM = regD;
        }
        public static void moveME()
        {
            regM = regE;
        }
        public static void moveMH()
        {
            regM = regH;
        }
        public static void moveML()
        {
            regM = regL;
        }
        public static void moveMM()
        {
            regM = regM;
        }
        public static void moveMA()
        {
            regM = regA;
        }
        //move functions for A
        public static void moveAB()
        {
            regA = regB;
        }
        public static void moveAC()
        {
            regA = regC;
        }
        public static void moveAD()
        {
            regA = regD;
        }
        public static void moveAE()
        {
            regA = regE;
        }
        public static void moveAH()
        {
            regA = regH;
        }
        public static void moveAL()
        {
            regA = regL;
        }
        public static void moveAM()
        {
            regA = regM;
        }
        public static void moveAA()
        {
            regA = regA;
        }

        //halt
        public static void HALT()
        {
            //code to stop
        }
        public static void exhg()
        {
            Byte Temp = regD;
            regD = regE;
            regE = Temp;

            Temp = regH;
            regH = regL;
            regL = Temp;
        }
        public static void cmpB()
        {
            int a = regB.CompareTo(regA);
            if (a == 0)
            { //carry flag 
            }
            else if (a > 0)
            { //carry flag
            }
            else
            {  //carry flag
            }
        }
        //mvi functions
        public static void mviB(byte xx) { regB = xx; }
        public static void mviC(byte xx) { regC = xx; }
        public static void mviD(byte xx) { regD = xx; }
        public static void mviE(byte xx) { regD = xx; }
        public static void mviH(byte xx) { regH = xx; }
        public static void mviL(byte xx) { regL = xx; }
        public static void cpi(byte xx)
        {
            int a = xx.CompareTo(regA);
            if (a == 0)
            { //carry flag 
            }
            else if (a > 0)
            { //carry flag
            }
            else
            {  //carry flag
            }
        }
        public static void adi(byte xx) { regA = (byte)(regA + xx); }
    }
}

