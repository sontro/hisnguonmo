
namespace MOS.SDO
{
    public class WorkPlaceSDO
    {
        public long RoomTypeId { get; set; }
        public string ApplicationCode { get; set; }
        public long? MediStockId { get; set; }
        public long? ExecuteRoomId { get; set; }
        public long? CashierRoomId { get; set; }
        public long? ReceptionRoomId { get; set; }
        public long? SampleRoomId { get; set; }
        public long? BedRoomId { get; set; }
        public long? DataStoreId { get; set; }
        public long? StationId { get; set; }
        public long? DeskId { get; set; }

        public string RoomName { get; set; }
        public string RoomCode { get; set; }
        public long RoomId { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string GCode { get; set; }
        public string GroupCode { get; set; }

        public WorkPlaceSDO()
        {
        }
    }
}
