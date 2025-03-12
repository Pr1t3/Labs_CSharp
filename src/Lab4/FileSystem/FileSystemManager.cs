using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class FileSystemManager : IFileSystemManager
{
    // Должен быть интерфейс IFileSystem, но есть только 1 его реализация, поэтому выдает ошибку
    private LocalFileSystem? _fileSystem;

    public FileSystemResult Connect(string path, string mode)
    {
        if (mode == "local")
        {
            _fileSystem = new LocalFileSystem();
        }
        else
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.Connect(path);
    }

    public FileSystemResult Disconnect()
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.Disconnect();
    }

    public FileSystemResult GotoDirectory(string path)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.GotoDirectory(path);
    }

    public FileSystemResult GetDirectory(int depth)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.GetDirectory(depth);
    }

    public FileSystemResult GetFileContent(string path)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.GetFileContent(path);
    }

    public FileSystemResult MoveFile(string sourcePath, string destinationPath)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.MoveFile(sourcePath, destinationPath);
    }

    public FileSystemResult CopyFile(string sourcePath, string destinationPath)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.CopyFile(sourcePath, destinationPath);
    }

    public FileSystemResult DeleteFile(string path)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.DeleteFile(path);
    }

    public FileSystemResult RenameFile(string path, string newName)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.RenameFile(path, newName);
    }

    public FileSystemResult GetFileName(string path)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.GetFileName(path);
    }

    public FileSystemResult GetDirectoryName(string path)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.GetDirectoryName(path);
    }

    public FileSystemResult GetFiles(string path)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.GetFiles(path);
    }

    public FileSystemResult GetDirectories(string path)
    {
        if (_fileSystem == null)
        {
            return new FileSystemResult.Fail();
        }

        return _fileSystem.GetDirectories(path);
    }
}