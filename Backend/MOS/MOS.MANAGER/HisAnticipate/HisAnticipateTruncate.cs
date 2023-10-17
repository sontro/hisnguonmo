using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAnticipateBlty;
using MOS.MANAGER.HisAnticipateMaty;
using MOS.MANAGER.HisAnticipateMety;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipate
{
    partial class HisAnticipateTruncate : BusinessBase
    {
        internal HisAnticipateTruncate()
            : base()
        {

        }

        internal HisAnticipateTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_ANTICIPATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateCheck checker = new HisAnticipateCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!new HisAnticipateMatyTruncate(param).TruncateByAnticipateId(raw.ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    if (!new HisAnticipateMetyTruncate(param).TruncateByAnticipateId(raw.ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    if (!new HisAnticipateBltyTruncate(param).TruncateByAnticipateId(raw.ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    result = DAOWorker.HisAnticipateDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_ANTICIPATE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateCheck checker = new HisAnticipateCheck(param);
                List<HIS_ANTICIPATE> listRaw = new List<HIS_ANTICIPATE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAnticipateDAO.TruncateList(listData);
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
    }
}
