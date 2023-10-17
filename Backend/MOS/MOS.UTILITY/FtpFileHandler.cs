using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace MOS.UTILITY
{
    public class FtpFileHandler
    {
        public static bool Write(string ip, string user, string pass, string data, string fileName, string saveFolder)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(data) && !String.IsNullOrEmpty(fileName) && !String.IsNullOrEmpty(saveFolder))
                {
                    string filePath = GetFtpFilePath(ip, saveFolder, fileName);
                    LogSystem.Debug("Write file by Ftp filepath: " + filePath);

                    //if (IsExistsFileOnServer(user, pass, filePath))
                    //{
                    //    DeleteFile(user, pass, filePath);
                    //}

                    byte[] bytes = Encoding.UTF8.GetBytes(data); 
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filePath);
                    request.Credentials = new NetworkCredential(user, pass);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    using (var requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(bytes, 0, bytes.Length);
                    }
                    var response = (FtpWebResponse)request.GetResponse();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Ftp write file fail: " + ex);
                result = false;
            }
            return result;
        }

        public static bool Move(string ip, string user, string pass, FileInfo file, string successFolder)
        {
            bool result = false;
            try
            {
                string destinationPath = GetFtpDestinationFilePath(ip, file.FolderName, file.FileName, successFolder);

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(file.PathFile);
                request.Credentials = new NetworkCredential(user, pass);
                request.Method = WebRequestMethods.Ftp.Rename;
                request.RenameTo = destinationPath;
                var response = (FtpWebResponse)request.GetResponse();
                result = response.StatusCode == FtpStatusCode.CommandOK || response.StatusCode == FtpStatusCode.FileActionOK;
            }
            catch (Exception ex)
            {
                LogSystem.Error("Ftp move file fail: " + ex);
                result = false;
            }

            return result;
        }

        public static List<FileInfo> Read(string ip, string user, string pass, string readFolder)
        {
            List<FileInfo> result = null;
            try
            {
                // Lay foderPath cua thu muc can doc cac files
                string folderPath = GetReadFolderPath(ip, readFolder);

                // Tao xac thuc FTP voi user va pass
                NetworkCredential authentication = new NetworkCredential(user, pass);

                // Lay ra ten cac file co trong thu muc ReadFolder
                List<string> fileNames = new List<string>();
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(folderPath);
                request.Credentials = authentication;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        while (!reader.EndOfStream)  
                        {
                            string fileName = reader.ReadLine().ToString();
                            fileNames.Add(fileName);
                        }
                    }
                }

                // Lay thong tin chi tiet cac file trong thu muc ReadFolder
                if (fileNames != null && fileNames.Count > 0)
                {
                    result = new List<FileInfo>();
                    foreach (var name in fileNames)
                    {
                        // Neu la file thi moi tiep tuc
                        if (IsExistsFileOnServer(authentication, folderPath + name))
                        {
                            FileInfo fileInfo = new FileInfo();
                            using (var client = new WebClient())
                            {
                                client.Credentials = authentication;
                                byte[] byteData = client.DownloadData(folderPath + name);
                                string stringData = System.Text.Encoding.UTF8.GetString(byteData);

                                // Gan du lieu tra ve
                                fileInfo.Data = stringData.Replace("\n", "");
                                fileInfo.FileName = name;
                                fileInfo.FolderName = folderPath;
                                fileInfo.PathFile = folderPath + name;
                                result.Add(fileInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        private static string GetReadFolderPath(string ip, string readFolder)
        {
            return string.Format(@"ftp://{0}/{1}/", ip, readFolder);  // ftp://192.168.1.201/ShareFolder/tichhop/Roche1/ORDERS/
        }

        private static string GetFtpFilePath(string ip, string saveFolder, string fileName)
        {
            return string.Format(@"ftp://{0}/{1}/{2}", ip, saveFolder, fileName);  // ftp://192.168.1.201/ShareFolder/tichhop/Roche1/ORDERS/samplefile.txt
        }

        private static string GetFtpDestinationFilePath(string ip, string folderPath, string fileName, string successFolder)
        {
            if (!folderPath.EndsWith("/"))
            {
                folderPath += "/";
            }
            string ftpHost = string.Format(@"ftp://{0}", ip);  // ftp://192.168.1.201
            string folderPathNoHost = folderPath.Replace(ftpHost, "");  // /ShareFolder/tichhop/Roche1/RESULTS/
            return string.Format(@"{0}{1}/{2}", folderPathNoHost, successFolder, fileName);  // /ShareFolder/tichhop/Roche1/RESULTS/success/samplefile.txt
        }

        public static bool IsExistsFileOnServer(NetworkCredential authentication, string filepath)
        {
            bool result = false;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filepath);
                request.Credentials = authentication;
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                result = true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return result;
        }

        public static bool DeleteFile(NetworkCredential authentication, string filepath)
        {
            bool result = false;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filepath);
                request.Credentials = authentication;
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    //return response.StatusDescription;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Ftp xoa file that bai: " + ex);
                result = false;
            }
            return result;
        }
    }
}
