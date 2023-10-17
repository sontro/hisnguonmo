using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisService;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisCoTreatment;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisDepartmentTran.Create
{
    class HisDepartmentTranCreateCheck : BusinessBase
    {
        internal HisDepartmentTranCreateCheck()
            : base()
        {

        }

        internal HisDepartmentTranCreateCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidDepartment(HisDepartmentTranSDO data, WorkPlaceSDO workPlace, ref HIS_DEPARTMENT_TRAN lastDt, ref List<HIS_DEPARTMENT_TRAN> allDts)
        {
            List<HIS_DEPARTMENT_TRAN> listDt = new HisDepartmentTranGet().GetByTreatmentId(data.TreatmentId);
            HIS_DEPARTMENT_TRAN dtNotReceive = listDt.FirstOrDefault(o => !o.DEPARTMENT_IN_TIME.HasValue);
            if (dtNotReceive != null)
            {
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == dtNotReceive.DEPARTMENT_ID).FirstOrDefault() : null;
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangChoTiepNhanVaoKhoa, (department != null) ? department.DEPARTMENT_NAME : "");
                return false;
            }

            lastDt = listDt.OrderByDescending(o => o.DEPARTMENT_IN_TIME).ThenByDescending(o => o.ID).FirstOrDefault();
            if (lastDt != null)
            {
                long lastDepartmentId = lastDt.DEPARTMENT_ID;
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == lastDepartmentId).FirstOrDefault() : null;
                string departmentName = department != null ? department.DEPARTMENT_NAME : "";

                if (workPlace == null || workPlace.DepartmentId != lastDt.DEPARTMENT_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_BenhNhanDangThuocKhoa, departmentName);
                    return false;
                }

                if (data.Time < lastDt.DEPARTMENT_IN_TIME.Value)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisDepartmentTran_ThoiGianKhongDuocNhoThoiGianVaoKhoaTruoc, departmentName);
                    return false;
                }
            }
            allDts = listDt;
            return true;
        }
    }
}
