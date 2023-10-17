using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestMatyDepa
{
    partial class HisMestMatyDepaCreate : BusinessBase
    {
        private List<HIS_MEST_MATY_DEPA> recentHisMestMatyDepas = new List<HIS_MEST_MATY_DEPA>();

        internal HisMestMatyDepaCreate()
            : base()
        {

        }

        internal HisMestMatyDepaCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_MATY_DEPA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestMatyDepaCheck checker = new HisMestMatyDepaCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsNotExists(data, null);
                if (valid)
                {
                    if (!DAOWorker.HisMestMatyDepaDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMatyDepa_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestMatyDepa that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestMatyDepas.Add(data);
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

        internal bool CreateList(List<HIS_MEST_MATY_DEPA> listData, bool notCheckExists)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestMatyDepaCheck checker = new HisMestMatyDepaCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && (notCheckExists || checker.IsNotExists(data, null));
                }
                if (valid)
                {
                    if (!DAOWorker.HisMestMatyDepaDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestMatyDepa_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestMatyDepa that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMestMatyDepas.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMestMatyDepas))
            {
                if (!DAOWorker.HisMestMatyDepaDAO.TruncateList(this.recentHisMestMatyDepas))
                {
                    LogSystem.Warn("Rollback du lieu HisMestMatyDepa that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestMatyDepas", this.recentHisMestMatyDepas));
                }
                this.recentHisMestMatyDepas = null;
            }
        }
    }
}
