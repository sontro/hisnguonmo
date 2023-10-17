using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.UTILITY
{
    public enum FileHandlerType
    {
        SharedFolderDefault = 0,
        SharedFolder = 1,
        FTP = 2
    }

    public class FileInfo
    {
        public string FolderName { get; set; }
        public string PathFile { get; set; }
        public string FileName { get; set; }
        public string Data { get; set; }
    }

    public class FileHandler
    {
        public static bool Write(string ip, string user, string pass, string data, string fileName, string saveFolder, FileHandlerType fileHandlerType = FileHandlerType.SharedFolder)
        {
            bool result = false;
            try
            {
                if (fileHandlerType == FileHandlerType.SharedFolderDefault || fileHandlerType == FileHandlerType.SharedFolder)
                {
                    result = SharedFolderFileHandler.Write(ip, user, pass, data, fileName, saveFolder);
                }
                else if (fileHandlerType == FileHandlerType.FTP)
                {
                    result = FtpFileHandler.Write(ip, user, pass, data, fileName, saveFolder);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public static void Move(string ip, string user, string pass, FileInfo file, string successFolder, FileHandlerType fileHandlerType = FileHandlerType.SharedFolder)
        {
            try
            {
                if (fileHandlerType == FileHandlerType.SharedFolderDefault || fileHandlerType == FileHandlerType.SharedFolder)
                {
                    SharedFolderFileHandler.Move(ip, user, pass, file, successFolder);
                }
                else if (fileHandlerType == FileHandlerType.FTP)
                {
                    FtpFileHandler.Move(ip, user, pass, file, successFolder);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public static List<FileInfo> Read(string ip, string user, string pass, string readFolder, FileHandlerType fileHandlerType = FileHandlerType.SharedFolder)
        {
            List<FileInfo> result = null;
            try
            {
                if (fileHandlerType == FileHandlerType.SharedFolderDefault || fileHandlerType == FileHandlerType.SharedFolder)
                {
                    result = SharedFolderFileHandler.Read(ip, user, pass, readFolder);
                }
                else if (fileHandlerType == FileHandlerType.FTP)
                {
                    result = FtpFileHandler.Read(ip, user, pass, readFolder);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}