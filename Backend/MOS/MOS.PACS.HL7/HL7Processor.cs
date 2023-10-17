using MedilinkHL7.SDK;
using MOS.EFMODEL.DataModels;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MOS.PACS.HL7
{
    public class HL7Processor
    {
        /// <summary>
        /// Hàm lấy thông tin kết quả từ hệ thống PACS cloud
        /// </summary>
        /// <param name="wcfAddress">Địa chỉ wcf của PIS </param>
        /// <param name="pacsAddress">Địa chỉ PACS cloud</param>
        /// <param name="pacsPort">Port PACS cloud</param>
        /// <param name="hl7Data">dữ liệu HL7</param>
        /// <param name="accessNumber">ID dịch vụ</param>
        /// <returns>dữ liệu HL7</returns>
        public static string GetLinkView(string wcfAddress, string pacsAddress, int pacsPort, HL7PACS hl7Data, string accessNumber)
        {
            string result = "";
            try
            {
                if (!String.IsNullOrWhiteSpace(wcfAddress))
                {
                    WSHttpBinding binding = new WSHttpBinding();
                    binding.Security.Mode = SecurityMode.None;
                    EndpointAddress epAdd = new EndpointAddress(string.Format("http://{0}/PacsService/GetLink", wcfAddress.Trim()));
                    var client = new WCFPacsInfo.PacsServiceClient(binding, epAdd);

                    ADO.PacsInfoADO data = new ADO.PacsInfoADO();
                    data.AccessNumber = accessNumber;
                    data.Address = pacsAddress;
                    data.Port = pacsPort;
                    data.HL7Message = hl7Data;

                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                    result = client.GetResultInfo(jsonData);
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
