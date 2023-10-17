using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebtGoods
{
    partial class HisDebtGoodsUpdate : BusinessBase
    {
		private List<HIS_DEBT_GOODS> beforeUpdateHisDebtGoodss = new List<HIS_DEBT_GOODS>();
		
        internal HisDebtGoodsUpdate()
            : base()
        {

        }

        internal HisDebtGoodsUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEBT_GOODS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebtGoodsCheck checker = new HisDebtGoodsCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEBT_GOODS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DEBT_GOODS_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisDebtGoodsDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebtGoods_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebtGoods that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisDebtGoodss.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_DEBT_GOODS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebtGoodsCheck checker = new HisDebtGoodsCheck(param);
                List<HIS_DEBT_GOODS> listRaw = new List<HIS_DEBT_GOODS>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DEBT_GOODS_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisDebtGoodsDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebtGoods_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebtGoods that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisDebtGoodss.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDebtGoodss))
            {
                if (!DAOWorker.HisDebtGoodsDAO.UpdateList(this.beforeUpdateHisDebtGoodss))
                {
                    LogSystem.Warn("Rollback du lieu HisDebtGoods that bai, can kiem tra lai." + LogUtil.TraceData("HisDebtGoodss", this.beforeUpdateHisDebtGoodss));
                }
				this.beforeUpdateHisDebtGoodss = null;
            }
        }
    }
}
