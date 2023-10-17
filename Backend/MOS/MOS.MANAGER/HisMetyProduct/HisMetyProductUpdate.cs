using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMetyProduct
{
    partial class HisMetyProductUpdate : BusinessBase
    {
        private List<HIS_METY_PRODUCT> beforeUpdateHisMetyProducts = new List<HIS_METY_PRODUCT>();

        internal HisMetyProductUpdate()
            : base()
        {

        }

        internal HisMetyProductUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_METY_PRODUCT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMetyProductCheck checker = new HisMetyProductCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_METY_PRODUCT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!DAOWorker.HisMetyProductDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyProduct_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMetyProduct that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisMetyProducts.Add(raw);
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

        internal bool UpdateList(List<HIS_METY_PRODUCT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMetyProductCheck checker = new HisMetyProductCheck(param);
                List<HIS_METY_PRODUCT> listRaw = new List<HIS_METY_PRODUCT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMetyProductDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMetyProduct_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMetyProduct that bai." + LogUtil.TraceData("listData", listData));
                    }

                    this.beforeUpdateHisMetyProducts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMetyProducts))
            {
                if (!DAOWorker.HisMetyProductDAO.UpdateList(this.beforeUpdateHisMetyProducts))
                {
                    LogSystem.Warn("Rollback du lieu HisMetyProduct that bai, can kiem tra lai." + LogUtil.TraceData("HisMetyProducts", this.beforeUpdateHisMetyProducts));
                }
                this.beforeUpdateHisMetyProducts = null;
            }
        }
    }
}
