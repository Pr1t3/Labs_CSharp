using Itmo.ObjectOrientedProgramming.Lab4.FileSystem.FileSystemElements;
using Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class TreeListVisitor : IVisitor
{
    private readonly IOutputHandler _outputHandler;
    private readonly int _maxDepth;
    private readonly string _fileSymbol;
    private readonly string _directorySymbol;
    private readonly string _spaceSymbol;
    private int _currentDepth;

    public TreeListVisitor(int maxDepth, IOutputHandler outputHandler, string fileSymbol, string directorySymbol, string spaceSymbol)
    {
        _maxDepth = maxDepth;
        _currentDepth = 0;
        _outputHandler = outputHandler;
        _fileSymbol = fileSymbol;
        _directorySymbol = directorySymbol;
        _spaceSymbol = spaceSymbol;
    }

    public void Visit(FileElement file)
    {
        if (_currentDepth <= _maxDepth)
        {
            _outputHandler.Write($"{string.Concat(Enumerable.Repeat(_spaceSymbol, _currentDepth * 2))}{_fileSymbol}File: {file.Name}");
        }
    }

    public void Visit(DirectoryElement directory)
    {
        if (_currentDepth <= _maxDepth)
        {
            _outputHandler.Write($"{string.Concat(Enumerable.Repeat(_spaceSymbol, _currentDepth * 2))}{_directorySymbol}Directory: {directory.Name}");
            _currentDepth++;
            foreach (IFileSystemElement element in directory.Elements)
            {
                element.Accept(this);
            }

            _currentDepth--;
        }
    }
}
