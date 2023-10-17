using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRestRetrType
{
    partial class HisRestRetrTypeUpdate : BusinessBase
    {
        private List<HIS_REST_RETR_TYPE> beforeUpdateHisRestRetrTypes = new List<HIS_REST_RETR_TYPE>();

        internal HisRestRetrTypeUpdate()
            : base()
        {

        }

        internal HisRestRetrTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REST_RETR_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRestRetrTypeCheck checker = new HisRestRetrTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REST_RETR_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsRehaServiceType(data);
                valid = valid && checker.CheckUnique(data);
                if (valid)
                {
                    this.beforeUpdateHisRestRetrTypes.Add(raw);
                    if (!DAOWorker.HisRestRetrTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRestRetrType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRestRetrType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_REST_RETR_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRestRetrTypeCheck checker = new HisRestRetrTypeCheck(param);
                List<HIS_REST_RETR_TYPE> listRaw = new List<HIS_REST_RETR_TYPE>();
                this.beforeUpdateHisRestRetrTypes.AddRange(listRaw);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsRehaServiceType(data);
                    valid = valid && checker.CheckUnique(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisRestRetrTypeDAO.UpdateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRestRetrTypes))
            {
                if (!new HisRestRetrTypeUpdate(param).UpdateList(this.beforeUpdateHisRestRetrTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisRestRetrType that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisRestRetrTypes", this.beforeUpdateHisRestRetrTypes));
                }
            }
        }
    }
}
