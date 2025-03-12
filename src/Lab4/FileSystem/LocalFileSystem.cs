using Itmo.ObjectOrientedProgramming.Lab4.FileSystem.FileSystemElements;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class LocalFileSystem : IFileSystem
{
    private string? _currentPath;

    public FileSystemResult Connect(string path)
    {
        if (Directory.Exists(path) && Path.IsPathRooted(path))
        {
            _currentPath = path;
        }
        else
        {
            return new FileSystemResult.Fail();
        }

        return new FileSystemResult.Success();
    }

    public FileSystemResult Disconnect()
    {
        if (_currentPath == null)
        {
            return new FileSystemResult.Fail();
        }

        _currentPath = null;

        return new FileSystemResult.Success();
    }

    public FileSystemResult GotoDirectory(string path)
    {
        if (string.IsNullOrEmpty(_currentPath))
        {
            return new FileSystemResult.Fail();
        }

        string newPath;
        if (Path.IsPathRooted(path))
        {
            newPath = path;
        }
        else
        {
            newPath = Path.Combine(_currentPath, path);
        }

        if (!Directory.Exists(newPath))
        {
            Console.WriteLine(newPath);
            return new FileSystemResult.Fail();
        }

        _currentPath = Path.GetFullPath(newPath);
        return new FileSystemResult.Success();
    }

    public FileSystemResult GetDirectory(int depth)
    {
        if (string.IsNullOrEmpty(_currentPath) || depth < 0)
        {
            return new FileSystemResult.Fail();
        }

        var curDirectory = new DirectoryElement(_currentPath, this);
        curDirectory.LoadElements(depth);

        return new FileSystemResult.SuccessWithResult<DirectoryElement>(curDirectory);
    }

    public FileSystemResult GetFileContent(string path)
    {
        if (!Path.IsPathRooted(path))
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return new FileSystemResult.Fail();
            }

            path = Path.Combine(_currentPath, path);
        }

        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);

            return new FileSystemResult.SuccessWithResult<string>(content);
        }

        return new FileSystemResult.Fail();
    }

    public FileSystemResult MoveFile(string sourcePath, string destinationPath)
    {
        if (!Path.IsPathRooted(sourcePath))
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return new FileSystemResult.Fail();
            }

            sourcePath = Path.Combine(_currentPath, sourcePath);
        }

        if (!Path.IsPathRooted(destinationPath))
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return new FileSystemResult.Fail();
            }

            destinationPath = Path.Combine(_currentPath, destinationPath);
            if (!Directory.Exists(destinationPath))
            {
                return new FileSystemResult.Fail();
            }

            destinationPath = Path.Combine(destinationPath, Path.GetFileName(sourcePath));
        }

        if (File.Exists(sourcePath))
        {
            File.Move(sourcePath, destinationPath);
        }
        else
        {
            return new FileSystemResult.Fail();
        }

        return new FileSystemResult.Success();
    }

    public FileSystemResult CopyFile(string sourcePath, string destinationPath)
    {
        if (!Path.IsPathRooted(sourcePath))
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return new FileSystemResult.Fail();
            }

            sourcePath = Path.Combine(_currentPath, sourcePath);
        }

        if (!Path.IsPathRooted(destinationPath))
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return new FileSystemResult.Fail();
            }

            destinationPath = Path.Combine(_currentPath, destinationPath);

            if (!Directory.Exists(destinationPath))
            {
                return new FileSystemResult.Fail();
            }

            destinationPath = Path.Combine(destinationPath, Path.GetFileName(sourcePath));
        }

        if (File.Exists(sourcePath))
        {
            File.Copy(sourcePath, destinationPath);
        }
        else
        {
            return new FileSystemResult.Fail();
        }

        return new FileSystemResult.Success();
    }

    public FileSystemResult DeleteFile(string path)
    {
        if (!Path.IsPathRooted(path))
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return new FileSystemResult.Fail();
            }

            path = Path.Combine(_currentPath, path);
        }

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            return new FileSystemResult.Fail();
        }

        return new FileSystemResult.Success();
    }

    public FileSystemResult RenameFile(string path, string newName)
    {
        if (!Path.IsPathRooted(path))
        {
            if (string.IsNullOrEmpty(_currentPath))
            {
                return new FileSystemResult.Fail();
            }

            path = Path.Combine(_currentPath, path);
        }

        if (File.Exists(path))
        {
            string? directoryName = Path.GetDirectoryName(path);
            if (directoryName == null)
            {
                return new FileSystemResult.Fail();
            }

            string newPath = Path.Combine(directoryName, newName);
            File.Move(path, newPath);
        }
        else
        {
            return new FileSystemResult.Fail();
        }

        return new FileSystemResult.Success();
    }

    public FileSystemResult GetFileName(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return new FileSystemResult.Fail();
        }

        if (File.Exists(path))
        {
            return new FileSystemResult.SuccessWithResult<string>(Path.GetFileName(path));
        }
        else
        {
            return new FileSystemResult.Fail();
        }
    }

    public FileSystemResult GetDirectoryName(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return new FileSystemResult.Fail();
        }

        if (Directory.Exists(path))
        {
            return new FileSystemResult.SuccessWithResult<string>(Path.GetFileName(path));
        }
        else
        {
            return new FileSystemResult.Fail();
        }
    }

    public FileSystemResult GetFiles(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return new FileSystemResult.Fail();
        }

        return new FileSystemResult.SuccessWithResult<IEnumerable<string>>(Directory.GetFiles(path));
    }

    public FileSystemResult GetDirectories(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return new FileSystemResult.Fail();
        }

        return new FileSystemResult.SuccessWithResult<IEnumerable<string>>(Directory.GetDirectories(path));
    }
}
