using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common
{
    class HisServiceReqNumOrderBase
    {
        public static void SetNumOrderBase(List<HIS_SERVICE_REQ> serviceReqs)
        {
            if (serviceReqs != null && serviceReqs.Count > 0)
            {
                //Neu co cau hinh sinh STT rieng cho cac chi dinh "uu tien" thi set tham so "base" rieng cho y lenh
                if (HisServiceReqCFG.IS_USING_OTHER_NUM_ORDER_FOR_PRIORITIZED)
                {
                    foreach (HIS_SERVICE_REQ sr in serviceReqs)
                    {
                        if (sr.PRIORITY != null && sr.PRIORITY != 0)
                        {
                            sr.NUM_ORDER = null;//neu xu ly theo luong uu tien thi ko dap ung nghiep vu "STT de danh" (dang ky qua tong dai)
                            long intructionDate = sr.INTRUCTION_TIME - sr.INTRUCTION_TIME % 1000000;
                            sr.NUM_ORDER_BASE = string.Format("{0}_{1}_{2}", sr.PRIORITY, sr.EXECUTE_ROOM_ID, intructionDate);
                        }
                    }
                }
            }
        }

        public static void SetNumOrderBase(HIS_SERVICE_REQ serviceReq)
        {
            SetNumOrderBase(new List<HIS_SERVICE_REQ>() { serviceReq });
        }
    }
}
