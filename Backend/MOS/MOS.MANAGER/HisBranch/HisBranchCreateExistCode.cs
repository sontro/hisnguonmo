using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;

namespace MOS.MANAGER.HisBranch
{
    partial class HisBranchCreate : BusinessBase
    {
        private List<HIS_BRANCH> recentHisBranchs = new List<HIS_BRANCH>();

        internal HisBranchCreate()
            : base()
        {

        }

        internal HisBranchCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisBranchSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBranchCheck checker = new HisBranchCheck(param);
                valid = valid && checker.VerifyRequireField(data.Branch);
                valid = valid && checker.ExistsCode(data.Branch.BRANCH_CODE, null);
                if (valid)
                {
                    this.ProcessImage(data);

                    if (!DAOWorker.HisBranchDAO.Create(data.Branch))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranch_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBranch that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBranchs.Add(data.Branch);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessImage(HisBranchSDO data)
        {
            try
            {
                if (data.ImageData != null && data.ImageData.Length > 0)
                {
                    List<FileHolder> fileHolders = new List<FileHolder>();
                    FileHolder bhytFile = new FileHolder();
                    MemoryStream bhytStream = new MemoryStream();
                    bhytStream.Write(data.ImageData, 0, data.ImageData.Length);
                    bhytStream.Position = 0;
                    bhytFile.Content = bhytStream;
                    bhytFile.FileName = data.Branch.BRANCH_CODE + "_LOGO.jpeg";
                    fileHolders.Add(bhytFile);
                    string url = "";
                    List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(Constant.APPLICATION_CODE, FileStoreLocation.BRANCH, fileHolders, true);
                    if (fileUploadInfos != null && fileUploadInfos.Count == fileHolders.Count)
                    {
                        foreach (FileUploadInfo info in fileUploadInfos)
                        {
                            if (!String.IsNullOrWhiteSpace(info.OriginalName))
                            {
                                if (info.OriginalName.Contains(data.Branch.BRANCH_CODE))
                                {
                                    url = info.Url;
                                    data.Branch.LOGO_URL = info.Url;
                                }
                            }
                        }
                    }
                    if (String.IsNullOrWhiteSpace(url))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisBranch_LuuAnhLogoThatBai);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisBranchs))
            {
                if (!new HisBranchTruncate(param).TruncateList(this.recentHisBranchs))
                {
                    LogSystem.Warn("Rollback du lieu HisBranch that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBranchs", this.recentHisBranchs));
                }
            }
        }
    }
}
