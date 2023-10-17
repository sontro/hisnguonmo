using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.React
{
    class VaccinationProcessor : BusinessBase
    {

        private HisVaccinationUpdate hisVaccinationUpdate;

        internal VaccinationProcessor()
            : base()
        {
            this.hisVaccinationUpdate = new HisVaccinationUpdate(param);
        }

        internal VaccinationProcessor(CommonParam param)
            : base(param)
        {
            this.hisVaccinationUpdate = new HisVaccinationUpdate(param);
        }

        internal bool Run(HisVaccReactInfoSDO data, HIS_VACCINATION raw)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_VACCINATION, HIS_VACCINATION>();
                HIS_VACCINATION before = Mapper.Map<HIS_VACCINATION>(raw);
                raw.VACCINATION_REACT_ID = data.VaccinationReactId;
                raw.VACCINATION_STT_ID = IMSys.DbConfig.HIS_RS.HIS_VACCINATION_STT.ID__FINISH;
                raw.VACC_HEALTH_STT_ID = data.VaccHealthSttId;
                raw.REACT_TIME = data.ReactTime;
                raw.PATHOLOGICAL_HISTORY = data.PathologicalHistory;
                if (data.IsReactResponse.HasValue && data.IsReactResponse.Value)
                {
                    raw.IS_REACT_RESPONSE = Constant.IS_TRUE;
                    raw.REACT_REPORTER = data.ReactReporter;
                    raw.REACT_RESPONSER = data.ReactResponser;
                }
                else
                {
                    raw.IS_REACT_RESPONSE = null;
                    raw.REACT_REPORTER = null;
                    raw.REACT_RESPONSER = null;
                }
                if (data.VaccHealthSttId == IMSys.DbConfig.HIS_RS.HIS_VACC_HEALTH_STT.ID__TU_VONG)
                {
                    raw.DEATH_TIME = data.DeathTime;
                }
                else
                {
                    raw.DEATH_TIME = null;
                }

                if (String.IsNullOrWhiteSpace(data.FollowLoginname))
                {
                    raw.FOLLOW_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.FOLLOW_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                }
                else
                {
                    raw.FOLLOW_LOGINNAME = data.FollowLoginname;
                    raw.FOLLOW_USERNAME = data.FollowUsername;
                }

                if (!this.hisVaccinationUpdate.Update(raw, before))
                {
                    throw new Exception("hisVaccinationUpdate.");
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisVaccinationUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
