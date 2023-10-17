using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisVaccAppointment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Appointment
{
    class HisVaccinationExamAppointment : BusinessBase
    {
        private HisVaccAppointmentCreate hisVaccAppointmentCreate;
        private HisVaccAppointmentTruncate hisVaccAppointmentTruncate;
        private HisVaccinationExamUpdate hisVaccinationExamUpdate;
        
        internal HisVaccinationExamAppointment()
            : base()
        {
            this.Init();
        }

        internal HisVaccinationExamAppointment(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisVaccAppointmentCreate = new HisVaccAppointmentCreate(param);
            this.hisVaccAppointmentTruncate = new HisVaccAppointmentTruncate(param);
            this.hisVaccinationExamUpdate = new HisVaccinationExamUpdate(param);
        }

        internal bool Run(HisVaccinationAppointmentSDO data, ref List<V_HIS_VACC_APPOINTMENT> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                long requestTime = 0;
                WorkPlaceSDO workPlace = null;
                HIS_VACCINATION_EXAM vaccinationExam = null;
                HisVaccinationExamAppointmentCheck checker = new HisVaccinationExamAppointmentCheck(param);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsValidData(data, workPlace, ref vaccinationExam);

                if (valid)
                {
                    List<HIS_VACC_APPOINTMENT> vaccAppointments = null;
                    this.ProcessVaccinationExam(vaccinationExam, data);
                    this.ProcessVaccAppointment(vaccinationExam, data, ref vaccAppointments);
                    this.PassResult(vaccAppointments, ref resultData);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void PassResult(List<HIS_VACC_APPOINTMENT> vaccAppointments, ref List<V_HIS_VACC_APPOINTMENT> resultData)
        {
            if (IsNotNullOrEmpty(vaccAppointments))
            {
                List<long> ids = vaccAppointments.Select(o => o.ID).ToList();
                resultData = new HisVaccAppointmentGet().GetViewByIds(ids);
            }
        }

        private void ProcessVaccinationExam(HIS_VACCINATION_EXAM vaccinationExam, HisVaccinationAppointmentSDO sdo)
        {
            if (vaccinationExam != null)
            {
                Mapper.CreateMap<HIS_VACCINATION_EXAM, HIS_VACCINATION_EXAM>();
                HIS_VACCINATION_EXAM before = Mapper.Map<HIS_VACCINATION_EXAM>(vaccinationExam);
                vaccinationExam.ADVISE = sdo.Advise;
                vaccinationExam.APPOINTMENT_TIME = sdo.AppointmentTime;
                if (!this.hisVaccinationExamUpdate.Update(vaccinationExam, before))
                {
                    throw new Exception("Cap nhat HIS_VACCINATION_EXAM that bai");
                }
            }
        }

        private void ProcessVaccAppointment(HIS_VACCINATION_EXAM vaccinationExam, HisVaccinationAppointmentSDO sdo, ref List<HIS_VACC_APPOINTMENT> vaccAppointments)
        {
            string sql = "DELETE FROM HIS_VACC_APPOINTMENT WHERE VACCINATION_EXAM_ID = :param1";

            //Xoa du lieu cu
            if (DAOWorker.SqlDAO.Execute(sql, vaccinationExam.ID))
            {
                if (IsNotNullOrEmpty(sdo.Details))
                {
                    vaccAppointments = sdo.Details.Select(o => new HIS_VACC_APPOINTMENT
                    {
                        VACCINATION_EXAM_ID = vaccinationExam.ID,
                        VACCINE_TURN = o.VaccineTurn,
                        VACCINE_TYPE_ID = o.VaccineTypeId
                    }).ToList();

                    if (!this.hisVaccAppointmentCreate.CreateList(vaccAppointments))
                    {
                        throw new Exception("Tao HIS_VACC_APPOINTMENT that bai");
                    }
                }

            }
            else
            {
                throw new Exception("Xoa HIS_VACC_APPOINTMENT cu that bai");
            }
        }

        private void Rollback()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
