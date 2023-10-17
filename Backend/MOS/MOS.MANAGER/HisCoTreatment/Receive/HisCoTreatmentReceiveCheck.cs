using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCoTreatment.Receive
{
    partial class HisCoTreatmentReceiveCheck : BusinessBase
    {
        internal HisCoTreatmentReceiveCheck()
            : base()
        {

        }

        internal HisCoTreatmentReceiveCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsAllow(HIS_CO_TREATMENT coTreat, HisCoTreatmentReceiveSDO data, WorkPlaceSDO workPlace)
        {
            bool valid = true;
            try
            {
                HIS_DEPARTMENT_TRAN dt = new HisDepartmentTranGet().GetById(coTreat.DEPARTMENT_TRAN_ID);

                if (coTreat.START_TIME.HasValue)
                {
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA != null ? HisDepartmentCFG.DATA.Where(o => o.ID == coTreat.DEPARTMENT_ID).FirstOrDefault() : null;
                    string departmentName = department != null ? department.DEPARTMENT_NAME : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCoTreatment_BenhNhanDaDuocTiepNhanVaoKhoa, departmentName);
                    return false;
                }

                if (dt.DEPARTMENT_IN_TIME.HasValue && data.StartTime < dt.DEPARTMENT_IN_TIME.Value)
                {
                    string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(dt.DEPARTMENT_IN_TIME.Value);
                    string startTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.StartTime);
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == dt.DEPARTMENT_ID).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCoTreatment_ThoiGianKhongDuocNhoHonThoiGianVaoKhoaChinh, department.DEPARTMENT_NAME, inTime);
                    return false;
                }

                if (workPlace == null || workPlace.DepartmentId != coTreat.DEPARTMENT_ID)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCoTreatment_PhongLamViecKhongThuocKhoaTiepNhan, workPlace != null ? workPlace.DepartmentName : "");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
