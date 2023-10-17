using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTestSampleType
{
    partial class HisTestSampleTypeUpdate : BusinessBase
    {
		private List<HIS_TEST_SAMPLE_TYPE> beforeUpdateHisTestSampleTypes = new List<HIS_TEST_SAMPLE_TYPE>();
		
        internal HisTestSampleTypeUpdate()
            : base()
        {

        }

        internal HisTestSampleTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TEST_SAMPLE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTestSampleTypeCheck checker = new HisTestSampleTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TEST_SAMPLE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TEST_SAMPLE_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTestSampleTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestSampleType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTestSampleType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTestSampleTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TEST_SAMPLE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTestSampleTypeCheck checker = new HisTestSampleTypeCheck(param);
                List<HIS_TEST_SAMPLE_TYPE> listRaw = new List<HIS_TEST_SAMPLE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TEST_SAMPLE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTestSampleTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTestSampleType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTestSampleType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTestSampleTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTestSampleTypes))
            {
                if (!DAOWorker.HisTestSampleTypeDAO.UpdateList(this.beforeUpdateHisTestSampleTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisTestSampleType that bai, can kiem tra lai." + LogUtil.TraceData("HisTestSampleTypes", this.beforeUpdateHisTestSampleTypes));
                }
				this.beforeUpdateHisTestSampleTypes = null;
            }
        }
    }
}
