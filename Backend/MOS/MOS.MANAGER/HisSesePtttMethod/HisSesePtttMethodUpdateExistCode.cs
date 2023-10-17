using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSesePtttMethod
{
    partial class HisSesePtttMethodUpdate : BusinessBase
    {
		private List<HIS_SESE_PTTT_METHOD> beforeUpdateHisSesePtttMethods = new List<HIS_SESE_PTTT_METHOD>();
		
        internal HisSesePtttMethodUpdate()
            : base()
        {

        }

        internal HisSesePtttMethodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SESE_PTTT_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSesePtttMethodCheck checker = new HisSesePtttMethodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SESE_PTTT_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SESE_PTTT_METHOD_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisSesePtttMethodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSesePtttMethod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSesePtttMethod that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisSesePtttMethods.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SESE_PTTT_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSesePtttMethodCheck checker = new HisSesePtttMethodCheck(param);
                List<HIS_SESE_PTTT_METHOD> listRaw = new List<HIS_SESE_PTTT_METHOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SESE_PTTT_METHOD_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisSesePtttMethodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSesePtttMethod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSesePtttMethod that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisSesePtttMethods.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSesePtttMethods))
            {
                if (!DAOWorker.HisSesePtttMethodDAO.UpdateList(this.beforeUpdateHisSesePtttMethods))
                {
                    LogSystem.Warn("Rollback du lieu HisSesePtttMethod that bai, can kiem tra lai." + LogUtil.TraceData("HisSesePtttMethods", this.beforeUpdateHisSesePtttMethods));
                }
				this.beforeUpdateHisSesePtttMethods = null;
            }
        }
    }
}
