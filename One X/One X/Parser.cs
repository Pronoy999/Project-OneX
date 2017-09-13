using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace One_X
{
    class Parser {
        enum StringType {
            Label,
            Mneumonic,
            Literal
        }        
        internal int startingAddress; // This is the staring address of the code. 
        internal List<Instruction> instructions = new List<Instruction>();  // The list containing the instructins without the label.
        internal Dictionary<int, String> labels = new Dictionary<int, string>();// The dictionary with the key as the memory address and the label as the value.        
        List<Tuple<StringType, int, int, int,String>> instructionList = new List<Tuple<StringType,int, int,  int,String>>();
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
            int colInd;
            int length;           
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
                    instructionList.Add(Tuple.Create<StringType, int, int, int,String>(StringType.Label, lineInd, 0, labelInst[0].Length,labelInst[0]));
                    if (inst.Bytes >= 2)   // For 2 or 3 Byte Instructions Length = length+1;
                        length = inst.Name.Length + 1;
                    else length = inst.Name.Length;
                    if (inst.Bytes == 2) {
                        Regex reg = new Regex(regbex12, RegexOptions.Singleline);
                        try {
                            String lit = labelInst[1].Substring(length).Trim();
                            Match match = reg.Match(lit);
                            if (match.Success) {
                                instructionList.Add(Tuple.Create<StringType, int, int, int,String>(StringType.Mneumonic, lineInd, labelInst[0].Length + 1, length,labelInst[1].Substring(labelInst[0].Length,length+1)));
                                //TODO: Add the Literal Part. 
                            }
                            else {
                                //Throw NoMatchException
                            }
                        }
                        catch(Exception e) {
                            //TODO:Catch the Exception.
                        }
                    }
                }
                else {
                    Instruction inst = Instruction.parse(line.Trim());
                    instructions.Add(inst);
                    address += inst.Bytes;
                }
                lineInd++;
            }
        }
    }
}
