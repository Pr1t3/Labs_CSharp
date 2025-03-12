namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem.FileSystemElements;

public interface IFileSystemElement
{
    void Accept(IVisitor visitor);
}
