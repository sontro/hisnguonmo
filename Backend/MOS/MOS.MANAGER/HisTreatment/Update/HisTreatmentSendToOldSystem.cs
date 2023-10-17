using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.OldSystemIntegrate;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentSendToOldSystem : BusinessBase
    {
        internal HisTreatmentSendToOldSystem()
            : base()
        {

        }

        internal HisTreatmentSendToOldSystem(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Gui thong tin ho so sang he thong cu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(long id, bool isOldPatient)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(id);
                HIS_PATIENT patient = treatment != null ? new HisPatientGet().GetById(treatment.PATIENT_ID) : null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(id);

                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.TREATMENT_ID = id;
                filter.TDL_SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                List<HIS_SERE_SERV> sereServs = new HisSereServGet().Get(filter);

                if (treatment != null && ptas != null && ptas.Count > 0 && sereServs != null && sereServs.Count > 0)
                {
                    HIS_PATIENT_TYPE_ALTER firstPta = ptas.OrderBy(o => o.LOG_TIME).ThenBy(o => o.ID).FirstOrDefault();
                    HIS_SERE_SERV sereServ = sereServs.OrderBy(o => o.TDL_INTRUCTION_TIME).ThenBy(o => o.ID).FirstOrDefault();

                    result = OldSystemIntegrateProcessor.CreateTreatment(isOldPatient, firstPta, patient, treatment, sereServ);
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
