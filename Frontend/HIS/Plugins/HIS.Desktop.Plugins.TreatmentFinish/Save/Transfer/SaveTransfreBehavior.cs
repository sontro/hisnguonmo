using DevExpress.XtraEditors;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.Save.Transfer
{
    class SaveTransfreBehavior : SaveAbstract, ISave
    {
        internal SaveTransfreBehavior(long RoomId,
            long? ServiceReqId,
            bool isSave,
            HIS_TREATMENT currentVHisTreatment,
            HisTreatmentFinishSDO hisTreatmentFinishSDO_process,
            FormTreatmentFinish Form)
            : base(RoomId, ServiceReqId, isSave, currentVHisTreatment, hisTreatmentFinishSDO_process, Form)
        { }

        object ISave.Run()
        {
            HisTreatmentFinishSDO result = null;
            try
            {
                if (this.CheckTranPati())
                {
                    result = new HisTreatmentFinishSDO();

                    result.TreatmentFinishTime = this.TreatmentFinishTime;
                    result.TreatmentId = this.TreatmentId;
                    result.EndRoomId = this.RoomId;
                    result.ServiceReqId = this.ServiceReqId;
                    result.TreatmentEndTypeId = this.TreatmentEndTypeId;
                    result.TreatmentResultId = this.TreatmentResultId;
                    result.IcdCode = this.IcdCode;
                    result.IcdName = this.IcdName;
                    result.IcdSubCode = this.IcdSubCode;
                    result.IcdText = this.IcdText;
                    result.IsChronic = this.IsChronic;
                    result.IsTemporary = this.IsTemporary;
                    result.DoctorLoginname = this.DoctorLoginname;
                    result.DoctorUsernname = this.DoctorUsernname;

                    //treatment out
                    result.Advise = this.Advised;
                    result.ClinicalNote = this.ClinicalNote;
                    result.SubclinicalResult = this.Subclinical;

                    //treatmetn transfer
                    result.PatientCondition = hisTreatmentFinishSDO_process.PatientCondition;
                    result.TranPatiFormId = hisTreatmentFinishSDO_process.TranPatiFormId;
                    result.TranPatiReasonId = hisTreatmentFinishSDO_process.TranPatiReasonId;
                    result.TransferOutMediOrgCode = hisTreatmentFinishSDO_process.TransferOutMediOrgCode;
                    result.TransferOutMediOrgName = hisTreatmentFinishSDO_process.TransferOutMediOrgName;
                    result.Transporter = hisTreatmentFinishSDO_process.Transporter;
                    result.TransportVehicle = hisTreatmentFinishSDO_process.TransportVehicle;
                    if(Config.ConfigKey.IsRequiredTreatmentMethodOption)
                        result.TreatmentMethod = hisTreatmentFinishSDO_process.TreatmentMethod;
                    else
                        result.TreatmentMethod = Treatment_Method;
                    result.TreatmentDirection = hisTreatmentFinishSDO_process.TreatmentDirection;
                    result.UsedMedicine = hisTreatmentFinishSDO_process.UsedMedicine;
                    result.TranPatiTechId = hisTreatmentFinishSDO_process.TranPatiTechId;

                    //treatment sick
                    result.SickLeaveDay = hisTreatmentFinishSDO_process.SickLeaveDay;
                    result.SickLeaveFrom = hisTreatmentFinishSDO_process.SickLeaveFrom;
                    result.SickLeaveTo = hisTreatmentFinishSDO_process.SickLeaveTo;
                    result.PatientRelativeName = hisTreatmentFinishSDO_process.PatientRelativeName;
                    result.PatientRelativeType = hisTreatmentFinishSDO_process.PatientRelativeType;
                    result.SickLoginname = hisTreatmentFinishSDO_process.SickLoginname;
                    result.SickUsername = hisTreatmentFinishSDO_process.SickUsername;
                    result.DocumentBookId = hisTreatmentFinishSDO_process.DocumentBookId;

                    result.TreatmentEndTypeExtId = hisTreatmentFinishSDO_process.TreatmentEndTypeExtId;
                    result.Babies = hisTreatmentFinishSDO_process.Babies;
                    result.PatientWorkPlace = hisTreatmentFinishSDO_process.PatientWorkPlace;
                    result.WorkPlaceId = hisTreatmentFinishSDO_process.WorkPlaceId;
                    result.TranPatiHospitalLoginname = hisTreatmentFinishSDO_process.TranPatiHospitalLoginname;               
                    result.TranPatiHospitalUsername = hisTreatmentFinishSDO_process.TranPatiHospitalUsername;
                    result.EndTypeExtNote = hisTreatmentFinishSDO_process.EndTypeExtNote;
                    result.IsPregnancyTermination = hisTreatmentFinishSDO_process.IsPregnancyTermination;
                    result.GestationalAge = hisTreatmentFinishSDO_process.GestationalAge;
                    result.PregnancyTerminationReason = hisTreatmentFinishSDO_process.PregnancyTerminationReason;
                    result.PregnancyTerminationTime = hisTreatmentFinishSDO_process.PregnancyTerminationTime;
                }
                else
                {
                    result = new HisTreatmentFinishSDO();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
