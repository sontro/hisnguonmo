﻿using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPackingUpdateSDO
    {
        public long Id { get; set; }
        public long RequestRoomId { get; set; }
        public long DispenseTime { get; set; }
        public decimal Amount { get; set; }
        public long? ExpiredDate { get; set; }//han su dung
        public string PackageNumber { get; set; }// so lo
        public string HeinDocumentNumber { get; set; }//So cong van gui BHYT
        public List<HIS_MATERIAL_PATY> MaterialPaties { get; set; }
        public List<HisPackingMatySDO> MaterialTypes { get; set; }
    }
}
