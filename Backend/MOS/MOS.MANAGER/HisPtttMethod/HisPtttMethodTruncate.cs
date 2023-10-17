using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttMethod
{
    partial class HisPtttMethodTruncate : BusinessBase
    {
        internal HisPtttMethodTruncate()
            : base()
        {

        }

        internal HisPtttMethodTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_PTTT_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttMethodCheck checker = new HisPtttMethodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisPtttMethodDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_PTTT_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttMethodCheck checker = new HisPtttMethodCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_PTTT_METHOD> listRaw = new List<HIS_PTTT_METHOD>();
                valid = valid && checker.VerifyIds(listData.Select(s => s.ID).ToList(), listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (HIS_PTTT_METHOD data in listData)
                {
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisPtttMethodDAO.TruncateList(listData);
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
