using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace One_X {
    public class Parser {
        public static string labelPattern = "[ \\t]*(?:(?<label>[\\w]+)[ \\t]*:)?[ \\t]*";
        public static string pattern = "^" + labelPattern + buildInstructionPattern() + "[ \\t]*$";

        public static Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
        public static string buildInstructionPattern() {
            string[] rxps = new string[3] { "(?<oneByte>", "(?<twoByte>", "(?<threeByte>" };

            foreach (var inst in Instruction.list) {
                rxps[inst.Bytes - 1] += inst.Name;
                if (inst.Bytes > 1) {
                    if (inst.Name.Contains(" ")) {
                        rxps[inst.Bytes - 1] += ",";
                    } else {
                        rxps[inst.Bytes - 1] += "[ \\t]";
                    }
                }
                rxps[inst.Bytes - 1] += "|";
            }
            
            rxps[0] = rxps[0].Remove(rxps[0].Length - 1) + ")";
            rxps[1] = rxps[1].Remove(rxps[1].Length - 1) + ")[ \\t]*(?<litByte>[0-9A-F]{1,2}H?)";
            rxps[2] = rxps[2].Remove(rxps[2].Length - 1) + ")[ \\t]*(?:(?<litShort>[0-9A-F]{1,4}H?)|(?<reference>[\\w]+))";
            
            return "(?<instruction>" + string.Join("|", rxps) + ")";
        }
    }
}
