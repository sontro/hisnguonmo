using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.Process
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

        public bool Run(HisVaccinationProcessSDO sdo, HIS_VACCINATION vaccination, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            try
            {
                if (sdo != null && vaccination != null)
                {
                    vaccination.EXECUTE_TIME = sdo.ExecuteTime;
                    vaccination.EXECUTE_LOGINNAME = sdo.ExecuteLoginname;
                    vaccination.EXECUTE_USERNAME = sdo.ExecuteUsername;

                    vaccination.VACCINATION_STT_ID = IMSys.DbConfig.HIS_RS.HIS_VACCINATION_STT.ID__PROCESSING;

                    if (!this.hisVaccinationUpdate.Update(vaccination))
                    {
                        LogSystem.Warn("Cap nhat du lieu HIS_VACCINATION that bai");
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return false;
        }

        internal void Rollback()
        {
            this.hisVaccinationUpdate.RollbackData();
        }
    }
}
