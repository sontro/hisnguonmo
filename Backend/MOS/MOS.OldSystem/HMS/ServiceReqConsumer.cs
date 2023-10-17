using Inventec.Common.Logging;
using MOS.OldSystem.HmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OldSystem.HMS
{
    public class ServiceReqConsumer
    {
        private const string OK = "OK";

        private IeMRMainServiceClient client;
        
        public ServiceReqConsumer (string baseUri)
        {
            this.client = new IeMRMainServiceClient("WSHttpBinding_IeMRMainService", baseUri);
        }

        public bool Create(ServiceReqData data)
        {
            bool result = false;
            string input = "";
            if (data != null)
            {
                try
                {
                    string subclinicalCodeStr = string.Join(",", data.SubclinicalCodes);
                    string icdName = !string.IsNullOrWhiteSpace(data.IcdName) ? data.IcdName.Replace(';', '/') : data.IcdName;
                    icdName = !string.IsNullOrWhiteSpace(data.Description) ? icdName + " | " + data.Description : icdName;

                    input = LogParamUtil.LogContent("DateMake", data.CreateTime, "PatID", data.PatientCode, "MedID", data.TreatmentCode, "RoomID", data.ReqRoomCode, "GroupID", data.GroupCode, "ParaclinicalID", subclinicalCodeStr, "DoctorID", data.RequestLoginname, "isEmergency", data.IsEmergency, "isPayCard", data.IsUseBhyt, "Description", icdName, "isService", data.IsUseService, "login_name", data.RequestLoginname, "CodeParaclinical_NewHis", data.ServiceReqCode);

                    string rs = this.client.srv_hms_AddNewParaclinical(data.CreateTime, data.PatientCode, data.TreatmentCode, data.ReqRoomCode, data.GroupCode, subclinicalCodeStr, data.RequestLoginname, data.IsEmergency, data.IsUseBhyt, icdName, data.IsUseService, data.RequestLoginname, data.ServiceReqCode);

                    result = rs == OK;

                    if (!result)
                    {
                        LogSystem.Warn("Tao y lenh sang he thong HMS that bai. Input: " + input + " Output: " + rs);
                    }
                    else
                    {
                        LogSystem.Info("Tao y lenh sang he thong HMS thanh cong. Input: " + input + " Output: " + rs);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Error("Tao y lenh sang he thong HMS that bai. Input: " + input);
                    LogSystem.Error(ex);
                    result = false;
                }
            }
            return result;
        }
    }
}
