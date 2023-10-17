using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Update.Package
{
    partial class HisSereServPackageUtil
    {
        internal static void AutoAssign(HIS_SERE_SERV hisSereServ)
        {
            AutoAssign(new List<HIS_SERE_SERV>() { hisSereServ });
        }
        
        /// <summary>
        /// Tu dong gan package_id neu co cau hinh tu dong gan tuong ung voi dich vu
        /// </summary>
        /// <param name="hisSereServs"></param>
        internal static void AutoAssign(List<HIS_SERE_SERV> hisSereServs)
        {
            try
            {
                if (hisSereServs != null && hisSereServs.Count > 0)
                {
                    foreach (HIS_SERE_SERV s in hisSereServs)
                    {
                        //Neu co thong tin goi, va goi khong duoc check "ko co dinh dich vu trong goi", tuc la dich vu duoc chi dinh su dung tinh nang "goi dich vu"
                        //--> khong tu dong gan thong tin goi
                        if (s.PACKAGE_ID.HasValue && (HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS == null || !HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS.Contains(s.PACKAGE_ID.Value)))
                        {
                            return;
                        }
                        else
                        {
                            //Kiem tra xem dich vu co duoc khai bao goi ko
                            V_HIS_SERVICE service = HisServiceCFG.HAS_PACKAGE_DATA_VIEW.Where(o => o.ID == s.SERVICE_ID).FirstOrDefault();
                            if (service != null && service.PACKAGE_ID.HasValue && HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS != null && HisPackageCFG.NOT_FIXED_SERVICE_PACKAGE_IDS.Contains(service.PACKAGE_ID.Value))
                            {
                                s.PACKAGE_ID = service.PACKAGE_ID;
                                s.PACKAGE_PRICE = service.PACKAGE_PRICE;
                            }
                            else
                            {
                                s.PACKAGE_ID = null;
                                s.PACKAGE_PRICE = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
