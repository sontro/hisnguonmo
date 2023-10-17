using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttMethod
{
    partial class HisPtttMethodUpdate : BusinessBase
    {
		private HIS_PTTT_METHOD beforeUpdateHisPtttMethodDTO;
		private List<HIS_PTTT_METHOD> beforeUpdateHisPtttMethodDTOs;
		
        internal HisPtttMethodUpdate()
            : base()
        {

        }

        internal HisPtttMethodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PTTT_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttMethodCheck checker = new HisPtttMethodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PTTT_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PTTT_METHOD_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisPtttMethodDTO = raw;
					if (!DAOWorker.HisPtttMethodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttMethod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttMethod that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PTTT_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttMethodCheck checker = new HisPtttMethodCheck(param);
                List<HIS_PTTT_METHOD> listRaw = new List<HIS_PTTT_METHOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PTTT_METHOD_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisPtttMethodDTOs = listRaw;
					if (!DAOWorker.HisPtttMethodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttMethod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttMethod that bai." + LogUtil.TraceData("listData", listData));
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
            if (this.beforeUpdateHisPtttMethodDTO != null)
            {
                if (!new HisPtttMethodUpdate(param).Update(this.beforeUpdateHisPtttMethodDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttMethod that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttMethodDTO", this.beforeUpdateHisPtttMethodDTO));
                }
            }
			
			if (this.beforeUpdateHisPtttMethodDTOs != null)
            {
                if (!new HisPtttMethodUpdate(param).UpdateList(this.beforeUpdateHisPtttMethodDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttMethod that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttMethodDTOs", this.beforeUpdateHisPtttMethodDTOs));
                }
            }
        }
    }
}
