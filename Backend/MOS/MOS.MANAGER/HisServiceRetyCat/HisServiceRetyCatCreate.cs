using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRetyCat
{
    partial class HisServiceRetyCatCreate : BusinessBase
    {
        private List<HIS_SERVICE_RETY_CAT> recentHisServiceRetyCats = new List<HIS_SERVICE_RETY_CAT>();

        internal HisServiceRetyCatCreate()
            : base()
        {

        }

        internal HisServiceRetyCatCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_RETY_CAT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceRetyCatCheck checker = new HisServiceRetyCatCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyExisted(data);
                if (valid)
                {
                    if (!DAOWorker.HisServiceRetyCatDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRetyCat_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceRetyCat that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceRetyCats.Add(data);
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

        internal bool CreateList(List<HIS_SERVICE_RETY_CAT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceRetyCatCheck checker = new HisServiceRetyCatCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceRetyCatDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceRetyCat_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceRetyCat that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceRetyCats.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceRetyCats))
            {
                if (!new HisServiceRetyCatTruncate(param).TruncateList(this.recentHisServiceRetyCats))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceRetyCat that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceRetyCat", this.recentHisServiceRetyCats));
                }
            }
        }
    }
}
