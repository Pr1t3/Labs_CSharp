using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem.FileSystemElements;

public class FileElement : IFileSystemElement
{
    public string Name { get; }

    public string Path { get; }

    private readonly IFileSystem _fileSystem;

    public FileElement(string path, IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        Path = path;
        FileSystemResult result = _fileSystem.GetFileName(path);
        if (result is FileSystemResult.SuccessWithResult<string> success)
        {
            Name = success.Result;
        }
        else
        {
            throw new ArgumentException("Path is not valid", path);
        }
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}