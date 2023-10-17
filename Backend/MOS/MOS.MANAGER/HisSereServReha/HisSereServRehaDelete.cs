using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRehaTrain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServReha
{
    partial class HisSereServRehaDelete : BusinessBase
    {
        private List<HIS_SERE_SERV_REHA> recentDeleteds = new List<HIS_SERE_SERV_REHA>();

        internal HisSereServRehaDelete()
            : base()
        {

        }

        internal HisSereServRehaDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERE_SERV_REHA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServRehaCheck checker = new HisSereServRehaCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_REHA raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!new HisRehaTrainDelete(param).DeleteBySereServRehaId(raw.ID))
                    {
                        throw new Exception("Xoa du lieu HIS_REHA_TRAIN that bai. Ket thuc nghiep vu");
                    }
                    this.recentDeleteds.Add(raw);
                    result = DAOWorker.HisSereServRehaDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERE_SERV_REHA> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServRehaCheck checker = new HisSereServRehaCheck(param);
                List<HIS_SERE_SERV_REHA> listRaw = new List<HIS_SERE_SERV_REHA>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    if (!new HisRehaTrainDelete(param).DeleteBySereServRehaIds(listId))
                    {
                        throw new Exception("Xoa du lieu HIS_SERE_SERV_REHA that bai. Ket thuc nghiep vu");
                    }
                    this.recentDeleteds.AddRange(listRaw);
                    result = DAOWorker.HisSereServRehaDAO.DeleteList(listData);
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

        internal bool DeleteBySereServIds(List<long> sereServIds)
        {
            bool result = true;
            List<HIS_SERE_SERV_REHA> listData = new HisSereServRehaGet().GetBySereServIds(sereServIds);
            if (IsNotNullOrEmpty(listData))
            {
                result = this.DeleteList(listData);
            }
            return result;
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.recentDeleteds))
            {
                this.recentDeleteds.ForEach(o => o.IS_DELETE = 0);
                if (!DAOWorker.HisSereServRehaDAO.UpdateList(this.recentDeleteds))
                {
                    LogSystem.Error("Rollback sau khi delete HIS_SERE_SERV_REHA that bai");
                }
            }
        }
    }
}
