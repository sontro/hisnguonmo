using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestUpdateDetailSDO
    {
        public long ImpMestId { get; set; }
        public long ReqestRoomId { get; set; }

        public decimal? DocumentPrice { get; set; }
        public string DocumentNumber { get; set; }
        public long? DocumentDate { get; set; }
        public decimal? DiscountPrice { get; set; }
        public decimal? DiscountRatio { get; set; }
        public string Deliverer { get; set; }
        public string Description { get; set; }
        public List<HisImpMestMedicineSDO> ImpMestMedicines { get; set; }
        public List<HisImpMestMaterialSDO> ImpMestMaterials { get; set; }
        public List<HisImpMestBloodSDO> ImpMestBloods { get; set; }
    }
}
