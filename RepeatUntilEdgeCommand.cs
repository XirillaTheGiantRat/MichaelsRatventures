using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michaelsparty
{
    public class RepeatUntilEdgeCommand : Commands
    {
        public List<Commands> Commands { get; set; }

        public RepeatUntilEdgeCommand(List<Commands> commands)
        {
            Commands = commands;
        }

        public override void Execute(Character character)
        {
            while (!character.IsAtEdge)
            {
                foreach (var command in Commands)
                {
                    command.Execute(character);
                    if (character.IsAtEdge) break;
                }
            }
        }

    }

}
