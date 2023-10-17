using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialBean
{
    class HisMaterialBeanCreate : BusinessBase
    {
        private List<HIS_MATERIAL_BEAN> recentHisMaterialBeans = new List<HIS_MATERIAL_BEAN>();

        internal HisMaterialBeanCreate()
            : base()
        {

        }

        internal HisMaterialBeanCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MATERIAL_BEAN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMaterialBeanDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialBean_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterialBean that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMaterialBeans.Add(data);
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

        internal bool CreateList(List<HIS_MATERIAL_BEAN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMaterialBeanDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterialBean_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterialBean that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMaterialBeans.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMaterialBeans))
            {
                if (!new HisMaterialBeanTruncate(param).TruncateList(this.recentHisMaterialBeans))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterialBean that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialBeans", this.recentHisMaterialBeans));
                }
            }
        }
    }
}
