using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace michaelsparty
{
    public abstract class Metrics
    {
        public abstract int CalculateMetric(string commandString);
    }

    public class NumberOfCommands : Metrics
    {
        public override int CalculateMetric(string commandString)
        {
            int commandCount = 0;
            string simpleCommandPattern = @"\b(Move \d+|Turn (left|right))\b";

            MatchCollection commandMatches = Regex.Matches(commandString, simpleCommandPattern);
            commandCount += commandMatches.Count;

            return commandCount;
        }
    }

    public class NumberOfRepeats : Metrics
    {
        public override int CalculateMetric(string commandString)
        {
            int repeatCount = 0;
            string repeatPattern = @"Repeat \d+ times";

            MatchCollection repeatMatches = Regex.Matches(commandString, repeatPattern);
            repeatCount += repeatMatches.Count;

            return repeatCount;
        }
    }

    public class MAXNesting : Metrics
    {
        public override int CalculateMetric(string commandString)
        {
            int maxNestingLevel = 0; 
            CalculateMaxNesting(commandString, ref maxNestingLevel, 0);
            return maxNestingLevel;
        }

        private void CalculateMaxNesting(string commandString, ref int maxNestingLevel, int currentNestingLevel)
        {
            maxNestingLevel = Math.Max(maxNestingLevel, currentNestingLevel); 
            string repeatPattern = @"Repeat \d+ times\(([^()]*(?:\((?:[^()]*)\))*[^()]*)\)";
            MatchCollection matches = Regex.Matches(commandString, repeatPattern);

            foreach (Match match in matches)
            {
                string innerCommands = match.Groups[1].Value;
                CalculateMaxNesting(innerCommands, ref maxNestingLevel, currentNestingLevel + 1); 
            }
        }
    }

}
