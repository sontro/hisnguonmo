using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServTein
{
    class HisSereServTeinCreate : BusinessBase
    {
        private List<HIS_SERE_SERV_TEIN> recentHisSereServTeins = new List<HIS_SERE_SERV_TEIN>();

        internal HisSereServTeinCreate()
            : base()
        {

        }

        internal HisSereServTeinCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool CreateList(List<HIS_SERE_SERV_TEIN> listData, long dob, long genderId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    foreach (HIS_SERE_SERV_TEIN t in listData)
                    {
                        HisSereServTeinDecorator.Decorator(t, dob, genderId);
                    }
                    if (!DAOWorker.HisSereServTeinDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServTein_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServTein that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServTeins.AddRange(listData);
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

        //Vinh sinh 
        internal bool CreateList(List<HIS_SERE_SERV_TEIN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServTeinCheck checker = new HisSereServTeinCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServTeinDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServTein_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServTein that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServTeins.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSereServTeins))
            {
                if (!new HisSereServTeinTruncate(param).TruncateList(this.recentHisSereServTeins))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServTein that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServTeins", this.recentHisSereServTeins));
                }
            }
        }
    }
}
