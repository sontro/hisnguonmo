using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MOS.UTILITY;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceRati;
using MOS.MANAGER.HisRationTime;
using MOS.MANAGER.HisServicePaty;
using MOS.ServicePaty;
using MOS.MANAGER.HisRationSum;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    partial class HisServiceReqRationCheck : BusinessBase
    {
        internal HisServiceReqRationCheck()
            : base()
        {
        }

        internal HisServiceReqRationCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool IsNotClosing(List<RationRequest> rationRequests)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(rationRequests))
                {
                    //bo check, ko can kiem tra da duyet chua
                    //List<long> roomIds = rationRequests.Select(o => o.RoomId).ToList();
                    //List<long> rationTimeIds = rationRequests.Select(o => o.RationTimeId).ToList();
                    //List<long> instructionTimes = rationRequests.Select(o => o.IntructionTime).ToList();

                    //HisRationSumFilterQuery filter = new HisRationSumFilterQuery();
                    //filter.ROOM_IDs = roomIds;
                    //filter.RATION_TIME_IDs = rationTimeIds;
                    //filter.INTRUCTION_DATEs = this.GetDate(instructionTimes);
                    //List<HIS_RATION_SUM> rationSums = new HisRationSumGet().Get(filter);

                    //List<HIS_RATION_TIME> rationTimes = new HisRationTimeGet().Get(new HisRationTimeFilterQuery());

                    //foreach(RationRequest r in rationRequests)
                    //{
                    //    if (rationSums != null && rationSums.Exists(t => t.INTRUCTION_DATE == (r.IntructionTime - r.IntructionTime % 1000000) && t.ROOM_ID == r.RoomId && t.RATION_TIME_ID == r.RationTimeId))
                    //    {
                    //        V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == r.RoomId).FirstOrDefault();
                    //        HIS_RATION_TIME rationTime = rationTimes.Where(o => o.ID == r.RationTimeId).FirstOrDefault();
                    //        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DaChotSuatAn, room.ROOM_NAME, rationTime.RATION_TIME_NAME);
                    //        result = false;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private List<long> GetDate(List<long> times)
        {
            List<long> result = new List<long>();
            foreach (long time in times)
            {
                long date = time - (time % 1000000);
                result.Add(date);
            }
            return result;
        }

        internal bool IsValidData(HisRationServiceReqSDO data)
        {
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.RationServices)) throw new ArgumentNullException("data.RationServices");
                if (!IsNotNullOrEmpty(data.InstructionTimes)) throw new ArgumentNullException("data.InstructionTimes");
                if (data.RationServices.Exists(t => t.RationTimeIds == null || t.RationTimeIds.Count == 0)) throw new ArgumentNullException("data.RationServices.RationTimeIds");
                if (data.RationServices.Exists(t => t.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongChoPhepChiDinhSuatAnSuDungDoiTuongBhyt);
                    return false;
                }
                return true;
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        internal bool IsProcessable(List<RationServiceSDO> rationServices)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(rationServices))
                {
                    //Kiem tra tinh kha thi cua cac RationServiceSDO (xem room co xu ly duoc dich vu khong)
                    List<RationServiceSDO> unhandlables = rationServices
                        .Where(o => !HisServiceRoomCFG.DATA_VIEW.Exists(t => t.SERVICE_ID == o.ServiceId && t.ROOM_ID == o.RoomId)).ToList();
                    if (IsNotNullOrEmpty(unhandlables))
                    {
                        List<string> unhandleServiceNames = HisServiceCFG.DATA_VIEW
                            .Where(o => unhandlables.Exists(t => t.ServiceId == o.ID))
                            .Select(o => o.SERVICE_NAME).ToList();
                        string extraMess = string.Join("; ", unhandleServiceNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_PhongDuocChiDinhKhongTheXuLyDichVu, extraMess);
                        result = false;
                    }

                    //Kiem tra xem thoi gian an cua cac suat an co thoa man ko
                    List<long> serviceIds = rationServices.Select(o => o.ServiceId).ToList();
                    List<HIS_SERVICE_RATI> serviceRatis = new HisServiceRatiGet().GetByServiceId(serviceIds);

                    foreach (RationServiceSDO sdo in rationServices)
                    {
                        List<long> tmp = sdo.RationTimeIds
                            .Where(t => serviceRatis == null || !serviceRatis.Exists(o => o.SERVICE_ID == sdo.ServiceId && t == o.RATION_TIME_ID)).ToList();
                        if (IsNotNullOrEmpty(tmp))
                        {
                            V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == sdo.ServiceId).FirstOrDefault();
                            List<HIS_RATION_TIME> rationTimes = new HisRationTimeGet().GetById(tmp);
                            List<string> rationTimeNames = IsNotNullOrEmpty(rationTimes) ? rationTimes.Select(o => o.RATION_TIME_NAME).ToList() : null;
                            string rationTimeNameStr = rationTimeNames != null ? string.Join(",", rationTimeNames) : "";
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_SuatAnKhongCungCapVaoThoiGian, service.SERVICE_NAME, rationTimeNameStr);
                            result = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool IsValidServicePaty(HisRationServiceReqSDO data, long roomId, long departmentId, List<HIS_TREATMENT> treatments, ref List<V_HIS_SERVICE_PATY> servicePaties)
        {
            bool result = true;
            try
            {
                if (IsNotNullOrEmpty(data.RationServices))
                {
                    List<V_HIS_SERVICE_PATY> sps = new List<V_HIS_SERVICE_PATY>();

                    foreach (HIS_TREATMENT treatment in treatments)
                    {
                        foreach (RationServiceSDO sdo in data.RationServices)
                        {
                            foreach (var item in sdo.RationTimeIds)
                            {
                                V_HIS_ROOM room = HisRoomCFG.DATA.Where(t => t.ID == sdo.RoomId).FirstOrDefault();
                                V_HIS_SERVICE_PATY servicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, room.BRANCH_ID, sdo.RoomId, roomId, departmentId, data.InstructionTimes[0], treatment.IN_TIME, sdo.ServiceId, sdo.PatientTypeId, null, null, null, null, treatment.TDL_PATIENT_CLASSIFY_ID,item); //chi lay intruction_times[0] cho don gian (vi nghiep vu nay ko qua quan trong)
                                if (servicePaty == null)
                                {
                                    HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == sdo.PatientTypeId).FirstOrDefault();
                                    V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == sdo.ServiceId).FirstOrDefault();
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_TYPE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                                    result = false;
                                }
                                else
                                {
                                    sps.Add(servicePaty);
                                }
                            }
                            
                        }
                        if (result)
                        {
                            servicePaties = sps;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool IsValidServicePaty(HisServiceReqRationUpdateSDO data, long roomId, long departmentId, HIS_TREATMENT treatment, long? instructionTime, ref List<V_HIS_SERVICE_PATY> servicePaties)
        {
            bool result = true;
            try
            {
                List<RationServiceSDO> dataCheck = new List<RationServiceSDO>();
                if (IsNotNullOrEmpty(data.InsertServices))
                {
                    dataCheck.AddRange(data.InsertServices);
                }
                if (IsNotNullOrEmpty(data.UpdateServices))
                {
                    dataCheck.AddRange(data.UpdateServices);
                }
                
                // Kiem tra thong tin chinh sach gia cua 2 danh sach them moi va danh sach cap nhat
                if (IsNotNullOrEmpty(dataCheck))
                {
                    List<V_HIS_SERVICE_PATY> sps = new List<V_HIS_SERVICE_PATY>();

                    foreach (RationServiceSDO sdo in dataCheck)
                    {
                        foreach (var item in sdo.RationTimeIds)
                        {
                            V_HIS_ROOM room = HisRoomCFG.DATA.Where(t => t.ID == sdo.RoomId).FirstOrDefault();
                            V_HIS_SERVICE_PATY servicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, room.BRANCH_ID, sdo.RoomId, roomId, departmentId, instructionTime.Value, treatment.IN_TIME, sdo.ServiceId, sdo.PatientTypeId, null, null, null, null, treatment.TDL_PATIENT_CLASSIFY_ID, item); //chi lay intruction_times[0] cho don gian (vi nghiep vu nay ko qua quan trong)
                            if (servicePaty == null)
                            {
                                HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == sdo.PatientTypeId).FirstOrDefault();
                                V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == sdo.ServiceId).FirstOrDefault();
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_TYPE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                                result = false;
                            }
                            else
                            {
                                sps.Add(servicePaty);
                            }
                        }
                    }

                    if (result)
                    {
                        servicePaties = sps;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
