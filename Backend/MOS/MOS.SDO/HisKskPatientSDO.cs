using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisKskPatientSDO
    {
        public HIS_PATIENT Patient { get; set; }
        public long IntructionTime { get; set; }
        public long KskOrder { get; set; }
        public long KskId { get; set; }
        public long? AdditionKskId { get; set; }
        public List<long> AdditionServiceIds { get; set; }
        public bool IsError { get; set; }
        public string Barcode { get; set; }
        public List<string> Descriptions = new List<string>();
        public string HrmKskCode { get; set; }
        public string HrmEmployeeCode { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdSubCode { get; set; }
        public string IcdText { get; set; }
    }
}
