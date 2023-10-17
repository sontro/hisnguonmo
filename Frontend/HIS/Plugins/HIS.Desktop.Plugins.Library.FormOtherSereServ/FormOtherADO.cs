using MOS.EFMODEL.DataModels;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.FormOtherSereServ
{
    class FormOtherADO
    {
        public HIS_SERE_SERV HisSereServ { get; set; }
        public HIS_SERE_SERV_EXT HisSereServExt { get; set; }
        public SAR_FORM_TYPE SarFormType { get; set; }
        public SAR_FORM SarForm { get; set; }
    }
}
