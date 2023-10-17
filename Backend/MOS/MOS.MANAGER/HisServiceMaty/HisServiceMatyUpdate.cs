using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceMaty
{
	partial class HisServiceMatyUpdate : BusinessBase
	{
		private List<HIS_SERVICE_MATY> beforeUpdateHisServiceMatys = new List<HIS_SERVICE_MATY>();
		
		internal HisServiceMatyUpdate()
			: base()
		{

		}

		internal HisServiceMatyUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_SERVICE_MATY data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisServiceMatyCheck checker = new HisServiceMatyCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_SERVICE_MATY raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisServiceMatys.Add(raw);
					if (!DAOWorker.HisServiceMatyDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMaty_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisServiceMaty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_SERVICE_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceMatyCheck checker = new HisServiceMatyCheck(param);
                List<HIS_SERVICE_MATY> listRaw = new List<HIS_SERVICE_MATY>();
                this.beforeUpdateHisServiceMatys.AddRange(listRaw);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisServiceMatys.AddRange(listRaw);
                    result = DAOWorker.HisServiceMatyDAO.UpdateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceMatys))
			{
                if (!new HisServiceMatyUpdate(param).UpdateList(this.beforeUpdateHisServiceMatys))
				{
                    LogSystem.Warn("Rollback du lieu HisServiceMaty that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisServiceMatys", this.beforeUpdateHisServiceMatys));
				}
			}
		}
	}
}
