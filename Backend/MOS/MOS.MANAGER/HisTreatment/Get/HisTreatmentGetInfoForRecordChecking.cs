using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Get
{
    class HisTreatmentGetInfoForRecordChecking : GetBase
    {

        internal HisTreatmentGetInfoForRecordChecking()
            : base()
        {

        }

        internal HisTreatmentGetInfoForRecordChecking(CommonParam param)
            : base(param)
        {

        }

        internal HisTreatmentForRecordCheckingSDO Run(HisTreatmentForRecordCheckingFilter filter)
        {
            HisTreatmentForRecordCheckingSDO result = null;
            try
            {
                if (filter.TREATMENT_ID.HasValue || !String.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                {
                    HIS_TREATMENT treatment = null;

                    if (filter.TREATMENT_ID.HasValue)
                    {
                        treatment = DAOWorker.SqlDAO.GetSqlSingle<HIS_TREATMENT>("SELECT * FROM HIS_TREATMENT WHERE ID = :param1", filter.TREATMENT_ID.Value);
                    }
                    else
                    {
                        treatment = DAOWorker.SqlDAO.GetSqlSingle<HIS_TREATMENT>("SELECT * FROM HIS_TREATMENT WHERE TREATMENT_CODE = :param1", filter.TREATMENT_CODE__EXACT);
                    }
                    if (treatment == null)
                    {
                        LogSystem.Warn("TREATMENT_ID OR TREATMENT_CODE Invalid:\n" + LogUtil.TraceData("Filter", filter));
                        return null;
                    }

                    result = new HisTreatmentForRecordCheckingSDO();
                    result.Treatment = treatment;
                    result.Trackings = DAOWorker.SqlDAO.GetSql<HIS_TRACKING>("SELECT * FROM HIS_TRACKING WHERE (IS_DELETE IS NULL OR IS_DELETE <> 1) AND TREATMENT_ID = :param1", treatment.ID);
                    result.Cares = DAOWorker.SqlDAO.GetSql<HIS_CARE>("SELECT * FROM HIS_CARE WHERE (IS_DELETE IS NULL OR IS_DELETE <> 1) AND TREATMENT_ID = :param1", treatment.ID);
                    result.Debates = DAOWorker.SqlDAO.GetSql<HIS_DEBATE>("SELECT * FROM HIS_DEBATE WHERE (IS_DELETE IS NULL OR IS_DELETE <> 1) AND TREATMENT_ID = :param1", treatment.ID);
                    result.Infusions = DAOWorker.SqlDAO.GetSql<HIS_INFUSION>("SELECT * FROM HIS_INFUSION INFU JOIN HIS_INFUSION_SUM INSU ON INFU.INFUSION_SUM_ID = INSU.ID WHERE (INFU.IS_DELETE IS NULL OR INFU.IS_DELETE <> 1) AND INSU.TREATMENT_ID = :param1", treatment.ID);
                    result.MediReacts = DAOWorker.SqlDAO.GetSql<HIS_MEDI_REACT>("SELECT * FROM HIS_MEDI_REACT MERE JOIN HIS_MEDI_REACT_SUM MRSU ON MERE.MEDI_REACT_SUM_ID = MRSU.ID WHERE (MERE.IS_DELETE IS NULL OR MERE.IS_DELETE <> 1) AND MRSU.TREATMENT_ID = :param1", treatment.ID);
                    result.ServiceReqs = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>("SELECT * FROM HIS_SERVICE_REQ WHERE (IS_DELETE IS NULL OR IS_DELETE <> 1) AND (IS_NO_EXECUTE IS NULL OR IS_NO_EXECUTE <> 1) AND TREATMENT_ID = :param1", treatment.ID);
                    result.Transfusions = DAOWorker.SqlDAO.GetSql<HIS_TRANSFUSION>("SELECT * FROM HIS_TRANSFUSION TRAN JOIN HIS_TRANSFUSION_SUM TRSU ON TRAN.TRANSFUSION_SUM_ID = TRSU.ID WHERE (TRAN.IS_DELETE IS NULL OR TRAN.IS_DELETE <> 1) AND TRSU.TREATMENT_ID = :param1", treatment.ID);


                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }
    }
}
