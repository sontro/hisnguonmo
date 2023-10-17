using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipateMety
{
	partial class HisAnticipateMetyUpdate : BusinessBase
	{
		private List<HIS_ANTICIPATE_METY> beforeUpdateHisAnticipateMetys = new List<HIS_ANTICIPATE_METY>();
		
		internal HisAnticipateMetyUpdate()
			: base()
		{

		}

		internal HisAnticipateMetyUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_ANTICIPATE_METY data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisAnticipateMetyCheck checker = new HisAnticipateMetyCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_ANTICIPATE_METY raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisAnticipateMetys.Add(raw);
					if (!DAOWorker.HisAnticipateMetyDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateMety_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisAnticipateMety that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ANTICIPATE_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateMetyCheck checker = new HisAnticipateMetyCheck(param);
                List<HIS_ANTICIPATE_METY> listRaw = new List<HIS_ANTICIPATE_METY>();
                this.beforeUpdateHisAnticipateMetys.AddRange(listRaw);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisAnticipateMetys.AddRange(listRaw);
                    result = DAOWorker.HisAnticipateMetyDAO.UpdateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAnticipateMetys))
			{
                if (!new HisAnticipateMetyUpdate(param).UpdateList(this.beforeUpdateHisAnticipateMetys))
				{
                    LogSystem.Warn("Rollback du lieu HisAnticipateMety that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisAnticipateMetys", this.beforeUpdateHisAnticipateMetys));
				}
			}
		}
	}
}
