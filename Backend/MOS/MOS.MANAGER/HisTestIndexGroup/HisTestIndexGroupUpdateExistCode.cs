using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTestIndexGroup
{
    partial class HisTestIndexGroupUpdate : BusinessBase
    {
		private List<HIS_TEST_INDEX_GROUP> beforeUpdateHisTestIndexGroups = new List<HIS_TEST_INDEX_GROUP>();
		
        internal HisTestIndexGroupUpdate()
            : base()
        {

        }

        internal HisTestIndexGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TEST_INDEX_GROUP data)
        {
            bool result = false;
            try
            {
                
                bool valid = true;
                HisTestIndexGroupCheck checker = new HisTestIndexGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TEST_INDEX_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TEST_INDEX_GROUP_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTestIndexGroupDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestIndexGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTestIndexGroup that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTestIndexGroups.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TEST_INDEX_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestIndexGroupCheck checker = new HisTestIndexGroupCheck(param);
                List<HIS_TEST_INDEX_GROUP> listRaw = new List<HIS_TEST_INDEX_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TEST_INDEX_GROUP_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTestIndexGroupDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestIndexGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTestIndexGroup that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTestIndexGroups.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTestIndexGroups))
            {
                if (!DAOWorker.HisTestIndexGroupDAO.UpdateList(this.beforeUpdateHisTestIndexGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisTestIndexGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisTestIndexGroups", this.beforeUpdateHisTestIndexGroups));
                }
				this.beforeUpdateHisTestIndexGroups = null;
            }
        }
    }
}
