using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public interface IFileSystemManager
{
    FileSystemResult Connect(string path, string mode);

    FileSystemResult Disconnect();

    FileSystemResult GotoDirectory(string path);

    FileSystemResult GetDirectory(int depth);

    FileSystemResult GetFileContent(string path);

    FileSystemResult MoveFile(string sourcePath, string destinationPath);

    FileSystemResult CopyFile(string sourcePath, string destinationPath);

    FileSystemResult DeleteFile(string path);

    FileSystemResult RenameFile(string path, string newName);

    FileSystemResult GetFileName(string path);

    FileSystemResult GetDirectoryName(string path);

    FileSystemResult GetFiles(string path);

    FileSystemResult GetDirectories(string path);
}
