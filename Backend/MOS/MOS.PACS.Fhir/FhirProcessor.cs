using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.PACS.Fhir
{
    public class FhirProcessor
    {
        string Uri;
        string Loginname;
        string Password;
        List<HIS_EMPLOYEE> ListEmployee;
        List<V_HIS_SERVICE> ListService;

        public FhirProcessor(string uri, string loginname, string password, List<HIS_EMPLOYEE> employees, List<V_HIS_SERVICE> services)
        {
            this.Uri = uri;
            this.Loginname = loginname;
            this.Password = password;
            this.ListEmployee = employees;
            this.ListService = services;
        }

        public bool SendNewOrder(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, V_HIS_ROOM room, HIS_SERE_SERV_EXT ext, ref string studyID)
        {
            try
            {
                Hl7.Fhir.Model.Bundle dataSend = FhirUtil.MakeFhirPACS(treatment, serviceReq, room, sereServ, ext, this.ListEmployee, this.ListService);
                if (dataSend != null)
                {
                    return FhirUtil.SendFhir(this.Uri, this.Loginname, this.Password, dataSend, ref studyID);
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Warn(string.Format("Loi tao du lieu FHIR SERE_SERV_ID: {0} ", sereServ.ID));
                    Inventec.Common.Logging.LogAction.Warn(string.Format("Loi tao du lieu FHIR SERE_SERV_ID: {0} ", sereServ.ID));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
                return false;
            }
            return false;
        }

        public bool SendResult(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, V_HIS_ROOM exeRoom, HIS_SERE_SERV_EXT ext, ref string studyID)
        {
            try
            {
                Hl7.Fhir.Model.Bundle dataSend = FhirUtil.MakeFhirPACS(treatment, serviceReq, exeRoom, sereServ, ext, this.ListEmployee, this.ListService);
                if (dataSend != null && dataSend.Entry != null && dataSend.Entry.Count > 5)
                {
                    return FhirUtil.SendFhir(this.Uri, this.Loginname, this.Password, dataSend, ref studyID);
                }
                else if (dataSend == null)
                {
                    Inventec.Common.Logging.LogSystem.Warn(string.Format("Loi tao du lieu FHIR SERE_SERV_ID: {0} ", sereServ.ID));
                    Inventec.Common.Logging.LogAction.Warn(string.Format("Loi tao du lieu FHIR SERE_SERV_ID: {0} ", sereServ.ID));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
                return false;
            }
            return false;
        }

        public bool SendDeleteFhir(string bundleId)
        {
            return FhirUtil.SendDeleteFhir(this.Uri, this.Loginname, this.Password, bundleId);
        }

        public PacsReceivedData GetResult(long accessNumber)
        {
            Hl7.Fhir.Model.Bundle dataResult = FhirUtil.GetResult(this.Uri, this.Loginname, this.Password, accessNumber);
            return FhirUtil.ProcessData(dataResult);
        }
    }
}
