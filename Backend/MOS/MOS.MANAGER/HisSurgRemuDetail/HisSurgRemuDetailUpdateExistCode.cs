using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailUpdate : BusinessBase
    {
		private List<HIS_SURG_REMU_DETAIL> beforeUpdateHisSurgRemuDetails = new List<HIS_SURG_REMU_DETAIL>();
		
        internal HisSurgRemuDetailUpdate()
            : base()
        {

        }

        internal HisSurgRemuDetailUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SURG_REMU_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSurgRemuDetailCheck checker = new HisSurgRemuDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SURG_REMU_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SURG_REMU_DETAIL_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisSurgRemuDetailDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSurgRemuDetail_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSurgRemuDetail that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisSurgRemuDetails.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SURG_REMU_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSurgRemuDetailCheck checker = new HisSurgRemuDetailCheck(param);
                List<HIS_SURG_REMU_DETAIL> listRaw = new List<HIS_SURG_REMU_DETAIL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SURG_REMU_DETAIL_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisSurgRemuDetailDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSurgRemuDetail_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSurgRemuDetail that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisSurgRemuDetails.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSurgRemuDetails))
            {
                if (!DAOWorker.HisSurgRemuDetailDAO.UpdateList(this.beforeUpdateHisSurgRemuDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisSurgRemuDetail that bai, can kiem tra lai." + LogUtil.TraceData("HisSurgRemuDetails", this.beforeUpdateHisSurgRemuDetails));
                }
				this.beforeUpdateHisSurgRemuDetails = null;
            }
        }
    }
}
