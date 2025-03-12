using Itmo.ObjectOrientedProgramming.Lab4.FileSystem.FileSystemElements;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public interface IVisitor
{
    void Visit(FileElement file);

    void Visit(DirectoryElement directory);
}
