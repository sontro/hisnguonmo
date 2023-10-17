using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEkipTempUser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkipTemp
{
    partial class HisEkipTempUpdate : BusinessBase
    {
		private List<HIS_EKIP_TEMP> beforeUpdateHisEkipTemps = new List<HIS_EKIP_TEMP>();
		
        internal HisEkipTempUpdate()
            : base()
        {

        }

        internal HisEkipTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EKIP_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipTempCheck checker = new HisEkipTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EKIP_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotDuplicate(data);
                if (valid)
                {
                    if (new HisEkipTempUserTruncate(param).TruncateByEkipTempId(data.ID))
                    {
                        List<HIS_EKIP_TEMP_USER> ekipTempUsers = data.HIS_EKIP_TEMP_USER.ToList();
                        ekipTempUsers.ForEach(t => t.EKIP_TEMP_ID = raw.ID);

                        if (new HisEkipTempUserCreate(param).CreateList(ekipTempUsers))
                        {
                            data.HIS_EKIP_TEMP_USER = null;
                            if (!DAOWorker.HisEkipTempDAO.Update(data))
                            {
                                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipTemp_CapNhatThatBai);
                                throw new Exception("Cap nhat thong tin HisEkipTemp that bai." + LogUtil.TraceData("data", data));
                            }
                            this.beforeUpdateHisEkipTemps.Add(raw);
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

        internal bool UpdateList(List<HIS_EKIP_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipTempCheck checker = new HisEkipTempCheck(param);
                List<HIS_EKIP_TEMP> listRaw = new List<HIS_EKIP_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisEkipTemps.AddRange(listRaw);
					if (!DAOWorker.HisEkipTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkipTemp that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEkipTemps))
            {
                if (!DAOWorker.HisEkipTempDAO.UpdateList(this.beforeUpdateHisEkipTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisEkipTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisEkipTemps", this.beforeUpdateHisEkipTemps));
                }
            }
        }
    }
}
