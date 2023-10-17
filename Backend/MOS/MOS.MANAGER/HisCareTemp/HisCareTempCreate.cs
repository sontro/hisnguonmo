using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareTemp
{
    partial class HisCareTempCreate : BusinessBase
    {
        private List<HIS_CARE_TEMP> recentHisCareTemps = new List<HIS_CARE_TEMP>();

        internal HisCareTempCreate()
            : base()
        {

        }

        internal HisCareTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CARE_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareTempCheck checker = new HisCareTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.CARE_TEMP_CODE, null);
                valid = valid && checker.IsNotDuplicate(data);
                if (valid)
                {
                    if (!DAOWorker.HisCareTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCareTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCareTemps.Add(data);
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

        internal bool CreateList(List<HIS_CARE_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCareTempCheck checker = new HisCareTempCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CARE_TEMP_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCareTempDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCareTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCareTemps.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCareTemps))
            {
                if (!DAOWorker.HisCareTempDAO.TruncateList(this.recentHisCareTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisCareTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCareTemps", this.recentHisCareTemps));
                }
                this.recentHisCareTemps = null;
            }
        }
    }
}
