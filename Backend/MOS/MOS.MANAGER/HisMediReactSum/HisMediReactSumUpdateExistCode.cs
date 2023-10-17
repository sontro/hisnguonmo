using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediReactSum
{
    partial class HisMediReactSumUpdate : BusinessBase
    {
		private List<HIS_MEDI_REACT_SUM> beforeUpdateHisMediReactSums = new List<HIS_MEDI_REACT_SUM>();
		
        internal HisMediReactSumUpdate()
            : base()
        {

        }

        internal HisMediReactSumUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_REACT_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediReactSumCheck checker = new HisMediReactSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_REACT_SUM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDI_REACT_SUM_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisMediReactSums.Add(raw);
					if (!DAOWorker.HisMediReactSumDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediReactSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediReactSum that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEDI_REACT_SUM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediReactSumCheck checker = new HisMediReactSumCheck(param);
                List<HIS_MEDI_REACT_SUM> listRaw = new List<HIS_MEDI_REACT_SUM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDI_REACT_SUM_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisMediReactSums.AddRange(listRaw);
					if (!DAOWorker.HisMediReactSumDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediReactSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediReactSum that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediReactSums))
            {
                if (!new HisMediReactSumUpdate(param).UpdateList(this.beforeUpdateHisMediReactSums))
                {
                    LogSystem.Warn("Rollback du lieu HisMediReactSum that bai, can kiem tra lai." + LogUtil.TraceData("HisMediReactSums", this.beforeUpdateHisMediReactSums));
                }
            }
        }
    }
}
