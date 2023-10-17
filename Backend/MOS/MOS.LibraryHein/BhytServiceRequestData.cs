
using Inventec.Common.Logging;
using Newtonsoft.Json;
using System;
using MOS.LibraryHein.Common;

namespace MOS.LibraryHein.Bhyt
{
    public enum ServiceTypeEnum
    {
        MATERIAL,
        MEDICINE,
        OTHER
    }

    public class BhytServiceRequestData : RequestServiceData
    {
        public BhytPatientTypeData PatientTypeData { get; set; }
        public string HeinRatioTypeCode { get; set; }
        public bool IsHighService { get; set; }
        public ServiceTypeEnum ServiceType { get; set; }
        public bool IsStent { get; set; } //co phai stent hay khong
        public long? StentOrder { get; set; }
        public long InstructionTime { get; set; }
        public long RequestRoomId { get; set; }
        public long ExecuteRoomId { get; set; }

        public BhytServiceRequestData()
        {
        }

        public BhytServiceRequestData(long id, long? parentId, decimal price, decimal? limitPrice, decimal amount, string jsonPatientTypeData, string heinRatioTypeCode, bool isStent, long? stentOrder, decimal originalPrice, long instructionTime, bool isHighService, ServiceTypeEnum serviceType, long requestRoomId, long executeRoomId)
            : base(id, parentId, price, limitPrice, amount, jsonPatientTypeData, originalPrice)
        {
            try
            {
                this.IsHighService = isHighService;
                this.ServiceType = serviceType;
                this.RequestRoomId = requestRoomId;
                this.ExecuteRoomId = executeRoomId;
                this.PatientTypeData = JsonConvert.DeserializeObject<BhytPatientTypeData>(jsonPatientTypeData);
                this.HeinRatioTypeCode = heinRatioTypeCode;
                this.IsStent = isStent;
                this.StentOrder = stentOrder;
                this.InstructionTime = instructionTime;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                throw ex;
            }
        }

        public BhytServiceRequestData(string jsonPatientTypeData, string heinRatioTypeCode)
            : base(jsonPatientTypeData)
        {
            try
            {
                this.PatientTypeData = JsonConvert.DeserializeObject<BhytPatientTypeData>(jsonPatientTypeData);
                this.HeinRatioTypeCode = heinRatioTypeCode;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                throw ex;
            }
        }

        public BhytServiceRequestData(BhytPatientTypeData patientTypeData, string heinRatioTypeCode)
        {
            try
            {
                this.PatientTypeData = patientTypeData;
                this.HeinRatioTypeCode = heinRatioTypeCode;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                throw ex;
            }
        }
    }
}
