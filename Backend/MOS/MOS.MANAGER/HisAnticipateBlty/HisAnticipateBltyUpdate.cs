using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipateBlty
{
    partial class HisAnticipateBltyUpdate : BusinessBase
    {
		private List<HIS_ANTICIPATE_BLTY> beforeUpdateHisAnticipateBltys = new List<HIS_ANTICIPATE_BLTY>();
		
        internal HisAnticipateBltyUpdate()
            : base()
        {

        }

        internal HisAnticipateBltyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ANTICIPATE_BLTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateBltyCheck checker = new HisAnticipateBltyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ANTICIPATE_BLTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisAnticipateBltys.Add(raw);
					if (!DAOWorker.HisAnticipateBltyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateBlty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAnticipateBlty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ANTICIPATE_BLTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateBltyCheck checker = new HisAnticipateBltyCheck(param);
                List<HIS_ANTICIPATE_BLTY> listRaw = new List<HIS_ANTICIPATE_BLTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisAnticipateBltys.AddRange(listRaw);
					if (!DAOWorker.HisAnticipateBltyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateBlty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAnticipateBlty that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAnticipateBltys))
            {
                if (!new HisAnticipateBltyUpdate(param).UpdateList(this.beforeUpdateHisAnticipateBltys))
                {
                    LogSystem.Warn("Rollback du lieu HisAnticipateBlty that bai, can kiem tra lai." + LogUtil.TraceData("HisAnticipateBltys", this.beforeUpdateHisAnticipateBltys));
                }
            }
        }
    }
}
