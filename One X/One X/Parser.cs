using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace One_X {
    /**<summary>
     * This is the Enum for the different parts of the Instructions. 
     * </summary>
     */
    public class Parser {
        public enum StringType {
            Label,
            Mnemonic,
            Literal,
            Error
        }
        internal int startingAddress; // This is the staring address of the code. 
        internal List<Instruction> instructions = new List<Instruction>();  // The list containing the instructins without the label.
        internal Dictionary<int, string> labels = new Dictionary<int, string>();// The dictionary with the key as the memory address and the label as the value.        

        string regex14 = "^[0-9a-fA-F]{1,4}H?$";
        string regbex12 = "^[0-9a-fA-F]{1,2}H?$";
        string regexRightLabel = "l1"; //TODO: Regex for Right label to be added. 
        /**<summary>
         * This is the Constructor of the Parser Class to
         * initialize the Starting address from the UI. 
         * </summary>
         * */
        public Parser(int startingAddress) {
            this.startingAddress = startingAddress;
        }

        public Parser() : this(0) { }  //Default Constructor. 

        /**<summary>
         * This is the method should be called using the object of the parser class. 
         * Create the object of the class and set the starting address. 
         * </summary>
         */
        public List<(StringType SType, int LineIndex, int ColIndex, int Length)> Parse(string code) {
            int lineInd = 0;
            int instructionLength;
            int address = startingAddress;
            char[] newLine = { '\n' };
            char[] lineSeparator = { ':' };

            var instructionList = new List<(StringType SType, int LineIndex, int ColIndex, int Length)>();

            // Checking more than One Colons in the line.
            string[] lines = code.Split(newLine);
            foreach (string line in lines) {
                if (line.Contains(":")) {
                    bool hasOneColon = code.IndexOf(lineSeparator[0]) == code.LastIndexOf(lineSeparator[0]);
                    if (hasOneColon) {
                        string[] labelInst = line.Split(lineSeparator);
                        Instruction inst = Instruction.parse(labelInst[1].Trim());
                        if (!string.IsNullOrWhiteSpace(inst.Name)) {
                            instructions.Add(inst);
                            labels.Add(address, labelInst[0].Trim());
                            address += inst.Bytes; //Increasing the Address Local Variable. 
                            instructionList.Add((StringType.Label, lineInd, 0, labelInst[0].Length));// Inserting the label. 
                            if (inst.Bytes >= 2)   // For 2 or 3 Byte Instructions Length = length+1;
                                instructionLength = inst.Name.Length + 1;
                            else instructionLength = inst.Name.Length;
                            if (inst.Bytes == 1) {
                                instructionList.Add((StringType.Mnemonic, lineInd, labelInst[0].Length + 1, instructionLength));//Adding the Mneumonics.                                
                            }
                            else if (inst.Bytes == 2) {
                                Regex reg = new Regex(regbex12, RegexOptions.Singleline);
                                try {
                                    string lit = labelInst[1].Substring(instructionLength).Trim();
                                    Match match = reg.Match(lit);
                                    instructionList.Add((StringType.Mnemonic, lineInd, labelInst[0].Length + 2, instructionLength));//Adding the Mneumonics
                                    if (match.Success) {
                                        instructionList.Add((StringType.Literal, lineInd, instructionLength, lit.Length));//Adding the Literal.
                                    }
                                    else {
                                        instructionList.Add((StringType.Error, lineInd, instructionLength, lit.Length));
                                    }
                                }
                                catch (Exception e) {
                                    //TODO:Catch the Exception.
                                }
                            }
                            else if (inst.Bytes == 3) {
                                Regex reg = new Regex(regex14, RegexOptions.Singleline);
                                try {
                                    string lit = labelInst[1].Substring(instructionLength).Trim();
                                    Match match = reg.Match(lit);
                                    instructionList.Add((StringType.Mnemonic, lineInd, labelInst[0].Length + 2, instructionLength));//Adding the Mneumonics
                                    if (match.Success) {
                                        instructionList.Add((StringType.Literal, lineInd, instructionLength, lit.Length));//Adding the Literal.
                                    }
                                    else {
                                        //instructionList.Add((StringType.Error, lineInd, length, lit.Length)); //NO Match ERROR. 
                                        Regex rightLabel = new Regex(regexRightLabel);
                                        Match m = rightLabel.Match(lit);
                                        if (m.Success) {
                                            if (isPresent(lit)) {
                                                instructionList.Add((StringType.Label, lineInd, instructionLength, lit.Length));
                                            }
                                            else {
                                                instructionList.Add((StringType.Error, lineInd, instructionLength, lit.Length));  // Label not defined previously. 
                                            }
                                        }
                                        else {
                                            instructionList.Add((StringType.Error, lineInd, instructionLength, lit.Length));  //Regex not match EXCEPTION. 
                                        }
                                    }
                                }
                                catch (Exception e) {
                                    //Handle the Regex No match Exception.
                                }
                            }
                        }
                        else {
                            instructionList.Add((StringType.Error, lineInd, labelInst[0].Length + 2, labelInst[1].Length)); //ERROR for NoSuchInstruction. 
                        }
                    }
                    else {
                        instructionList.Add((StringType.Error, lineInd, 0, -1));  // Putting the Error with more than One Colons. 
                    }
                }
                else if (line.Length > 0) {
                    Instruction inst = Instruction.parse(line.Trim());
                    if (!string.IsNullOrWhiteSpace(inst.Name)) {
                        instructions.Add(inst);
                        address += inst.Bytes;
                        if (inst.Bytes >= 2) {
                            instructionLength = inst.Name.Length + 1;  //For 2 or 2 Byte Instructions. Length=length+1.
                        }
                        else instructionLength = inst.Name.Length;
                        if (inst.Bytes == 1) {
                            instructionList.Add((StringType.Mnemonic, lineInd, 0, instructionLength));//Adding the Mneumonics.    
                        }
                        else if (inst.Bytes == 2) {
                            string mneumonics = line.Substring(0, instructionLength).Trim();
                            string lit = line.Substring(instructionLength);
                            Regex reg = new Regex(regbex12, RegexOptions.Singleline);
                            instructionList.Add((StringType.Mnemonic, lineInd, 0, instructionLength));//Adding the Mnemonics.
                            Match match = reg.Match(lit);
                            if (match.Success) {
                                instructionList.Add((StringType.Literal, lineInd, instructionLength, lit.Length)); //Adding the Literal. 
                            }
                            else {
                                instructionList.Add((StringType.Error, lineInd, instructionLength, lit.Length)); //Putting ERROR. 
                            }
                        }
                        else if (inst.Bytes == 3) {
                            string mneumonics = line.Substring(0, instructionLength).Trim();
                            string lit = line.Substring(instructionLength);
                            Regex reg = new Regex(regex14, RegexOptions.Singleline);
                            instructionList.Add((StringType.Mnemonic, lineInd, 0, instructionLength));
                            Match match = reg.Match(lit);
                            if (match.Success) {
                                instructionList.Add((StringType.Literal, lineInd, instructionLength + 1, lit.Length)); //Adding the Literal. 
                            }
                            else {
                                //instructionList.Add((StringType.Error, lineInd, length + 1, lit.Length)); //Putting ERROR. 
                                Regex rightLabel = new Regex(regexRightLabel);
                                Match m = rightLabel.Match(lit);
                                if (m.Success) {
                                    if (isPresent(lit)) {
                                        instructionList.Add((StringType.Label, lineInd, instructionLength, lit.Length));
                                    }
                                    else {
                                        instructionList.Add((StringType.Error, lineInd, instructionLength, lit.Length));  // Label not defined previously. 
                                    }
                                }
                                else {
                                    instructionList.Add((StringType.Error, lineInd, instructionLength, lit.Length));  //Regex not match EXCEPTION. 
                                }
                            }
                        }
                    }
                    else {
                        instructionList.Add((StringType.Error, lineInd, 0, line.Length)); //ERROR for NoSuchInstruction. 
                    }
                }
                lineInd++;
            }
            return instructionList;
        }
        private bool isPresent(string label) {
            foreach (var item in labels) {
                if (item.Value.Equals(label))
                    return true;
            }
            return false;
        }
    }
}