using HIS.Desktop.LocalStorage.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignServiceTestMulti.ADO
{
    class SereServADO : MOS.EFMODEL.DataModels.V_HIS_SERE_SERV
    {
        public long? SERVICE_NUM_ORDER { get; set; }
        public long? MIN_DURATION { get; set; }
        public string SERVICE_CODE_HIDDEN { get; set; }
        public string SERVICE_NAME_HIDDEN { get; set; }
        public SereServADO()
        {

        }
        public SereServADO(MOS.EFMODEL.DataModels.V_HIS_SERE_SERV expMestMedicine)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<SereServADO>(this, expMestMedicine);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public SereServADO(MOS.EFMODEL.DataModels.V_HIS_SERVICE service, MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE patientType, bool isAssignDay)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<SereServADO>(this, service);
                this.AMOUNT = 1;
                this.IsExpend = false;
                this.IsKHBHYT = false;
                this.SERVICE_ID = service.ID;
                this.SERVICE_NUM_ORDER = service.NUM_ORDER;
                if (patientType != null)
                {
                    this.PATIENT_TYPE_ID = patientType.ID;
                    this.PATIENT_TYPE_CODE = patientType.PATIENT_TYPE_CODE;
                    this.PATIENT_TYPE_NAME = patientType.PATIENT_TYPE_NAME;
                }
                this.MIN_DURATION = service.MIN_DURATION;
                this.IsAssignDay = isAssignDay;
                this.TDL_SERVICE_CODE = service.SERVICE_CODE;
                this.TDL_SERVICE_NAME = service.SERVICE_NAME;
                this.TDL_SERVICE_TYPE_ID = service.SERVICE_TYPE_ID;
                this.TDL_SERVICE_UNIT_ID = service.SERVICE_UNIT_ID;
                this.TDL_HEIN_SERVICE_TYPE_ID = service.HEIN_SERVICE_TYPE_ID;
                this.TDL_HEIN_SERVICE_BHYT_CODE = service.HEIN_SERVICE_BHYT_CODE;
                this.TDL_HEIN_SERVICE_BHYT_NAME = service.HEIN_SERVICE_BHYT_NAME;
                this.TDL_HEIN_ORDER = service.HEIN_ORDER;
                if (this.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT)
                {
                    var surgServiceType = BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_SERVICE>().SingleOrDefault(o => o.ID == this.SERVICE_ID);
                    if (surgServiceType != null && (surgServiceType.PTTT_GROUP_ID ?? 0) > 0)
                        this.PTTT_GROUP_NAME = (BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PTTT_GROUP>().FirstOrDefault(o => o.ID == (surgServiceType.PTTT_GROUP_ID ?? 0)) ?? new MOS.EFMODEL.DataModels.HIS_PTTT_GROUP()).PTTT_GROUP_NAME;
                    else
                        this.PTTT_GROUP_NAME = surgServiceType.SERVICE_TYPE_NAME;
                }
                else if (this.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT)
                {
                    var misuServiceType = BackendDataWorker.Get<MOS.EFMODEL.DataModels.V_HIS_SERVICE>().SingleOrDefault(o => o.ID == this.SERVICE_ID);
                    if (misuServiceType != null && (misuServiceType.PTTT_GROUP_ID ?? 0) > 0)
                        this.PTTT_GROUP_NAME = (BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_PTTT_GROUP>().FirstOrDefault(o => o.ID == (misuServiceType.PTTT_GROUP_ID ?? 0)) ?? new MOS.EFMODEL.DataModels.HIS_PTTT_GROUP()).PTTT_GROUP_NAME;
                    else
                        this.PTTT_GROUP_NAME = misuServiceType.SERVICE_TYPE_NAME;
                }

                this.SERVICE_NAME_HIDDEN = convertToUnSign3(service.SERVICE_NAME) + service.SERVICE_NAME;
                this.SERVICE_CODE_HIDDEN = convertToUnSign3(service.SERVICE_CODE) + service.SERVICE_CODE;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public string convertToUnSign3(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return "";

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public List<long> SERVICE_GROUP_ID_SELECTEDs { get; set; }
        public string PTTT_GROUP_NAME { get; set; }
        public short? IS_MULTI_REQUEST { get; set; }
        public bool IsChecked { get; set; }
        public double IdRow { get; set; }
        public bool? IsExpend { get; set; }
        public bool? IsKHBHYT { get; set; }
        public bool IsAssignDay { get; set; }
        public bool IsOutParentFee { get; set; }
        public bool? IsOutKtcFee { get; set; }
        public long? ShareCount { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeAmount { get; set; }
        public string ErrorMessageAmount { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypePatientTypeId { get; set; }
        public string ErrorMessagePatientTypeId { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeIsAssignDay { get; set; }
        public string ErrorMessageIsAssignDay { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeExecuteRoom { get; set; }
        public string ErrorMessageExecuteRoom { get; set; }
    }
}
