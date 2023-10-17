using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReqMaty
{
    partial class HisServiceReqMatyCreate : BusinessBase
    {
        private List<HIS_SERVICE_REQ_MATY> recentHisServiceReqMatys = new List<HIS_SERVICE_REQ_MATY>();

        internal HisServiceReqMatyCreate()
            : base()
        {

        }

        internal HisServiceReqMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_REQ_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqMatyCheck checker = new HisServiceReqMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExisted(data);
                if (valid)
                {
                    if (!DAOWorker.HisServiceReqMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceReqMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceReqMatys.Add(data);
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

        internal bool CreateList(List<HIS_SERVICE_REQ_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqMatyCheck checker = new HisServiceReqMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceReqMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReqMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceReqMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceReqMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceReqMatys))
            {
                if (!new HisServiceReqMatyTruncate(param).TruncateList(this.recentHisServiceReqMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceReqMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceReqMatys", this.recentHisServiceReqMatys));
                }
                else
                {
                    this.recentHisServiceReqMatys = null;
                }
            }
        }
    }
}
