using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update
{
    class HisTreatmentUpdateEyeInfo : BusinessBase
    {
        internal HisTreatmentUpdateEyeInfo()
            : base()
        {

        }

        internal HisTreatmentUpdateEyeInfo(CommonParam param)
            : base(param)
        {

        }

        internal void Run(long treatmentId)
        {
            try
            {
                this.InitThread(treatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }


        private void InitThread(long treatmentId)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(UpdateEyeInfo));
                thread.Priority = System.Threading.ThreadPriority.Highest;
                thread.Start(treatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void UpdateEyeInfo(object threadData)
        {
            try
            {
                long treatmentId = (long)threadData;
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(treatmentId);
                if (treatment == null || treatment.IS_ACTIVE == Constant.IS_FALSE || treatment.IS_PAUSE == Constant.IS_TRUE)
                {
                    LogSystem.Info("Treatment is null or isactive  or ispause: " + treatmentId);
                    return;
                }

                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(treatment);

                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.TREATMENT_ID = treatment.ID;
                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT;
                filter.IS_HOME_PRES = true;
                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);
                HIS_SERVICE_REQ serviceReq = serviceReqs != null ? serviceReqs.Where(o => HasEyeInfo(o)).OrderByDescending(o => o.INTRUCTION_TIME).FirstOrDefault() : null;
                if (serviceReq != null)
                {
                    treatment.EYE_TENSION_LEFT = serviceReq.TREAT_EYE_TENSION_LEFT;
                    treatment.EYE_TENSION_RIGHT = serviceReq.TREAT_EYE_TENSION_RIGHT;
                    treatment.EYESIGHT_GLASS_LEFT = serviceReq.TREAT_EYESIGHT_GLASS_LEFT;
                    treatment.EYESIGHT_GLASS_RIGHT = serviceReq.TREAT_EYESIGHT_GLASS_RIGHT;
                    treatment.EYESIGHT_LEFT = serviceReq.TREAT_EYESIGHT_LEFT;
                    treatment.EYESIGHT_RIGHT = serviceReq.TREAT_EYESIGHT_RIGHT;
                }
                else
                {
                    treatment.EYE_TENSION_LEFT = null;
                    treatment.EYE_TENSION_RIGHT = null;
                    treatment.EYESIGHT_GLASS_LEFT = null;
                    treatment.EYESIGHT_GLASS_RIGHT = null;
                    treatment.EYESIGHT_LEFT = null;
                    treatment.EYESIGHT_RIGHT = null;
                }

                if (!ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(before, treatment)) return;

                if (!DAOWorker.SqlDAO.Execute("UPDATE HIS_TREATMENT SET EYE_TENSION_LEFT = :param1, EYE_TENSION_RIGHT = :param2, EYESIGHT_GLASS_LEFT = :param3, EYESIGHT_GLASS_RIGHT = :param4, EYESIGHT_LEFT = :param5, EYESIGHT_RIGHT = :param6 WHERE ID = :param7", treatment.EYE_TENSION_LEFT, treatment.EYE_TENSION_RIGHT, treatment.EYESIGHT_GLASS_LEFT, treatment.EYESIGHT_GLASS_RIGHT, treatment.EYESIGHT_LEFT, treatment.EYESIGHT_RIGHT, treatment.ID))
                {
                    LogSystem.Warn("Update EYE_INFO cho HIS_TREATMENT that bai: " + treatment.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static bool HasEyeInfo(HIS_SERVICE_REQ o)
        {
            return (o != null) &&
                (!String.IsNullOrWhiteSpace(o.TREAT_EYE_TENSION_LEFT)
                || !String.IsNullOrWhiteSpace(o.TREAT_EYE_TENSION_RIGHT)
                || !String.IsNullOrWhiteSpace(o.TREAT_EYESIGHT_GLASS_LEFT)
                || !String.IsNullOrWhiteSpace(o.TREAT_EYESIGHT_GLASS_RIGHT)
                || !String.IsNullOrWhiteSpace(o.TREAT_EYESIGHT_LEFT)
                || !String.IsNullOrWhiteSpace(o.TREAT_EYESIGHT_RIGHT)
                );
        }
    }
}
