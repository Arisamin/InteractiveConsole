using System;
using InteractiveConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public static void Main()
        {
            TestMethod();
        }

        [TestMethod]
        public static void TestMethod()
        {
            ICommandHandlerFactory<TestCommand> handlersFactory = new HandlerFactory();

            new CommandConsole<TestCommand>()
                .StartConsole(typeof(TestCommand), TestCommand.Quit, TestCommand.Help, handlersFactory);
        }

        enum TestCommand
        {
            Command1, Command2, Quit, Help
        }

        class HandlerFactory : ICommandHandlerFactory<TestCommand>
        {
            public ICommandHandler GetCommandHandler(TestCommand command)
            {
                switch (command)
                {
                    case TestCommand.Command1:
                        return new Command1Handler();
                    case TestCommand.Command2:
                        return new Command2Handler();
                    case TestCommand.Quit:
                        return new Command3Handler();

                    default:
                        throw new Exception($"Unrecognized command '{command}'");
                }
            }
        }

        class Command1Handler : ICommandHandler
        {
            public void Handle()
            {
                Console.WriteLine("Handled command 1");
            }
        }

        class Command2Handler : ICommandHandler
        {
            public void Handle()
            {
                Console.WriteLine("Handled command 2");
            }
        }

        class Command3Handler : ICommandHandler
        {
            public void Handle()
            {
                Console.WriteLine("Handled command 3");
            }
        }
    }
}
