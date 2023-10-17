using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using LIS.SDO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTestIndex;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisSenderV1
{
    class LisUtil : BusinessBase
    {
        public const long LIS_STT_ID__SUCCESS = (long)1;
        public const long LIS_STT_ID__UPDATE = (long)2;
        public const long LIS_STT_ID__UPDATE_FAILED = (long)3;

        internal LisUtil()
            : base()
        {

        }

        internal LisUtil(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        public bool SendOrderToLis(long serviceReqId, List<HIS_SERE_SERV> hisSereServs, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                {
                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(serviceReqId);
                    if (serviceReq == null || serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong co thong tin HIS_SERVICE_REQ thuoc loai XN tuong ung voi service_req_id:" + serviceReqId);
                    }

                    if (!IsNotNullOrEmpty(hisSereServs))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.LoiDuLieu);
                        throw new Exception("Khong co thong tin HIS_SERE_SERV tuong ung voi service_req_id:" + serviceReqId);
                    }

                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                    HIS_KSK_CONTRACT contract = null;
                    if (treatment.TDL_KSK_CONTRACT_ID.HasValue)
                    {
                        contract = new HisKskContractGet().GetById(treatment.TDL_KSK_CONTRACT_ID.Value);
                    }

                    result = (new LisUtil(param)).SendOrderToLis(serviceReq, hisSereServs, contract, ref sqls);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Gui y/c xet nghiem sang he thong LIS cua Lis that bai. ", ex);
            }
            return result;
        }

        public bool SendOrderToLis(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> hisSereServs, HIS_KSK_CONTRACT contract, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                {
                    V_HIS_EXECUTE_ROOM hisExecuteRoom = HisExecuteRoomCFG.DATA
                        .Where(o => o.ROOM_ID == serviceReq.EXECUTE_ROOM_ID)
                        .FirstOrDefault();

                    V_HIS_ROOM hisRequestRoom = HisRoomCFG.DATA
                        .Where(o => o.ID == serviceReq.REQUEST_ROOM_ID).FirstOrDefault();

                    string lisAddress = null;

                    if (IsNotNullOrEmpty(LisCFG.LIS_ADDRESSES))
                    {
                        lisAddress = LisCFG.LIS_ADDRESSES
                            .Where(o => o.RoomCode == hisExecuteRoom.EXECUTE_ROOM_CODE)
                            .Select(o => o.Url).FirstOrDefault();
                    }

                    if (string.IsNullOrWhiteSpace(lisAddress))
                    {
                        throw new Exception("Chua cau hinh dia chi url he thong lis tuong ung voi phong XN: " + hisExecuteRoom.EXECUTE_ROOM_NAME + " - " + hisExecuteRoom.EXECUTE_ROOM_CODE);
                    }

                    V_HIS_SAMPLE_ROOM hisSampleRoom = HisSampleRoomCFG.DATA
                        .Where(o => o.ID == serviceReq.SAMPLE_ROOM_ID)
                        .FirstOrDefault();

                    HIS_TREATMENT hisTreatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                    HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == serviceReq.TDL_PATIENT_TYPE_ID);

                    List<ServiceReqSDO> sdos = new List<ServiceReqSDO>();
                    List<ServiceSDO> services = new List<ServiceSDO>();
                    string parentServiceCode = "";
                    foreach (HIS_SERE_SERV s in hisSereServs)
                    {
                        ServiceSDO service = new ServiceSDO();
                        V_HIS_SERVICE hs = HisServiceCFG.DATA_VIEW.Where(o => o.ID == s.SERVICE_ID).FirstOrDefault();
                        if (hs != null)
                        {
                            service.ServiceCode = hs.SERVICE_CODE;
                            service.ServiceName = hs.SERVICE_NAME;
                            if (hs.PARENT_ID.HasValue && String.IsNullOrWhiteSpace(parentServiceCode))
                            {
                                V_HIS_SERVICE pa = HisServiceCFG.DATA_VIEW.Where(o => o.ID == hs.PARENT_ID.Value).FirstOrDefault();
                                if (pa != null)
                                {
                                    parentServiceCode = pa.SERVICE_CODE;
                                }
                            }
                        }
                        service.ServiceNumOrder = s.ID;
                        service.TestIndexs = HisTestIndexCFG.DATA_VIEW
                        .Where(o => s.SERVICE_ID == o.TEST_SERVICE_TYPE_ID && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        .Select(o => new TestIndexSDO
                        {
                            TestIndexCode = o.TEST_INDEX_CODE,
                            TestIndexName = o.TEST_INDEX_NAME,
                            TestIndexUnit = o.TEST_INDEX_UNIT_SYMBOL
                        }).ToList();
                        services.Add(service);
                    }

                    ServiceReqSDO sdo = new ServiceReqSDO();
                    sdo.FirstName = serviceReq.TDL_PATIENT_FIRST_NAME;
                    sdo.LastName = serviceReq.TDL_PATIENT_LAST_NAME;
                    sdo.PatientCode = serviceReq.TDL_PATIENT_CODE;
                    sdo.NumOrder = serviceReq.NUM_ORDER;
                    sdo.InstructionTime = serviceReq.INTRUCTION_TIME;
                    sdo.Dob = serviceReq.TDL_PATIENT_DOB;
                    sdo.GenderCode = this.GetGenderCode(serviceReq.TDL_PATIENT_GENDER_ID);
                    if (hisSampleRoom != null)
                    {
                        sdo.SampleRoomCode = hisSampleRoom.SAMPLE_ROOM_CODE;
                        sdo.SampleRoomName = hisSampleRoom.SAMPLE_ROOM_NAME;
                    }
                    if (hisExecuteRoom != null)
                    {
                        sdo.ExecuteRoomCode = hisExecuteRoom.EXECUTE_ROOM_CODE;
                        sdo.ExecuteRoomName = hisExecuteRoom.EXECUTE_ROOM_NAME;
                        sdo.ExecuteMediOrgCode = hisExecuteRoom.HEIN_MEDI_ORG_CODE;
                    }
                    if (hisRequestRoom != null)
                    {
                        sdo.RequestRoomCode = hisRequestRoom.ROOM_CODE;
                        sdo.RequestRoomName = hisRequestRoom.ROOM_NAME;
                        sdo.RequestDepartmentCode = hisRequestRoom.DEPARTMENT_CODE;
                        sdo.RequestDepartmentName = hisRequestRoom.DEPARTMENT_NAME;
                    }
                    sdo.RequestLoginname = serviceReq.REQUEST_LOGINNAME;
                    sdo.RequestUsername = serviceReq.REQUEST_USERNAME;
                    sdo.ServiceReqCode = serviceReq.SERVICE_REQ_CODE;
                    sdo.TreatmentCode = serviceReq.TDL_TREATMENT_CODE;
                    sdo.CallSampleOrder = serviceReq.CALL_SAMPLE_ORDER;
                    if (serviceReq.IS_SEND_BARCODE_TO_LIS == Constant.IS_TRUE)
                    {
                        sdo.Barcode = serviceReq.BARCODE;
                    }
                    sdo.Services = services;
                    sdo.IsAntibioticResistance = serviceReq.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE;
                    if (contract != null && hisSereServs.Any(a => a.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__KSK))
                    {
                        sdo.KskContractCode = contract.KSK_CONTRACT_CODE;
                    }
                    sdo.ParentServiceCode = parentServiceCode;
                    sdo.HeinCardFromTime = IsNotNull(hisTreatment) ? hisTreatment.TDL_HEIN_CARD_FROM_TIME : null;
                    sdo.HeinCardToTime = IsNotNull(hisTreatment) ? hisTreatment.TDL_HEIN_CARD_TO_TIME : null;
                    sdo.PatientTypeName = IsNotNull(hisPatientType) ? hisPatientType.PATIENT_TYPE_NAME : "";
                    sdo.HeinMediOrgCode = serviceReq.TDL_HEIN_MEDI_ORG_CODE;
                    sdo.HeinMediOrgName = serviceReq.TDL_HEIN_MEDI_ORG_NAME;
                    sdo.HasNoDobDate = serviceReq.TDL_PATIENT_IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE;
                    sdo.CommuneCode = serviceReq.TDL_PATIENT_COMMUNE_CODE;
                    sdo.CommuneName = serviceReq.TDL_PATIENT_COMMUNE_NAME;
                    sdo.DistrictCode = serviceReq.TDL_PATIENT_DISTRICT_CODE;
                    sdo.DistrictName = serviceReq.TDL_PATIENT_DISTRICT_NAME;
                    sdo.ProvinceCode = serviceReq.TDL_PATIENT_PROVINCE_CODE;
                    sdo.ProvinceName = serviceReq.TDL_PATIENT_PROVINCE_NAME;
                    sdo.NationalCode = serviceReq.TDL_PATIENT_NATIONAL_CODE;
                    sdo.NationalName = serviceReq.TDL_PATIENT_NATIONAL_NAME;
                    sdo.Address = serviceReq.TDL_PATIENT_ADDRESS;
                    sdo.PhoneNumber = serviceReq.TDL_PATIENT_PHONE;
                    sdo.CareerName = serviceReq.TDL_PATIENT_CAREER_NAME;
                    sdo.WorkPlace = serviceReq.TDL_PATIENT_WORK_PLACE_NAME;
                    sdo.HeinCardNumber = serviceReq.TDL_HEIN_CARD_NUMBER;
                    sdo.CccdNumber = serviceReq.TDL_PATIENT_CCCD_NUMBER;
                    sdo.CccdPlace = serviceReq.TDL_PATIENT_CCCD_PLACE;
                    sdo.CccdDate = serviceReq.TDL_PATIENT_CCCD_DATE;
                    sdo.CmndNumber = serviceReq.TDL_PATIENT_CMND_NUMBER;
                    sdo.CmndPlace = serviceReq.TDL_PATIENT_CMND_PLACE;
                    sdo.CmndDate = serviceReq.TDL_PATIENT_CMND_DATE;
                    if (serviceReq.IS_EMERGENCY == Constant.IS_TRUE)
                    {
                        sdo.IsEmergency = true;
                    }
                    else
                    {
                        sdo.IsEmergency = false;
                    }
                    sdo.TreatmentTypeId = IsNotNull(hisTreatment) ? hisTreatment.TDL_TREATMENT_TYPE_ID : null;

                    sdos.Add(sdo);
                    string tokenCode = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenCode();
                    ApiConsumer serviceConsumer = new ApiConsumer(lisAddress, tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);

                    var ro = serviceConsumer.Post<Inventec.Core.ApiResultObject<bool>>("/api/LisSample/CreateByServiceReqSDO", null, sdos);
                    string sqlUpdate = "";

                    if (ro != null && ro.Param != null)
                    {
                        if (ro.Param.BugCodes != null)
                        {
                            param.BugCodes.AddRange(ro.Param.BugCodes);
                        }
                        if (ro.Param.Messages != null)
                        {
                            param.Messages.AddRange(ro.Param.Messages);
                        }
                    }
                   
                    if (!ro.Success)
                    {
                        if (ro.Param != null && ro.Param.BugCodes != null && ro.Param.BugCodes.Contains("LIS015"))
                        {
                            sqlUpdate = String.Format("UPDATE HIS_SERVICE_REQ SET LIS_STT_ID = {0} WHERE ID = {1}", LIS_STT_ID__UPDATE_FAILED, serviceReq.ID);
                            LogSystem.Warn("Cap nhat y/c xet nghiem sang he thong LIS that bai: " + LogUtil.TraceData("ro", ro));
                        }
                        else
                        {
                            throw new Exception("Gui y/c xet nghiem sang he thong LIS cua Lis that bai. Ket qua: " + LogUtil.TraceData("ro", ro));
                        }
                    }
                    else
                    {
                        if (serviceReq.LIS_STT_ID.HasValue && serviceReq.LIS_STT_ID.Value == LIS_STT_ID__UPDATE)
                        {
                            sqlUpdate = String.Format("UPDATE HIS_SERVICE_REQ SET IS_SENT_EXT = 1, LIS_STT_ID = {0} WHERE ID = {1}", LIS_STT_ID__SUCCESS, serviceReq.ID);
                        }
                        else
                        {
                            sqlUpdate = String.Format("UPDATE HIS_SERVICE_REQ SET IS_SENT_EXT = 1, LIS_STT_ID = {0} WHERE ID = {1} AND (LIS_STT_ID IS NULL OR LIS_STT_ID <> {2})", LIS_STT_ID__SUCCESS, serviceReq.ID, LIS_STT_ID__UPDATE);
                        }
                    }
                    if (!String.IsNullOrEmpty(sqlUpdate))
                    {
                        sqls.Add(sqlUpdate);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        public bool SendDeleteOrderToLis(long serviceReqId)
        {
            bool result = false;
            try
            {
                if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                {
                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(serviceReqId);

                    if (serviceReq == null || serviceReq.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong co thong tin HIS_SERVICE_REQ thuoc loai XN tuong ung voi service_req_id:" + serviceReqId);
                    }

                    V_HIS_EXECUTE_ROOM hisExecuteRoom = HisExecuteRoomCFG.DATA
                        .Where(o => o.ROOM_ID == serviceReq.EXECUTE_ROOM_ID)
                        .FirstOrDefault();

                    string lisAddress = null;

                    if (IsNotNullOrEmpty(LisCFG.LIS_ADDRESSES))
                    {
                        lisAddress = LisCFG.LIS_ADDRESSES
                            .Where(o => o.RoomCode == hisExecuteRoom.EXECUTE_ROOM_CODE)
                            .Select(o => o.Url).FirstOrDefault();
                    }

                    if (string.IsNullOrWhiteSpace(lisAddress))
                    {
                        throw new Exception("Chua cau hinh dia chi url he thong lis tuong ung voi phong XN: " + hisExecuteRoom.EXECUTE_ROOM_NAME + " - " + hisExecuteRoom.EXECUTE_ROOM_CODE);
                    }

                    string tokenCode = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenCode();
                    ApiConsumer serviceConsumer = new ApiConsumer(lisAddress, tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);

                    var ro = serviceConsumer.Post<Inventec.Core.ApiResultObject<bool>>("/api/LisSample/DeleteByServiceReqCode", null, serviceReq.SERVICE_REQ_CODE);
                    if (!ro.Success)
                    {
                        LogSystem.Error(string.Format("Gui huy y/c xet nghiem (ma: {0}) sang he thong LIS cua Lis that bai. Ket qua: {1} ", serviceReq.SERVICE_REQ_CODE, LogUtil.TraceData("ro", ro)));
                        if (ro.Param.BugCodes.Contains("LIS001"))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_YeuCauXetNghiemDaDuocLayMauHoacDaCoKetQua);
                        }
                    }
                    result = ro.Success;
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        public bool SendDeleteOrderToLis(HIS_SERVICE_REQ serviceReq, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (LisCFG.LIS_INTEGRATE_OPTION == (int)LisCFG.LisIntegrateOption.LIS)
                {
                    V_HIS_EXECUTE_ROOM hisExecuteRoom = HisExecuteRoomCFG.DATA
                        .Where(o => o.ROOM_ID == serviceReq.EXECUTE_ROOM_ID)
                        .FirstOrDefault();

                    string lisAddress = null;

                    if (IsNotNullOrEmpty(LisCFG.LIS_ADDRESSES))
                    {
                        lisAddress = LisCFG.LIS_ADDRESSES
                            .Where(o => o.RoomCode == hisExecuteRoom.EXECUTE_ROOM_CODE)
                            .Select(o => o.Url).FirstOrDefault();
                    }

                    if (string.IsNullOrWhiteSpace(lisAddress))
                    {
                        throw new Exception("Chua cau hinh dia chi url he thong lis tuong ung voi phong XN: " + hisExecuteRoom.EXECUTE_ROOM_NAME + " - " + hisExecuteRoom.EXECUTE_ROOM_CODE);
                    }

                    string tokenCode = Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenCode();
                    ApiConsumer serviceConsumer = new ApiConsumer(lisAddress, tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);

                    var ro = serviceConsumer.Post<Inventec.Core.ApiResultObject<bool>>("/api/LisSample/DeleteByServiceReqCode", null, serviceReq.SERVICE_REQ_CODE);
                    string updateSql = "";
                    if (!ro.Success)
                    {
                        updateSql = String.Format("UPDATE HIS_SERVICE_REQ SET LIS_STT_ID = {0} WHERE ID = {1}", LisUtil.LIS_STT_ID__UPDATE_FAILED, serviceReq.ID);
                        LogSystem.Error(string.Format("Gui huy y/c xet nghiem (ma: {0}) sang he thong LIS cua Lis that bai. Ket qua: {1} ", serviceReq.SERVICE_REQ_CODE, LogUtil.TraceData("ro", ro)));
                    }
                    else
                    {
                        updateSql = String.Format("UPDATE HIS_SERVICE_REQ SET IS_SENT_EXT = 1, LIS_STT_ID = {0} WHERE ID = {1}", LisUtil.LIS_STT_ID__SUCCESS, serviceReq.ID);
                    }
                    result = true;
                    if (!String.IsNullOrWhiteSpace(updateSql))
                    {
                        sqls.Add(updateSql);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        private string GetGenderCode(long? genderId)
        {
            if (genderId.HasValue && genderId.Value > 0)
            {
                return HisGenderCFG.DATA.FirstOrDefault(o => o.ID == genderId.Value).GENDER_CODE;
            }
            return null;
        }
    }
}
