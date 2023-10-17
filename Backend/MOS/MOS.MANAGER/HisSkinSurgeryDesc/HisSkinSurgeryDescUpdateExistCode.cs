using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescUpdate : BusinessBase
    {
		private List<HIS_SKIN_SURGERY_DESC> beforeUpdateHisSkinSurgeryDescs = new List<HIS_SKIN_SURGERY_DESC>();
		
        internal HisSkinSurgeryDescUpdate()
            : base()
        {

        }

        internal HisSkinSurgeryDescUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SKIN_SURGERY_DESC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSkinSurgeryDescCheck checker = new HisSkinSurgeryDescCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SKIN_SURGERY_DESC raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SKIN_SURGERY_DESC_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisSkinSurgeryDescDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSkinSurgeryDesc_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSkinSurgeryDesc that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisSkinSurgeryDescs.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SKIN_SURGERY_DESC> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSkinSurgeryDescCheck checker = new HisSkinSurgeryDescCheck(param);
                List<HIS_SKIN_SURGERY_DESC> listRaw = new List<HIS_SKIN_SURGERY_DESC>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SKIN_SURGERY_DESC_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisSkinSurgeryDescDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSkinSurgeryDesc_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSkinSurgeryDesc that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisSkinSurgeryDescs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSkinSurgeryDescs))
            {
                if (!DAOWorker.HisSkinSurgeryDescDAO.UpdateList(this.beforeUpdateHisSkinSurgeryDescs))
                {
                    LogSystem.Warn("Rollback du lieu HisSkinSurgeryDesc that bai, can kiem tra lai." + LogUtil.TraceData("HisSkinSurgeryDescs", this.beforeUpdateHisSkinSurgeryDescs));
                }
				this.beforeUpdateHisSkinSurgeryDescs = null;
            }
        }
    }
}
