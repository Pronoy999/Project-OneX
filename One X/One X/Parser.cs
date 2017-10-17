using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace One_X {
    public class Parser {
        /**<summary>
         * This is the Enum to store the level of Debug. 
         * </summary>
         */
        public enum DebugLevel {
            Information,
            Error,
            Verbose
        }
        /**<summary>
         * This is the list of Tuple for Error List. 
         * </summary>
         */
        List<(DebugLevel debugLevel, int lineInd, int colInd, int length)> errorList = new List<(DebugLevel debugLevel, int lineInd, int colInd, int length)>();
        Dictionary<ushort, Instruction> instructions = new Dictionary<ushort, Instruction>();

        Dictionary<string, ushort> labels = new Dictionary<string, ushort>();

        List<int> memory = new List<int>();// To generate the warning. 

        List<(int lineInd, int colInd, int length, ushort address, string reference)> tempReference = 
            new List<(int lineInd, int colInd, int length, ushort address, string reference)>();

        /**<summary>
         * Variables to store the names of the Regex Groups. 
         *</summary>
         */
        string LABEL = "label", REFERENCE = "reference", ONEBYTE_INSTRUCTION = "oneByte", TWOBYTE_INSTRUCTION = "twoByte", THREEBYTE_INSTRUCTION = "threeByte",
            LIT_BYTE = "litByte", LIT_USHORT = "litUshort";

        char[] newLine = { '\n' };

        ushort address;     //Starting Address. 

        public Parser(ushort address) {
            this.address = address; //Setting address.
        }
        public void parse(string code) {

            string label = string.Empty; //A Local Variable to store the labels. 

            string instruction_name = string.Empty;//A Local Variable to store the instrctions. 
            string instruction_type = string.Empty;      
            
            string rightLit = string.Empty;//A Local variable to store the right literal.
            string rightLit_type = string.Empty;
            ushort right_LIT=0;

            instructions.Clear();
            labels.Clear();
            errorList.Clear();
            tempReference.Clear();
            memory.Clear();

            string[] line = code.Split(newLine);
            int i, lineNum = line.Length;
            for (i = 0; i < lineNum; i++) {
                Match match = RegexHelper.rxInstructionLine.Match(line[i]);
                label = match.Groups[LABEL].Value;
                if (!label.Equals(string.Empty)) {
                    labels.Add(label, 00);      // Adding the labels only. 
                }
            }
            //Parsing the Whole Code.         
            label = string.Empty;
            for (i = 0; i < lineNum; i++) {
                Match match = RegexHelper.rxInstructionLine.Match(line[i]);
                if (!string.IsNullOrWhiteSpace(match.Groups[LABEL].Value)) {
                    label = match.Groups[LABEL].Value;
                }
                if (!string.IsNullOrWhiteSpace(label)) {
                    if (!labels.ContainsKey(label)) {
                        labels.Add(label, address);
                    }
                    else if (labels.ContainsKey(label)) {
                        labels[label] = address;
                    }
                }
                //Getting the instruction.                   
                if (!string.IsNullOrWhiteSpace(match.Groups[ONEBYTE_INSTRUCTION].Value)) {
                    instruction_name = match.Groups[ONEBYTE_INSTRUCTION].Value;
                    instruction_type = ONEBYTE_INSTRUCTION;
                }
                else if (!string.IsNullOrWhiteSpace(match.Groups[TWOBYTE_INSTRUCTION].Value)) {
                    instruction_name = match.Groups[TWOBYTE_INSTRUCTION].Value;
                    instruction_name = instruction_name.Remove(instruction_name.Length-1);
                    instruction_type = TWOBYTE_INSTRUCTION;
                }
                else if (!string.IsNullOrWhiteSpace(match.Groups[THREEBYTE_INSTRUCTION].Value)) {
                    instruction_name = match.Groups[THREEBYTE_INSTRUCTION].Value;
                    instruction_name = instruction_name.Remove(instruction_name.Length-1);
                    instruction_type = THREEBYTE_INSTRUCTION;
                }

                if (string.IsNullOrWhiteSpace(instruction_name)) {
                    if (line[i].Length != match.Length) {
                        errorList.Add((DebugLevel.Error, i, match.Length, (line[i].Length - match.Length))); //Lable with Invalid Characters. 
                    }
                    continue; //Blank Label. 
                }

                //Getting the Right Literal. 
                if (!string.IsNullOrWhiteSpace(match.Groups[LIT_BYTE].Value)) {
                    rightLit = match.Groups[LIT_BYTE].Value;
                    rightLit_type = LIT_BYTE;
                }
                else if (!string.IsNullOrWhiteSpace(match.Groups[LIT_USHORT].Value)) {
                    rightLit = match.Groups[LIT_USHORT].Value;
                    rightLit_type = LIT_USHORT;
                    if (!memory.Contains(Convert.ToInt32(rightLit))) {
                        memory.Add(Convert.ToInt32(rightLit));
                    }
                    else {
                        errorList.Add((DebugLevel.Information, i, (label.Length + instruction_name.Length), rightLit.Length));
                    }
                }
                else if (!string.IsNullOrWhiteSpace(match.Groups[REFERENCE].Value)) {
                    rightLit = match.Groups[REFERENCE].Value;
                    rightLit_type = REFERENCE;
                }
                //Checking for MisMatch ERROR.
                /*if (instruction_type == ONEBYTE_INSTRUCTION && (rightLit_type == LIT_USHORT || rightLit_type == REFERENCE)) {
                    errorList.Add((DebugLevel.Error, i, (label.Length + instruction_name.Length), rightLit.Length));
                }*/
                if (instruction_type == TWOBYTE_INSTRUCTION && (rightLit_type == REFERENCE || rightLit_type == LIT_USHORT)) {
                    errorList.Add((DebugLevel.Error, i, (label.Length + instruction_name.Length), rightLit.Length));
                }
                if (rightLit_type != REFERENCE) {
                    try {
                        right_LIT = ushort.Parse(rightLit);
                    }
                    catch (Exception) { }
                }
                else if (rightLit_type == REFERENCE) {
                    tempReference.Add((i, label.Length + instruction_name.Length, rightLit.Length, address, rightLit)); //Adding the temporary reference. 
                }
                Instruction inst = null;
                foreach (Instruction ins in Instruction.list) {
                    if (ins.Name == instruction_name) {
                        inst = ins;
                        break;
                    }
                }
                //Adding the instructions. 
                if (rightLit_type != REFERENCE) {
                    try {
                        instructions
                            .Add(address, new Instruction(inst.Name, inst.Bytes, inst.MCycles, inst.TStates, inst.method, right_LIT.ToBytes()));
                    }
                    catch (Exception) { }
                 }
                else {
                    try {
                        instructions
                            .Add(address, new Instruction(inst.Name, inst.Bytes, inst.MCycles, inst.TStates, ""));
                    }
                    catch (Exception) { }
                }
                inst = null;
                
            }
            addReferences();
        }
        private void addReferences() {
            foreach(var refer in tempReference) {
                foreach(var label in labels) {
                    if (refer.reference == label.Key) {
                        ushort refAddress = label.Value;
                        Instruction originalInstruction = null;
                        foreach(Instruction i in Instruction.list) {
                            if (instructions[label.Value].Name == i.Name) {
                                originalInstruction = i;
                                break;
                            }
                        }
                        instructions[label.Value].Bytes = originalInstruction.Bytes;
                        instructions[label.Value].MCycles = originalInstruction.MCycles;
                        instructions[label.Value].TStates = originalInstruction.TStates;
                        instructions[label.Value].method = originalInstruction.method;
                        instructions[label.Value].Arguments = refAddress.ToBytes();
                    }
                }
            }
        }
    }
}