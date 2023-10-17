using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBlood
{
    partial class HisBloodCreate : BusinessBase
    {
        private List<HIS_BLOOD> recentHisBloods = new List<HIS_BLOOD>();

        internal HisBloodCreate()
            : base()
        {

        }

        internal HisBloodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodCheck checker = new HisBloodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BLOOD_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisBloodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBlood_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBlood that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBloods.Add(data);
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

        internal bool CreateList(List<HIS_BLOOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodCheck checker = new HisBloodCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                valid = valid && checker.ExistsCode(listData);

                if (valid)
                {
                    if (!DAOWorker.HisBloodDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBlood_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBlood that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBloods.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBloods))
            {
                if (!new HisBloodTruncate(param).TruncateList(this.recentHisBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisBlood that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBloods", this.recentHisBloods));
                }
            }
        }
    }
}
