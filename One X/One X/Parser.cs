using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace One_X
{
    /**<summary>
     * This is the Enum for the different parts of the Instructions. 
     * </summary>
     */
    class Parser {
        enum StringType {
            Label,
            Mnemonic,
            Literal,
            Error
        }        
        internal int startingAddress; // This is the staring address of the code. 
        internal List<Instruction> instructions = new List<Instruction>();  // The list containing the instructins without the label.
        internal Dictionary<int, String> labels = new Dictionary<int, string>();// The dictionary with the key as the memory address and the label as the value.        
        List<Tuple<StringType, int, int, int>> instructionList = new List<Tuple<StringType,int, int,  int>>();//StringType,lineIndex,ColIndex,Length.
        string regex14 = "^[0-9a-fA-F]{1,4}H?$";
        string regbex12 = "^[0-9a-fA-F]{1,2}H?$";
        public Parser(int startingAddress) {
            this.startingAddress = startingAddress;
        }
        /**<summary>
         * This is the method should be called using the object of the parser class. 
         * Create the object of the class and set the starting address. 
         * </summary>
         */
        public void parse(String code) {
            StringType type;
            int lineInd=0;
            int length;           
            int address = startingAddress;
            char[] newLine = { '\n' }; 
            char[] lineSeparator = { ':' };
            Boolean hasTwoColons = code.IndexOf(lineSeparator[0]) == code.LastIndexOf(lineSeparator[0]);
            if (!hasTwoColons) {
                String[] lines = code.Split(newLine);
                foreach (String line in lines) {
                    if (line.Contains(":")) {
                        String[] labelInst = line.Split(lineSeparator);
                        Instruction inst = Instruction.parse(labelInst[1].Trim());
                        if (!(inst.Name.Equals(String.Empty))) {
                            instructions.Add(inst);
                            labels.Add(address, labelInst[0].Trim());
                            address += inst.Bytes; //Increasing the Address Local Variable. 
                            instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Label, lineInd, 0, labelInst[0].Length));// Inserting the label. 
                            if (inst.Bytes >= 2)   // For 2 or 3 Byte Instructions Length = length+1;
                                length = inst.Name.Length + 1;
                            else length = inst.Name.Length;
                            if (inst.Bytes == 2) {
                                Regex reg = new Regex(regbex12, RegexOptions.Singleline);
                                try {
                                    String lit = labelInst[1].Substring(length).Trim();
                                    Match match = reg.Match(lit);
                                    instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Mnemonic, lineInd, labelInst[0].Length + 2, length));//Adding the Mneumonics
                                    if (match.Success) {                                        
                                        instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Literal, lineInd, length, lit.Length));//Adding the Literal.
                                    }
                                    else {
                                        instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Error, lineInd, length, lit.Length));
                                        //TODO: Throw Exception.
                                    }
                                }
                                catch (Exception e) {
                                    //TODO:Catch the Exception.
                                }
                            }
                            else if (inst.Bytes==3){
                                Regex reg = new Regex(regex14, RegexOptions.Singleline);
                                try {
                                    String lit=labelInst[1].Substring(length).Trim();
                                    Match match = reg.Match(lit);
                                    instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Mnemonic, lineInd, labelInst[0].Length + 2, length));//Adding the Mneumonics
                                    if (match.Success) {                                        
                                        instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Literal, lineInd, length, lit.Length));//Adding the Literal.
                                    }
                                    else {
                                        instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Error, lineInd,length, lit.Length)); //NO Match ERROR. 
                                    }
                                }
                                catch (Exception e) {
                                    //Handle the Regex No match Exception.
                                }
                            }
                        }
                        else {
                            instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Error, lineInd,labelInst[0].Length+2 , -1)); //ERROR for NoSuchInstruction. 
                        }
                    }
                    else {
                        Instruction inst = Instruction.parse(line.Trim());
                        if (!(inst.Name.Equals(String.Empty))) {
                            instructions.Add(inst);
                            address += inst.Bytes;
                            if (inst.Bytes >= 2) {
                                length = inst.Name.Length + 1;  //For 2 or 2 Byte Instructions. Length=length+1.
                            }
                            else length = inst.Name.Length;
                            String mneumonics = line.Substring(0, length).Trim();
                            String lit = line.Substring(length+1);
                            if (inst.Bytes == 2) {
                                Regex reg = new Regex(regbex12, RegexOptions.Singleline);
                                instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Mnemonic, lineInd, 0, length));//Adding the Mnemonics.
                                Match match = reg.Match(lit);
                                if (match.Success) {
                                    instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Literal, lineInd, length + 1, lit.Length)); //Adding the Literal. 
                                }
                                else {
                                    instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Error, lineInd, length+1, lit.Length)); //Putting ERROR. 
                                }
                            }
                            else if (inst.Bytes == 3) {
                                Regex reg = new Regex(regex14,RegexOptions.Singleline);
                                instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Mnemonic, lineInd, 0, length));
                                Match match = reg.Match(lit);
                                if (match.Success) {
                                    instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Literal, lineInd, length + 1, lit.Length)); //Adding the Literal. 
                                }
                                else {
                                    instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Error, lineInd, length + 1, lit.Length)); //Putting ERROR. 
                                }
                            }
                        }
                    }
                 lineInd++;                    
                }
            }
            else {
                instructionList.Add(Tuple.Create<StringType, int, int, int>(StringType.Error, lineInd, 0, -1));  // Putting the Error with more than One Colons. 
            }
        }
    }
}
