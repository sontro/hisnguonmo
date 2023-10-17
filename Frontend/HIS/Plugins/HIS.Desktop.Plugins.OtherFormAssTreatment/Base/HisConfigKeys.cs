using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherFormAssTreatment.Base
{
    internal class HisConfigKeys
    {
        internal const string HIS_CONFIG_KEY__PATIENT_TYPE_CODE__BHYT = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.BHYT";//Doi tuong BHYT
        internal const string HIS_CONFIG_KEY__PATIENT_TYPE_CODE__VP = "MOS.HIS_PATIENT_TYPE.PATIENT_TYPE_CODE.HOSPITAL_FEE";//Doi tuong VP

        internal const string HIS_CONFIG_KEY__OtherFormAssTreatment__FormType = "HIS.Desktop.Plugins.OtherFormAssTreatment.FormType";//Loại xử lý văn bản(dùng thư viện Inventec.Common.RichEditor, Inventec.Common.WordContent)
    }
}
