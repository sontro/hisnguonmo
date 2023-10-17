using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Utility;
using MOS.SDO;
using HIS.UC.AddressCombo.ADO;

namespace HIS.Desktop.Plugins.RegisterV3.Run3
{
    public partial class UCRegister : UserControlBase
    {
        private void FillDataIntoUCAddressInfo(HisPatientSDO data)
        {
            try
            {
                UCAddressADO dataAddress = new UCAddressADO();
                dataAddress.Address = data.VIR_ADDRESS;
                dataAddress.Commune_Name = data.COMMUNE_NAME;
                dataAddress.District_Code = data.DISTRICT_CODE;
                dataAddress.District_Name = data.DISTRICT_NAME;
                dataAddress.Province_Code = data.PROVINCE_CODE;
                dataAddress.Province_Name = data.PROVINCE_NAME;
                dataAddress.Address = data.ADDRESS;
                this.ucAddressCombo1.SetValue(dataAddress);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
