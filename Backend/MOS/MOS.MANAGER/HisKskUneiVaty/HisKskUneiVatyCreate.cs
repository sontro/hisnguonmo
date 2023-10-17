using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskUneiVaty
{
    partial class HisKskUneiVatyCreate : BusinessBase
    {
		private List<HIS_KSK_UNEI_VATY> recentHisKskUneiVatys = new List<HIS_KSK_UNEI_VATY>();
		
        internal HisKskUneiVatyCreate()
            : base()
        {

        }

        internal HisKskUneiVatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_UNEI_VATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskUneiVatyCheck checker = new HisKskUneiVatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisKskUneiVatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskUneiVaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskUneiVaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskUneiVatys.Add(data);
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
		
		internal bool CreateList(List<HIS_KSK_UNEI_VATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskUneiVatyCheck checker = new HisKskUneiVatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskUneiVatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskUneiVaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskUneiVaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisKskUneiVatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisKskUneiVatys))
            {
                if (!DAOWorker.HisKskUneiVatyDAO.TruncateList(this.recentHisKskUneiVatys))
                {
                    LogSystem.Warn("Rollback du lieu HisKskUneiVaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskUneiVatys", this.recentHisKskUneiVatys));
                }
				this.recentHisKskUneiVatys = null;
            }
        }
    }
}
