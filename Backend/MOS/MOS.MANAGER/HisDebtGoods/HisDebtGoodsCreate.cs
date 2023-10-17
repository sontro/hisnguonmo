using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebtGoods
{
    partial class HisDebtGoodsCreate : BusinessBase
    {
		private List<HIS_DEBT_GOODS> recentHisDebtGoodss = new List<HIS_DEBT_GOODS>();
		
        internal HisDebtGoodsCreate()
            : base()
        {

        }

        internal HisDebtGoodsCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEBT_GOODS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebtGoodsCheck checker = new HisDebtGoodsCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisDebtGoodsDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebtGoods_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebtGoods that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDebtGoodss.Add(data);
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
		
		internal bool CreateList(List<HIS_DEBT_GOODS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebtGoodsCheck checker = new HisDebtGoodsCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisDebtGoodsDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebtGoods_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebtGoods that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisDebtGoodss.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisDebtGoodss))
            {
                if (!DAOWorker.HisDebtGoodsDAO.TruncateList(this.recentHisDebtGoodss))
                {
                    LogSystem.Warn("Rollback du lieu HisDebtGoods that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDebtGoodss", this.recentHisDebtGoodss));
                }
				this.recentHisDebtGoodss = null;
            }
        }
    }
}
