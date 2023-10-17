using HXT.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Lock
{
    partial class HisTreatmentLockSendDataXml : BusinessBase
    {
        internal HisTreatmentLockSendDataXml()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentLockSendDataXml(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
        }

        internal bool Run(HIS_TREATMENT data)
        {
            bool result = false;
            try
            {
                string uri = "";
                string loginame = "";
                string password = "";

                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && !String.IsNullOrWhiteSpace(Config.HisHeinApprovalCFG.SYNC_XML_FPT_OPTION);
                valid = valid && ProcessConfigData(Config.HisHeinApprovalCFG.SYNC_XML_FPT_OPTION, ref uri, ref loginame, ref password);
                valid = valid && ProcessLogin(uri, loginame, password);
                valid = valid && data.TDL_PATIENT_TYPE_ID == Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                if (valid)
                {
                    List<HXT_BENH_NHAN_VS> listBenhNhan = new List<HXT_BENH_NHAN_VS>();
                    ProcessDataBenhNhan(data, ref listBenhNhan);

                    if (IsNotNullOrEmpty(listBenhNhan))
                    {
                        CommonParam createParam = new CommonParam();
                        List<HXT_BENH_NHAN_VS> apiResult = ApiConsumerStore.hxtConsumerWrapper.Post<List<HXT_BENH_NHAN_VS>>(true, "/api/HxtBenhNhanVs/CreateList", createParam, listBenhNhan);
                        if (!IsNotNullOrEmpty(apiResult))
                        {
                            Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => createParam), createParam));
                            Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listBenhNhan), listBenhNhan));
                            throw new Exception("Tao du lieu dong bo DB XML that bai");
                        }

                        result = true;
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

        private void ProcessDataBenhNhan(HIS_TREATMENT data, ref List<HXT_BENH_NHAN_VS> listBenhNhan)
        {
            try
            {
                if (IsNotNullOrEmpty(listBenhNhan))
                {
                    listBenhNhan = new List<HXT_BENH_NHAN_VS>();
                }

                List<V_HIS_DEPARTMENT_TRAN> listDepartmentTrans = new HisDepartmentTran.HisDepartmentTranGet().GetViewByTreatmentId(data.ID);

                if (!IsNotNullOrEmpty(listDepartmentTrans))
                {
                    return;
                }

                listDepartmentTrans = listDepartmentTrans.OrderBy(o => o.DEPARTMENT_IN_TIME).ThenBy(o => o.ID).ToList();

                HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == data.BRANCH_ID);
                V_HIS_TREATMENT_3 hisTreatment = new HisTreatment.HisTreatmentGet().GetView3ById(data.ID);
                List<HIS_DHST> dhsts = new HisDhst.HisDhstGet().GetByTreatmentId(data.ID);
                List<V_HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlter.HisPatientTypeAlterGet().GetViewByTreatmentId(data.ID);
                List<V_HIS_SERE_SERV_2> listSereServ = new HisSereServ.HisSereServGet().GetView2ByTreatmentId(data.ID);

                Dictionary<string, string> dicDisploma = new Dictionary<string, string>();
                List<HIS_EMPLOYEE> lstEmp = HisEmployeeCFG.DATA.Where(o => !String.IsNullOrWhiteSpace(o.DIPLOMA)).ToList();
                if (IsNotNullOrEmpty(lstEmp))
                {
                    lstEmp = lstEmp.OrderByDescending(o => o.MODIFY_TIME ?? 0).GroupBy(o => o.LOGINNAME).Select(s => s.First()).ToList();
                    dicDisploma = lstEmp.ToDictionary(o => o.LOGINNAME, o => o.DIPLOMA);
                }

                Dictionary<long, List<HXT_DICH_VU_VS>> dicDepartmentDetail = new Dictionary<long, List<HXT_DICH_VU_VS>>();

                ProcessDataSereServ(data, dicDisploma, ref dicDepartmentDetail);

                foreach (var dichvu in dicDepartmentDetail)
                {
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == dichvu.Key);
                    if (!IsNotNull(department))
                    {
                        continue;
                    }

                    HXT_BENH_NHAN_VS benhNhan = new HXT_BENH_NHAN_VS();
                    benhNhan.DEPARTMENT_CODE = department.DEPARTMENT_CODE;
                    benhNhan.DEPARTMENT_ID = department.ID;
                    benhNhan.DEPARTMENT_NAME = department.DEPARTMENT_NAME;

                    V_HIS_DEPARTMENT_TRAN departmentTran = listDepartmentTrans.LastOrDefault(o => o.DEPARTMENT_ID == department.ID) ?? new V_HIS_DEPARTMENT_TRAN();

                    if (departmentTran.PREVIOUS_ID.HasValue)
                    {
                        V_HIS_DEPARTMENT_TRAN previousDepartmentTran = listDepartmentTrans.FirstOrDefault(o => o.ID == departmentTran.PREVIOUS_ID);
                        benhNhan.PREVIOUS_DEPARTMENT_CODE = IsNotNull(previousDepartmentTran) ? previousDepartmentTran.DEPARTMENT_CODE : "";
                    }

                    long inTime = departmentTran.DEPARTMENT_IN_TIME ?? hisTreatment.IN_TIME;
                    long outTime = 0;

                    V_HIS_DEPARTMENT_TRAN nextDepartment = listDepartmentTrans.FirstOrDefault(o => o.PREVIOUS_ID == departmentTran.ID);
                    if (IsNotNull(nextDepartment))
                    {
                        outTime = nextDepartment.DEPARTMENT_IN_TIME ?? (hisTreatment.OUT_TIME ?? (Inventec.Common.DateTime.Get.Now() ?? 0));
                    }
                    else
                    {
                        outTime = hisTreatment.OUT_TIME ?? (Inventec.Common.DateTime.Get.Now() ?? 0);
                    }

                    benhNhan.IN_TIME = inTime;
                    benhNhan.OUT_TIME = outTime;

                    if (hisTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU ||
                       hisTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU ||
                        hisTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                    {
                        List<V_HIS_DEPARTMENT_TRAN> departmentTranCountTime = listDepartmentTrans.Where(o => o.DEPARTMENT_ID == department.ID && o.DEPARTMENT_IN_TIME.HasValue).ToList();
                        int hour = 0;
                        foreach (var item in departmentTranCountTime)
                        {
                            long timeIn = item.DEPARTMENT_IN_TIME ?? 0;
                            long timeOut = 0;
                            V_HIS_DEPARTMENT_TRAN nextCountDepartment = listDepartmentTrans.FirstOrDefault(o => o.PREVIOUS_ID == item.ID);
                            if (IsNotNull(nextCountDepartment))
                            {
                                timeOut = nextCountDepartment.DEPARTMENT_IN_TIME ?? (hisTreatment.OUT_TIME ?? (Inventec.Common.DateTime.Get.Now() ?? 0));
                            }
                            else
                            {
                                timeOut = hisTreatment.OUT_TIME ?? (Inventec.Common.DateTime.Get.Now() ?? 0);
                            }

                            hour += Inventec.Common.DateTime.Calculation.DifferenceTime(timeIn, timeOut, Inventec.Common.DateTime.Calculation.UnitDifferenceTime.HOUR);
                        }

                        if (hour > 4)
                        {
                            benhNhan.TREATMENT_DAY_COUNT = Math.Round((decimal)hour / 24, 1, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            benhNhan.TREATMENT_DAY_COUNT = 0;
                        }
                    }
                    else
                    {
                        benhNhan.TREATMENT_DAY_COUNT = hisTreatment.TREATMENT_DAY_COUNT;
                    }

                    V_HIS_PATIENT_TYPE_ALTER patientTypeAlter = patientTypeAlters.FirstOrDefault(o => o.DEPARTMENT_TRAN_ID == departmentTran.ID);
                    if (!IsNotNull(patientTypeAlter))
                    {
                        patientTypeAlter = patientTypeAlters.OrderByDescending(o => o.LOG_TIME).ThenByDescending(o => o.ID).FirstOrDefault();
                    }

                    benhNhan.ADDRESS = !String.IsNullOrWhiteSpace(patientTypeAlter.ADDRESS) ? patientTypeAlter.ADDRESS : hisTreatment.TDL_PATIENT_ADDRESS;

                    benhNhan.FREE_CO_PAID_TIME = patientTypeAlter.FREE_CO_PAID_TIME;
                    benhNhan.HEIN_CARD_FROM_TIME = patientTypeAlter.HEIN_CARD_FROM_TIME;
                    benhNhan.HEIN_CARD_NUMBER = patientTypeAlter.HEIN_CARD_NUMBER;
                    benhNhan.HEIN_CARD_TO_TIME = patientTypeAlter.HEIN_CARD_TO_TIME;
                    benhNhan.HEIN_MEDI_ORG_CODE = patientTypeAlter.HEIN_MEDI_ORG_CODE;
                    benhNhan.JOIN_5_YEAR_TIME = patientTypeAlter.JOIN_5_YEAR_TIME;
                    benhNhan.LIVE_AREA_CODE = patientTypeAlter.LIVE_AREA_CODE;
                    benhNhan.RIGHT_ROUTE_CODE = patientTypeAlter.RIGHT_ROUTE_CODE;
                    benhNhan.MALOAIKCB = patientTypeAlter.TREATMENT_TYPE_ID;
                    benhNhan.HEIN_RATIO = new MOS.LibraryHein.Bhyt.BhytHeinProcessor().GetDefaultHeinRatio(patientTypeAlter.TREATMENT_TYPE_CODE, patientTypeAlter.HEIN_CARD_NUMBER, patientTypeAlter.LEVEL_CODE, patientTypeAlter.RIGHT_ROUTE_CODE);

                    if (patientTypeAlter.RIGHT_ROUTE_CODE == MOS.LibraryHein.Bhyt.HeinRightRoute.HeinRightRouteCode.TRUE)
                    {
                        string province = !String.IsNullOrWhiteSpace(patientTypeAlter.HEIN_MEDI_ORG_CODE) ? patientTypeAlter.HEIN_MEDI_ORG_CODE.Substring(0, 2) : "";
                        var mediOrg = HisMediOrgCFG.DATA.FirstOrDefault(o => o.MEDI_ORG_CODE == patientTypeAlter.HEIN_MEDI_ORG_CODE);

                        if (patientTypeAlter.RIGHT_ROUTE_TYPE_CODE == MOS.LibraryHein.Bhyt.HeinRightRouteType.HeinRightRouteTypeCode.EMERGENCY)
                            benhNhan.MA_LYDO_VVIEN = 2;
                        else if (!String.IsNullOrWhiteSpace(patientTypeAlter.HEIN_MEDI_ORG_CODE) &&
                            (patientTypeAlter.HEIN_MEDI_ORG_CODE == branch.HEIN_MEDI_ORG_CODE
                            || (!String.IsNullOrWhiteSpace(branch.ACCEPT_HEIN_MEDI_ORG_CODE) && branch.ACCEPT_HEIN_MEDI_ORG_CODE.Contains(patientTypeAlter.HEIN_MEDI_ORG_CODE))
                            ))
                            benhNhan.MA_LYDO_VVIEN = 1;
                        else if (branch.HEIN_LEVEL_CODE == MOS.LibraryHein.Bhyt.HeinLevel.HeinLevelCode.DISTRICT || branch.HEIN_LEVEL_CODE == MOS.LibraryHein.Bhyt.HeinLevel.HeinLevelCode.COMMUNE
                            )
                        {
                            benhNhan.MA_LYDO_VVIEN = 3;
                            if (province == branch.HEIN_PROVINCE_CODE && mediOrg != null && (mediOrg.LEVEL_CODE == MOS.LibraryHein.Bhyt.HeinLevel.HeinLevelCode.DISTRICT || mediOrg.LEVEL_CODE == MOS.LibraryHein.Bhyt.HeinLevel.HeinLevelCode.COMMUNE))
                            {
                                benhNhan.MA_LYDO_VVIEN = 4;
                            }
                        }
                        else
                            benhNhan.MA_LYDO_VVIEN = 1;
                    }
                    else if (patientTypeAlter.RIGHT_ROUTE_CODE == MOS.LibraryHein.Bhyt.HeinRightRoute.HeinRightRouteCode.FALSE)
                        benhNhan.MA_LYDO_VVIEN = 3;

                    if (IsNotNullOrEmpty(dhsts))
                    {
                        HIS_DHST dhst = dhsts.OrderByDescending(o => o.EXECUTE_TIME ?? 0).ThenByDescending(o => o.ID).FirstOrDefault(o => o.WEIGHT.HasValue);
                        if (IsNotNull(dhst))
                        {
                            benhNhan.CANNANG = dhst.WEIGHT;
                        }
                    }

                    benhNhan.EXECUTE_TIME = hisTreatment.FEE_LOCK_TIME;

                    benhNhan.ICD_CODE = hisTreatment.ICD_CODE;
                    benhNhan.ICD_NAME = hisTreatment.ICD_NAME;
                    benhNhan.ICD_SUB_CODE = hisTreatment.ICD_SUB_CODE;

                    benhNhan.IN_CODE = hisTreatment.IN_CODE;
                    benhNhan.DOB = hisTreatment.TDL_PATIENT_DOB;
                    benhNhan.GENDER_ID = hisTreatment.TDL_PATIENT_GENDER_ID;
                    benhNhan.GENDER_NAME = hisTreatment.TDL_PATIENT_GENDER_NAME;
                    benhNhan.PATIENT_CODE = hisTreatment.TDL_PATIENT_CODE;
                    benhNhan.PATIENT_NAME = hisTreatment.TDL_PATIENT_NAME;
                    benhNhan.TRANSFER_IN_MEDI_ORG_CODE = hisTreatment.TRANSFER_IN_MEDI_ORG_CODE;
                    benhNhan.TREATMENT_CODE = hisTreatment.TREATMENT_CODE;

                    if (hisTreatment.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN)
                        benhNhan.TINHTRANGRAVIEN = "2";
                    else if (hisTreatment.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__TRON)
                        benhNhan.TINHTRANGRAVIEN = "3";
                    else if (hisTreatment.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__XINRAVIEN)
                        benhNhan.TINHTRANGRAVIEN = "4";
                    else if (hisTreatment.TREATMENT_END_TYPE_ID.HasValue)
                        benhNhan.TINHTRANGRAVIEN = "1";

                    if (hisTreatment.ACCIDENT_HURT_TYPE_ID.HasValue && !String.IsNullOrEmpty(hisTreatment.ACCIDENT_HURT_TYPE_BHYT_CODE))
                    {
                        benhNhan.ACCIDENT_HURT_TYPE_CODE = hisTreatment.ACCIDENT_HURT_TYPE_BHYT_CODE;
                    }
                    else
                    {
                        benhNhan.ACCIDENT_HURT_TYPE_CODE = "0";
                    }

                    HIS_TREATMENT_END_TYPE endType = HisTreatmentEndTypeCFG.DATA.FirstOrDefault(o => o.ID == hisTreatment.TREATMENT_END_TYPE_ID);
                    if (IsNotNull(endType))
                    {
                        benhNhan.TREATMENT_END_TYPE_CODE = endType.TREATMENT_END_TYPE_CODE;
                        benhNhan.TREATMENT_END_TYPE_NAME = endType.TREATMENT_END_TYPE_NAME;
                    }

                    if (IsNotNull(nextDepartment))
                    {
                        if (!IsNotNull(endType) && !patientTypeAlters.Exists(o => o.DEPARTMENT_TRAN_ID == nextDepartment.ID))
                        {
                            benhNhan.TREATMENT_END_TYPE_CODE = "VV";
                            benhNhan.TREATMENT_END_TYPE_NAME = "Vào viện";
                        }
                        else
                        {
                            benhNhan.TREATMENT_END_TYPE_CODE = "CK";
                            benhNhan.TREATMENT_END_TYPE_NAME = "Chuyển khoa";
                        }
                    }

                    List<HXT_DICH_VU_VS> listDichVu = dichvu.Value.ToList();

                    benhNhan.HXT_DICH_VU_VS = listDichVu;

                    benhNhan.TOTAL_HEIN_PRICE = listDichVu.Sum(s => s.TOTAL_HEIN_PRICE ?? 0);
                    benhNhan.TOTAL_OTHER_SOURCE_PRICE = listDichVu.Sum(s => s.TOTAL_OTHER_SOURCE_PRICE ?? 0);
                    benhNhan.TOTAL_PATIENT_PRICE = listDichVu.Sum(s => s.TOTAL_PATIENT_PRICE ?? 0);
                    benhNhan.TOTAL_PATIENT_PRICE_BHYT = listDichVu.Sum(s => s.TOTAL_PATIENT_PRICE_BHYT ?? 0);
                    benhNhan.TOTAL_PRICE = listDichVu.Sum(s => s.TOTAL_PRICE ?? 0);

                    benhNhan.BRANCH_MEDI_ORG_CODE = branch != null ? branch.HEIN_MEDI_ORG_CODE : "";
                    benhNhan.END_DEPARTMENT_CODE = hisTreatment.END_BHYT_CODE;

                    if (hisTreatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__KHOI)
                        benhNhan.KET_QUA_DTRI = TreatmentResultBhytCFG.Khoi;
                    else if (hisTreatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__DO)
                        benhNhan.KET_QUA_DTRI = TreatmentResultBhytCFG.Do;
                    else if (hisTreatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__KTD)
                        benhNhan.KET_QUA_DTRI = TreatmentResultBhytCFG.KhongThayDoi;
                    else if (hisTreatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__NANG)
                        benhNhan.KET_QUA_DTRI = TreatmentResultBhytCFG.NangHon;
                    else if (hisTreatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__CHET)
                        benhNhan.KET_QUA_DTRI = TreatmentResultBhytCFG.TuVong;

                    listBenhNhan.Add(benhNhan);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Công khám thuộc khoa nào xử lý thì được tính cho khoa đó
        /// Thuốc/vật tư do phòng khám (phòng xử lý công khám) kê sẽ được tính cho khoa của phòng khám đó
        /// Với các thuốc/vật tư phát sinh khi thực hiện CLS sẽ được tính cho khoa chỉ định dịch vụ CLS đó.
        /// Các dịch vụ còn lại do khoa nào chỉ định thì được tính cho khoa đó.
        /// </summary>
        /// <param name="listSereServ"></param>
        /// <param name="dicDisploma"></param>
        /// <param name="dichvus"></param>
        private void ProcessDataSereServ(HIS_TREATMENT data, Dictionary<string, string> dicDisploma, ref Dictionary<long, List<HXT_DICH_VU_VS>> dicDepartmentDetail)
        {
            try
            {
                List<V_HIS_SERE_SERV_2> listSereServ = new HisSereServ.HisSereServGet().GetView2ByTreatmentId(data.ID);
                if (IsNotNullOrEmpty(listSereServ))
                {
                    listSereServ = listSereServ.Where(o =>
                            o.PRICE > 0 &&
                            o.AMOUNT > 0 &&
                            o.IS_EXPEND != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE &&
                            o.IS_NO_EXECUTE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).ToList();

                    if (!IsNotNullOrEmpty(listSereServ))
                    {
                        return;
                    }

                    List<V_HIS_BED_LOG> bedLogs = new HisBedLog.HisBedLogGet().GetViewByTreatmentId(data.ID);

                    List<long> ekipIds = listSereServ.Select(s => s.EKIP_ID ?? 0).Distinct().ToList();
                    List<HIS_EKIP_USER> hisEkipUsers = new HisEkipUser.HisEkipUserGet().GetByEkipIds(ekipIds);
                    List<HIS_SERVICE_REQ> listServiceReq = new HisServiceReq.HisServiceReqGet().GetByTreatmentId(data.ID);

                    Dictionary<long, List<V_HIS_SERE_SERV_2>> dicChildSereServ = new Dictionary<long, List<V_HIS_SERE_SERV_2>>();
                    foreach (var hisSereServ in listSereServ)
                    {
                        if (hisSereServ.PARENT_ID.HasValue)
                        {
                            if (!dicChildSereServ.ContainsKey(hisSereServ.PARENT_ID.Value))
                                dicChildSereServ[hisSereServ.PARENT_ID.Value] = new List<V_HIS_SERE_SERV_2>();
                            dicChildSereServ[hisSereServ.PARENT_ID.Value].Add(hisSereServ);
                        }
                    }

                    var listHighTech = listSereServ.Where(o => dicChildSereServ.ContainsKey(o.ID)).OrderBy(b => b.INTRUCTION_TIME).ToList();
                    Dictionary<long, string> dicHighTech = new Dictionary<long, string>();

                    List<HIS_ICD> icdNds = HisIcdCFG.DATA.Where(o => o.IS_HEIN_NDS == 1).ToList();

                    foreach (var sereServ in listSereServ)
                    {
                        long departmentId = 0;

                        ProcessGetDepartment(sereServ, listServiceReq, ref departmentId);

                        if (departmentId == 0)
                        {
                            continue;
                        }

                        if (!dicDepartmentDetail.ContainsKey(departmentId))
                        {
                            dicDepartmentDetail[departmentId] = new List<HXT_DICH_VU_VS>();
                        }

                        HXT_DICH_VU_VS dv = new HXT_DICH_VU_VS();
                        dv.AMOUNT = sereServ.AMOUNT;
                        dv.HEIN_SERVICE_BHYT_CODE = sereServ.TDL_HEIN_SERVICE_BHYT_CODE;
                        dv.HEIN_SERVICE_BHYT_NAME = sereServ.TDL_HEIN_SERVICE_BHYT_NAME;
                        dv.HST_BHYT_CODE = sereServ.TDL_HST_BHYT_CODE;
                        dv.INTRUCTION_TIME = sereServ.TDL_INTRUCTION_TIME;
                        dv.SERVICE_CODE = sereServ.TDL_SERVICE_CODE;
                        dv.SERVICE_NAME = sereServ.TDL_SERVICE_NAME;
                        dv.SERVICE_UNIT_NAME = sereServ.SERVICE_UNIT_NAME;

                        decimal t = 0;
                        if (sereServ.HEIN_LIMIT_PRICE.HasValue)
                        {
                            t = Math.Round(sereServ.HEIN_LIMIT_PRICE.Value / (sereServ.ORIGINAL_PRICE * (1 + sereServ.VAT_RATIO)), 2);
                        }
                        else if (sereServ.LIMIT_PRICE.HasValue)
                        {
                            t = Math.Round(sereServ.LIMIT_PRICE.Value / (sereServ.ORIGINAL_PRICE * (1 + sereServ.VAT_RATIO)), 2);
                        }
                        else
                        {
                            t = Math.Round(sereServ.PRICE / sereServ.ORIGINAL_PRICE, 2);
                        }

                        if (sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                            || sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT)
                        {
                            dv.HEIN_RATIO = t;
                            dv.SERVICE_RATIO = 1;
                        }
                        else
                        {
                            dv.HEIN_RATIO = 1;
                            dv.SERVICE_RATIO = t;
                        }

                        List<String> lstMaBacSi = new List<string>();
                        if (sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC)
                        {
                            if (!String.IsNullOrWhiteSpace(sereServ.REQUEST_LOGINNAME) && dicDisploma.ContainsKey(sereServ.REQUEST_LOGINNAME))
                            {
                                lstMaBacSi.Add(dicDisploma[sereServ.REQUEST_LOGINNAME]);
                            }
                        }
                        else
                        {
                            if (sereServ.EKIP_ID.HasValue && hisEkipUsers.Exists(o => o.EKIP_ID == sereServ.EKIP_ID.Value))
                            {
                                List<HIS_EKIP_USER> listEkipUsers = hisEkipUsers.Where(o => o.EKIP_ID == sereServ.EKIP_ID.Value).ToList();
                                foreach (var ekipUser in listEkipUsers)
                                {
                                    if (!String.IsNullOrWhiteSpace(ekipUser.LOGINNAME) && dicDisploma.ContainsKey(ekipUser.LOGINNAME))
                                    {
                                        lstMaBacSi.Add(dicDisploma[ekipUser.LOGINNAME]);
                                    }
                                }
                            }
                            else
                            {
                                if (!String.IsNullOrWhiteSpace(sereServ.REQUEST_LOGINNAME) && dicDisploma.ContainsKey(sereServ.REQUEST_LOGINNAME))
                                {
                                    lstMaBacSi.Add(dicDisploma[sereServ.REQUEST_LOGINNAME]);
                                }

                                if (!String.IsNullOrWhiteSpace(sereServ.EXECUTE_LOGINNAME) && dicDisploma.ContainsKey(sereServ.EXECUTE_LOGINNAME))
                                {
                                    lstMaBacSi.Add(dicDisploma[sereServ.EXECUTE_LOGINNAME]);
                                }
                            }
                        }

                        dv.DIPLOMA = string.Join(";", lstMaBacSi.Distinct().ToList());

                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == sereServ.SERVICE_ID);
                        if (IsNotNull(service))
                        {
                            if (service.PARENT_ID.HasValue)
                            {
                                V_HIS_SERVICE parent = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == service.PARENT_ID.Value);
                                if (IsNotNull(parent))
                                {
                                    dv.PARENT_SERVICE_CODE = parent.SERVICE_CODE;
                                    dv.PARENT_SERVICE_NAME = parent.SERVICE_NAME;
                                }
                            }

                            if (service.DIIM_TYPE_ID.HasValue)
                            {
                                dv.SERVICE_TYPE_GROUP_NAME = service.DIIM_TYPE_NAME;
                            }
                            else if (service.FUEX_TYPE_ID.HasValue)
                            {
                                dv.SERVICE_TYPE_GROUP_NAME = service.FUEX_TYPE_NAME;
                            }
                            else if (service.TEST_TYPE_ID.HasValue)
                            {
                                dv.SERVICE_TYPE_GROUP_NAME = service.TEST_TYPE_NAME;
                            }

                            dv.SERVICE_TYPE_CODE = service.SERVICE_TYPE_CODE;
                            dv.SERVICE_TYPE_NAME = service.SERVICE_TYPE_NAME;
                        }

                        HIS_SERVICE_CONDITION serviceCondition = HisServiceConditionCFG.DATA.FirstOrDefault(o => o.ID == sereServ.SERVICE_CONDITION_ID);
                        if (IsNotNull(serviceCondition))
                        {
                            dv.SERVICE_CONDITION_CODE = serviceCondition.SERVICE_CONDITION_CODE;
                        }

                        HIS_PACKAGE package = HisPackageCFG.DATA.FirstOrDefault(o => o.ID == sereServ.PACKAGE_ID);
                        if (IsNotNull(package))
                        {
                            dv.PACKAGE_CODE = package.PACKAGE_CODE;
                        }

                        dv.PRICE = sereServ.VIR_PRICE ?? 0;
                        dv.ORIGINAL_PRICE = sereServ.ORIGINAL_PRICE;
                        dv.TOTAL_HEIN_PRICE = sereServ.VIR_TOTAL_HEIN_PRICE;
                        dv.TOTAL_OTHER_SOURCE_PRICE = (sereServ.OTHER_SOURCE_PRICE ?? 0) * (sereServ.AMOUNT);
                        dv.TOTAL_PATIENT_PRICE = sereServ.VIR_TOTAL_PATIENT_PRICE;
                        dv.TOTAL_PATIENT_PRICE_BHYT = sereServ.VIR_TOTAL_PATIENT_PRICE_BHYT;
                        dv.TOTAL_PRICE = sereServ.VIR_TOTAL_PRICE;

                        dv.DEPARTMENT_BHYT_CODE = sereServ.REQUEST_BHYT_CODE;
                        if (sereServ.PARENT_ID.HasValue)
                        {
                            //Thêm điều kiện "phòng chỉ định" phải cùng "phòng xử lý" của dịch vụ cha (KTC hoặc PTTT)
                            var parent = listHighTech.FirstOrDefault(o => o.ID == sereServ.PARENT_ID.Value);
                            if (parent != null)
                            {
                                if (parent.TDL_EXECUTE_DEPARTMENT_ID == sereServ.TDL_REQUEST_DEPARTMENT_ID)
                                {
                                    dv.DEPARTMENT_BHYT_CODE = parent.REQUEST_BHYT_CODE ?? "";
                                    dv.HEIN_PARENT_CODE = parent.TDL_HEIN_SERVICE_BHYT_CODE ?? "";
                                    dv.HEIN_PARENT_NAME = parent.TDL_HEIN_SERVICE_BHYT_NAME ?? "";
                                    if (!dicHighTech.ContainsKey(sereServ.PARENT_ID.Value))
                                    {
                                        dicHighTech[sereServ.PARENT_ID.Value] = "G" + (dicHighTech.Count + 1);
                                    }

                                    dv.GOI_VTYT = dicHighTech[sereServ.PARENT_ID.Value];
                                }
                            }
                        }


                        dv.CONCENTRA = sereServ.CONCENTRA;
                        dv.MEDICINE_USE_FORM_CODE = sereServ.MEDICINE_USE_FORM_CODE;
                        dv.TUTORIAL = sereServ.TUTORIAL;
                        dv.FINISH_TIME = sereServ.END_TIME ?? sereServ.FINISH_TIME;
                        dv.ICD_CODE = sereServ.ICD_CODE;
                        dv.ACTIVE_INGR_BHYT_CODE = sereServ.ACTIVE_INGR_BHYT_CODE;
                        dv.REGISTER_NUMBER = sereServ.MEDICINE_REGISTER_NUMBER;
                        dv.HEIN_LIMIT_PRICE = sereServ.HEIN_LIMIT_PRICE;
                        dv.BHYT_RATIO = sereServ.HEIN_RATIO;

                        if (sereServ.MEDICINE_ID.HasValue)
                        {
                            dv.BID_NUMBER = sereServ.MEDICINE_BID_NUMBER;
                            dv.BID_YEAR = sereServ.MEDICINE_BID_YEAR;
                            dv.BID_PACKAGE_CODE = sereServ.MEDICINE_BID_PACKAGE_CODE;
                            dv.BID_GROUP_CODE = sereServ.MEDICINE_BID_GROUP_CODE;
                        }
                        else if (sereServ.MATERIAL_ID.HasValue)
                        {
                            dv.BID_NUMBER = sereServ.MATERIAL_BID_NUMBER;
                            dv.BID_YEAR = sereServ.MATERIAL_BID_YEAR;
                            dv.BID_PACKAGE_CODE = sereServ.MATERIAL_BID_PACKAGE_CODE;
                            dv.BID_GROUP_CODE = sereServ.MATERIAL_BID_GROUP_CODE;
                        }

                        string maGiuong = "";
                        if (sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_NGT ||
                            sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_NT ||
                            sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_BN ||
                            sereServ.TDL_HEIN_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_HEIN_SERVICE_TYPE.ID__GI_L)
                        {
                            maGiuong = ".";
                            if (bedLogs != null && bedLogs.Count > 0)
                            {
                                var bedlog = bedLogs.Where(o => o.SERVICE_REQ_ID.HasValue && o.SERVICE_REQ_ID.Value == sereServ.SERVICE_REQ_ID && o.BED_SERVICE_TYPE_ID == sereServ.SERVICE_ID && o.SHARE_COUNT == sereServ.SHARE_COUNT).ToList();

                                //nếu không có service_req_id thì tìm theo thời gian y lệnh
                                if (bedlog == null || bedlog.Count <= 0)
                                {
                                    bedlog = bedLogs.Where(o => o.ID == sereServ.BED_LOG_ID).ToList();
                                }

                                //nếu không có service_req_id thì tìm theo thời gian y lệnh
                                if (bedlog == null || bedlog.Count <= 0)
                                {
                                    bedlog = bedLogs.Where(o => o.BED_SERVICE_TYPE_ID == sereServ.SERVICE_ID && o.SHARE_COUNT == sereServ.SHARE_COUNT && o.START_TIME <= sereServ.TDL_INTRUCTION_TIME && (o.FINISH_TIME.HasValue && o.FINISH_TIME.Value >= sereServ.TDL_INTRUCTION_TIME)).ToList();
                                }

                                if (bedlog != null && bedlog.Count > 0)
                                {
                                    List<string> bedCodes = bedlog.Select(s => s.BED_CODE).Distinct().ToList();
                                    maGiuong = String.Join(";", bedCodes);
                                }
                            }
                        }

                        dv.BED_CODE = maGiuong;

                        if (CheckBhytNsd(HisIcdCFG.ListIcdCode_Nds, HisIcdCFG.ListIcdCode_Nds_Te, sereServ.ICD_CODE, sereServ.HEIN_CARD_NUMBER, service, icdNds))
                        {
                            dv.TOTAL_OUT_DS = sereServ.VIR_TOTAL_HEIN_PRICE;
                        }
                        else
                        {
                            dv.TOTAL_OUT_DS = 0;
                        }

                        dicDepartmentDetail[departmentId].Add(dv);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Công khám thuộc khoa nào xử lý thì được tính cho khoa đó
        /// Thuốc/vật tư do phòng khám (phòng xử lý công khám) kê sẽ được tính cho khoa của phòng khám đó
        /// Với các thuốc/vật tư phát sinh khi thực hiện CLS sẽ được tính cho khoa chỉ định dịch vụ CLS đó.
        /// Các dịch vụ còn lại do khoa nào chỉ định thì được tính cho khoa đó.
        /// </summary>
        /// <param name="sereServ"></param>
        /// <param name="listServiceReq"></param>
        /// <param name="departmentId"></param>
        private void ProcessGetDepartment(V_HIS_SERE_SERV_2 sereServ, List<HIS_SERVICE_REQ> listServiceReq, ref long departmentId)
        {
            try
            {
                departmentId = sereServ.TDL_REQUEST_DEPARTMENT_ID;

                if (sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH)
                {
                    departmentId = sereServ.TDL_EXECUTE_DEPARTMENT_ID;
                }
                else if (sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC || sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT || sereServ.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU)
                {
                    HIS_SERVICE_REQ serviceReq = listServiceReq.FirstOrDefault(o => o.ID == sereServ.SERVICE_REQ_ID);
                    if (IsNotNull(serviceReq) && serviceReq.PARENT_ID.HasValue)
                    {
                        HIS_SERVICE_REQ parent = listServiceReq.FirstOrDefault(o => o.ID == serviceReq.PARENT_ID.Value);
                        if (IsNotNull(parent) && parent.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                        {
                            departmentId = parent.REQUEST_DEPARTMENT_ID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private bool ProcessLogin(string uri, string loginame, string password)
        {
            if (ApiConsumerStore.hxtConsumerWrapper == null || !ApiConsumerStore.hxtConsumerWrapper.LoginSuccess)
            {
                ApiConsumerStore.hxtConsumerWrapper = new ApiConsumerWrapper(true, MOS.UTILITY.Constant.APPLICATION_CODE, uri,
                              ConfigurationManager.AppSettings["Inventec.AcsConsumer.Base.Uri"],
                              loginame, password);
                ApiConsumerStore.hxtConsumerWrapper.UseRegistry(false);
            }

            return ApiConsumerStore.hxtConsumerWrapper.LoginSuccess;
        }

        private bool ProcessConfigData(string config, ref string uri, ref string loginame, ref string password)
        {
            string[] split = config.Split('|');
            uri = split[0].Trim();
            loginame = split[1].Trim();
            password = split[2].Trim();
            return true;
        }

        private bool CheckBhytNsd(List<string> listIcdCode, List<string> listIcdCodeTe, string icdCode, string heinCardNumber, V_HIS_SERVICE service, List<HIS_ICD> icdNds)
        {
            var result = false;
            try
            {
                if (!String.IsNullOrWhiteSpace(heinCardNumber) &&
                    (heinCardNumber.Substring(0, 2).Equals("CA") ||
                    heinCardNumber.Substring(0, 2).Equals("CY") ||
                    heinCardNumber.Substring(0, 2).Equals("QN")))
                {
                    return true;
                }

                if ((listIcdCode == null || listIcdCode.Count == 0) && (listIcdCodeTe == null || listIcdCodeTe.Count == 0) && (icdNds == null || icdNds.Count == 0))
                {
                    return result;
                }

                if (service != null && service.IS_OUT_OF_DRG == 1 && !string.IsNullOrEmpty(icdCode))
                {
                    if (listIcdCode == null || listIcdCode.Count == 0)
                    {
                        listIcdCode = new List<string>();
                    }

                    if (listIcdCodeTe == null || listIcdCodeTe.Count == 0)
                    {
                        listIcdCodeTe = new List<string>();
                    }

                    if (icdNds == null || icdNds.Count == 0)
                    {
                        icdNds = new List<HIS_ICD>();
                    }

                    if ((listIcdCode.Contains(icdCode) || icdNds.Exists(o => o.ICD_CODE == icdCode)))
                        result = true;
                    else if (!String.IsNullOrWhiteSpace(heinCardNumber) && heinCardNumber.Substring(0, 2).Equals("TE") && (listIcdCodeTe.Contains(icdCode.Substring(0, 3)) || icdNds.Exists(o => o.ICD_CODE == icdCode)))
                        result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        class TreatmentResultBhytCFG
        {
            public const string Khoi = "1"; //khỏi
            public const string Do = "2"; //đỡ
            public const string KhongThayDoi = "3"; //không thay đổi
            public const string NangHon = "4"; //nặng hơn
            public const string TuVong = "5"; //tử vong
        }
    }
}
