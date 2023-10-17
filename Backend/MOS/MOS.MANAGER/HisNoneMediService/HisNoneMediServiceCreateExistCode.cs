using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNoneMediService
{
    partial class HisNoneMediServiceCreate : BusinessBase
    {
        private List<HIS_NONE_MEDI_SERVICE> recentHisNoneMediServices = new List<HIS_NONE_MEDI_SERVICE>();

        internal HisNoneMediServiceCreate()
            : base()
        {

        }

        internal HisNoneMediServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_NONE_MEDI_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNoneMediServiceCheck checker = new HisNoneMediServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.NONE_MEDI_SERVICE_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisNoneMediServiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNoneMediService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisNoneMediService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisNoneMediServices.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisNoneMediServices))
            {
                if (!DAOWorker.HisNoneMediServiceDAO.TruncateList(this.recentHisNoneMediServices))
                {
                    LogSystem.Warn("Rollback du lieu HisNoneMediService that bai, can kiem tra lai." + LogUtil.TraceData("recentHisNoneMediServices", this.recentHisNoneMediServices));
                }
                this.recentHisNoneMediServices = null;
            }
        }
    }
}
