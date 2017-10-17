using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace One_X {
    public class RegexHelper {
        private const string label = "(?<label>[A-Z_]\\w*)";
        private const string litByte = "(?<litByte>[0-9A-F]{1,2}H?)";
        private const string litUShort = "(?<litUShort>[0-9A-F]{1,4}H?)";
        private const RegexOptions rxOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;
        
        public static readonly Regex rxRangeLabelOnly;

        public static readonly Regex rxRangeOneByte;
        public static readonly Regex rxRangeTwoByte;
        public static readonly Regex rxRangeThreeByte;

        public static readonly Regex rxRangeLiteralByte;
        public static readonly Regex rxRangeLiteralUShort;

        public static readonly Regex rxRangeReference;

        public static readonly Regex rxInstructionLine;
        public static readonly Regex rxLabelOnly;

        static RegexHelper() {
            string oneByte = "(?<oneByte>";
            string twoByte = "(?<twoByte>";
            string threeByte = "(?<threeByte>";

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
            
            string tempThreeByte = threeByte + rxps[2].Remove(rxps[2].Length - 1) + ")[ \\t]*";

            oneByte += rxps[0].Remove(rxps[0].Length - 1) + ")";
            twoByte += rxps[1].Remove(rxps[1].Length - 1) + ")[ \\t]*" + litByte;
            threeByte = tempThreeByte + "(?:" + litUShort + "|" + label.Replace("label", "reference") + ")";

            string instruction = "(?<instruction>" + string.Join("|", oneByte, twoByte, threeByte) + ")";

            // compile all the regex and store
            rxInstructionLine = new Regex("^[ \\t]*(?:" + label + "[ \\t]*:)?[ \\t]*" + instruction + "[ \\t]*$", rxOptions);
            rxLabelOnly = new Regex("^[ \\t]*(?:" + label + "[ \\t]*:)?[ \\t]*$", rxOptions);
            // compile the range regex and store
            rxRangeLabelOnly = new Regex("^[ \\t]*" + label.Replace("<label>", "<range>") + "[ \\t]*:", rxOptions);

            rxRangeOneByte = new Regex(oneByte.Replace("<oneByte>", "<range>"), rxOptions);
            rxRangeTwoByte = new Regex(twoByte.Replace("<twoByte>", "<range>"), rxOptions);
            rxRangeThreeByte = new Regex(threeByte.Replace("<threeByte>", "<range>"), rxOptions);

            rxRangeLiteralByte = new Regex(twoByte.Replace("<litByte>", "<range>"), rxOptions);
            rxRangeLiteralUShort = new Regex(tempThreeByte + litUShort, rxOptions);

            rxRangeReference = new Regex(tempThreeByte + label.Replace("<label>", "<range>"), rxOptions);
        }
    }
}
