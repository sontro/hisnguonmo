using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheFile
{
    public class RemoveFile
    {
        public void Remove(ReadFileData fileData, string successFolder)
        {
            try
            {
                if (fileData != null)
                {
                    string successPath = fileData.FolderName + successFolder;
                    if (!System.IO.Directory.Exists(successPath))
                        System.IO.Directory.CreateDirectory(successPath);
                    File.Copy(Path.Combine(fileData.FolderName, fileData.FileName), Path.Combine(successPath, fileData.FileName), true);
                    File.Delete(fileData.PathFile);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Remove file result that bai: " + ex);
            }
        }
    }
}
