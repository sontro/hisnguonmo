using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheFile
{
    public class ReadFileData
    {
        public string FolderName { get; set; }
        public string PathFile { get; set; }
        public string FileName { get; set; }
        public string Result { get; set; }
    }

    public class ReadFile
    {
        public List<ReadFileData> ReadFolder(string folderName)
        {
            List<ReadFileData> result = null;
            try
            {
                if (!System.IO.Directory.Exists(folderName))
                {
                    throw new Exception("Khong ton tai folder can doc ket qua xet nghiem: " + folderName);
                }
                string[] fileEntries = System.IO.Directory.GetFiles(folderName).OrderBy(f => f).ToArray();

                if (fileEntries == null || fileEntries.Length == 0)
                {
                    LogSystem.Warn("Folder can doc ket qua xet nghiem khong co file result nao: " + folderName);
                    return null;
                }
                result = new List<ReadFileData>();
                foreach (var file in fileEntries)
                {
                    ReadFileData data = new ReadFileData();
                    data.FolderName = folderName;
                    data.PathFile = file;
                    data.FileName = file.Substring(file.LastIndexOf("\\") + 1).ToString();
                    string line = "";
                    List<string> testResult = new List<string>();
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            testResult.Add(line);
                        }
                    }
                    string resultTest = "";
                    if (testResult != null && testResult.Count > 0)
                    {
                        for (int i = 0; i < testResult.Count; i++)
                        {
                            resultTest += testResult[i] + "\r";
                        }
                    }

                    if (!String.IsNullOrEmpty(resultTest))
                    {
                        data.Result = resultTest;
                        result.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
