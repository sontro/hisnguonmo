using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMetyDepa
{
    partial class HisMestMetyDepaCreate : BusinessBase
    {
        private List<HIS_MEST_METY_DEPA> recentHisMestMetyDepas = new List<HIS_MEST_METY_DEPA>();

        internal HisMestMetyDepaCreate()
            : base()
        {

        }

        internal HisMestMetyDepaCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_METY_DEPA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMetyDepaCheck checker = new HisMestMetyDepaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExists(data, null);
                if (valid)
                {
                    if (!DAOWorker.HisMestMetyDepaDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMetyDepa_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestMetyDepa that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestMetyDepas.Add(data);
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

        internal bool CreateList(List<HIS_MEST_METY_DEPA> listData, bool notCheckExists)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMetyDepaCheck checker = new HisMestMetyDepaCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && (notCheckExists || checker.IsNotExists(data, null));
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestMetyDepaDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMetyDepa_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestMetyDepa that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestMetyDepas.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMestMetyDepas))
            {
                if (!new HisMestMetyDepaTruncate(param).TruncateList(this.recentHisMestMetyDepas))
                {
                    LogSystem.Warn("Rollback du lieu HisMestMetyDepa that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestMetyDepas", this.recentHisMestMetyDepas));
                }
            }
        }
    }
}
