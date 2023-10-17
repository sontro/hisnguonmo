using AutoMapper;
using Inventec.Common.Logging;
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

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    class HisServiceReqProcessor : BusinessBase
    {
        private HisServiceReqCreate hisServiceReqCreate;

        internal HisServiceReqProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqCreate = new HisServiceReqCreate(param);
        }

        internal bool Run(HIS_TREATMENT treatment, InPatientPresSDO data, List<HIS_PATIENT_TYPE_ALTER> ptas, List<long> instructionTimes, List<long> mediStockIds, string sessionCode, ref List<HIS_SERVICE_REQ> resultData, ref List<HIS_SERVICE_REQ> inStockServiceReqs, ref List<HIS_SERVICE_REQ> outStockServiceReqs)
        {
            try
            {
                if (IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.Medicines) || IsNotNullOrEmpty(data.SerialNumbers) || IsNotNullOrEmpty(data.ServiceReqMaties) || IsNotNullOrEmpty(data.ServiceReqMeties))
                {
                    List<HIS_SERVICE_REQ> osServiceReqs = null;
                    List<HIS_SERVICE_REQ> inServiceReqs = null;
                    List<HIS_SERVICE_REQ> serviceReqs = null;
                    this.MakeData(data, ptas, instructionTimes, mediStockIds, sessionCode, ref serviceReqs, ref osServiceReqs);
                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        inServiceReqs = serviceReqs.Where(o => osServiceReqs == null || !osServiceReqs.Contains(o)).ToList();

                        if (!this.hisServiceReqCreate.CreateList(serviceReqs, treatment))
                        {
                            return false;
                        }
                        resultData = serviceReqs;
                        outStockServiceReqs = osServiceReqs;
                        inStockServiceReqs = inServiceReqs;
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return true;
        }

        //Tao service_req dua vao thong tin chi dinh thuoc trong kho
        private void MakeData(InPatientPresSDO data, List<HIS_PATIENT_TYPE_ALTER> ptas, List<long> instructionTimes, List<long> mediStockIds, string sessionCode, ref List<HIS_SERVICE_REQ> resultData, ref List<HIS_SERVICE_REQ> outStockServiceReqs)
        {
            //Neu co ke don thuoc ngoai kho va co cau hinh tach don thuoc ngoai kho
            //Hoac don chi co thuoc ngoai kho chu ko co thuoc trong kho
            //==> phai tao service_req rieng cho thuoc ngoai kho
            if (IsNotNullOrEmpty(data.ServiceReqMaties) || IsNotNullOrEmpty(data.ServiceReqMeties))
            {
                if (HisServiceReqCFG.PRESCRIPTION_SPLIT_OUT_MEDISTOCK ||
                    (!IsNotNullOrEmpty(data.Medicines) && !IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.SerialNumbers)))
                {
                    if (mediStockIds == null)
                    {
                        mediStockIds = new List<long>();
                    }
                    mediStockIds.Add(0);//0 de danh dau "medi_stock_id" cua don ngoai kho
                }

            }

            if (IsNotNullOrEmpty(mediStockIds))
            {
                if (resultData == null)
                {
                    resultData = new List<HIS_SERVICE_REQ>();
                }
                if (outStockServiceReqs == null)
                {
                    outStockServiceReqs = new List<HIS_SERVICE_REQ>();
                }

                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();

                foreach (long mediStockId in mediStockIds)
                {
                    foreach (long instructionTime in instructionTimes)
                    {
                        List<PresMedicineSDO> medicines = null;
                        List<PresMaterialSDO> materials = null;
                        List<PresMaterialBySerialNumberSDO> serialNumbers = null;
                        List<PresOutStockMatySDO> serviceReqMaties = null;
                        List<PresOutStockMetySDO> serviceReqMeties = null;

                        //Neu cau hinh ke don nhieu ngay theo ca don, thi voi moi intruction_time luon co 1 don 
                        if (HisServiceReqCFG.MANY_DAYS_PRESCRIPTION_OPTION == HisServiceReqCFG.ManyDaysPrescriptionOption.BY_PRES)
                        {
                            medicines = data.Medicines != null ? data.Medicines.Where(t => t.MediStockId == mediStockId).ToList() : null;
                            materials = data.Materials != null ? data.Materials.Where(t => t.MediStockId == mediStockId).ToList() : null;
                            serialNumbers = data.SerialNumbers != null ? data.SerialNumbers.Where(t => t.MediStockId == mediStockId).ToList() : null;
                            serviceReqMaties = data.ServiceReqMaties;
                            serviceReqMeties = data.ServiceReqMeties;
                        }
                        //Neu cau hinh ke nhieu ngay theo tung thuoc/vat tu thi kiem tra xem tuong ung voi ngay do, kho do, co thuoc/vat tu nao ko --> co don nao ko
                        else
                        {
                            medicines = IsNotNullOrEmpty(data.Medicines) ? data.Medicines.Where(t => t.MediStockId == mediStockId && t.InstructionTimes != null && t.InstructionTimes.Contains(instructionTime)).ToList() : null;
                            materials = IsNotNullOrEmpty(data.Materials) ? data.Materials.Where(t => t.MediStockId == mediStockId && t.InstructionTimes != null && t.InstructionTimes.Contains(instructionTime)).ToList() : null;
                            serialNumbers = IsNotNullOrEmpty(data.SerialNumbers) ? data.SerialNumbers.Where(t => t.MediStockId == mediStockId && t.InstructionTimes != null && t.InstructionTimes.Contains(instructionTime)).ToList() : null;
                            serviceReqMaties = IsNotNullOrEmpty(data.ServiceReqMaties) ? data.ServiceReqMaties.Where(t => (mediStockId == 0 || mediStockId == mediStockIds[0]) && t.InstructionTimes != null && t.InstructionTimes.Contains(instructionTime)).ToList() : null;//Neu thuoc ngoai kho thi co medi_stock_id = 0 (neu tach don) hoac gan vao kho dau tien
                            serviceReqMeties = IsNotNullOrEmpty(data.ServiceReqMeties) ? data.ServiceReqMeties.Where(t => (mediStockId == 0 || mediStockId == mediStockIds[0]) && t.InstructionTimes != null && t.InstructionTimes.Contains(instructionTime)).ToList() : null;//Neu thuoc ngoai kho thi co medi_stock_id = 0 (neu tach don) hoac gan vao kho dau tien;
                        }

                        bool existPres = IsNotNullOrEmpty(medicines)
                            || IsNotNullOrEmpty(materials)
                            || IsNotNullOrEmpty(serialNumbers)
                            || IsNotNullOrEmpty(serviceReqMaties)
                            || IsNotNullOrEmpty(serviceReqMeties);

                        if (existPres)
                        {
                            HIS_PATIENT_TYPE_ALTER usingPta = ptas
                            .Where(o => o.LOG_TIME <= instructionTime)
                            .OrderByDescending(o => o.LOG_TIME).FirstOrDefault();

                            HIS_SERVICE_REQ serviceReq = new HIS_SERVICE_REQ();

                            if (mediStockId == 0) //neu la don ngoai kho thi phong xu ly lay theo phong y/c
                            {
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
                                outStockServiceReqs.Add(serviceReq);
                            }
                            else //neu la don ke trong kho thi phong xu ly lay theo kho
                            {
                                V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA
                                    .Where(o => o.ID == mediStockId).FirstOrDefault();
                                serviceReq.EXECUTE_DEPARTMENT_ID = mediStock.DEPARTMENT_ID;
                                serviceReq.EXECUTE_ROOM_ID = mediStock.ROOM_ID;
                            }
                            serviceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                            serviceReq.PARENT_ID = data.ParentServiceReqId;
                            serviceReq.INTRUCTION_TIME = instructionTime;
                            serviceReq.TREATMENT_ID = data.TreatmentId;
                            serviceReq.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
                            serviceReq.ICD_NAME = data.IcdName;
                            serviceReq.ICD_CODE = CommonUtil.ToUpper(data.IcdCode);
                            serviceReq.ICD_CAUSE_NAME = data.IcdCauseName;
                            serviceReq.ICD_CAUSE_CODE = CommonUtil.ToUpper(data.IcdCauseCode);
                            serviceReq.ICD_SUB_CODE = CommonUtil.ToUpper(HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode));
                            serviceReq.REQUEST_ROOM_ID = data.RequestRoomId;
                            serviceReq.REQUEST_LOGINNAME = data.RequestLoginName;
                            serviceReq.REQUEST_USERNAME = data.RequestUserName;
                            serviceReq.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.RequestLoginName);
                            serviceReq.ADVISE = data.Advise;
                            serviceReq.USE_TIME = data.UseTime;
                            serviceReq.REMEDY_COUNT = data.RemedyCount;
                            serviceReq.IS_TEMPORARY_PRES = data.IsTemporaryPres;

                            serviceReq.TRACKING_ID = data.TrackingId;
                            if (IsNotNullOrEmpty(data.TrackingInfos))
                            {
                                var trackingInfo = data.TrackingInfos.FirstOrDefault(o => o.IntructionTime == serviceReq.INTRUCTION_TIME);
                                if (IsNotNull(trackingInfo))
                                {
                                    serviceReq.TRACKING_ID = trackingInfo.TrackingId;
                                }
                            }

                            serviceReq.TREATMENT_TYPE_ID = usingPta.TREATMENT_TYPE_ID;
                            serviceReq.PRESCRIPTION_TYPE_ID = (short)data.PrescriptionTypeId;
                            serviceReq.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT;
                            serviceReq.SESSION_CODE = sessionCode;
                            serviceReq.IS_HOME_PRES = data.IsHomePres ? (short?)Constant.IS_TRUE : null;
                            serviceReq.IS_KIDNEY = data.IsKidney ? (short?)Constant.IS_TRUE : null;
                            serviceReq.IS_EXECUTE_KIDNEY_PRES = data.IsExecuteKidneyPres ? (short?)Constant.IS_TRUE : null;
                            serviceReq.KIDNEY_TIMES = data.KidneyTimes;
                            serviceReq.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
                            serviceReq.SPECIAL_MEDICINE_TYPE = data.SpecialMedicineType;
                            serviceReq.INTERACTION_REASON = data.InteractionReason;
                            resultData.Add(serviceReq);

                            if (data.IsHomePres)
                            {
                                serviceReq.TREAT_EYE_TENSION_LEFT = data.TreatEyeTensionLeft;
                                serviceReq.TREAT_EYE_TENSION_RIGHT = data.TreatEyeTensionRight;
                                serviceReq.TREAT_EYESIGHT_GLASS_LEFT = data.TreatEyesightGlassLeft;
                                serviceReq.TREAT_EYESIGHT_GLASS_RIGHT = data.TreatEyesightGlassRight;
                                serviceReq.TREAT_EYESIGHT_LEFT = data.TreatEyesightLeft;
                                serviceReq.TREAT_EYESIGHT_RIGHT = data.TreatEyesightRight;
                            }

                            //Neu co cau hinh tach don thuoc dau * thi thuc hien xu ly service_req
                            if (HisExpMestCFG.IS_SPLIT_STAR_MARK)
                            {
                                bool hasStarMark = (IsNotNullOrEmpty(medicines) && medicines.Exists(t => HisMedicineTypeCFG.STAR_IDs != null && HisMedicineTypeCFG.STAR_IDs.Contains(t.MedicineTypeId)))
                                    || (IsNotNullOrEmpty(serviceReqMeties) && serviceReqMeties.Exists(t => HisMedicineTypeCFG.STAR_IDs != null && t.MedicineTypeId.HasValue && HisMedicineTypeCFG.STAR_IDs.Contains(t.MedicineTypeId.Value)));

                                bool hasNonStarMark = (IsNotNullOrEmpty(medicines) && medicines.Exists(t => HisMedicineTypeCFG.STAR_IDs != null && !HisMedicineTypeCFG.STAR_IDs.Contains(t.MedicineTypeId)))
                                    || (IsNotNullOrEmpty(serviceReqMeties) && serviceReqMeties.Exists(t => HisMedicineTypeCFG.STAR_IDs != null && t.MedicineTypeId.HasValue && !HisMedicineTypeCFG.STAR_IDs.Contains(t.MedicineTypeId.Value)))
                                    || IsNotNullOrEmpty(materials)
                                    || IsNotNullOrEmpty(serviceReqMeties);

                                //Neu co ca thuoc dau * va thuoc thuong thi tao them service_req
                                if (hasStarMark && hasNonStarMark)
                                {
                                    HIS_SERVICE_REQ starMarkReq = Mapper.Map<HIS_SERVICE_REQ>(serviceReq);
                                    starMarkReq.IS_STAR_MARK = Constant.IS_TRUE;
                                    resultData.Add(starMarkReq);
                                }
                                else if (hasStarMark && !hasNonStarMark)
                                {
                                    serviceReq.IS_STAR_MARK = Constant.IS_TRUE;
                                }
                            }

                            serviceReq.SPECIAL_MEDICINE_TYPE = data.SpecialMedicineType;
                        }
                    }
                }
            }
        }

        internal void Rollback()
        {
            this.hisServiceReqCreate.RollbackData();
        }
    }
}
