using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace michaelsparty
{
    public class CommandExecutor
    {
        public static List<Commands> GenerateCommandInstances(string commandString, Character character)
        {
            return ParseCommandsHelper(commandString, character);
        }

        private static List<Commands> ParseCommandsHelper(string input, Character character = null)
        {
            List<Commands> commands = new List<Commands>();

            string repeatUntilEdgePattern = @"repeatUntilEdge\((.*)\)";
            Match matchRepeatUntilEdge = Regex.Match(input, repeatUntilEdgePattern);

            string repeatPattern = @"Repeat (\d+) times\(([^()]*(?:\(([^()]*?)\))*[^()]*)\)";
            Match matchRepeat = Regex.Match(input, repeatPattern);

            int lastIndex = 0;

            while (matchRepeatUntilEdge.Success || matchRepeat.Success)
            {
                if (matchRepeatUntilEdge.Success && matchRepeatUntilEdge.Index < matchRepeat.Index)
                {
                    if (matchRepeatUntilEdge.Index > lastIndex)
                    {
                        var precedingCommands = input.Substring(lastIndex, matchRepeatUntilEdge.Index - lastIndex);
                        commands.AddRange(ParseSimpleCommands(precedingCommands));
                    }

                    string innerCommands = matchRepeatUntilEdge.Groups[1].Value;
                    var expandedCommands = ParseCommandsHelper(innerCommands, character);

                    commands.Add(new RepeatUntilEdgeCommand(expandedCommands));

                    lastIndex = matchRepeatUntilEdge.Index + matchRepeatUntilEdge.Length;
                    matchRepeatUntilEdge = matchRepeatUntilEdge.NextMatch();
                }
                else if (matchRepeat.Success)
                {
                    if (matchRepeat.Index > lastIndex)
                    {
                        var precedingCommands = input.Substring(lastIndex, matchRepeat.Index - lastIndex);
                        commands.AddRange(ParseSimpleCommands(precedingCommands));
                    }

                    int repeatCount = int.Parse(matchRepeat.Groups[1].Value);
                    string innerCommands = matchRepeat.Groups[2].Value;

                    var expandedCommands = ParseCommandsHelper(innerCommands, character);

                    for (int i = 0; i < repeatCount; i++)
                    {
                        commands.AddRange(expandedCommands);
                    }

                    lastIndex = matchRepeat.Index + matchRepeat.Length;
                    matchRepeat = matchRepeat.NextMatch();
                }
            }

            if (lastIndex < input.Length)
            {
                var remainingCommands = input.Substring(lastIndex);
                commands.AddRange(ParseSimpleCommands(remainingCommands));
            }

            return commands;
        }




        private static List<Commands> ParseSimpleCommands(string input)
        {
            var commandList = new List<Commands>();
            foreach (var command in input.Split(','))
            {
                string trimmedCommand = command.Trim();

                if (trimmedCommand.StartsWith("Move"))
                {
                    int steps = int.Parse(trimmedCommand.Split(' ')[1]);
                    for (int i = 0; i < steps; i++)
                    {
                        commandList.Add(new Move(1));
                    }
                }
                else if (trimmedCommand.StartsWith("Turn"))
                {
                    string direction = trimmedCommand.Split(' ')[1];
                    commandList.Add(new Turn(direction));
                }
            }
            return commandList;
        }
    }

    
}
