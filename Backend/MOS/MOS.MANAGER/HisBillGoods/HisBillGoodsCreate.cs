using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBillGoods
{
    partial class HisBillGoodsCreate : BusinessBase
    {
        private List<HIS_BILL_GOODS> recentHisBillGoodss = new List<HIS_BILL_GOODS>();

        internal HisBillGoodsCreate()
            : base()
        {

        }

        internal HisBillGoodsCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BILL_GOODS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBillGoodsCheck checker = new HisBillGoodsCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisBillGoodsDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBillGoods_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBillGoods that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBillGoodss.Add(data);
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

        internal bool CreateList(List<HIS_BILL_GOODS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBillGoodsCheck checker = new HisBillGoodsCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBillGoodsDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBillGoods_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBillGoods that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBillGoodss.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisBillGoodss))
            {
                if (!new HisBillGoodsTruncate(param).TruncateList(this.recentHisBillGoodss))
                {
                    LogSystem.Warn("Rollback du lieu HisBillGoods that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBillGoodss", this.recentHisBillGoodss));
                }
            }
        }
    }
}
