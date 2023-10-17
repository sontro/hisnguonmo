using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPaanPosition
{
    partial class HisPaanPositionUpdate : BusinessBase
    {
		private List<HIS_PAAN_POSITION> beforeUpdateHisPaanPositions = new List<HIS_PAAN_POSITION>();
		
        internal HisPaanPositionUpdate()
            : base()
        {

        }

        internal HisPaanPositionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PAAN_POSITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPaanPositionCheck checker = new HisPaanPositionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PAAN_POSITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PAAN_POSITION_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisPaanPositions.Add(raw);
					if (!DAOWorker.HisPaanPositionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPaanPosition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPaanPosition that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PAAN_POSITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPaanPositionCheck checker = new HisPaanPositionCheck(param);
                List<HIS_PAAN_POSITION> listRaw = new List<HIS_PAAN_POSITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PAAN_POSITION_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisPaanPositions.AddRange(listRaw);
					if (!DAOWorker.HisPaanPositionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPaanPosition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPaanPosition that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPaanPositions))
            {
                if (!new HisPaanPositionUpdate(param).UpdateList(this.beforeUpdateHisPaanPositions))
                {
                    LogSystem.Warn("Rollback du lieu HisPaanPosition that bai, can kiem tra lai." + LogUtil.TraceData("HisPaanPositions", this.beforeUpdateHisPaanPositions));
                }
            }
        }
    }
}
