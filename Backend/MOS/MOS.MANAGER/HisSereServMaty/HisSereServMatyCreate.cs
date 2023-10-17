using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServMaty
{
    partial class HisSereServMatyCreate : BusinessBase
    {
        private List<HIS_SERE_SERV_MATY> recentHisSereServMatys = new List<HIS_SERE_SERV_MATY>();

        internal HisSereServMatyCreate()
            : base()
        {

        }

        internal HisSereServMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServMatyCheck checker = new HisSereServMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisSereServMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServMatys.Add(data);
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

        internal bool CreateList(List<HIS_SERE_SERV_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServMatyCheck checker = new HisSereServMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSereServMatys))
            {
                if (!new HisSereServMatyTruncate(param).TruncateList(this.recentHisSereServMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServMatys", this.recentHisSereServMatys));
                }
            }
        }
    }
}
