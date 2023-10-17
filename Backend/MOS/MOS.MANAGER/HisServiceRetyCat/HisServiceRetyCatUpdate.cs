using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceRetyCat
{
	partial class HisServiceRetyCatUpdate : BusinessBase
	{
		private List<HIS_SERVICE_RETY_CAT> beforeUpdateHisServiceRetyCats = new List<HIS_SERVICE_RETY_CAT>();
		
		internal HisServiceRetyCatUpdate()
			: base()
		{

		}

		internal HisServiceRetyCatUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_SERVICE_RETY_CAT data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisServiceRetyCatCheck checker = new HisServiceRetyCatCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_SERVICE_RETY_CAT raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisServiceRetyCats.Add(raw);
					if (!DAOWorker.HisServiceRetyCatDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRetyCat_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisServiceRetyCat that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERVICE_RETY_CAT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRetyCatCheck checker = new HisServiceRetyCatCheck(param);
                List<HIS_SERVICE_RETY_CAT> listRaw = new List<HIS_SERVICE_RETY_CAT>();
                this.beforeUpdateHisServiceRetyCats.AddRange(listRaw);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceRetyCatDAO.UpdateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceRetyCats))
			{
                if (!new HisServiceRetyCatUpdate(param).UpdateList(this.beforeUpdateHisServiceRetyCats))
				{
                    LogSystem.Warn("Rollback du lieu HisServiceRetyCat that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisServiceRetyCats", this.beforeUpdateHisServiceRetyCats));
				}
			}
		}
	}
}
