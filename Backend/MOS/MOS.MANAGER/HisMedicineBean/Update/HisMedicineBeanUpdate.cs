using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineBean.Update
{
	class HisMedicineBeanUpdate : BusinessBase
	{
		private List<HIS_MEDICINE_BEAN> beforeUpdateHisMedicineBeanDTOs = new List<HIS_MEDICINE_BEAN>();
		
		internal HisMedicineBeanUpdate()
			: base()
		{

		}

		internal HisMedicineBeanUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_MEDICINE_BEAN data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HIS_MEDICINE_BEAN raw = null;
				HisMedicineBeanCheck checker = new HisMedicineBeanCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisMedicineBeanDTOs.Add(raw);
					if (!DAOWorker.HisMedicineBeanDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineBean_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisMedicineBean that bai." + LogUtil.TraceData("data", data));
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

		internal bool UpdateList(List<HIS_MEDICINE_BEAN> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisMedicineBeanCheck checker = new HisMedicineBeanCheck(param);
				List<HIS_MEDICINE_BEAN> listRaw = new List<HIS_MEDICINE_BEAN>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					this.beforeUpdateHisMedicineBeanDTOs.AddRange(listRaw);
					if (!DAOWorker.HisMedicineBeanDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineBean_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisMedicineBean that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateListNoCheckLock(List<HIS_MEDICINE_BEAN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineBeanCheck checker = new HisMedicineBeanCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicineBeanDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineBean_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineBean that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineBeanDTOs))
			{
                if (!DAOWorker.HisMedicineBeanDAO.UpdateList(this.beforeUpdateHisMedicineBeanDTOs))
				{
					LogSystem.Warn("Rollback du lieu HisMedicineBean that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineBeanDTOs", this.beforeUpdateHisMedicineBeanDTOs));
				}
                this.beforeUpdateHisMedicineBeanDTOs = null;
			}
		}
	}
}
