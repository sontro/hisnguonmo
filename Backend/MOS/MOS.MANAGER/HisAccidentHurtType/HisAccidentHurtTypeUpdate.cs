using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentHurtType
{
	partial class HisAccidentHurtTypeUpdate : BusinessBase
	{
		private List<HIS_ACCIDENT_HURT_TYPE> beforeUpdateHisAccidentHurtTypes = new List<HIS_ACCIDENT_HURT_TYPE>();
		
		internal HisAccidentHurtTypeUpdate()
			: base()
		{

		}

		internal HisAccidentHurtTypeUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_ACCIDENT_HURT_TYPE data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisAccidentHurtTypeCheck checker = new HisAccidentHurtTypeCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_ACCIDENT_HURT_TYPE raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisAccidentHurtTypes.Add(raw);
					if (!DAOWorker.HisAccidentHurtTypeDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHurtType_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisAccidentHurtType that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ACCIDENT_HURT_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentHurtTypeCheck checker = new HisAccidentHurtTypeCheck(param);
                List<HIS_ACCIDENT_HURT_TYPE> listRaw = new List<HIS_ACCIDENT_HURT_TYPE>();
                this.beforeUpdateHisAccidentHurtTypes.AddRange(listRaw);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisAccidentHurtTypeDAO.UpdateList(listData);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAccidentHurtTypes))
			{
                if (!new HisAccidentHurtTypeUpdate(param).UpdateList(this.beforeUpdateHisAccidentHurtTypes))
				{
                    LogSystem.Warn("Rollback du lieu HisAccidentHurtType that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisAccidentHurtTypes", this.beforeUpdateHisAccidentHurtTypes));
				}
			}
		}
	}
}
