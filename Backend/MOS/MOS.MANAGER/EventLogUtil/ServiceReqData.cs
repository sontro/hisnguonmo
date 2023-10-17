using MOS.LibraryEventLog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class ServiceReqData
    {
        public string ServiceReqCode { get; set; }
        public string ExpMestCode { get; set; }
        public string RequestRoomName { get; set; }
        public string ExecuteRoomName { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdSubCode { get; set; }
        public string IcdText { get; set; }
        public long IntructionTime { get; set; }

        public List<string> ServiceNames { get; set; }

        public ServiceReqData()
        {
        }

        public ServiceReqData(string serviceReqCode, string expMestCode, List<string> serviceNames)
        {
            this.ServiceReqCode = serviceReqCode;
            this.ExpMestCode = expMestCode;
            this.ServiceNames = serviceNames;
        }

        public ServiceReqData(string serviceReqCode, List<string> serviceNames)
        {
            this.ServiceReqCode = serviceReqCode;
            this.ServiceNames = serviceNames;
        }

        public ServiceReqData(string serviceReqCode, List<string> serviceNames, string requestRoomName, string executeRoomName)
        {
            this.ServiceReqCode = serviceReqCode;
            this.ServiceNames = serviceNames;
            this.RequestRoomName = requestRoomName;
            this.ExecuteRoomName = executeRoomName;
        }

        public ServiceReqData(string serviceReqCode, List<string> serviceNames, string IcdCode, string IcdName, string IcdSubCode, string IcdText, long IntructionTime)
        {
            this.ServiceReqCode = serviceReqCode;
            this.ServiceNames = serviceNames;
            this.IcdCode = IcdCode;
            this.IcdName = IcdName;
            this.IcdSubCode = IcdSubCode;
            this.IcdText = IcdText;
            this.IntructionTime = IntructionTime;
        }

        public override string ToString()
        {
            string serviceName = ServiceNames != null ?
                string.Join(",", ServiceNames) : "";
            string srCode = string.IsNullOrWhiteSpace(this.ServiceReqCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.SERVICE_REQ_CODE, this.ServiceReqCode);
            string expCode = string.IsNullOrWhiteSpace(this.ExpMestCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.EXP_MEST_CODE, this.ExpMestCode);

            string icdMain = string.Format("{0} {1}",LogCommonUtil.GetEventLogContent(EventLog.Enum.BenhChinh), this.IcdCode + " - " + this.IcdName);
            string icdSub = string.Format("{0} {1}",LogCommonUtil.GetEventLogContent(EventLog.Enum.BenhPhu), this.IcdSubCode + " - " + this.IcdText);

            string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(this.IntructionTime);
            string intructionTime = string.Format("{0} {1}", LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianYLenh), time);

            return string.Format("{0} {1} ({2}). {3}. {4}, {5}", srCode, expCode, serviceName, intructionTime, icdMain, icdSub);
        }
    }
}
