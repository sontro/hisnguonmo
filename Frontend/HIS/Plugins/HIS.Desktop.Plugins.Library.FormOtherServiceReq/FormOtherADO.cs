using MOS.EFMODEL.DataModels;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.FormOtherServiceReq
{
    class FormOtherADO
    {
        public HIS_SERVICE_REQ HisServiceReq { get; set; }
        public SAR_FORM_TYPE SarFormType { get; set; }
        public SAR_FORM SarForm { get; set; }
    }
}
