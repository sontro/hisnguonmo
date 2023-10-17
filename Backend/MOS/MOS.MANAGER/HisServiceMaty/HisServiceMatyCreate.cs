using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceMaty
{
    partial class HisServiceMatyCreate : BusinessBase
    {
        private List<HIS_SERVICE_MATY> recentHisServiceMatys = new List<HIS_SERVICE_MATY>();

        internal HisServiceMatyCreate()
            : base()
        {

        }

        internal HisServiceMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceMatyCheck checker = new HisServiceMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisServiceMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceMatys.Add(data);
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

        internal bool CreateList(List<HIS_SERVICE_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceMatyCheck checker = new HisServiceMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceMatys))
            {
                if (!new HisServiceMatyTruncate(param).TruncateList(this.recentHisServiceMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceMaty", this.recentHisServiceMatys));
                }
            }
        }
    }
}
