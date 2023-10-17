using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCareDetail
{
	partial class HisCareDetailUpdate : BusinessBase
	{
		private List<HIS_CARE_DETAIL> beforeUpdateHisCareDetails = new List<HIS_CARE_DETAIL>();
		
		internal HisCareDetailUpdate()
			: base()
		{

		}

		internal HisCareDetailUpdate(CommonParam paramUpdate)
			: base(paramUpdate)
		{

		}

		internal bool Update(HIS_CARE_DETAIL data)
		{
			bool result = false;
			try
			{
				bool valid = true;
				HisCareDetailCheck checker = new HisCareDetailCheck(param);
				valid = valid && checker.VerifyRequireField(data);
				HIS_CARE_DETAIL raw = null;
				valid = valid && checker.VerifyId(data.ID, ref raw);
				valid = valid && checker.IsUnLock(raw);
				if (valid)
				{
                    this.beforeUpdateHisCareDetails.Add(raw);
					if (!DAOWorker.HisCareDetailDAO.Update(data))
					{
						MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareDetail_CapNhatThatBai);
						throw new Exception("Cap nhat thong tin HisCareDetail that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_CARE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareDetailCheck checker = new HisCareDetailCheck(param);
                List<HIS_CARE_DETAIL> listRaw = new List<HIS_CARE_DETAIL>();
                this.beforeUpdateHisCareDetails.AddRange(listRaw);
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisCareDetailDAO.UpdateList(listData);
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

        internal bool UpdateList(List<HIS_CARE_DETAIL> listData,List<HIS_CARE_DETAIL> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareDetailCheck checker = new HisCareDetailCheck(param);
                valid = valid && checker.IsUnLock(listBefore);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    result = DAOWorker.HisCareDetailDAO.UpdateList(listData);
                    if (result)
                    {
                        this.beforeUpdateHisCareDetails.AddRange(listBefore);
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

		internal void RollbackData()
		{
            if (IsNotNullOrEmpty(this.beforeUpdateHisCareDetails))
			{
                if (!new HisCareDetailUpdate(param).UpdateList(this.beforeUpdateHisCareDetails))
				{
                    LogSystem.Warn("Rollback du lieu HisCareDetail that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisCareDetails", this.beforeUpdateHisCareDetails));
				}
			}
		}
	}
}
