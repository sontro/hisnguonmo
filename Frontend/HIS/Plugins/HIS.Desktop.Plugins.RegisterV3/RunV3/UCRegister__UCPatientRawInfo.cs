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
using HIS.UC.UCPatientRaw.ADO;
using MOS.SDO;

namespace HIS.Desktop.Plugins.RegisterV3.Run3
{
    public partial class UCRegister : UserControlBase
    {
        private void FillDataPatientRawInfo(HisPatientSDO data)
        {
            try
            {
                var oldDataPatient = this.ucPatientRaw1.GetValue();
                UCPatientRawADO dataAddress = new UCPatientRawADO();
                dataAddress.CARRER_ID = data.CAREER_ID;
                dataAddress.DOB = data.DOB;
                dataAddress.GENDER_ID = data.GENDER_ID;
                dataAddress.HEIN_CARD_NUMBER = data.HeinCardNumber;
                dataAddress.IS_HAS_NOT_DAY_DOB = data.IS_HAS_NOT_DAY_DOB;
                dataAddress.PATIENT_CODE = data.PATIENT_CODE;
                dataAddress.PATIENT_NAME = data.VIR_PATIENT_NAME;
                if (oldDataPatient != null)
                    dataAddress.PATIENTTYPE_ID = oldDataPatient.PATIENTTYPE_ID;
                this.ucPatientRaw1.SetValue(dataAddress);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
