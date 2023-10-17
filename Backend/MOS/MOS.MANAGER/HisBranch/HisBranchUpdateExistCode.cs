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
using System.Linq;

namespace MOS.MANAGER.HisBranch
{
    partial class HisBranchUpdate : BusinessBase
    {
		private List<HIS_BRANCH> beforeUpdateHisBranchs = new List<HIS_BRANCH>();
		
        internal HisBranchUpdate()
            : base()
        {

        }

        internal HisBranchUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HisBranchSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBranchCheck checker = new HisBranchCheck(param);
                valid = valid && checker.VerifyRequireField(data.Branch);
                HIS_BRANCH raw = null;
                valid = valid && checker.VerifyId(data.Branch.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.Branch.BRANCH_CODE, data.Branch.ID);
                if (valid)
                {
                    this.ProcessImage(data);
					if (!DAOWorker.HisBranchDAO.Update(data.Branch))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranch_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBranch that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisBranchs.Add(raw);
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

        internal bool UpdateList(List<HIS_BRANCH> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBranchCheck checker = new HisBranchCheck(param);
                List<HIS_BRANCH> listRaw = new List<HIS_BRANCH>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BRANCH_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBranchs.AddRange(listRaw);
					if (!DAOWorker.HisBranchDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranch_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBranch that bai." + LogUtil.TraceData("listData", listData));
                    }
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisBranchs))
            {
                if (!new HisBranchUpdate(param).UpdateList(this.beforeUpdateHisBranchs))
                {
                    LogSystem.Warn("Rollback du lieu HisBranch that bai, can kiem tra lai." + LogUtil.TraceData("HisBranchs", this.beforeUpdateHisBranchs));
                }
            }
        }
    }
}
