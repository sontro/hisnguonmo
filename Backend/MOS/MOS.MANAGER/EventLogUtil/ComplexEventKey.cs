using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ComplexEventKey : Attribute
    {
        public const string SERVICE_REQ_DATA = "SERVICE_REQ_DATA";
        public const string VACCINATION_DATA = "VACCINATION_DATA";
        public const string AGGR_EXP_MEST_DATA = "AGGR_EXP_MEST_DATA";
        public const string AGGR_IMP_MEST_DATA = "AGGR_IMP_MEST_DATA";
        public const string SERVICE_REQ_DATA_1 = "SERVICE_REQ_DATA_1";
        public const string SERVICE_REQ_DATA_2 = "SERVICE_REQ_DATA_2";
        public const string PATIENT_DATA = "PATIENT_DATA";
        public const string SERVICE_REQ_LIST = "SERVICE_REQ_LIST";
        public const string VACCINATION_LIST = "VACCINATION_LIST";
        public const string AGGR_EXP_MEST_LIST = "AGGR_EXP_MEST_LIST";
        public const string AGGR_IMP_MEST_LIST = "AGGR_IMP_MEST_LIST";
        public const string PREPARE_DETAIL_LIST = "PREPARE_DETAIL_LIST";
        public const string ASSIGN_RATION_DATA = "ASSIGN_RATION_DATA";
        public const string UPDATE_RATION_DATA = "UPDATE_RATION_DATA";

        public string Value { get; set; }

        public ComplexEventKey(string value)
        {
            this.Value = value;
        }
    } 
}
