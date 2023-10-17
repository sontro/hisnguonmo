using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskService
{
    partial class HisKskServiceUpdate : BusinessBase
    {
        private List<HIS_KSK_SERVICE> beforeUpdateHisKskServices = new List<HIS_KSK_SERVICE>();

        internal HisKskServiceUpdate()
            : base()
        {

        }

        internal HisKskServiceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckServicePaty(data);
                valid = valid && checker.CheckExecuteRoom(new List<HIS_KSK_SERVICE>() { data });
                if (valid)
                {
                    if (!DAOWorker.HisKskServiceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisKskServices.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskServiceCheck checker = new HisKskServiceCheck(param);
                List<HIS_KSK_SERVICE> listRaw = new List<HIS_KSK_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && checker.CheckExecuteRoom(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.CheckServicePaty(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisKskServiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskService that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisKskServices.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskServices))
            {
                if (!DAOWorker.HisKskServiceDAO.UpdateList(this.beforeUpdateHisKskServices))
                {
                    LogSystem.Warn("Rollback du lieu HisKskService that bai, can kiem tra lai." + LogUtil.TraceData("HisKskServices", this.beforeUpdateHisKskServices));
                }
                this.beforeUpdateHisKskServices = null;
            }
        }
    }
}
