using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAnticipateMety
{
    partial class HisAnticipateMetyCreate : BusinessBase
    {
        private List<HIS_ANTICIPATE_METY> recentHisAnticipateMetys = new List<HIS_ANTICIPATE_METY>();

        internal HisAnticipateMetyCreate()
            : base()
        {

        }

        internal HisAnticipateMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTICIPATE_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateMetyCheck checker = new HisAnticipateMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisAnticipateMetyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAnticipateMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAnticipateMetys.Add(data);
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

        internal bool CreateList(List<HIS_ANTICIPATE_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateMetyCheck checker = new HisAnticipateMetyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisAnticipateMetyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAnticipateMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAnticipateMety that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisAnticipateMetys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisAnticipateMetys))
            {
                if (!new HisAnticipateMetyTruncate(param).TruncateList(this.recentHisAnticipateMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisAnticipateMety that bai, can kiem tra lai." + LogUtil.TraceData("HisAnticipateMety", this.recentHisAnticipateMetys));
                }
            }
        }
    }
}
