using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpUserTempDt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpUserTemp
{
    partial class HisImpUserTempUpdate : BusinessBase
    {
        private List<HIS_IMP_USER_TEMP> beforeUpdateHisImpUserTemps = new List<HIS_IMP_USER_TEMP>();

        internal HisImpUserTempUpdate()
            : base()
        {

        }

        internal HisImpUserTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_USER_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpUserTempCheck checker = new HisImpUserTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_USER_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (new HisImpUserTempDtTruncate(param).TruncateByImpUserTempId(data.ID))
                    {
                        List<HIS_IMP_USER_TEMP_DT> ekipTempUsers = data.HIS_IMP_USER_TEMP_DT.ToList();
                        ekipTempUsers.ForEach(t => t.IMP_USER_TEMP_ID = raw.ID);

                        if (new HisImpUserTempDtCreate(param).CreateList(ekipTempUsers))
                        {
                            data.HIS_IMP_USER_TEMP_DT = null;
                            if (!DAOWorker.HisImpUserTempDAO.Update(data))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpUserTemp_CapNhatThatBai);
                                throw new Exception("Cap nhat thong tin HisImpUserTemp that bai." + LogUtil.TraceData("data", data));
                            }
                            this.beforeUpdateHisImpUserTemps.Add(raw);
                            result = true;
                        }
                    }
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

        internal bool UpdateList(List<HIS_IMP_USER_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpUserTempCheck checker = new HisImpUserTempCheck(param);
                List<HIS_IMP_USER_TEMP> listRaw = new List<HIS_IMP_USER_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisImpUserTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpUserTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpUserTemp that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisImpUserTemps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpUserTemps))
            {
                if (!DAOWorker.HisImpUserTempDAO.UpdateList(this.beforeUpdateHisImpUserTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisImpUserTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisImpUserTemps", this.beforeUpdateHisImpUserTemps));
                }
                this.beforeUpdateHisImpUserTemps = null;
            }
        }
    }
}
