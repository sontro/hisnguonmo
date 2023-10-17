using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareTempDetail
{
    partial class HisCareTempDetailCreate : BusinessBase
    {
		private List<HIS_CARE_TEMP_DETAIL> recentHisCareTempDetails = new List<HIS_CARE_TEMP_DETAIL>();
		
        internal HisCareTempDetailCreate()
            : base()
        {

        }

        internal HisCareTempDetailCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARE_TEMP_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareTempDetailCheck checker = new HisCareTempDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisCareTempDetailDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareTempDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCareTempDetail that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCareTempDetails.Add(data);
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
		
		internal bool CreateList(List<HIS_CARE_TEMP_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareTempDetailCheck checker = new HisCareTempDetailCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCareTempDetailDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareTempDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCareTempDetail that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCareTempDetails.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCareTempDetails))
            {
                if (!DAOWorker.HisCareTempDetailDAO.TruncateList(this.recentHisCareTempDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisCareTempDetail that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCareTempDetails", this.recentHisCareTempDetails));
                }
				this.recentHisCareTempDetails = null;
            }
        }
    }
}
