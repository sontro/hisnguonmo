using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttCatastrophe
{
    partial class HisPtttCatastropheUpdate : BusinessBase
    {
		private HIS_PTTT_CATASTROPHE beforeUpdateHisPtttCatastropheDTO;
		private List<HIS_PTTT_CATASTROPHE> beforeUpdateHisPtttCatastropheDTOs;
		
        internal HisPtttCatastropheUpdate()
            : base()
        {

        }

        internal HisPtttCatastropheUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PTTT_CATASTROPHE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttCatastropheCheck checker = new HisPtttCatastropheCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PTTT_CATASTROPHE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PTTT_CATASTROPHE_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisPtttCatastropheDTO = raw;
					if (!DAOWorker.HisPtttCatastropheDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCatastrophe_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttCatastrophe that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PTTT_CATASTROPHE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttCatastropheCheck checker = new HisPtttCatastropheCheck(param);
                List<HIS_PTTT_CATASTROPHE> listRaw = new List<HIS_PTTT_CATASTROPHE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PTTT_CATASTROPHE_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisPtttCatastropheDTOs = listRaw;
					if (!DAOWorker.HisPtttCatastropheDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCatastrophe_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttCatastrophe that bai." + LogUtil.TraceData("listData", listData));
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
            if (this.beforeUpdateHisPtttCatastropheDTO != null)
            {
                if (!new HisPtttCatastropheUpdate(param).Update(this.beforeUpdateHisPtttCatastropheDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttCatastrophe that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttCatastropheDTO", this.beforeUpdateHisPtttCatastropheDTO));
                }
            }
			
			if (this.beforeUpdateHisPtttCatastropheDTOs != null)
            {
                if (!new HisPtttCatastropheUpdate(param).UpdateList(this.beforeUpdateHisPtttCatastropheDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttCatastrophe that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttCatastropheDTOs", this.beforeUpdateHisPtttCatastropheDTOs));
                }
            }
        }
    }
}
