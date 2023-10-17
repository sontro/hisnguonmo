using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using His.Bhyt.InsuranceExpertise;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using HIS.Desktop.Plugins.Register.ADO;
using HIS.Desktop.Plugins.Register.ValidationRule;
using HIS.Desktop.Utility;
using Inventec.Common.Logging;
using Inventec.Desktop.Common.Controls.ValidationRule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Register.Run
{
    public partial class UCRegister : UserControlBase
    {
        private async void ProcessGetDataHrm(string valueSearch)
        {
            try
            {
                MOS.SDO.HisPatientSDO _PatientSDOByHrm = new MOS.SDO.HisPatientSDO();
                var _addressConnectHrm = (AddressConnectHrm)Newtonsoft.Json.JsonConvert.DeserializeObject<AddressConnectHrm>(HisConfigCFG.AddressConnectHrm);
                HIS.Desktop.Plugins.Library.RegisterConnectHrm.RegisterConnectHrmProcessor _RegisterConnectHrmProcessor = new Library.RegisterConnectHrm.RegisterConnectHrmProcessor();
                _PatientSDOByHrm = await _RegisterConnectHrmProcessor.GetDataHrm1(_addressConnectHrm.Address, _addressConnectHrm.Loginname, _addressConnectHrm.Password, _addressConnectHrm.GrantType, _addressConnectHrm.ClientId, _addressConnectHrm.ClientSecret, valueSearch);//"000017"
                this.Invoke(new MethodInvoker(delegate()
                                    {
                                        this.ProcessPatientCodeKeydown(_PatientSDOByHrm);
                                    }));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
