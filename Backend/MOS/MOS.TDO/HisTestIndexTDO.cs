
using System.Collections.Generic;
namespace MOS.TDO
{
    public class HisTestIndexTDO
    {
        public string TestIndexCode { get; set; }
        public string TestIndexName { get; set; }
        public string TestServiceTypeCode { get; set; }
        public string TestServiceTypeName { get; set; }
        public string TestIndexUnitCode { get; set; }
        public string TestIndexUnitName { get; set; }
        public string TestIndexUnitSymbol { get; set; }
        public List<HisTestIndexRangeTDO> TestIndexRange { get; set; }
    }

    public class HisTestIndexRangeTDO
    {
        public short? IsFemale { get; set; }
        public short? IsMale { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public string NormalValue { get; set; }
        public long? AgeFrom { get; set; }
        public long? AgeTo { get; set; }
        public long? AgeTypeId { get; set; }
    }
}
