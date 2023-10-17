using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update.Status
{
    class HisServiceReqStatusCheck : BusinessBase
    {
        internal HisServiceReqStatusCheck()
            : base()
        {

        }

        internal HisServiceReqStatusCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal static bool IsNeedToVerifyTreatment(HIS_SERVICE_REQ serviceReq)
        {
            //Dich vu XN van cho phep xu ly (nhap ket qua) sau khi ho so da khoa
            if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
            {
                return false;
            }
            //Neu co cau hinh "cho phep xu ly dich vu CLS sau khi khoa ho so" va y lenh thuoc loai la CLS
            //thi cung ko can check ho so
            if (HisServiceReqCFG.IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT
                && HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(serviceReq.SERVICE_REQ_TYPE_ID))
            {
                return false;
            }
            //Neu co cau hinh "cho phep xu ly PTTT sau khi khoa ho so" va y lenh thuoc loai la PTTT
            //thi cung ko can phai check ho so
            if (HisServiceReqCFG.ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT
                && (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PT
                || serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TT))
            {
                return false;
            }

            //if (serviceReq.EXE_SERVICE_MODULE_ID == IMSys.DbConfig.HIS_RS.HIS_EXE_SERVICE_MODULE.ID__DOTHILUC)
            //{
            //    return false;
            //}
            return true;
        }

        internal bool IsValidSrTypeCodeAndExeServiceModuleId(HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                if (serviceReq != null)
                {
                    if (IsNotNullOrEmpty(HisServiceReqCFG.AUTO_ADD_EXCUTE_ROLE__SERVICE_REQ_TYPE_ID)
                        && HisServiceReqCFG.AUTO_ADD_EXCUTE_ROLE__SERVICE_REQ_TYPE_ID.Contains(serviceReq.SERVICE_REQ_TYPE_ID)
                        && serviceReq.EXE_SERVICE_MODULE_ID == IMSys.DbConfig.HIS_RS.HIS_EXE_SERVICE_MODULE.ID__XULYDV)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
