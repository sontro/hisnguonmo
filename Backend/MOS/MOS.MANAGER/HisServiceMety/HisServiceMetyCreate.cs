using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceMety
{
    partial class HisServiceMetyCreate : BusinessBase
    {
        private List<HIS_SERVICE_METY> recentHisServiceMetys = new List<HIS_SERVICE_METY>();

        internal HisServiceMetyCreate()
            : base()
        {

        }

        internal HisServiceMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceMetyCheck checker = new HisServiceMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisServiceMetyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceMetys.Add(data);
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

        internal bool CreateList(List<HIS_SERVICE_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceMetyCheck checker = new HisServiceMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceMetyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceMety that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceMetys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceMetys))
            {
                if (!new HisServiceMetyTruncate(param).TruncateList(this.recentHisServiceMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceMety that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceMety", this.recentHisServiceMetys));
                }
            }
        }
    }
}
