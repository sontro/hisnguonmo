using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyProduct
{
    partial class HisMetyProductCreate : BusinessBase
    {
        private List<HIS_METY_PRODUCT> recentHisMetyProducts = new List<HIS_METY_PRODUCT>();

        internal HisMetyProductCreate()
            : base()
        {

        }

        internal HisMetyProductCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_METY_PRODUCT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMetyProductCheck checker = new HisMetyProductCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisMetyProductDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyProduct_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMetyProduct that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMetyProducts.Add(data);
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

        internal bool CreateList(List<HIS_METY_PRODUCT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMetyProductCheck checker = new HisMetyProductCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMetyProductDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyProduct_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMetyProduct that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMetyProducts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMetyProducts))
            {
                if (!DAOWorker.HisMetyProductDAO.TruncateList(this.recentHisMetyProducts))
                {
                    LogSystem.Warn("Rollback du lieu HisMetyProduct that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMetyProducts", this.recentHisMetyProducts));
                }
                this.recentHisMetyProducts = null;
            }
        }
    }
}
