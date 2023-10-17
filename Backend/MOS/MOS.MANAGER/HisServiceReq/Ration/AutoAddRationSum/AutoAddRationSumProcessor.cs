using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServRation;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisRationSum;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.ServicePaty;
using MOS.LibraryMessage;

namespace MOS.MANAGER.HisServiceReq.Ration.AutoAddRationSum
{
    public class AutoAddRationSumProcessor : BusinessBase
    {
        private static bool IS_RUNNING = false;

        public AutoAddRationSumProcessor()
            : base()
        {
        }

        public AutoAddRationSumProcessor(CommonParam param)
            : base(param)
        {
        }

        public static void Run()
        {
            try
            {
                if (IS_RUNNING)
                {
                    LogSystem.Debug("Tien trinh tu dong AutoAddRationSum: dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                IS_RUNNING = true;

                if (!HisServiceReqCFG.IS_ALLOW_AUTO_ADD_RATION_SUM)
                {
                    LogSystem.Debug("Tien trinh tu dong AutoAddRationSum: Chua cau hinh key MOS.HIS_SERVICE_REQ.AUTO_ADD_RATION_SUM");
                    return;
                }

                HisRationSumFilterQuery filter = new HisRationSumFilterQuery();
                filter.RATION_SUM_STT_ID = IMSys.DbConfig.HIS_RS.HIS_RATION_SUM_STT.ID__REQ;
                List<HIS_RATION_SUM> rationSums = new HisRationSumGet().Get(filter);
                if (rationSums != null && rationSums.Count > 0)
                {
                    // Lấy ra danh sách phiếu tổng hợp có thời gian yêu cầu lớn nhất của từng nhóm theo khoa chỉ định va phòng xử lý
                    List<HIS_RATION_SUM> sums = rationSums.GroupBy(o => new { o.DEPARTMENT_ID, o.ROOM_ID }).SelectMany(a => a.Where(b => b.REQ_TIME.HasValue && b.REQ_TIME.Value == a.Max(c => c.REQ_TIME))).ToList();

                    if (sums != null && sums.Count > 0)
                    {
                        foreach (var sum in sums)
                        {

                            HisServiceReqFilterQuery reqFilter = new HisServiceReqFilterQuery();
                            reqFilter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__AN;
                            reqFilter.HAS_RATION_SUM_ID = false;
                            reqFilter.REQUEST_DEPARTMENT_ID = sum.DEPARTMENT_ID;
                            reqFilter.EXECUTE_ROOM_ID = sum.ROOM_ID;
                            reqFilter.CREATE_TIME_FROM = sum.CREATE_TIME;
                            reqFilter.CREATE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber((Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sum.CREATE_TIME.Value).Value.AddDays(1)));

                            List<HIS_SERVICE_REQ> reqs = new HisServiceReqGet().Get(reqFilter);

                            if (reqs != null && reqs.Count > 0)
                            {
                                List<long> treatIds = reqs.Select(o => o.TREATMENT_ID).ToList();
                                List<HIS_TREATMENT> treats = new HisTreatmentGet().GetByIds(treatIds);
                                List<long> validTreatIds = treats != null && treats.Count > 0 ? treats.Where(o =>
                                       o.IS_TEMPORARY_LOCK != Constant.IS_TRUE
                                    && o.IS_PAUSE != Constant.IS_TRUE
                                    && o.IS_LOCK_HEIN != Constant.IS_TRUE
                                    && !o.OUT_TIME.HasValue // Check dieu kien chua tam thoi ket thuc dieu tri
                                    && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                                    ).Select(s => s.ID).ToList() : null;

                                if (validTreatIds != null && validTreatIds.Count > 0)
                                {
                                    List<HIS_SERVICE_REQ> validReqs = reqs.Where(o => validTreatIds.Contains(o.TREATMENT_ID)).ToList();
                                    if (validReqs != null && validReqs.Count > 0)
                                    {
                                        List<HIS_SERVICE_REQ> updateServicereq = null;
                                        //xu ly chi sach gia dich vu truoc.
                                        UpdateSereServ(validReqs, sum, ref updateServicereq);
                                        UpdateServiceReq(updateServicereq, sum.ID);
                                        UpdateRationSum(sum);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    LogSystem.Debug("Tien trinh tu dong AutoAddRationSum: Khong co HIS_RATIOM_SUM thoa man dieu kien");
                }

                IS_RUNNING = false;
            }
            catch (Exception ex)
            {
                IS_RUNNING = false;
                LogSystem.Error(ex);
            }
        }

        private static void UpdateRationSum(HIS_RATION_SUM sum)
        {
            try
            {
                if (sum != null)
                {
                    Mapper.CreateMap<HIS_RATION_SUM, HIS_RATION_SUM>();
                    HIS_RATION_SUM before = Mapper.Map<HIS_RATION_SUM>(sum);

                    sum.REQ_TIME = Inventec.Common.DateTime.Get.Now();

                    if (!new HisRationSumUpdate().Update(sum, before))
                    {
                        throw new Exception("Tien trinh tu dong AutoAddRationSum: Cap nhat trang thai va gan tong hop cho y lenh that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private static List<HIS_SERE_SERV> MakeData(List<HIS_SERVICE_REQ> validReqs, HIS_RATION_SUM sum, ref Dictionary<HIS_SERE_SERV, HIS_SERE_SERV_EXT> dicSSExt)
        {
            List<HIS_SERE_SERV> result = new List<HIS_SERE_SERV>();
            try
            {
                CommonParam param = new CommonParam();
                if (validReqs != null && validReqs.Count > 0 && sum != null)
                {
                    List<long> treatmentIds = validReqs.Select(s => s.TREATMENT_ID).Distinct().ToList();
                    List<HIS_TREATMENT> treatmentList = new HisTreatmentGet().GetByIds(treatmentIds);

                    foreach (HIS_SERVICE_REQ serviceReq in validReqs)
                    {
                        List<HIS_SERE_SERV_RATION> ssRations = new HisSereServRationGet().GetByServiceReqId(serviceReq.ID);

                        try
                        {
                            List<HIS_SERE_SERV> toInsert = new List<HIS_SERE_SERV>();
                            foreach (var ration in ssRations)
                            {
                                HIS_SERE_SERV ss = new HIS_SERE_SERV();
                                ss.SERVICE_REQ_ID = serviceReq.ID;
                                ss.SERVICE_ID = ration.SERVICE_ID;
                                ss.AMOUNT = ration.AMOUNT;
                                ss.PATIENT_TYPE_ID = ration.PATIENT_TYPE_ID;
                                ss.DISCOUNT = ration.DISCOUNT;
                                
                                HIS_TREATMENT treatment = treatmentList != null ? treatmentList.FirstOrDefault(o => o.ID == serviceReq.TREATMENT_ID) : new HIS_TREATMENT();
                                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.EXECUTE_DEPARTMENT_ID).FirstOrDefault();
                                V_HIS_SERVICE_PATY servicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, department != null ? department.BRANCH_ID : 0, serviceReq.EXECUTE_ROOM_ID, serviceReq.REQUEST_ROOM_ID, serviceReq.REQUEST_DEPARTMENT_ID, serviceReq.INTRUCTION_TIME, treatment.IN_TIME, ration.SERVICE_ID, ration.PATIENT_TYPE_ID, null, null, null, null, treatment.TDL_PATIENT_CLASSIFY_ID, serviceReq.RATION_TIME_ID);

                                if (servicePaty == null)
                                {
                                    throw new Exception("Khong ton tai chinh sach gia tuong ung voi suat an: " + ration.SERVICE_ID + "va patient_type_id: " + ration.PATIENT_TYPE_ID);
                                }

                                ss.PRICE = servicePaty.PRICE;
                                ss.VAT_RATIO = servicePaty.VAT_RATIO;
                                ss.ACTUAL_PRICE = servicePaty.ACTUAL_PRICE;
                                ss.ORIGINAL_PRICE = servicePaty.PRICE;

                                HisSereServUtil.SetTdl(ss, serviceReq);
                                if (!String.IsNullOrWhiteSpace(ration.INSTRUCTION_NOTE))
                                {
                                    HIS_SERE_SERV_EXT ssExt = new HIS_SERE_SERV_EXT();
                                    ssExt.INSTRUCTION_NOTE = ration.INSTRUCTION_NOTE;
                                    dicSSExt[ss] = ssExt;
                                }

                                toInsert.Add(ss);
                            }

                            if (toInsert.Count > 0)
                            {
                                result.AddRange(toInsert);
                            }
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        private static void UpdateSereServ(List<HIS_SERVICE_REQ> validReqs, HIS_RATION_SUM sum, ref List<HIS_SERVICE_REQ> updateServiceReq)
        {
            try
            {
                Dictionary<HIS_SERE_SERV, HIS_SERE_SERV_EXT> dicSereServExt = new Dictionary<HIS_SERE_SERV, HIS_SERE_SERV_EXT>();

                List<HIS_SERE_SERV> toInserts = MakeData(validReqs, sum, ref dicSereServExt);
                if (toInserts == null || toInserts.Count <= 0)
                {
                    return;
                }

                List<long> reqIds = toInserts.Select(s => s.SERVICE_REQ_ID ?? 0).Distinct().ToList();
                updateServiceReq = validReqs.Where(o => reqIds.Contains(o.ID)).ToList();

                if (!new HisSereServCreate().CreateList(toInserts, validReqs, false))
                {
                    throw new Exception("Tien trinh tu dong AutoAddRationSum: Cap nhat cac HIS_SERE_SERV that bai");
                }

                if (dicSereServExt.Count > 0)
                {
                    List<HIS_SERE_SERV_EXT> createList = new List<HIS_SERE_SERV_EXT>();
                    foreach (var dic in dicSereServExt)
                    {
                        HisSereServExtUtil.SetTdl(dic.Value, dic.Key);
                        createList.Add(dic.Value);
                    }
                    if (!new HisSereServExtCreate().CreateList(createList))
                    {
                        throw new Exception("Tien trinh tu dong AutoAddRationSum: Cap nhat cac HIS_SERE_SERV_EXT that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private static void UpdateServiceReq(List<HIS_SERVICE_REQ> validReqs, long rationSumId)
        {
            try
            {
                if (validReqs != null && validReqs.Count > 0)
                {
                    validReqs.ForEach(o =>
                    {
                        o.RATION_SUM_ID = rationSumId;
                        o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                    });
                    if (!new HisServiceReqUpdate().UpdateList(validReqs))
                    {
                        throw new Exception("Tien trinh tu dong AutoAddRationSum: Cap nhat trang thai va gan tong hop cho y lenh that bai");
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