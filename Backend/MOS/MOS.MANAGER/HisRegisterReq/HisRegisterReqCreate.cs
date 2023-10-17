using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegisterReq
{
    partial class HisRegisterReqCreate : BusinessBase
    {
        private List<HIS_REGISTER_REQ> recentHisRegisterReqs = new List<HIS_REGISTER_REQ>();

        internal HisRegisterReqCreate()
            : base()
        {

        }

        internal HisRegisterReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REGISTER_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegisterReqCheck checker = new HisRegisterReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisRegisterReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegisterReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRegisterReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRegisterReqs.Add(data);
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

        internal bool CreateList(List<HIS_REGISTER_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRegisterReqCheck checker = new HisRegisterReqCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRegisterReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRegisterReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRegisterReq that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRegisterReqs.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisRegisterReqs))
            {
                if (!new HisRegisterReqTruncate(param).TruncateList(this.recentHisRegisterReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisRegisterReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRegisterReqs", this.recentHisRegisterReqs));
                }
            }
        }
    }
}
