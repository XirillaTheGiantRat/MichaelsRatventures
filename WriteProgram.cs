using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michaelsparty
{
    public class WriteProgram
    {
        public List<Commands> GenerateCommandsFromInput(string userInput, Character character)
        {
            List<Commands> commandList = new List<Commands>();

            if (string.IsNullOrEmpty(userInput))
            {
                return commandList;
            }

            try
            {
                commandList = CommandExecutor.GenerateCommandInstances(userInput, character);
            }
            catch (Exception ex)
            {
                return new List<Commands>();
            }
            return commandList;
        }
    }

}
