using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRehaTrainType
{
    partial class HisRehaTrainTypeUpdate : BusinessBase
    {
		private List<HIS_REHA_TRAIN_TYPE> beforeUpdateHisRehaTrainTypes = new List<HIS_REHA_TRAIN_TYPE>();
		
        internal HisRehaTrainTypeUpdate()
            : base()
        {

        }

        internal HisRehaTrainTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REHA_TRAIN_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainTypeCheck checker = new HisRehaTrainTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REHA_TRAIN_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.REHA_TRAIN_TYPE_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisRehaTrainTypes.Add(raw);
					if (!DAOWorker.HisRehaTrainTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrainType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRehaTrainType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_REHA_TRAIN_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaTrainTypeCheck checker = new HisRehaTrainTypeCheck(param);
                List<HIS_REHA_TRAIN_TYPE> listRaw = new List<HIS_REHA_TRAIN_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.REHA_TRAIN_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisRehaTrainTypes.AddRange(listRaw);
					if (!DAOWorker.HisRehaTrainTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaTrainType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRehaTrainType that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRehaTrainTypes))
            {
                if (!new HisRehaTrainTypeUpdate(param).UpdateList(this.beforeUpdateHisRehaTrainTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisRehaTrainType that bai, can kiem tra lai." + LogUtil.TraceData("HisRehaTrainTypes", this.beforeUpdateHisRehaTrainTypes));
                }
            }
        }
    }
}
