using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisReportTypeCat
{
    partial class HisReportTypeCatUpdate : BusinessBase
    {
		private List<HIS_REPORT_TYPE_CAT> beforeUpdateHisReportTypeCats = new List<HIS_REPORT_TYPE_CAT>();
		
        internal HisReportTypeCatUpdate()
            : base()
        {

        }

        internal HisReportTypeCatUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REPORT_TYPE_CAT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisReportTypeCatCheck checker = new HisReportTypeCatCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REPORT_TYPE_CAT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNoExisted(raw);
                if (valid)
                {
                    this.beforeUpdateHisReportTypeCats.Add(raw);
					if (!DAOWorker.HisReportTypeCatDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisReportTypeCat_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisReportTypeCat that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_REPORT_TYPE_CAT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisReportTypeCatCheck checker = new HisReportTypeCatCheck(param);
                List<HIS_REPORT_TYPE_CAT> listRaw = new List<HIS_REPORT_TYPE_CAT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisReportTypeCats = listRaw;
					if (!DAOWorker.HisReportTypeCatDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisReportTypeCat_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisReportTypeCat that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisReportTypeCats))
            {
                if (!DAOWorker.HisReportTypeCatDAO.UpdateList(this.beforeUpdateHisReportTypeCats))
                {
                    LogSystem.Warn("Rollback du lieu HisReportTypeCat that bai, can kiem tra lai." + LogUtil.TraceData("HisReportTypeCats", this.beforeUpdateHisReportTypeCats));
                }
            }
        }
    }
}
