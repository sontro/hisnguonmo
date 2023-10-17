using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisMediStock;

namespace MOS.MANAGER.HisExpMest.Common.Create
{
    class HisExpMestCreate : BusinessBase
    {
        private List<HIS_EXP_MEST> recentHisExpMests = new List<HIS_EXP_MEST>();

        internal HisExpMestCreate()
            : base()
        {
        }

        internal HisExpMestCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Create(HIS_EXP_MEST data)
        {
            return this.Create(data, null);
        }

        internal bool Create(HIS_EXP_MEST data, HIS_SERVICE_REQ serviceReq)
        {
            List<HIS_SERVICE_REQ> serviceReqs = serviceReq != null ? new List<HIS_SERVICE_REQ>() { serviceReq } : null;
            List<HIS_EXP_MEST> listData = data != null ? new List<HIS_EXP_MEST>() { data } : null;
            return this.CreateList(listData, serviceReqs);
        }

        internal bool CreateList(List<HIS_EXP_MEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestCheck checker = new HisExpMestCheck(param);
                HisMediStockCheck mediStockChecker = new HisMediStockCheck(param);
                foreach (var data in listData)
                {
                    HisExpMestUtil.SetTdl(data);
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && mediStockChecker.IsUnLockCache(data.MEDI_STOCK_ID);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMest_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMest that bai." + LogUtil.TraceData("HisExpMest", listData));
                    }

                    this.recentHisExpMests.AddRange(listData);
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

        internal bool CreateList(List<HIS_EXP_MEST> listData, List<HIS_SERVICE_REQ> serviceReqs)
        {
            if (IsNotNullOrEmpty(listData))
            {
                foreach (var data in listData)
                {
                    HIS_SERVICE_REQ sr = serviceReqs != null ? serviceReqs.Where(o => o.ID == data.SERVICE_REQ_ID).FirstOrDefault() : null;
                    HisExpMestUtil.SetTdl(data, sr);
                }
                return this.CreateList(listData);
            }
            return false;
        }

        internal bool CreateList(List<HIS_EXP_MEST> listData, List<HIS_VACCINATION> vaccinations)
        {
            if (IsNotNullOrEmpty(listData))
            {
                foreach (var data in listData)
                {
                    HIS_VACCINATION sr = vaccinations != null ? vaccinations.Where(o => o.ID == data.VACCINATION_ID).FirstOrDefault() : null;
                    HisExpMestUtil.SetTdl(data, sr);
                }
                return this.CreateList(listData);
            }
            return false;
        }

        internal void RollbackData()
        {
            //Rollback du lieu HisServiceReq
            if (IsNotNullOrEmpty(this.recentHisExpMests))
            {
                if (!DAOWorker.HisExpMestDAO.TruncateList(this.recentHisExpMests))
                {
                    LogSystem.Warn("Rollback thong tin HisExpMest that bai. Can kiem tra lai log.");
                }
                this.recentHisExpMests = null;
            }
        }
    }
}
