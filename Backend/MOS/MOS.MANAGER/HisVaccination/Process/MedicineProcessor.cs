using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.Process
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;

        internal MedicineProcessor()
            : base()
        {
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        internal MedicineProcessor(CommonParam param)
            : base(param)
        {
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        public bool Run(List<HisVaccinationInjectionSDO> vaccinationInjections, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            try
            {
                if (IsNotNullOrEmpty(vaccinationInjections) && IsNotNullOrEmpty(expMestMedicines))
                {
                    List<HIS_EXP_MEST_MEDICINE> toUpdates = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> befores = new List<HIS_EXP_MEST_MEDICINE>();
                    Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                    foreach (HisVaccinationInjectionSDO sdo in vaccinationInjections)
                    {
                        HIS_EXP_MEST_MEDICINE medicine = expMestMedicines
                            .Where(o => o.ID == sdo.ExpMestMedicineId)
                            .FirstOrDefault();
                        
                        if (medicine != null)
                        {
                            HIS_EXP_MEST_MEDICINE before = Mapper.Map<HIS_EXP_MEST_MEDICINE>(medicine);
                            medicine.VACCINATION_RESULT_ID = sdo.VaccinationResultId;
                            toUpdates.Add(medicine);
                            befores.Add(before);
                        }
                    }

                    if (!this.hisExpMestMedicineUpdate.UpdateList(toUpdates, befores))
                    {
                        LogSystem.Warn("Cap nhat du lieu HIS_EXP_MEST_MEDICINE that bai");
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
            this.hisExpMestMedicineUpdate.RollbackData();
        }
    }
}
