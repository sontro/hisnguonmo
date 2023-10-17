using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentTranPatiSDO
    {
        public HIS_TREATMENT HisTreatment { get; set; }

        public bool? IsTranIn { get; set; }

        public string IcdText { get; set; }
        public string IcdName { get; set; }
        public string IcdCode { get; set; }
        public string IcdSubCode { get; set; }
        public short? TransferInReviews { get; set; }
    }
}
