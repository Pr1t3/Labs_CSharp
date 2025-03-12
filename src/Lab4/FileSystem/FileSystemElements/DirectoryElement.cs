using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem.FileSystemElements;

public class DirectoryElement : IFileSystemElement
{
    public string Name { get; }

    public string Path { get; }

    public Collection<IFileSystemElement> Elements { get; private set; }

    private readonly IFileSystem _fileSystem;

    public DirectoryElement(string path, IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        Path = path;
        Elements = new Collection<IFileSystemElement>();
        FileSystemResult result = _fileSystem.GetDirectoryName(Path);
        if (result is FileSystemResult.SuccessWithResult<string> success)
        {
            Name = success.Result;
        }
        else
        {
            throw new ArgumentException("Path is not valid", path);
        }
    }

    public void LoadElements(int depth, int currentDepth = 0)
    {
        if (currentDepth >= depth) return;

        FileSystemResult result = _fileSystem.GetFiles(Path);
        if (result is FileSystemResult.SuccessWithResult<IEnumerable<string>> success1)
        {
            foreach (string file in success1.Result)
            {
                Elements.Add(new FileElement(file, _fileSystem));
            }
        }

        result = _fileSystem.GetDirectories(Path);
        if (result is FileSystemResult.SuccessWithResult<IEnumerable<string>> success2)
        {
            foreach (string dir in success2.Result)
            {
                var directory = new DirectoryElement(dir, _fileSystem);
                directory.LoadElements(depth, currentDepth + 1);
                Elements.Add(directory);
            }
        }
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}