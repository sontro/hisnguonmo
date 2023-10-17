using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateMaty
{
    partial class HisAnticipateMatyCreate : BusinessBase
    {
        private List<HIS_ANTICIPATE_MATY> recentHisAnticipateMatys = new List<HIS_ANTICIPATE_MATY>();

        internal HisAnticipateMatyCreate()
            : base()
        {

        }

        internal HisAnticipateMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTICIPATE_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateMatyCheck checker = new HisAnticipateMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisAnticipateMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAnticipateMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAnticipateMatys.Add(data);
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

        internal bool CreateList(List<HIS_ANTICIPATE_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateMatyCheck checker = new HisAnticipateMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAnticipateMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAnticipateMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAnticipateMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAnticipateMatys))
            {
                if (!new HisAnticipateMatyTruncate(param).TruncateList(this.recentHisAnticipateMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisAnticipateMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisAnticipateMaty", this.recentHisAnticipateMatys));
                }
            }
        }
    }
}
