
namespace MOS.TDO
{
    public class HisTestIndexResultTDO
    {
        public string ServiceCode { get; set; }
        public string Value { get; set; }
        public string TestIndexCode { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string ResultCode { get; set; }
        public string MayXetNghiemID { get; set; }
        public long? MachineId { get; set; }
        public string OldValue { get; set; }

        public string Mic { get; set; }
        public string SriCode { get; set; }
        public string AntibioticCode { get; set; }
        public string AntibioticName { get; set; }
        public string BacteriumCode { get; set; }
        public string BacteriumName { get; set; }
        public string BacteriumNote { get; set; }
        public string BacteriumAmount { get; set; }
        public string BacteriumDensity { get; set; }
        public string BacteriumFamilyCode { get; set; }
        public string BacteriumFamilyName { get; set; }
        public string Leaven { get; set; }

        public string MicrobiologicalResult { get; set; }

        public string ExecuteLoginname { get; set; }
        public string ExecuteUsername { get; set; }

        public string ResultDescription { get; set; }
    }
}
