using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescCreate : BusinessBase
    {
		private List<HIS_SKIN_SURGERY_DESC> recentHisSkinSurgeryDescs = new List<HIS_SKIN_SURGERY_DESC>();
		
        internal HisSkinSurgeryDescCreate()
            : base()
        {

        }

        internal HisSkinSurgeryDescCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SKIN_SURGERY_DESC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSkinSurgeryDescCheck checker = new HisSkinSurgeryDescCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSkinSurgeryDescDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSkinSurgeryDesc_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSkinSurgeryDesc that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSkinSurgeryDescs.Add(data);
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
		
		internal bool CreateList(List<HIS_SKIN_SURGERY_DESC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSkinSurgeryDescCheck checker = new HisSkinSurgeryDescCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSkinSurgeryDescDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSkinSurgeryDesc_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSkinSurgeryDesc that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSkinSurgeryDescs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSkinSurgeryDescs))
            {
                if (!DAOWorker.HisSkinSurgeryDescDAO.TruncateList(this.recentHisSkinSurgeryDescs))
                {
                    LogSystem.Warn("Rollback du lieu HisSkinSurgeryDesc that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSkinSurgeryDescs", this.recentHisSkinSurgeryDescs));
                }
				this.recentHisSkinSurgeryDescs = null;
            }
        }
    }
}
