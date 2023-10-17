using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareType
{
    partial class HisCareTypeUpdate : BusinessBase
    {
		private List<HIS_CARE_TYPE> beforeUpdateHisCareTypeDTOs = new List<HIS_CARE_TYPE>();
		
        internal HisCareTypeUpdate()
            : base()
        {

        }

        internal HisCareTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareTypeCheck checker = new HisCareTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CARE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CARE_TYPE_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisCareTypeDTOs.Add(raw);
					if (!DAOWorker.HisCareTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCareType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_CARE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareTypeCheck checker = new HisCareTypeCheck(param);
                List<HIS_CARE_TYPE> listRaw = new List<HIS_CARE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CARE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisCareTypeDTOs.AddRange(listRaw);
					if (!DAOWorker.HisCareTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCareType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCareTypeDTOs))
            {
                if (!new HisCareTypeUpdate(param).UpdateList(this.beforeUpdateHisCareTypeDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisCareType that bai, can kiem tra lai." + LogUtil.TraceData("HisCareTypeDTOs", this.beforeUpdateHisCareTypeDTOs));
                }
            }
        }
    }
}
