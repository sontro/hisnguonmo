using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    public class InputCheckDataADO
    {
        /// <summary>
        /// Cần các thông tin Icd, thời gian chỉ định, khoa phòng chỉ định
        /// </summary>
        public HIS_SERVICE_REQ HisServiceReq { get; set; }

        public List<MediMateCheckADO> ListMediMateCheck { get; set; }
    }
}
