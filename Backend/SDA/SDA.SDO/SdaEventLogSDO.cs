
namespace SDA.SDO
{
    public class SdaEventLogSDO
    {
        public string LogginName { get; set; }
        public string Ip { get; set; }
        public long? EventLogTypeId { get; set; }
        public bool? IsSuccess { get; set; }
        public string Description { get; set; }
        public string AppCode { get; set; }
        public long? EventTime { get; set; }
    }
}
