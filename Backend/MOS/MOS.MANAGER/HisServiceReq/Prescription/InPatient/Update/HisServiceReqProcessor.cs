using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update
{
    class HisServiceReqProcessor : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisServiceReqProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(InPatientPresSDO data, long? mediStockId, HIS_SERVICE_REQ serviceReq, ref HIS_SERVICE_REQ resultData)
        {
            try
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                serviceReq.PARENT_ID = data.ParentServiceReqId;
                serviceReq.INTRUCTION_TIME = data.InstructionTimes[0];
                serviceReq.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
                serviceReq.ICD_NAME = data.IcdName;
                serviceReq.ICD_CAUSE_NAME = data.IcdCauseName;
                serviceReq.ICD_CAUSE_CODE = CommonUtil.ToUpper(data.IcdCauseCode);
                serviceReq.ICD_CODE = CommonUtil.ToUpper(data.IcdCode);
                serviceReq.ICD_SUB_CODE = CommonUtil.ToUpper(HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode));
                //Khong cap nhat thong tin phong chi dinh, de tranh loi BS sang khoa/phong khac sua lai don cua minh
                //dan den don bi cap nhat lai thong tin khoa/phong chi dinh
                //serviceReq.REQUEST_ROOM_ID = data.RequestRoomId;
                serviceReq.REQUEST_LOGINNAME = data.RequestLoginName;
                serviceReq.REQUEST_USERNAME = data.RequestUserName;
                serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
                serviceReq.ADVISE = data.Advise;
                serviceReq.USE_TIME = data.UseTime;
                serviceReq.REMEDY_COUNT = data.RemedyCount;
                serviceReq.TRACKING_ID = data.TrackingId;
                serviceReq.PRESCRIPTION_TYPE_ID = (short)data.PrescriptionTypeId;
                serviceReq.IS_HOME_PRES = data.IsHomePres ? (short?)Constant.IS_TRUE : null;
                serviceReq.IS_KIDNEY = data.IsKidney ? (short?)Constant.IS_TRUE : null;
                serviceReq.IS_EXECUTE_KIDNEY_PRES = data.IsExecuteKidneyPres ? (short?)Constant.IS_TRUE : null;
                serviceReq.KIDNEY_TIMES = data.KidneyTimes;
                serviceReq.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
                serviceReq.INTERACTION_REASON = data.InteractionReason;
                serviceReq.IS_TEMPORARY_PRES = data.IsTemporaryPres;

                if (data.IsHomePres)
                {
                    serviceReq.TREAT_EYE_TENSION_LEFT = data.TreatEyeTensionLeft;
                    serviceReq.TREAT_EYE_TENSION_RIGHT = data.TreatEyeTensionRight;
                    serviceReq.TREAT_EYESIGHT_GLASS_LEFT = data.TreatEyesightGlassLeft;
                    serviceReq.TREAT_EYESIGHT_GLASS_RIGHT = data.TreatEyesightGlassRight;
                    serviceReq.TREAT_EYESIGHT_LEFT = data.TreatEyesightLeft;
                    serviceReq.TREAT_EYESIGHT_RIGHT = data.TreatEyesightRight;
                }
                else
                {
                    serviceReq.TREAT_EYE_TENSION_LEFT = null;
                    serviceReq.TREAT_EYE_TENSION_RIGHT = null;
                    serviceReq.TREAT_EYESIGHT_GLASS_LEFT = null;
                    serviceReq.TREAT_EYESIGHT_GLASS_RIGHT = null;
                    serviceReq.TREAT_EYESIGHT_LEFT = null;
                    serviceReq.TREAT_EYESIGHT_RIGHT = null;
                }
                //Neu ko co thong tin kho --> don chi co thuoc ngoai kho --> thi phong xu ly/khoa xu ly lay theo phong y/c, khoa y/c
                if (!mediStockId.HasValue)
                {
                    //serviceReq.EXECUTE_ROOM_ID = serviceReq.REQUEST_ROOM_ID;
                    //serviceReq.EXECUTE_DEPARTMENT_ID = serviceReq.REQUEST_DEPARTMENT_ID;
                    //Neu co chon nha thuoc thi lay theo nha thuoc duoc chon
                    if (data.DrugStoreId.HasValue)
                    {
                        V_HIS_MEDI_STOCK room = HisMediStockCFG.DATA.Where(o => o.ID == data.DrugStoreId.Value).FirstOrDefault();
                        serviceReq.EXECUTE_DEPARTMENT_ID = room.DEPARTMENT_ID;
                        serviceReq.EXECUTE_ROOM_ID = room.ROOM_ID;
                    }
                    //Neu ko tu dong tao phieu xuat ban (ko xac dinh nha thuoc) thi lay thong tin phong/khoa xu ly theo phong/khoa chi dinh
                    else
                    {
                        V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == data.RequestRoomId).FirstOrDefault();
                        serviceReq.EXECUTE_DEPARTMENT_ID = room.DEPARTMENT_ID;
                        serviceReq.EXECUTE_ROOM_ID = data.RequestRoomId;
                    }
                }
                //Neu co thong tin kho --> phong xu ly/khoa xu ly lay theo thong tin kho
                else
                {
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == mediStockId.Value).FirstOrDefault();
                    serviceReq.EXECUTE_ROOM_ID = mediStock.ROOM_ID;
                    serviceReq.EXECUTE_DEPARTMENT_ID = mediStock.DEPARTMENT_ID;
                }

                //Thoi gian su dung cua don lay theo thoi gian su dung lon nhat cua thuoc co trong don 
                long? maxNumOfDays = IsNotNullOrEmpty(data.Medicines) ?
                    data.Medicines.Where(o => o.NumOfDays.HasValue).Max(o => o.NumOfDays) : null;
                if (maxNumOfDays.HasValue)
                {
                    long dayCount = maxNumOfDays.Value == 0 ? 1 : maxNumOfDays.Value;

                    DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.UseTime > 0 ? data.UseTime.Value : data.InstructionTimes[0]).Value;
                    DateTime useTimeTo = time.AddDays(dayCount - 1);
                    serviceReq.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                }

                //Neu co thay doi thi moi thuc hien update
                if (ValueChecker.IsPrimitiveDiff(before, serviceReq) && !this.hisServiceReqUpdate.Update(serviceReq, before, false))
                {
                    throw new Exception("Rollback du lieu");
                }

                resultData = serviceReq;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal void Rollback()
        {
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
