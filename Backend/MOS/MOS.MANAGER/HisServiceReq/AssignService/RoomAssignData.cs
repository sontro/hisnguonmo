using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService
{
    class RoomAssignData
    {
        public List<ServiceReqDetail> ServiceReqDetails { get; set; }
        public long RoomId { get; set; }
        public long DepartmentId { get; set; }
        public long BranchId { get; set; }
        public long ServiceReqTypeId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? ExeServiceModuleId { get; set; }
        public bool IsAntibioticResistance { get; set; }
        public string AssignedExecuteLoginName { get; set; }
        public string AssignedExecuteUserName { get; set; }
        public long? NumOrderBlockId { get; set; }
        public long? NumOrderIssueId { get; set; }
        public long? NumOrder { get; set; }
        public string SampleTypeCode { get; set; }

        public RoomAssignData()
        {
        }

        public RoomAssignData(long serviceReqTypeId, long roomId, long departmentId, List<ServiceReqDetail> serviceReqDetails, long? patientTypeId, long? exeServiceModuleId, bool isAntibioticResistance, string assignedExecuteLoginName, string assignedExecuteUserName, long? numOrderBlockId, long? numOrderIssueId, long? numOrder, string sampleTypeCode)
        {
            this.RoomId = roomId;
            this.DepartmentId = departmentId;
            this.ServiceReqTypeId = serviceReqTypeId;
            this.ServiceReqDetails = serviceReqDetails;
            this.PatientTypeId = patientTypeId;
            this.ExeServiceModuleId = exeServiceModuleId;
            this.IsAntibioticResistance = isAntibioticResistance;
            this.AssignedExecuteLoginName = assignedExecuteLoginName;
            this.AssignedExecuteUserName = assignedExecuteUserName;
            this.NumOrder = numOrder;
            this.NumOrderBlockId = numOrderBlockId;
            this.NumOrderIssueId = numOrderIssueId;
            this.SampleTypeCode = sampleTypeCode;
        }
    }
}
