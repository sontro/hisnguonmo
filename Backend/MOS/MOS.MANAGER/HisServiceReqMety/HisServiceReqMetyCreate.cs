using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMety
{
    partial class HisServiceReqMetyCreate : BusinessBase
    {
        private List<HIS_SERVICE_REQ_METY> recentHisServiceReqMetys = new List<HIS_SERVICE_REQ_METY>();

        internal HisServiceReqMetyCreate()
            : base()
        {

        }

        internal HisServiceReqMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_REQ_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqMetyCheck checker = new HisServiceReqMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisServiceReqMetyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceReqMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceReqMetys.Add(data);
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

        internal bool CreateList(List<HIS_SERVICE_REQ_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqMetyCheck checker = new HisServiceReqMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceReqMetyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceReqMety that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceReqMetys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceReqMetys))
            {
                if (!new HisServiceReqMetyTruncate(param).TruncateList(this.recentHisServiceReqMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceReqMety that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceReqMetys", this.recentHisServiceReqMetys));
                }
                else
                {
                    this.recentHisServiceReqMetys = null;
                }
            }
        }
    }
}
