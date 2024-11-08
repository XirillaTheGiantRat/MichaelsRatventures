using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michaelsparty
{
    internal class PreMadePrograms
    {
        List<string> basicPrograms = new List<string>();
        List<string> advancedPrograms = new List<string>();
        List<string> expertPrograms = new List<string>();
        Random rand = new Random();
        public PreMadePrograms() 
        {
            //basic
            basicPrograms.Add("Move 2, Turn right, Move 2, Turn right, Move 2, Turn right, Move 2, Turn right");
            basicPrograms.Add("Move 2, Turn right, Move 1, Turn left, Move 2, Turn right, Move 2, Turn left");
            basicPrograms.Add("Turn right, Move 5, Turn left, Move 1, Turn left, Move 2");

            //advanced
            advancedPrograms.Add("Move 5, repeatUntilEdge(Move 5, Turn Right)");
            //advancedPrograms.Add("repeatUntilEdge(Move 10, Turn right), Turn Right, Move 2");
            //advancedPrograms.Add("repeatUntilEdge(Move 10, Turn right), Turn Right, Move 2");
            advancedPrograms.Add("Repeat 1 times(Move 3, Turn right)");
            advancedPrograms.Add("Repeat 3 times(Turn right, Move 5), Repeat 2 times(Move 2)");

            //expert
            expertPrograms.Add("Repeat 3 times(Move 4, Turn right, Repeat 2 times(Move 2, Turn right))"); 
            expertPrograms.Add("Repeat 1 times(Move 3, Repeat 3 times(Move 2, Turn left))");
            expertPrograms.Add("Repeat 2 times(Move 1, Turn right, Repeat 1 times(Move 2, Turn right))");

        }

        public string ChooseProgram(List<string> programs)
        {
            int randomIndex = rand.Next(programs.Count); 
            return programs[randomIndex];
        }

        public string ChooseBasicProgram()
        {
            return ChooseProgram(basicPrograms);
        }

        public string ChooseAdvancedProgram()
        {
            return ChooseProgram(advancedPrograms);
        }

        public string ChooseExpertProgram()
        {
            return ChooseProgram(expertPrograms);
        }
    }
}
