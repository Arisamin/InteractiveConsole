using System;

namespace InteractiveConsole
{
    public class CommandConsole<TCommand> where TCommand : struct, Enum
    {
        static TCommand[] AvailableCommands;

        public void StartConsole(
            Type commandsEnum,
            TCommand terminateCommand,
            TCommand helpCommand,
            ICommandHandlerFactory<TCommand> handlerFactory)
        {
            try
            {
                Console.WriteLine("=========================");
                Console.WriteLine("******** Started ********");
                Console.WriteLine("=========================");

                LoadAvailableCommands(commandsEnum);

                TCommand userCommand;

                do
                {
                    Console.Write("Command> ");
                    Enum.TryParse<TCommand>(Console.ReadLine(), out userCommand);
                    Console.WriteLine();

                    if (Equals(userCommand, terminateCommand))
                        break;

                    if(Equals(userCommand, helpCommand))
                    {
                        Console.WriteLine(PrintAllCommands(AvailableCommands));
                        continue;
                    }

                    handlerFactory.GetCommandHandler(userCommand).Handle();

                } while (true);

                Console.WriteLine("=======================================");
                Console.WriteLine("******** Finished Successfully ********");
                Console.WriteLine("=======================================");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Finished with exception:\n{e}");
            }

            Console.WriteLine("Press ENTER to terminate...");
            Console.ReadLine();
            Console.WriteLine("Terminated.");
        }

        private static void LoadAvailableCommands(Type commandsEnum)
        {
            var commandsArray = Enum.GetValues(commandsEnum);

            AvailableCommands = new TCommand[commandsArray.Length];

            commandsArray.CopyTo(AvailableCommands, 0);

            Console.WriteLine($"The following commands are available:\n{PrintAllCommands(AvailableCommands)}");
        }

        private static string PrintAllCommands(TCommand[] availableCommands)
        {
            return string.Join("\n", availableCommands);
        }
    }
}
