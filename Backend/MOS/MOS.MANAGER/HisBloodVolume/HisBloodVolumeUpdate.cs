using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodVolume
{
    partial class HisBloodVolumeUpdate : BusinessBase
    {
		private List<HIS_BLOOD_VOLUME> beforeUpdateHisBloodVolumes = new List<HIS_BLOOD_VOLUME>();
		
        internal HisBloodVolumeUpdate()
            : base()
        {

        }

        internal HisBloodVolumeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLOOD_VOLUME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodVolumeCheck checker = new HisBloodVolumeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BLOOD_VOLUME raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckExists(data.VOLUME, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBloodVolumes.Add(raw);
					if (!DAOWorker.HisBloodVolumeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodVolume_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodVolume that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BLOOD_VOLUME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodVolumeCheck checker = new HisBloodVolumeCheck(param);
                List<HIS_BLOOD_VOLUME> listRaw = new List<HIS_BLOOD_VOLUME>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisBloodVolumes.AddRange(listRaw);
					if (!DAOWorker.HisBloodVolumeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodVolume_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodVolume that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBloodVolumes))
            {
                if (!new HisBloodVolumeUpdate(param).UpdateList(this.beforeUpdateHisBloodVolumes))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodVolume that bai, can kiem tra lai." + LogUtil.TraceData("HisBloodVolumes", this.beforeUpdateHisBloodVolumes));
                }
            }
        }
    }
}
