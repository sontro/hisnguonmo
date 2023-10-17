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
    partial class HisSereServRehaTruncate : BusinessBase
    {
        internal HisSereServRehaTruncate()
            : base()
        {

        }

        internal HisSereServRehaTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_SERE_SERV_REHA data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_SERE_SERV_REHA> listRaw = new HisSereServRehaGet().GetBySereServIdAndRehaTrainTypeId(data.SERE_SERV_ID, data.REHA_TRAIN_TYPE_ID);
                HisSereServRehaCheck checker = new HisSereServRehaCheck(param);
                valid = valid && IsNotNullOrEmpty(listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    List<long> listId = listRaw.Select(o => o.ID).ToList();
                    if (!new HisRehaTrainTruncate(param).TruncateBySereServRehaIds(listId))
                    {
                        throw new Exception("Xoa du lieu HIS_REHA_TRAIN that bai. Ket thuc nghiep vu");
                    }
                    result = DAOWorker.HisSereServRehaDAO.TruncateList(listRaw);
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

        internal bool TruncateList(List<HIS_SERE_SERV_REHA> listData)
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
                    if (!new HisRehaTrainTruncate(param).TruncateBySereServRehaIds(listId))
                    {
                        throw new Exception("Xoa du lieu HIS_REHA_TRAIN that bai. Ket thuc nghiep vu");
                    }
                    result = DAOWorker.HisSereServRehaDAO.TruncateList(listData);
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

        internal bool TruncateBySereServIds(List<long> sereServIds)
        {
            bool result = false;
            List<HIS_SERE_SERV_REHA> listData = new HisSereServRehaGet().GetBySereServIds(sereServIds);
            if (IsNotNullOrEmpty(listData))
            {
                result = this.TruncateList(listData);
            }
            return result;
        }
    }
}
