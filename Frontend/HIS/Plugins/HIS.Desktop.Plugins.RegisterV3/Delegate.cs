using HIS.UC.UCTransPati.ADO;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterV3
{
    public delegate void UpdatePatientInfo(MOS.SDO.HisPatientSDO patient);
    public delegate void FocusNextControl();
    public delegate void SelectPerson(HID.EFMODEL.DataModels.HID_PERSON person);
    public delegate void UpdateSelectedTranPati(UCTransPatiADO transpatiADO);
}
