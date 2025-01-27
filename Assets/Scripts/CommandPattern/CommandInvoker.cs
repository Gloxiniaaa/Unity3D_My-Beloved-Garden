
using System.Collections.Generic;

public class CommandInvoker
{
    protected Stack<ICommand> _commandList;
    public CommandInvoker()
    {
        _commandList = new Stack<ICommand>();
    }

    public void DoAndSaveCommand(ICommand newCommand)
    {
        _commandList.Push(newCommand);
        newCommand.Execute();
    }

    public void UndoCommand()
    {
        if (_commandList.Count > 0)
            _commandList.Pop().Undo();
    }
}