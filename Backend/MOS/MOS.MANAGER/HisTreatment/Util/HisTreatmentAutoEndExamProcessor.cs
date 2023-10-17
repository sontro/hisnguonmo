using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatment.Update.Finish;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisDepartmentTran;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Util
{
    public class HisTreatmentAutoEndExamProcessor : BusinessBase
    {
        private static bool IS_SENDING = false;

        public static void Run()
        {
            try
            {
                if (IS_SENDING)
                {
                    LogSystem.Debug("Tien trinh tu dong AutoEndExamTreatment dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                if (string.IsNullOrWhiteSpace(HisTreatmentCFG.DEFAULT_OF_AUTO_END))
                {
                    LogSystem.Debug("Tien trinh tu dong ket thuc dieu tri chua cau hinh ma loai ra vien");
                    return;
                }

                IS_SENDING = true;

                HIS_TREATMENT_END_TYPE type = HisTreatmentEndTypeCFG.DATA.FirstOrDefault(o => o.TREATMENT_END_TYPE_CODE == HisTreatmentCFG.DEFAULT_OF_AUTO_END);

                if (type == null)
                {
                    LogSystem.Debug("Tien trinh tu dong ket thuc dieu tri ma loai ra vien khong hop le");
                    return;
                }

                List<long> typeIds = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHET,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN
                };

                if (typeIds.Contains(type.ID))
                {
                    LogSystem.Warn(string.Format("Tien trinh tu dong ket thuc dieu tri ma loai ra vien la {0} ({1}) khong hop le", type.TREATMENT_END_TYPE_CODE, type.TREATMENT_END_TYPE_NAME));
                    return;
                }

                if (HisTreatmentResultCFG.TREATMENT_RESULT_ID__DEFAULT_OF_EXAM <= 0)
                {
                    LogSystem.Debug("Tien trinh tu dong ket thuc dieu tri chua cau hinh he thong ket qua dieu tri");
                    return;
                }

                List<long> resuldIds = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__CHET,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__DO,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__KHOI,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__KTD,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__NANG
                };
                if (!resuldIds.Contains(HisTreatmentResultCFG.TREATMENT_RESULT_ID__DEFAULT_OF_EXAM))
                {
                    LogSystem.Debug("Tien trinh tu dong ket thuc dieu tri cau hinh he thong ket qua dieu tri khong hop le");
                    return;
                }

                HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                filter.IN_DATE = Convert.ToInt64(DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "000000");
                filter.TDL_TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM;
                filter.IS_BHYT_PATIENT_TYPE = false;
                filter.IS_PAUSE = false;

                var treatments = new HisTreatmentGet().Get(filter);
                treatments = treatments != null && treatments.Count > 0 ? treatments.Where(o => !new HisTreatmentGet().GetUnpaid(o.ID).HasValue || (new HisTreatmentGet().GetUnpaid(o.ID).HasValue && new HisTreatmentGet().GetUnpaid(o.ID).Value <= 0)).ToList() : null;
                LogSystem.Debug(string.Format("AutoEndExamTreatmentJob has {0} treatments:", treatments.Count.ToString()));


                if (treatments != null && treatments.Count > 0)
                {
                    foreach (var treatment in treatments)
                    {
                        Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                        HIS_TREATMENT oldTreatment = Mapper.Map<HIS_TREATMENT>(treatment);

                        List<HIS_SERE_SERV> existsSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                        List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);

                        WorkPlaceSDO workPlace = new WorkPlaceSDO();
                        HIS_PROGRAM program = null;
                        HIS_MEDI_RECORD mediRecord = null;
                        V_HIS_DEATH_CERT_BOOK deathCertBook = null;
                        HIS_TREATMENT resultData = null;
                        HIS_DEPARTMENT_TRAN lastDt = new HisDepartmentTranGet().GetLastByTreatmentId(treatment.ID);
                        HisTreatmentFinishSDO sdo = new HisTreatmentFinishSDO();
                        sdo.TreatmentId = treatment.ID;
                        sdo.TreatmentResultId = HisTreatmentResultCFG.TREATMENT_RESULT_ID__DEFAULT_OF_EXAM;
                        sdo.TreatmentEndTypeId = type.ID;
                        sdo.TreatmentDayCount = 1;

                        var ss = existsSereServs.FirstOrDefault(o => o.TDL_IS_MAIN_EXAM == Constant.IS_TRUE);
                        if (ss != null)
                        {
                            sdo.EndRoomId = ss.TDL_EXECUTE_ROOM_ID;
                            workPlace.RoomId = ss.TDL_EXECUTE_ROOM_ID;
                            workPlace.BranchId = ss.TDL_EXECUTE_BRANCH_ID;
                            workPlace.DepartmentId = ss.TDL_EXECUTE_DEPARTMENT_ID;
                        }
                        else
                        {
                            var sereServ = existsSereServs.OrderByDescending(o => o.TDL_INTRUCTION_TIME).FirstOrDefault();
                            if (sereServ != null)
                            {
                                sdo.EndRoomId = sereServ.TDL_EXECUTE_ROOM_ID;
                                workPlace.RoomId = sereServ.TDL_EXECUTE_ROOM_ID;
                                workPlace.BranchId = sereServ.TDL_EXECUTE_BRANCH_ID;
                                workPlace.DepartmentId = sereServ.TDL_EXECUTE_DEPARTMENT_ID;
                            }
                        }
                        sdo.TreatmentFinishTime = Inventec.Common.DateTime.Get.Now().Value;
                        sdo.IcdText = treatment.ICD_TEXT;
                        sdo.IcdName = treatment.ICD_NAME;
                        sdo.IcdCode = treatment.ICD_CODE;
                        sdo.IcdSubCode = treatment.ICD_SUB_CODE;
                        sdo.IsExpXml4210Collinear = true;

                        CommonParam param = new CommonParam();
                        HisTreatmentFinish hisTreatmentFinish = new HisTreatmentFinish(param);
                        if (hisTreatmentFinish.FinishWithoutValidate(sdo, treatment, oldTreatment, existsSereServs, ptas, workPlace, program, lastDt, deathCertBook, ref resultData, ref mediRecord))
                        {
                            LogSystem.Debug("Tu dong ket thuc dieu tri thanh cong");
                        }
                        else
                        {
                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                            hisTreatmentFinish.RollBackData();
                            LogSystem.Debug("Tu dong ket thuc dieu tri that bai");
                        }
                    }
                }

                IS_SENDING = false;
            }
            catch (Exception ex)
            {
                IS_SENDING = false;
                LogSystem.Error(ex);
            }
        }
    }
}
