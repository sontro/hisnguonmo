using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceMety
{
	partial class HisServiceMetyUpdate : BusinessBase
	{
		private List<HIS_SERVICE_METY> beforeUpdateHisServiceMetys = new List<HIS_SERVICE_METY>();
		
		internal HisServiceMetyUpdate()
			: base()
		{

		}

		internal HisServiceMetyUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_SERVICE_METY data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisServiceMetyCheck checker = new HisServiceMetyCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_SERVICE_METY raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisServiceMetys.Add(raw);
					if (!DAOWorker.HisServiceMetyDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMety_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisServiceMety that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERVICE_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceMetyCheck checker = new HisServiceMetyCheck(param);
                List<HIS_SERVICE_METY> listRaw = new List<HIS_SERVICE_METY>();
                this.beforeUpdateHisServiceMetys.AddRange(listRaw);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisServiceMetys.AddRange(listRaw);
                    result = DAOWorker.HisServiceMetyDAO.UpdateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceMetys))
			{
                if (!new HisServiceMetyUpdate(param).UpdateList(this.beforeUpdateHisServiceMetys))
				{
                    LogSystem.Warn("Rollback du lieu HisServiceMety that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisServiceMetys", this.beforeUpdateHisServiceMetys));
				}
			}
		}
	}
}
