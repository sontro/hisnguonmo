using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatient;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccinationExam.Register
{
    class HisVaccinationExamRegister : BusinessBase
    {
        private PatientProcessor patientProcessor;
        private VaccinationExamProcessor vaccinationExamProcessor;
        private ExpMestProcessor expMestProcessor;
        private MedicineProcessor medicineProcessor;
        private VaccinationProcessor vaccinationProcessor;
        private DhstProcessor dhstProcessor;

        internal HisVaccinationExamRegister()
            : base()
        {
            this.Init();
        }

        internal HisVaccinationExamRegister(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.patientProcessor = new PatientProcessor(param);
            this.vaccinationExamProcessor = new VaccinationExamProcessor(param);
            this.expMestProcessor = new ExpMestProcessor(param);
            this.vaccinationProcessor = new VaccinationProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.dhstProcessor = new DhstProcessor(param);
        }

        internal bool Run(HisPatientVaccinationSDO data, ref VaccinationRegisterResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                long requestTime = 0;
                WorkPlaceSDO workPlace = null;
                HIS_PATIENT hisPatient = null;
                HIS_CARD hisCard = null;
                List<HIS_VACCINATION> vaccinations = null;
                List<HIS_EXP_MEST> expMests = null;
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                HisVaccinationExamCheck commonChecker = new HisVaccinationExamCheck(param);
                HisVaccinationExamRegisterCheck checker = new HisVaccinationExamRegisterCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && this.IsValidCardInfo(data, ref hisCard);
                if (valid)
                {
                    requestTime = data.HisVaccinationExam.REQUEST_TIME > 0 ? data.HisVaccinationExam.REQUEST_TIME : Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    data.HisVaccinationExam.REQUEST_TIME = requestTime;
                }
                if (valid && IsNotNullOrEmpty(data.VaccinationMeties))
                {
                    valid = valid && checker.IsValidServicePaty(requestTime, data, workPlace);
                    valid = valid && checker.IsAllowMediStock(data);
                }
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    if (!this.patientProcessor.Run(data, hisCard, ref hisPatient))
                    {
                        throw new Exception("patientProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.vaccinationExamProcessor.Run(data, workPlace, hisPatient))
                    {
                        throw new Exception("vaccinationExamProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.dhstProcessor.Run(data, workPlace))
                    {
                        throw new Exception("dhstProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.vaccinationProcessor.Run(data, workPlace, ref vaccinations))
                    {
                        throw new Exception("vaccinationProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMestProcessor.Run(vaccinations, ref expMests))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.medicineProcessor.Run(data.VaccinationMeties, vaccinations, expMests, ref medicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(ref resultData, hisPatient, data.HisVaccinationExam, vaccinations, expMests, medicines);

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

        private bool IsValidCardInfo(HisPatientVaccinationSDO data, ref HIS_CARD hisCard)
        {
            if (IsNotNullOrEmpty(data.CardCode))
            {
                hisCard = new HisCardGet().GetByCardCode(data.CardCode);
                if (hisCard == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatient_SoTheKhongHopLe);
                    return false;
                }
                if (hisCard.PATIENT_ID.HasValue && hisCard.PATIENT_ID.Value != data.HisPatient.ID)
                {
                    HIS_PATIENT patient = new HisPatientGet().GetById(hisCard.PATIENT_ID.Value);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatient_TheDaDuocSuDungBoiBenhNhanKhac, patient.VIR_PATIENT_NAME, patient.PATIENT_CODE);
                    return false;
                }
            }
            return true;
        }

        private void PassResult(ref VaccinationRegisterResultSDO resultData, HIS_PATIENT hisPatient, HIS_VACCINATION_EXAM vaccExam, List<HIS_VACCINATION> vaccinations, List<HIS_EXP_MEST> expMests, List<HIS_EXP_MEST_MEDICINE> medicines)
        {
            resultData = new VaccinationRegisterResultSDO();
            resultData.ExpMests = expMests;
            resultData.Medicines = medicines;
            resultData.Patient = hisPatient;
            resultData.VaccinationExam = vaccExam;
            resultData.Vaccinations = vaccinations;
        }

        private void Rollback()
        {
            try
            {
                this.medicineProcessor.Rollback();
                this.expMestProcessor.Rollback();
                this.vaccinationProcessor.Rollback();
                this.vaccinationExamProcessor.Rollback();
                this.patientProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
