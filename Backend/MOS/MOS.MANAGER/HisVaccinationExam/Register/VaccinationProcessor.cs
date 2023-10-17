using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisVaccination;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Register
{
    class VaccinationProcessor : BusinessBase
    {
        private HisVaccinationCreate hisVaccinationCreate;

        internal VaccinationProcessor()
            : base()
        {
            this.hisVaccinationCreate = new HisVaccinationCreate(param);
        }

        internal VaccinationProcessor(CommonParam param)
            : base(param)
        {
            this.hisVaccinationCreate = new HisVaccinationCreate(param);
        }

        public bool Run(HisPatientVaccinationSDO sdo, WorkPlaceSDO workPlace, ref List<HIS_VACCINATION> vaccinations)
        {
            try
            {
                if (sdo != null && IsNotNullOrEmpty(sdo.VaccinationMeties))
                {
                    List<HIS_VACCINATION> toInserts = new List<HIS_VACCINATION>();
                    var groups = sdo.VaccinationMeties.GroupBy(o => new { o.PatientTypeId, o.MediStockId }).ToList();
                    foreach (var g in groups)
                    {
                        HIS_VACCINATION vaccination = this.MakeData(sdo, workPlace, g.Key.PatientTypeId, g.Key.MediStockId);
                        toInserts.Add(vaccination);
                    }
                    if (!this.hisVaccinationCreate.CreateList(toInserts))
                    {
                        LogSystem.Warn("Tao du lieu HIS_VACCINATION that bai");
                        return false;
                    }
                    vaccinations = toInserts;
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

        private HIS_VACCINATION MakeData(HisPatientVaccinationSDO sdo, WorkPlaceSDO workPlace, long patientTypeId, long mediStockId)
        {
            HIS_VACCINATION vaccination = new HIS_VACCINATION();
            V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == mediStockId).FirstOrDefault();

            vaccination.BRANCH_ID = sdo.HisVaccinationExam.BRANCH_ID;
            vaccination.PATIENT_ID = sdo.HisVaccinationExam.PATIENT_ID;
            vaccination.PATIENT_TYPE_ID = patientTypeId;
            vaccination.REQUEST_LOGINNAME = sdo.RequestLoginname;
            vaccination.REQUEST_USERNAME = sdo.RequestUsername;
            vaccination.REQUEST_ROOM_ID = workPlace.RoomId;
            vaccination.EXECUTE_ROOM_ID = mediStock.ROOM_ID;
            vaccination.REQUEST_TIME = sdo.HisVaccinationExam.REQUEST_TIME;
            vaccination.EXECUTE_DEPARTMENT_ID = mediStock.DEPARTMENT_ID;
            vaccination.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
            vaccination.VACCINATION_EXAM_ID = sdo.HisVaccinationExam.ID;
            vaccination.VACCINATION_STT_ID = IMSys.DbConfig.HIS_RS.HIS_VACCINATION_STT.ID__NEW;

            HisVaccinationUtil.SetTdl(vaccination, sdo.HisVaccinationExam);

            return vaccination;
        }

        internal void Rollback()
        {
            this.hisVaccinationCreate.RollbackData();
        }
    }
}
