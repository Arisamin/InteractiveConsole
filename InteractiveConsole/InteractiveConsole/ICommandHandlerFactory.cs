using System;

namespace InteractiveConsole
{
    public interface ICommandHandlerFactory<TCommand> where TCommand : Enum
    {
        ICommandHandler GetCommandHandler(TCommand command);
    }
}
