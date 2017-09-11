using System;
using System.Collections.Generic;

namespace One_X
{
    class Parser {
        internal int startingAddress; // This is the staring address of the code. 
        internal List<Instruction> instructions = new List<Instruction>();  // The list containing the instructins without the label.
        internal Dictionary<int, String> labels = new Dictionary<int, string>();// The dictionary with the key as the memory address and the label as the value. 
        public Parser(int startingAddress) {
            this.startingAddress = startingAddress;
        }
        /**<summary>
         * This is the method should be called using the object of the parser class. 
         * Create the object of the class and set the starting address. 
         * </summary>
         */
        public void parse(String code) {
            int address = startingAddress;
            char[] newLine = { '\n' }; 
            char[] lineSeparator = { ':' };
            String[] lines = code.Split(newLine);
            foreach (String line in lines) {
                if (line.Contains(":")) {
                    String[] labelInst = line.Split(lineSeparator);
                    Instruction inst = Instruction.parse(labelInst[1].Trim());
                    instructions.Add(inst);
                    labels.Add(address, labelInst[0].Trim());
                    address += inst.Bytes; 
                }
                else {
                    Instruction inst = Instruction.parse(line.Trim());
                    instructions.Add(inst);
                    address += inst.Bytes;
                }
            }
        }
    }
}
