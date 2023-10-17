using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMetyMaty
{
    partial class HisMetyMatyCreate : BusinessBase
    {
        private List<HIS_METY_MATY> recentHisMetyMatys = new List<HIS_METY_MATY>();

        internal HisMetyMatyCreate()
            : base()
        {

        }

        internal HisMetyMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_METY_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMetyMatyCheck checker = new HisMetyMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMetyMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMetyMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMetyMatys.Add(data);
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

        internal bool CreateList(List<HIS_METY_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMetyMatyCheck checker = new HisMetyMatyCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMetyMatyDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMetyMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMetyMatys.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMetyMatys))
            {
                if (!DAOWorker.HisMetyMatyDAO.TruncateList(this.recentHisMetyMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMetyMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMetyMatys", this.recentHisMetyMatys));
                }
                this.recentHisMetyMatys = null;
            }
        }
    }
}
