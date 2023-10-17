using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientType;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatient;
using MOS.LibraryHein.Common;
using MOS.MANAGER.HisTreatment;
using AutoMapper;
using MOS.MANAGER.HisDepartmentTran;

namespace MOS.MANAGER.HisPatientTypeAlter
{
    partial class HisPatientTypeAlterCreate : BusinessBase
    {
        internal bool CreateByChangeTreatmentType(long treatmentId, long treatmentTypeId, long logTime, long requestRoomId, ref HIS_PATIENT_TYPE_ALTER resultData)
        {
            HIS_PATIENT_TYPE_ALTER lastPta = new HisPatientTypeAlterGet().GetLastByTreatmentId(treatmentId);
            if (lastPta == null)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.LoiDuLieu);
                throw new Exception("Khong tim thay du lieu patient_type_alter cuoi cung tuong ung voi treatment_id:" + treatmentId);
            }

            HIS_DEPARTMENT_TRAN departmentTran = new HisDepartmentTranGet().GetLastByTreatmentId(lastPta.TREATMENT_ID);
            //return this.CreateByChangeTreatmentType(lastPta, treatmentTypeId, logTime, requestRoomId, ref resultData);

            return this.CreateByChangeTreatmentType(lastPta, departmentTran, treatmentTypeId, logTime, requestRoomId, ref resultData);
        }

        internal bool CreateByChangeTreatmentType(HIS_PATIENT_TYPE_ALTER lastPta, HIS_DEPARTMENT_TRAN departmentTran, long treatmentTypeId, long logTime, long requestRoomId, ref HIS_PATIENT_TYPE_ALTER resultData)
        {
            bool result = true;
            try
            {
                //Neu co su thay doi dien doi tuong thi moi thuc hien them ban ghi
                if (lastPta.TREATMENT_TYPE_ID != treatmentTypeId)
                {
                    if (departmentTran == null)
                    {
                        string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(logTime);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_ChuaCoThongTinVaoKhoaTruocThoiDiem, time);
                        return false;
                    }

                    Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, HIS_PATIENT_TYPE_ALTER>();
                    HIS_PATIENT_TYPE_ALTER pta = Mapper.Map<HIS_PATIENT_TYPE_ALTER>(lastPta);
                    pta.LOG_TIME = logTime;
                    pta.EXECUTE_ROOM_ID = requestRoomId;
                    pta.TREATMENT_TYPE_ID = treatmentTypeId;
                    pta.DEPARTMENT_TRAN_ID = departmentTran.ID;
                    result = this.Create(pta, ref resultData);
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
