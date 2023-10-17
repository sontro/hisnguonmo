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

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Update
{
    class HisServiceReqProcessor : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        
        internal HisServiceReqProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Run(SubclinicalPresSDO data, List<HIS_PATIENT_TYPE_ALTER> ptas, long? mediStockId, HIS_SERVICE_REQ serviceReq, ref HIS_SERVICE_REQ resultData)
        {
            try
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);

                serviceReq.PARENT_ID = data.ParentServiceReqId;
                serviceReq.INTRUCTION_TIME = data.InstructionTime;
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
                serviceReq.TRACKING_ID = data.TrackingId;
                serviceReq.PRESCRIPTION_TYPE_ID = (short) PrescriptionType.SUBCLINICAL;
                serviceReq.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
                serviceReq.INTERACTION_REASON = data.InteractionReason;

                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == mediStockId.Value).FirstOrDefault();
                serviceReq.EXECUTE_DEPARTMENT_ID = mediStock.DEPARTMENT_ID;
                serviceReq.EXECUTE_ROOM_ID = mediStock.ROOM_ID;

                if (mediStock.IS_CABINET == MOS.UTILITY.Constant.IS_TRUE)
                {
                    serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT;
                }
                else
                {
                    HIS_PATIENT_TYPE_ALTER currentPta = ptas != null ? ptas.OrderByDescending(o => o.LOG_TIME).FirstOrDefault() : null;

                    if (currentPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU ||
                        currentPta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_BenhNhanDaVaoDieuTri);
                        throw new Exception("Benh nhan da va dieu tri khong cho phep tao don phong kham");
                    }
                    serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK;
                }

                //Thoi gian su dung cua don lay theo thoi gian su dung lon nhat cua thuoc co trong don 
                long? maxNumOfDays = IsNotNullOrEmpty(data.Medicines) ?
                    data.Medicines.Where(o => o.NumOfDays.HasValue).Max(o => o.NumOfDays) : null;

                if (!maxNumOfDays.HasValue) maxNumOfDays = data.NumOfDays;
                if (maxNumOfDays.HasValue)
                {
                    long dayCount = maxNumOfDays.Value == 0 ? 1 : maxNumOfDays.Value;

                    DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.UseTime > 0 ? data.UseTime.Value : data.InstructionTime).Value;
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
