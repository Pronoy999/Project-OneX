using System;
using System.Collections.Generic;
using System.Linq;
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
        public Dictionary<ushort, Instruction> instructions = new Dictionary<ushort, Instruction>();

        Dictionary<string, ushort> labels = new Dictionary<string, ushort>();

        List<ushort> memory = new List<ushort>();// To generate the warning. 

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
            bool isLabelInserted = false;
            label = string.Empty;
            for (i = 0; i < lineNum; i++) {
                Match match = RegexHelper.rxInstructionLine.Match(line[i]);
                if (!string.IsNullOrWhiteSpace(match.Groups[LABEL].Value)) {
                    label = match.Groups[LABEL].Value;
                }
                if (!string.IsNullOrWhiteSpace(label)) {
                    isLabelInserted = true;
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
                    isLabelInserted = false;
                    if (line[i].Length != match.Length) {
                        errorList.Add((DebugLevel.Error, i, match.Length, (line[i].Length - match.Length))); //Lable with Invalid Characters. 
                    }
                    continue; //Blank Label. 
                }

                //Getting the Right Literal. 
                if (!string.IsNullOrWhiteSpace(match.Groups[LIT_BYTE].Value)) {
                    rightLit = match.Groups[LIT_BYTE].Value;
                    if ((rightLit.ElementAt(rightLit.Length - 1)) == 'H' || (rightLit.ElementAt(rightLit.Length - 1) == 'h')) {
                        rightLit = rightLit.Remove(rightLit.Length - 1);
                    }
                    rightLit_type = LIT_BYTE;
                }
                else if (!string.IsNullOrWhiteSpace(match.Groups[LIT_USHORT].Value)) {
                    rightLit = match.Groups[LIT_USHORT].Value;
                    if ((rightLit.ElementAt(rightLit.Length - 1)) == 'H' || (rightLit.ElementAt(rightLit.Length - 1) == 'h')) {
                        rightLit = rightLit.Remove(rightLit.Length - 1);
                    }
                    rightLit_type = LIT_USHORT;
                    if (!memory.Contains(ushort.Parse(rightLit,System.Globalization.NumberStyles.HexNumber))) {
                        memory.Add(ushort.Parse(rightLit,System.Globalization.NumberStyles.HexNumber));
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
                        right_LIT = ushort.Parse(rightLit,System.Globalization.NumberStyles.HexNumber);
                    }
                    catch (Exception e) { }
                }
                else if (rightLit_type == REFERENCE) {
                    tempReference.Add((i, label.Length + instruction_name.Length, rightLit.Length, address, rightLit)); //Adding the temporary reference. 
                }
                Instruction inst = getInstructions(instruction_name);
                //Adding the instructions. 
                if (rightLit_type != REFERENCE) {
                    try {
                        instructions
                            .Add(address, new Instruction(inst.Name, inst.Bytes, inst.MCycles, inst.TStates, inst.method, right_LIT.ToBytes()));
                        address += inst.Bytes;
                    }
                    catch (Exception e) { }
                 }
                else {
                    try {
                        instructions
                             .Add(address, new Instruction(inst.Name, inst.Bytes, inst.MCycles, inst.TStates, inst.method, ((ushort)0).ToBytes()));
                        address += inst.Bytes;
                    }
                    catch (Exception e) { }
                }
                inst = null;
                if (isLabelInserted) { label=string.Empty;}
            }
            addReferences();
        }
        private void addReferences() {
            foreach (var refer in tempReference) {
                try {
                    var lit = labels[refer.reference];
                    instructions[refer.address].Arguments = lit.ToBytes();
                }
                catch(Exception e) { }
            }
        }
        private Instruction getInstructions(string instructionName) {
            Instruction inst = null;
            foreach (Instruction ins in Instruction.list) {
                if (ins.Name == instructionName) {
                    inst = ins;
                    return inst;
                }
            }
            return inst;
        }
    }
}