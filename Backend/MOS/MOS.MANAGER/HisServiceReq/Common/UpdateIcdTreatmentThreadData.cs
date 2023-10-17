using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common
{
    class UpdateIcdTreatmentThreadData
    {
        public string IcdText { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdSubCode { get; set; }
        public long? UpDepartmentId { get; set; }
        public bool UpdateEyeInfo { get; set; }
        public HIS_TREATMENT Treatment { get; set; }
    }
}
