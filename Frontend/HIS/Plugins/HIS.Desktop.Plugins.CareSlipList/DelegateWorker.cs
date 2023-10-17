using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CareSlipList
{
    public delegate void SelectRowOnLoadHanler(long treatmentId, long patientId, long intructionTime, long finishTime);
    public delegate void SelectImageHanler(List<byte[]> arrImages);
    public delegate void UpdatePatientInfo(MOS.EFMODEL.DataModels.V_HIS_PATIENT patient);
    public delegate bool UpdatePatientType(MOS.EFMODEL.DataModels.HIS_SERE_SERV hisSereServ);
    public delegate bool UpdateSereServ(MOS.EFMODEL.DataModels.HIS_SERE_SERV hisSereServ);
    public delegate void UpdatePatientStatus(MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ servicereq);
    public delegate void DelegateRefeshData();
    public delegate void DelegateRefeshRowData(int rowHandle);
    public delegate void DelegateFocusMoveout();
    public delegate void DelegateRefeshTreatmentPartialData(long treatmentId);
    public delegate void DelegateRefeshDataIcd(MOS.EFMODEL.DataModels.HIS_ICD icd);
    public delegate void DelegateRefeshDataInfusion(MOS.EFMODEL.DataModels.V_HIS_INFUSION infusion);
    public delegate void DelegateRefeshDataTreatmentBedRoom(MOS.SDO.HisTreatmentCommonInfoSDO treatmentCommon);
    public delegate void DelegateRefeshDataServiceReqView(MOS.SDO.HisServiceReqViewSDO serviceReqView);
    public delegate void DelegateRefeshIcdChandoanphu(string icds);
    public delegate void DelegateRefPatientHouseHold(HIS_PATIENT_HOUSEHOLD patientHouseHold);
    public delegate void PatientInfoResult(object o);
    public delegate void DelegateSereServResult(object sereServs);
    public delegate void DelegateSwapService(object sereServ);

}
