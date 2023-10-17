using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMaty;
using MOS.MANAGER.HisPrepareMety;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPrepare
{
    partial class HisPrepareTruncate : BusinessBase
    {
        internal HisPrepareTruncate()
            : base()
        {

        }

        internal HisPrepareTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPrepareCheck checker = new HisPrepareCheck(param);
                HisPrepareMatyCheck matyChecker = new HisPrepareMatyCheck(param);
                HisPrepareMetyCheck metyChecker = new HisPrepareMetyCheck(param);
                HIS_PREPARE raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotApprove(raw);
                valid = valid && checker.IsCreatorOrAdmin(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HIS_PREPARE_MATY> materials = new HisPrepareMatyGet().GetByPrepareId(raw.ID);
                    List<HIS_PREPARE_METY> medicines = new HisPrepareMetyGet().GetByPrepareId(raw.ID);
                    if (IsNotNullOrEmpty(materials))
                    {
                        valid = valid && matyChecker.IsUnLock(materials);
                        sqls.Add(String.Format("DELETE HIS_PREPARE_MATY WHERE PREPARE_ID = {0}", raw.ID));
                    }
                    if (IsNotNullOrEmpty(medicines))
                    {
                        valid = valid && metyChecker.IsUnLock(medicines);
                        sqls.Add(String.Format("DELETE HIS_PREPARE_METY WHERE PREPARE_ID = {0}", raw.ID));
                    }

                    if (valid)
                    {
                        sqls.Add(String.Format("DELETE HIS_PREPARE WHERE ID = {0}", raw.ID));
                        result = DAOWorker.SqlDAO.Execute(sqls);
                    }
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

        internal bool TruncateList(List<HIS_PREPARE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPrepareCheck checker = new HisPrepareCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisPrepareDAO.TruncateList(listData);
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
