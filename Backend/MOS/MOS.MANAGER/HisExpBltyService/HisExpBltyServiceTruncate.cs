using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpBltyService
{
    partial class HisExpBltyServiceTruncate : BusinessBase
    {
        internal HisExpBltyServiceTruncate()
            : base()
        {

        }

        internal HisExpBltyServiceTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                HIS_EXP_BLTY_SERVICE raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisExpBltyServiceDAO.Truncate(raw);
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

        internal bool TruncateList(List<HIS_EXP_BLTY_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisExpBltyServiceDAO.TruncateList(listData);
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

        internal bool TruncateList(List<HIS_EXP_BLTY_SERVICE> listData, List<HIS_EXP_BLTY_SERVICE> listBefore)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpBltyServiceCheck checker = new HisExpBltyServiceCheck(param);
                valid = valid && checker.IsUnLock(listBefore);

                if (valid)
                {
                    result = DAOWorker.HisExpBltyServiceDAO.TruncateList(listData);
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

        internal bool TruncateByExpMestId(long expMestId)
        {
            bool result = false;
            try
            {
                List<HIS_EXP_BLTY_SERVICE> listData = new HisExpBltyServiceGet().GetByExpMestId(expMestId);
                if (IsNotNullOrEmpty(listData))
                {
                    result = DAOWorker.HisExpBltyServiceDAO.TruncateList(listData);
                }
                else
                {
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
    }
}
