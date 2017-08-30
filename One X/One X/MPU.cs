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
        static void loadB(byte x) {
            regB = x;
        }
       static void loadD(byte x)
        {
            regD = x;
        }
        public static void loadH(byte x)
        {
            regH = x;
        }
        //add flags

         static void None()
        {

        }
        //ADD
         static void addB()
        {
            regA = (byte)(regA + regB);
        }
         static void addC()
        {
            regA = (byte)(regA + regC);
        }
         static void addD()
        {
            regA = (byte)(regA + regD);
        }
         static void addE()
        {
            regA = (byte)(regA + regE);
        }
        public static void addH()
        {
            regA = (byte)(regA + regH);
        }
        public static void addL()
        {
            regA = (byte)(regA + regL);
        }
        public static void addM()
        {
            regA = (byte)(regA + regM);
        }
        public static void addA()
        {
            regA = (byte)(regA + regA);
        }
        //DAD
        public static void dadB()
        {
            regB = (byte)(regB + regH);
            regC = (byte)(regC + regL);//WHAT ABOUT CARRY?
        }
        public static void dadD()
        {
            regD = (byte)(regD + regH);
            regE = (byte)(regE + regL);//WHAT ABOUT CARRY?
        }
        public static void dadH()
        {
            regH = (byte)(regH + regH);
            regL = (byte)(regL + regL);//WHAT ABOUT CARRY?
        }
        //SUBTRACT
        public static void subB()
        {
            regA = (byte)(regA - regB);
        }
        public static void subC()
        {
            regA = (byte)(regA - regC);
        }
        public static void subD()
        {
            regA = (byte)(regA - regD);
        }
        public static void subE()
        {
            regA = (byte)(regA - regE);
        }
        public static void subH()
        {
            regA = (byte)(regA - regH);
        }
        public static void subL()
        {
            regA = (byte)(regA - regL);
        }
        public static void subM()
        {
            regA = (byte)(regA - regM);
        }
        public static void subA()
        {
            regA = (byte)(regA - regA);
        }
        //INCREMENT 
        public static void inrA()
        {
            regA = (byte)(regA + 1);
        }
        public static void inrB()
        {
            regB = (byte)(regB + 1);
        }
        public static void inrC()
        {
            regC = (byte)(regC + 1);
        }
        public static void inrD()
        {
            regD = (byte)(regD + 1);
        }
        public static void inrE()
        {
            regE = (byte)(regE + 1);
        }
        public static void inrH()
        {
            regH = (byte)(regH + 1);
        }
        public static void inrL()
        {
            regL = (byte)(regL + 1);
        }
        public static void inxB()
        {
            regB = (byte)(regB + 1);//AS PER INSTRUCTIONS
            regC = (byte)(regC + 1);
        }
        //DECREMENT
        public static void dcrA()
        {
            regA = (byte)(regA - 1);
        }
        public static void dcrB()
        {
            regB = (byte)(regB - 1);
        }
        public static void dcrC()
        {
            regC = (byte)(regC - 1);
        }
        public static void dcrD()
        {
            regD = (byte)(regD - 1);
        }
        public static void dcrE()
        {
            regE = (byte)(regE - 1);
        }
        public static void dcrH()
        {
            regH = (byte)(regH - 1);
        }
        public static void dcrL()
        {
            regL = (byte)(regL - 1);
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

    }
}

