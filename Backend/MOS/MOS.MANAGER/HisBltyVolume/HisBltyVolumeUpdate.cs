using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBltyVolume
{
    partial class HisBltyVolumeUpdate : BusinessBase
    {
		private List<HIS_BLTY_VOLUME> beforeUpdateHisBltyVolumes = new List<HIS_BLTY_VOLUME>();
		
        internal HisBltyVolumeUpdate()
            : base()
        {

        }

        internal HisBltyVolumeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLTY_VOLUME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBltyVolumeCheck checker = new HisBltyVolumeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BLTY_VOLUME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VerifyDuplicate(data);
                if (valid)
                {                    
					if (!DAOWorker.HisBltyVolumeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBltyVolume_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBltyVolume that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisBltyVolumes.Add(raw);
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

        internal bool UpdateList(List<HIS_BLTY_VOLUME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBltyVolumeCheck checker = new HisBltyVolumeCheck(param);
                List<HIS_BLTY_VOLUME> listRaw = new List<HIS_BLTY_VOLUME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyDuplicate(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisBltyVolumeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBltyVolume_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBltyVolume that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisBltyVolumes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBltyVolumes))
            {
                if (!DAOWorker.HisBltyVolumeDAO.UpdateList(this.beforeUpdateHisBltyVolumes))
                {
                    LogSystem.Warn("Rollback du lieu HisBltyVolume that bai, can kiem tra lai." + LogUtil.TraceData("HisBltyVolumes", this.beforeUpdateHisBltyVolumes));
                }
				this.beforeUpdateHisBltyVolumes = null;
            }
        }
    }
}
