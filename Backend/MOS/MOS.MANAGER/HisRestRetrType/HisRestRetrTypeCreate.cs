using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRestRetrType
{
    partial class HisRestRetrTypeCreate : BusinessBase
    {
        private List<HIS_REST_RETR_TYPE> recentHisRestRetrTypes = new List<HIS_REST_RETR_TYPE>();

        internal HisRestRetrTypeCreate()
            : base()
        {

        }

        internal HisRestRetrTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REST_RETR_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRestRetrTypeCheck checker = new HisRestRetrTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsRehaServiceType(data);
                valid = valid && checker.CheckUnique(data);
                if (valid)
                {
                    if (!DAOWorker.HisRestRetrTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRestRetrType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRestRetrType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRestRetrTypes.Add(data);
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

        internal bool CreateList(List<HIS_REST_RETR_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRestRetrTypeCheck checker = new HisRestRetrTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsRehaServiceType(data);
                    valid = valid && checker.CheckUnique(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisRestRetrTypeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRestRetrType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRestRetrType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisRestRetrTypes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisRestRetrTypes))
            {
                if (!new HisRestRetrTypeTruncate(param).TruncateList(this.recentHisRestRetrTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisRestRetrType that bai, can kiem tra lai." + LogUtil.TraceData("HisRestRetrType", this.recentHisRestRetrTypes));
                }
            }
        }
    }
}
