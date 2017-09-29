using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace One_X {
    /**<summary>
     * This is the Enum for the different parts of the Instructions. 
     * </summary>
     */
    public class Parser {
        public enum StringType : uint {
            Label = 0xFF008000,
            Mnemonic = 0xFF0000FF,
            Literal = 0xFFFFA500,
            Error = 0xFFFF0000
        }

        internal int startingAddress; // This is the staring address of the code. 
        internal List<Instruction> instructions = new List<Instruction>();  // The list containing the instructins without the label.
        internal Dictionary<int, string> labels = new Dictionary<int, string>();// The dictionary with the key as the memory address and the label as the value.
        string rxLitShort = "^[0-9a-fA-F]{1,4}H?$";
        string rxLitByte = "^[0-9a-fA-F]{1,2}H?$";
        string rxLabel = "^[0-9A-Za-z]+$"; //Regex for Right label.

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
        public List<(StringType SType, int LineIndex, string Word)> Parse(string code) {
            int address = startingAddress;
            char[] newLine = { '\n' };
            char[] lineSeparator = { ':' };

            instructions.Clear();  //Clearing the Lists. 
            labels.Clear();

            var instructionList = new List<(StringType, int, string)>();

            // Checking more than One Colons in the line.
            string[] lines = code.Split(newLine);
            for (int i = 0; i < lines.Length; i++) {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue; // increments counter
                if (line.Contains(":")) {
                    bool hasOneColon = line.IndexOf(lineSeparator[0]) == line.LastIndexOf(lineSeparator[0]);
                    if (hasOneColon) {
                        string[] labelInst = line.Split(lineSeparator);

                        labelInst[0] = labelInst[0].Trim();
                        labelInst[1] = labelInst[1].Trim();

                        if (isValid(labelInst[0], rxLabel)) {
                            labels.Add(address, labelInst[0]);
                            instructionList.Add((StringType.Label, i, labelInst[0].Trim()));// Inserting the label. 
                        }
                        else {
                            instructionList.Add((StringType.Error, i, labelInst[0].Trim()));// Inserting the label ERROR (INVALID LABEL). 
                        }

                        Instruction inst = Instruction.parse(labelInst[1].Trim());
                        if (!string.IsNullOrWhiteSpace(inst.Name)) {
                            address += inst.Bytes; //Increasing the Address Local Variable.      

                            if (inst.Bytes == 1) {
                                instructionList.Add((StringType.Mnemonic, i, inst.Name));//Adding the Mneumonics. The column index will also include that of the Label.
                            }
                            else if (inst.Bytes == 2) {
                                try {
                                    string lit = labelInst[1].Substring(inst.Name.Length + 1).Trim();
                                    instructionList.Add((StringType.Mnemonic, i, inst.Name + labelInst[1].ElementAt(inst.Name.Length)));//Adding the Mneumonics
                                    if (isValid(lit, rxLitByte)) {
                                        instructionList.Add((StringType.Literal, i, lit));//Adding the Literal.
                                    }
                                    else {
                                        instructionList.Add((StringType.Error, i, lit));
                                        if (labels.Values.Contains(labelInst[0]))
                                            labels.Remove(address - inst.Bytes); // Removing the Label if the Instruction is not valid. 
                                    }
                                }
                                catch (Exception e) {
                                    //TODO:Catch the Exception.
                                    if (labels.Values.Contains(labelInst[0]))
                                        labels.Remove(address - inst.Bytes); // Removing the Label if the Instruction is not valid. 
                                }
                            }
                            else if (inst.Bytes == 3) {
                                try {
                                    string lit = labelInst[1].Substring(inst.Name.Length + 1).Trim();
                                    instructionList.Add((StringType.Mnemonic, i, inst.Name + labelInst[1].ElementAt(inst.Name.Length)));//Adding the Mneumonics
                                    if (isValid(lit, rxLitShort)) {
                                        instructionList.Add((StringType.Literal, i, lit));//Adding the Literal.
                                    }
                                    else {
                                        //instructionList.Add((StringType.Error, lineInd, length, lit.Length)); //NO Match ERROR. 
                                        if (isValid(lit, rxLabel)) {
                                            if (labels.Values.Contains(lit)) {
                                                instructionList.Add((StringType.Label, i, lit));
                                            }
                                            else {
                                                instructionList.Add((StringType.Error, i, lit));  // Label not defined previously. 
                                                if (labels.Values.Contains(labelInst[0]))
                                                    labels.Remove(address - inst.Bytes); // Removing the Label if the Instruction is not valid. 
                                            }
                                        }
                                        else {
                                            instructionList.Add((StringType.Error, i, lit));  //Regex not match EXCEPTION. 
                                            if (labels.Values.Contains(labelInst[0]))
                                                labels.Remove(address - inst.Bytes); // Removing the Label if the Instruction is not valid.
                                        }
                                    }
                                }
                                catch (Exception e) {
                                    //Handle the Regex No match Exception.
                                    if (labels.Values.Contains(labelInst[0]))
                                        labels.Remove(address - inst.Bytes); // Removing the Label if the Instruction is not valid.
                                }
                            }
                        }
                        else {
                            instructionList.Add((StringType.Error, i, labelInst[1])); //ERROR for NoSuchInstruction.
                            if (labels.Values.Contains(labelInst[0]))
                                labels.Remove(address - inst.Bytes); // Removing the Label if the Instruction is not valid.
                        }
                    }
                    else {
                        instructionList.Add((StringType.Error, i, line));  // Putting the Error with more than One Colons.
                    }
                }
                else if (line.Length > 0) {
                    Instruction inst = Instruction.parse(line.Trim());
                    if (!string.IsNullOrWhiteSpace(inst.Name)) {
                        instructions.Add(inst);
                        address += inst.Bytes;

                        if (inst.Bytes == 1) {
                            instructionList.Add((StringType.Mnemonic, i, inst.Name));//Adding the Mneumonics.    
                        }
                        else if (inst.Bytes == 2) {
                            string lit = line.Substring(inst.Name.Length + 1);
                            instructionList.Add((StringType.Mnemonic, i, inst.Name + line.ElementAt(inst.Name.Length)));//Adding the Mnemonics.
                            if (isValid(lit, rxLitByte)) {
                                instructionList.Add((StringType.Literal, i, lit)); //Adding the Literal. 
                            }
                            else {
                                instructionList.Add((StringType.Error, i, lit)); //Putting ERROR. 
                            }
                        }
                        else if (inst.Bytes == 3) {
                            string lit = line.Substring(inst.Name.Length + 1);
                            instructionList.Add((StringType.Mnemonic, i, inst.Name + line.ElementAt(inst.Name.Length)));
                            if (isValid(lit, rxLitShort)) {
                                instructionList.Add((StringType.Literal, i, lit)); //Adding the Literal. 
                            }
                            else {
                                //instructionList.Add((StringType.Error, lineInd, length + 1, lit.Length)); //Putting ERROR. 
                                if (isValid(lit, rxLabel)) {
                                    if (labels.Values.Contains(lit)) {
                                        instructionList.Add((StringType.Label, i, lit));
                                    }
                                    else {
                                        instructionList.Add((StringType.Error, i, lit));  // Label not defined previously. 
                                    }
                                }
                                else {
                                    instructionList.Add((StringType.Error, i, lit));  //Regex not match EXCEPTION. 
                                }
                            }
                        }
                    }
                    else {
                        instructionList.Add((StringType.Error, i, line)); //ERROR for NoSuchInstruction. 
                    }
                }
            }
            return instructionList;
        }
        private bool isValid(String text, String reg) {
            Regex regex = new Regex(reg, RegexOptions.Singleline);
            Match match = regex.Match(text);
            if (match.Success)
                return true;
            return false;
        }
    }
}