using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.Assign.Update
{
    class VaccinationProcessor : BusinessBase
    {
        private HisVaccinationUpdate hisVaccinationUpdate;

        internal VaccinationProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisVaccinationUpdate = new HisVaccinationUpdate(param);
        }

        internal bool Run(HisVaccinationAssignSDO sdo, HIS_VACCINATION vaccination, ref HIS_VACCINATION resultData)
        {
            try
            {
                Mapper.CreateMap<HIS_VACCINATION, HIS_VACCINATION>();
                HIS_VACCINATION before = Mapper.Map<HIS_VACCINATION>(vaccination);
                vaccination.REQUEST_LOGINNAME = sdo.RequestLoginname;
                vaccination.REQUEST_USERNAME = sdo.RequestUsername;
                vaccination.REQUEST_TIME = sdo.RequestTime;

                //Neu co thay doi thi moi thuc hien update
                if (ValueChecker.IsPrimitiveDiff(before, vaccination) && !this.hisVaccinationUpdate.Update(vaccination, before))
                {
                    throw new Exception("Rollback du lieu");
                }

                resultData = vaccination;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal void Rollback()
        {
            this.hisVaccinationUpdate.RollbackData();
        }
    }
}
