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
    public class SharedFolderFileHandler
    {
        private const string SHARED_FOLDER_ACCESS_COMMAND = @"/C net use \\{0} {1} /user:{2}";
        private const string EVERYONE_SHARED_FOLDER_ACCESS_COMMAND = @"/C net use \\{0}";

        private const int TIME_OUT = 60000;

        public static bool Write(string ip, string user, string pass, string data, string fileName, string saveFolder)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(data) && !String.IsNullOrEmpty(fileName) && !String.IsNullOrEmpty(saveFolder))
                {
                    if (!OpenConnect(ip, user, pass))
                    {
                        LogSystem.Warn(string.Format("Dang nhap vao IP: {0}, user: {1}, pass: {2} that bai", ip, user, pass));
                        return false;
                    }

                    string folder = GetSharedFolderPath(ip, saveFolder);

                    string filePath = Path.Combine(folder, fileName);

                    if (File.Exists(filePath))
                    {
                        //đã tồn tại file thì xóa
                        File.Delete(filePath);
                    }

                    //Thuc hien ghi file
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine(data);
                    }
                    if (!File.Exists(filePath))
                    {
                        result = false;
                        LogSystem.Warn("Luu file that bai. data: " + data + "; fileName: " + fileName + "; folder: " + folder);
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public static void Move(string ip, string user, string pass, FileInfo file, string successFolder)
        {
            try
            {
                if (file != null)
                {
                    if (!OpenConnect(ip, user, pass))
                    {
                        LogSystem.Warn(string.Format("Dang nhap vao IP: {0}, user: {1}, pass: {2} that bai", ip, user, pass));
                        return;
                    }

                    string successPath = file.FolderName + "\\" + successFolder;
                    if (!System.IO.Directory.Exists(successPath))
                        System.IO.Directory.CreateDirectory(successPath);
                    File.Copy(Path.Combine(file.FolderName, file.FileName), Path.Combine(successPath, file.FileName), true);
                    File.Delete(file.PathFile);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Move file that bai: " + ex);
            }
        }

        public static List<FileInfo> Read(string ip, string user, string pass, string readFolder)
        {
            List<FileInfo> result = null;
            try
            {
                if (!OpenConnect(ip, user, pass))
                {
                    LogSystem.Warn(string.Format("Dang nhap vao IP: {0}, user: {1}, pass: {2} that bai", ip, user, pass));
                    return null;
                }

                string folderName = GetSharedFolderPath(ip, readFolder);

                if (!System.IO.Directory.Exists(folderName))
                {
                    LogSystem.Warn("Khong ton tai folder can doc: " + folderName);
                    return null;
                }

                string[] fileEntries = System.IO.Directory.GetFiles(folderName).OrderBy(f => f).ToArray();

                if (fileEntries == null || fileEntries.Length == 0)
                {
                    LogSystem.Debug("Folder can doc khong co file nao: " + folderName);
                    return null;
                }

                result = new List<FileInfo>();
                foreach (var file in fileEntries)
                {
                    FileInfo fileInfo = new FileInfo();
                    fileInfo.FolderName = folderName;//\\192.168.1.201\sharefolder\readFolder
                    fileInfo.PathFile = file;//\\192.168.1.201\sharefolder\readFolder\ORU_212323.HL7
                    fileInfo.FileName = file.Substring(file.LastIndexOf("\\") + 1).ToString();//ORU_212323.HL7

                    string line = "";
                    string data = "";
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            data += line + "\r";
                        }
                    }

                    fileInfo.Data = data;
                    result.Add(fileInfo);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        private static bool OpenConnect(string ip, string user, string pass)
        {
            bool result = true;
            try
            {
                if (!string.IsNullOrEmpty(ip) && !String.IsNullOrEmpty(user))
                {
                    string url = "";
                    if (!String.IsNullOrEmpty(user))
                    {
                        url = String.Format(SHARED_FOLDER_ACCESS_COMMAND, ip, pass, user);
                    }
                    else
                    {
                        url = String.Format(EVERYONE_SHARED_FOLDER_ACCESS_COMMAND, ip);
                    }

                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = "cmd.exe";
                        process.StartInfo.Arguments = url;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.RedirectStandardError = true;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                        StringBuilder output = new StringBuilder();
                        StringBuilder error = new StringBuilder();

                        using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                        using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                        {
                            process.OutputDataReceived += (sender, e) =>
                            {
                                if (e.Data == null)
                                {
                                    outputWaitHandle.Set();
                                }
                                else
                                {
                                    output.AppendLine(e.Data);
                                }
                            };
                            process.ErrorDataReceived += (sender, e) =>
                            {
                                if (e.Data == null)
                                {
                                    errorWaitHandle.Set();
                                }
                                else
                                {
                                    error.AppendLine(e.Data);
                                }
                            };

                            process.Start();

                            process.BeginOutputReadLine();
                            process.BeginErrorReadLine();

                            if (process.WaitForExit(TIME_OUT) &&
                                outputWaitHandle.WaitOne(TIME_OUT) &&
                                errorWaitHandle.WaitOne(TIME_OUT))
                            {
                                // Process completed. Check process.ExitCode here.
                                if (process.ExitCode != 0)
                                {
                                    result = false;
                                }
                            }
                            else
                            {
                                // Timed out.
                                result = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private static string GetSharedFolderPath(string ip, string folder)
        {
            return string.Format(@"\\{0}\{1}", ip, folder);
        }
    }
}