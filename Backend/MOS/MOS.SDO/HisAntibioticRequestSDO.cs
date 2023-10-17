using MOS.EFMODEL.DataModels; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAntibioticRequestSDO
    {
        public long ExpMestId { get; set;}
        public long WorkingRoomId { get; set;}
        public HIS_ANTIBIOTIC_REQUEST AntibioticRequest {get; set;} // Id cho phep = 0. Neu = 0 la tao moi, > 0 thi đuoc hieu la update)
        public decimal? Temperature { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public List<HIS_ANTIBIOTIC_MICROBI> AntibioticMicrobis {get; set;}  //  Danh sach cac xet nghiem vi sinh
        public List<HIS_ANTIBIOTIC_OLD_REG> AntibioticOldRegs  {get; set;}  //  Phac do dang dieu tri (danh sach cac thuoc khang sinh dang su dung)
        public List<HIS_ANTIBIOTIC_NEW_REG> AntibioticNewRegs {get; set;}  //  Phac do khang sinh yeu cau (danh sach cac hoat chat dang yeu cau su dung)
    }
}
