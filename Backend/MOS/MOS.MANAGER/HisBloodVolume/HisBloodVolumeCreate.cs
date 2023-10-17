using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBloodVolume
{
    partial class HisBloodVolumeCreate : BusinessBase
    {
		private List<HIS_BLOOD_VOLUME> recentHisBloodVolumes = new List<HIS_BLOOD_VOLUME>();
		
        internal HisBloodVolumeCreate()
            : base()
        {

        }

        internal HisBloodVolumeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLOOD_VOLUME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodVolumeCheck checker = new HisBloodVolumeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckExists(data.VOLUME, null);
                if (valid)
                {
					if (!DAOWorker.HisBloodVolumeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodVolume_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBloodVolume that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBloodVolumes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBloodVolumes))
            {
                if (!new HisBloodVolumeTruncate(param).TruncateList(this.recentHisBloodVolumes))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodVolume that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBloodVolumes", this.recentHisBloodVolumes));
                }
            }
        }
    }
}
