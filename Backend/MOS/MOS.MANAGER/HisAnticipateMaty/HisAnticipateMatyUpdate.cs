using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipateMaty
{
	partial class HisAnticipateMatyUpdate : BusinessBase
	{
		private List<HIS_ANTICIPATE_MATY> beforeUpdateHisAnticipateMatys = new List<HIS_ANTICIPATE_MATY>();
		
		internal HisAnticipateMatyUpdate()
			: base()
		{

		}

		internal HisAnticipateMatyUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_ANTICIPATE_MATY data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisAnticipateMatyCheck checker = new HisAnticipateMatyCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_ANTICIPATE_MATY raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisAnticipateMatys.Add(raw);
					if (!DAOWorker.HisAnticipateMatyDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateMaty_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisAnticipateMaty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ANTICIPATE_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateMatyCheck checker = new HisAnticipateMatyCheck(param);
                List<HIS_ANTICIPATE_MATY> listRaw = new List<HIS_ANTICIPATE_MATY>();
                this.beforeUpdateHisAnticipateMatys.AddRange(listRaw);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisAnticipateMatys.AddRange(listRaw);
                    result = DAOWorker.HisAnticipateMatyDAO.UpdateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAnticipateMatys))
			{
                if (!new HisAnticipateMatyUpdate(param).UpdateList(this.beforeUpdateHisAnticipateMatys))
				{
                    LogSystem.Warn("Rollback du lieu HisAnticipateMaty that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisAnticipateMatys", this.beforeUpdateHisAnticipateMatys));
				}
			}
		}
	}
}
