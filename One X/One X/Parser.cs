using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace One_X {
    public class Parser {
        private static string labelPattern = "[ \\t]*(?:(?<label>[A-Z_][\\w]+)[ \\t]*:)?[ \\t]*";
        public static string oneByte = "(?<range>";
        public static string twoByte = "(?<range>";
        public static string threeByte = "(?<range>";
        public static string pattern = "^" + labelPattern + buildInstructionPattern() + "[ \\t]*$";

        private static Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static string buildInstructionPattern() {
            string[] rxps = new string[3];

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

            oneByte += rxps[0].Remove(rxps[0].Length - 1) + ")";
            twoByte += rxps[1].Remove(rxps[1].Length - 1) + ")[ \\t]*(?<litByte>[0-9A-F]{1,2}H?)";
            threeByte += rxps[2].Remove(rxps[2].Length - 1) + ")[ \\t]*(?:(?<litShort>[0-9A-F]{1,4}H?)|(?<reference>[\\w]+))";

            rxps[0] = oneByte.Replace("<range>", "<oneByte>");
            rxps[1] = twoByte.Replace("<range>", "<twoByte>");
            rxps[2] = threeByte.Replace("<range>", "<threeByte>");

            return "(?<instruction>" + string.Join("|", rxps) + ")";
        }
    }
}
