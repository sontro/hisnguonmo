using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqKskExecuteV2SDO
    {
        public long ServiceReqId { get; set; }
        public long RequestRoomId { get; set; }

        public KskGeneralV2SDO KskGeneral { get; set; }
        public KskOverEighteenV2SDO KskOverEighteen { get; set; }
        public KskUnderEighteenV2SDO KskUnderEighteen { get; set; }
        public KskPeriodDriverV2SDO KskPeriodDriver { get; set; }
        public HIS_KSK_DRIVER_CAR HisKskDriverCar { get; set; }
        public HIS_KSK_OTHER HisKskOther { get; set; }
    }

    public class KskGeneralV2SDO
    {
        public HIS_DHST HisDhst { get; set; }
        public HIS_KSK_GENERAL HisKskGeneral { get; set; }
    }

    public class KskOverEighteenV2SDO
    {
        public HIS_DHST HisDhst { get; set; }
        public HIS_KSK_OVER_EIGHTEEN HisKskOverEighteen { get; set; }
    }

    public class KskUnderEighteenV2SDO
    {
        public HIS_DHST HisDhst { get; set; }
        public HIS_KSK_UNDER_EIGHTEEN HisKskUnderEighteen { get; set; }
        public List<HIS_KSK_UNEI_VATY> HisKskUneiVatys { get; set; }
    }

    public class KskPeriodDriverV2SDO
    {
        public HIS_KSK_PERIOD_DRIVER HisKskPeriodDriver { get; set; }
        public List<HIS_PERIOD_DRIVER_DITY> HisPeriodDriverDitys { get; set; }
    }

    public class KskExecuteResultV2SDO
    {
        public V_HIS_SERVICE_REQ HisServiceReq { get; set; }
        public HIS_KSK_GENERAL HisKskGeneral { get; set; }
        public HIS_KSK_OVER_EIGHTEEN HisKskOverEighteen { get; set; }
        public HIS_KSK_UNDER_EIGHTEEN HisKskUnderEighteen { get; set; }
        public HIS_KSK_PERIOD_DRIVER HisKskPeriodDriver { get; set; }
        public HIS_KSK_DRIVER_CAR HisKskDriverCar { get; set; }
        public HIS_KSK_OTHER HisKskOther { get; set; }
    }
}
