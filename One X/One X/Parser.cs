using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_X
{
    class Parser
    {
        public static void parse(String code)
        {
            List<Instruction> instructions=new List<Instruction>();
            char[] separator = { '\n' };
            char[] lineSeparator = { ':' };
            String[] lines = code.Split(separator);
            foreach(String line in lines){
                if (line.Contains(":"))
                {
                    String[] instruct = line.Split(lineSeparator);
                    instructions.Add(Instruction.parse(instruct[1].Trim()));
                    //TODO: store the label.
                }
                else
                {   //NO Label.
                    instructions.Add(Instruction.parse(line.Trim()));
                }
            }
        }
    }
}
