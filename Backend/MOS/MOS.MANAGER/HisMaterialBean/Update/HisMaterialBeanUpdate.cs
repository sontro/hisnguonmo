using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterialBean.Update
{
	class HisMaterialBeanUpdate : BusinessBase
	{
		private List<HIS_MATERIAL_BEAN> beforeUpdateHisMaterialBeans = new List<HIS_MATERIAL_BEAN>();
		
		internal HisMaterialBeanUpdate()
			: base()
		{

		}

		internal HisMaterialBeanUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_MATERIAL_BEAN data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HIS_MATERIAL_BEAN raw = null;
				HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisMaterialBeans.Add(raw);
					if (!DAOWorker.HisMaterialBeanDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialBean_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisMaterialBean that bai." + LogUtil.TraceData("data", data));
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

		internal bool UpdateList(List<HIS_MATERIAL_BEAN> listData)
		{
			bool result = false;
			try
			{
				bool valid = true;
				valid = IsNotNullOrEmpty(listData);
				HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
				List<HIS_MATERIAL_BEAN> listRaw = new List<HIS_MATERIAL_BEAN>();
				List<long> listId = listData.Select(o => o.ID).ToList();
				valid = valid && checker.VerifyIds(listId, listRaw);
				valid = valid && checker.IsUnLock(listRaw);
				foreach (var data in listData)
				{
					valid = valid && checker.VerifyRequireField(data);
				}
				if (valid)
				{
					this.beforeUpdateHisMaterialBeans.AddRange(listRaw);
					if (!DAOWorker.HisMaterialBeanDAO.UpdateList(listData))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialBean_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisMaterialBean that bai." + LogUtil.TraceData("listData", listData));
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

        internal bool UpdateListNoCheckLock(List<HIS_MATERIAL_BEAN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMaterialBeanDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialBean_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterialBean that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMaterialBeans))
			{
				if (!new HisMaterialBeanUpdate(param).UpdateList(this.beforeUpdateHisMaterialBeans))
				{
					LogSystem.Warn("Rollback du lieu HisMaterialBean that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialBeans", this.beforeUpdateHisMaterialBeans));
				}
			}
		}
	}
}
