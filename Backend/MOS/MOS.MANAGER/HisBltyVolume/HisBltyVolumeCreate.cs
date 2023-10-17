using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBltyVolume
{
    partial class HisBltyVolumeCreate : BusinessBase
    {
        private List<HIS_BLTY_VOLUME> recentHisBltyVolumes = new List<HIS_BLTY_VOLUME>();

        internal HisBltyVolumeCreate()
            : base()
        {

        }

        internal HisBltyVolumeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLTY_VOLUME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBltyVolumeCheck checker = new HisBltyVolumeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyDuplicate(data);
                if (valid)
                {
                    if (!DAOWorker.HisBltyVolumeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBltyVolume_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBltyVolume that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBltyVolumes.Add(data);
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

        internal bool CreateList(List<HIS_BLTY_VOLUME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBltyVolumeCheck checker = new HisBltyVolumeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyDuplicate(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBltyVolumeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBltyVolume_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBltyVolume that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBltyVolumes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBltyVolumes))
            {
                if (!DAOWorker.HisBltyVolumeDAO.TruncateList(this.recentHisBltyVolumes))
                {
                    LogSystem.Warn("Rollback du lieu HisBltyVolume that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBltyVolumes", this.recentHisBltyVolumes));
                }
                this.recentHisBltyVolumes = null;
            }
        }
    }
}
