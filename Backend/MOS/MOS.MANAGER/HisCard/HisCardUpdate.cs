using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCard
{
    partial class HisCardUpdate : BusinessBase
    {
        private List<HIS_CARD> beforeUpdateHisCards = new List<HIS_CARD>();

        internal HisCardUpdate()
            : base()
        {

        }

        internal HisCardUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCardCheck checker = new HisCardCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CARD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisCards.Add(raw);
                    if (!DAOWorker.HisCardDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCard_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCard that bai." + LogUtil.TraceData("data", data));
                    }

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

        internal bool UpdateList(List<HIS_CARD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCardCheck checker = new HisCardCheck(param);
                List<HIS_CARD> listRaw = new List<HIS_CARD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisCards.AddRange(listRaw);
                    if (!DAOWorker.HisCardDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCard_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCard that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCards))
            {
                if (!new HisCardUpdate(param).UpdateList(this.beforeUpdateHisCards))
                {
                    LogSystem.Warn("Rollback du lieu HisCard that bai, can kiem tra lai." + LogUtil.TraceData("HisCards", this.beforeUpdateHisCards));
                }
            }
        }
    }
}
