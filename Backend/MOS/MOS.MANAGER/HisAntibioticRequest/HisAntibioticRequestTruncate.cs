using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using MOS.MANAGER.HisAntibioticMicrobi;
using MOS.MANAGER.HisAntibioticNewReg;
using MOS.MANAGER.HisAntibioticOldReg;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisExpMest.Common.Get;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAntibioticRequest
{
    partial class HisAntibioticRequestTruncate : BusinessBase
    {
        internal HisAntibioticRequestTruncate()
            : base()
        {

        }

        internal HisAntibioticRequestTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);
                HIS_ANTIBIOTIC_REQUEST raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    this.ProcessUpdateExpMest(raw.ID);
                    this.ProcessDeleteAntibioticMicrobis(raw.ID);
                    this.ProcessDeleteAntibioticOldRegs(raw.ID);
                    this.ProcessDeleteAntibioticNewRegs(raw.ID);
                    if (!DAOWorker.HisAntibioticRequestDAO.Truncate(raw))
                    {
                        throw new Exception("Xoa HIS_ANTIBIOTIC_REQUEST that bai");
                    }
                    this.ProcessDeleteDHST(raw.DHST_ID);
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

        private void ProcessDeleteDHST(long dhstId)
        {
            var dhst = new HisDhstGet().GetById(dhstId);
            if (dhst != null)
            {
                if (!DAOWorker.HisDhstDAO.Truncate(dhst))
                {
                    throw new Exception("Xoa HIS_DHST that bai");
                }
            }
        }

        private void ProcessUpdateExpMest(long antibioticRequestId)
        {
            HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
            filter.ANTIBIOTIC_REQUEST_ID = antibioticRequestId;
            var expMest =  new HisExpMestGet().Get(filter);
            expMest.ForEach(o => o.ANTIBIOTIC_REQUEST_ID = null);
            if (IsNotNullOrEmpty(expMest))
            {
                if (!DAOWorker.HisExpMestDAO.UpdateList(expMest))
                {
                    throw new Exception("Cap nhat HIS_EXP_MEST khi xoa HIS_ANTIBIOTIC_REQUEST that bai");
                }
            }
        }

        private void ProcessDeleteAntibioticOldRegs(long antibioticRequestId)
        {
            HisAntibioticOldRegFilterQuery filter = new HisAntibioticOldRegFilterQuery();
            filter.ANTIBIOTIC_REQUEST_ID = antibioticRequestId;
            var oldAntibioticOldReg = new HisAntibioticOldRegGet().Get(filter);
            if (IsNotNullOrEmpty(oldAntibioticOldReg))
            {
                if (!DAOWorker.HisAntibioticOldRegDAO.TruncateList(oldAntibioticOldReg))
                {
                    throw new Exception("Xoa du lieu cu HIS_ANTIBIOTIC_OLD_REG that bai");
                }
            }
        }

        private void ProcessDeleteAntibioticMicrobis(long antibioticRequestId)
        {
            HisAntibioticMicrobiFilterQuery filter = new HisAntibioticMicrobiFilterQuery();
            filter.ANTIBIOTIC_REQUEST_ID = antibioticRequestId;
            var oldAntibioticMicrobi = new HisAntibioticMicrobiGet().Get(filter);
            if (IsNotNullOrEmpty(oldAntibioticMicrobi))
            {
                if (!DAOWorker.HisAntibioticMicrobiDAO.TruncateList(oldAntibioticMicrobi))
                {
                    throw new Exception("Xoa du lieu cu HIS_ANTIBIOTIC_MICROBI that bai");
                }
            }
        }

        private void ProcessDeleteAntibioticNewRegs(long antibioticRequestId)
        {
            HisAntibioticNewRegFilterQuery filter = new HisAntibioticNewRegFilterQuery();
            filter.ANTIBIOTIC_REQUEST_ID = antibioticRequestId;
            var oldAntibioticNewReg = new HisAntibioticNewRegGet().Get(filter);
            if (IsNotNullOrEmpty(oldAntibioticNewReg))
            {
                if (!DAOWorker.HisAntibioticNewRegDAO.TruncateList(oldAntibioticNewReg))
                {
                    throw new Exception("Xoa du lieu cu HIS_ANTIBIOTIC_NEW_REG that bai");
                }
            }
        }

        internal bool TruncateList(List<HIS_ANTIBIOTIC_REQUEST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAntibioticRequestCheck checker = new HisAntibioticRequestCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAntibioticRequestDAO.TruncateList(listData);
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
