using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTestType
{
    partial class HisTestTypeUpdate : BusinessBase
    {
		private List<HIS_TEST_TYPE> beforeUpdateHisTestTypes = new List<HIS_TEST_TYPE>();
		
        internal HisTestTypeUpdate()
            : base()
        {

        }

        internal HisTestTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TEST_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestTypeCheck checker = new HisTestTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TEST_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TEST_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTestTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTestType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTestTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TEST_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestTypeCheck checker = new HisTestTypeCheck(param);
                List<HIS_TEST_TYPE> listRaw = new List<HIS_TEST_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TEST_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTestTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTestType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTestTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTestTypes))
            {
                if (!DAOWorker.HisTestTypeDAO.UpdateList(this.beforeUpdateHisTestTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisTestType that bai, can kiem tra lai." + LogUtil.TraceData("HisTestTypes", this.beforeUpdateHisTestTypes));
                }
				this.beforeUpdateHisTestTypes = null;
            }
        }
    }
}
