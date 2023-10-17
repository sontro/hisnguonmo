using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using His.Bhyt.ExportXml;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisHeinApproval;
using AutoMapper;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using His.Bhyt.ExportXml.Base;
using MOS.MANAGER.HisEmployee;
using Inventec.Fss.Utility;
using Inventec.Fss.Client;
using MOS.UTILITY;
using System.IO;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisTracking;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisFinancePeriod;
using MOS.MANAGER.HisDebate;

namespace MOS.MANAGER.HisTreatment
{
    //Xu ly nghiep vu giam dinh BHYT toan bo ho so dieu tri
    class HisTreatmentHeinApproval : BusinessBase
    {
        private static bool IS_SET_XML_CONFIG = false;

        internal HisTreatmentHeinApproval()
            : base()
        {

        }

        internal HisTreatmentHeinApproval(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool Run(HisTreatmentHeinApprovalSDO data)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;

                if (this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace))
                {
                    if (!workPlace.CashierRoomId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                        return false;
                    }

                }
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                result = this.Run(data.TreatmentId, data.ExecuteTime, workPlace.CashierRoomId.Value, loginName, userName);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool Run(long treatmentId, long executeTime, long cashierRoomId, string executeLoginName, string executeUserName)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT hisTreatment = null;
                HisHeinApprovalCheck heinApprovalChecker = new HisHeinApprovalCheck(param);
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck(param);
                HIS_BRANCH branch = null;
                valid = valid && this.HasBranchInfo(cashierRoomId, ref branch);
                valid = valid && this.HasNotExistedHeinApproval(treatmentId);
                valid = valid && checker.VerifyId(treatmentId, ref hisTreatment);
                valid = valid && checker.IsLock(hisTreatment);
                valid = valid && checker.IsUnLockHein(hisTreatment);
                valid = valid && heinApprovalChecker.IsValidExecuteTime(hisTreatment.FEE_LOCK_TIME.Value, executeTime);
                valid = valid && financePeriodChecker.HasNotFinancePeriod(branch.ID, executeTime);

                if (valid)
                {
                    List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlterGet().GetDistinct(treatmentId);

                    if (IsNotNullOrEmpty(patientTypeAlters))
                    {
                        List<long> heinApprovalIds = new List<long>();
                        patientTypeAlters = patientTypeAlters.OrderBy(o => o.LOG_TIME).ThenBy(o => o.ID).ToList();
                        foreach (HIS_PATIENT_TYPE_ALTER t in patientTypeAlters)
                        {
                            try
                            {
                                if (t.PATIENT_TYPE_ID != Config.HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                                {
                                    continue;
                                }
                                HIS_HEIN_APPROVAL bhyt = new HIS_HEIN_APPROVAL();
                                bhyt.ADDRESS = t.ADDRESS;
                                bhyt.CASHIER_ROOM_ID = cashierRoomId;
                                bhyt.HAS_BIRTH_CERTIFICATE = t.HAS_BIRTH_CERTIFICATE;
                                bhyt.HEIN_CARD_FROM_TIME = t.HEIN_CARD_FROM_TIME.Value;
                                bhyt.HEIN_CARD_NUMBER = t.HEIN_CARD_NUMBER;
                                bhyt.HEIN_CARD_TO_TIME = t.HEIN_CARD_TO_TIME.Value;
                                bhyt.HEIN_MEDI_ORG_CODE = t.HEIN_MEDI_ORG_CODE;
                                bhyt.HEIN_MEDI_ORG_NAME = t.HEIN_MEDI_ORG_NAME;
                                bhyt.JOIN_5_YEAR = t.JOIN_5_YEAR;
                                bhyt.LEVEL_CODE = t.LEVEL_CODE;
                                bhyt.LIVE_AREA_CODE = t.LIVE_AREA_CODE;
                                bhyt.PAID_6_MONTH = t.PAID_6_MONTH;
                                bhyt.RIGHT_ROUTE_CODE = t.RIGHT_ROUTE_CODE;
                                bhyt.RIGHT_ROUTE_TYPE_CODE = t.RIGHT_ROUTE_TYPE_CODE;
                                bhyt.FREE_CO_PAID_TIME = t.FREE_CO_PAID_TIME;
                                bhyt.TREATMENT_ID = treatmentId;
                                bhyt.EXECUTE_TIME = executeTime;
                                bhyt.EXECUTE_LOGINNAME = executeLoginName;
                                bhyt.EXECUTE_USERNAME = executeUserName;

                                //Da validate o tren, nen ko can validate nua
                                if (!new HisHeinApprovalCreate(param).Create(bhyt, hisTreatment))
                                {
                                    LogSystem.Warn("Tu dong tao ho so BHYT that bai");
                                    result = false;
                                }
                                else
                                {
                                    heinApprovalIds.Add(bhyt.ID);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogSystem.Error(ex);
                            }
                        }

                        if (IsNotNullOrEmpty(heinApprovalIds))
                        {
                            result = true;
                            this.ProcessAutoExportXml(treatmentId, branch, heinApprovalIds);
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

        private bool HasNotExistedHeinApproval(long treatmentId)
        {
            try
            {
                List<HIS_HEIN_APPROVAL> heinApprovals = new HisHeinApprovalGet().GetByTreatmentId(treatmentId);
                if (IsNotNullOrEmpty(heinApprovals))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_DaCoThongTinGiamDinhBhyt);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void ProcessAutoExportXml(long treatmentId, HIS_BRANCH branch, List<long> heinApprovalIds)
        {
            try
            {
                if (IsNotNullOrEmpty(heinApprovalIds) 
                    && ((HisHeinApprovalCFG.IS_AUTO_EXPORT_XML && !string.IsNullOrWhiteSpace(HisHeinApprovalCFG.XML4210_FOLDER_PATH))
                    || !string.IsNullOrWhiteSpace(EhrCFG.XML4210_FOLDER_PATH))
                    )
                {
                    string folderPath = string.Format("{0}\\{1}", HisHeinApprovalCFG.XML4210_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);

                    
                    List<V_HIS_HEIN_APPROVAL> heinApprovals = new HisHeinApprovalGet().GetByIds(heinApprovalIds);

                    heinApprovals = heinApprovals != null ? heinApprovals.Where(o => HisHeinApprovalCFG.HEIN_CARD_NUMBER_PREFIX_RESTRICTS == null || !HisHeinApprovalCFG.HEIN_CARD_NUMBER_PREFIX_RESTRICTS.Any(a => !string.IsNullOrWhiteSpace(a) && o.HEIN_CARD_NUMBER.StartsWith(a))).ToList() : null;

                    if (!IsNotNullOrEmpty(heinApprovals))
                    {
                        LogSystem.Warn("Ko ton tai du lieu duyet BHYT nao cho phep xuat tu dong");
                        return;
                    }
                    heinApprovalIds = heinApprovals.Select(s => s.ID).ToList();
                    InputADO ado = new InputADO();
                    ado.HeinApprovals = heinApprovals;
                    ado.ListSereServ = new HisSereServGet().GetView2ByHeinApprovalIds(heinApprovalIds);
                    ado.Treatment = new HisTreatmentGet().GetView3ById(treatmentId);

                    List<HIS_DHST> dhsts = new HisDhstGet().GetByTreatmentId(treatmentId);
                    ado.Dhst = IsNotNullOrEmpty(dhsts) ? dhsts.OrderByDescending(o => o.ID).FirstOrDefault(o => o.WEIGHT.HasValue) ?? dhsts.OrderByDescending(o => o.ID).FirstOrDefault() : null;
                    ado.Branch = branch;
                    ado.SereServTeins = new HisSereServTeinGet().GetViewByTreatmentId(treatmentId);
                    ado.Trackings = new HisTrackingGet().GetByTreatmentId(treatmentId);
                    ado.SereServPttts = new HisSereServPtttGet().GetViewByTreatmentId(treatmentId);
                    ado.BedLogs = new HisBedLogGet().GetViewByTreatmentId(treatmentId);
                    ado.ListDebate = new HisDebateGet().GetByTreatmentId(treatmentId);
                    ado.ListDhsts = dhsts;
                    List<long> ekipIds = ado.ListSereServ != null ? ado.ListSereServ.Where(o => o.EKIP_ID.HasValue).Select(s => s.EKIP_ID.Value).ToList() : null;
                    if (IsNotNullOrEmpty(ekipIds))
                    {
                        ado.EkipUsers = new HisEkipUserGet().GetByEkipIds(ekipIds);
                    }

                    bool noConstraintRoomWithMaterialPackage = HisHeinBhytCFG.CALC_MATERIAL_PACKAGE_PRICE_OPTION == HisHeinBhytCFG.CalcMaterialPackagePriceOption.NO_CONSTRAINT_ROOM;

                    ado.MaterialPackageOption = noConstraintRoomWithMaterialPackage ? "1" : null;
                    ado.MaterialPriceOriginalOption = HisHeinBhytCFG.XML__4210__MATERIAL_PRICE_OPTION;
                    ado.MaterialStentRatio = HisHeinBhytCFG.XML__4210__MATERIAL_STENT_RATIO_OPTION;
                    ado.TenBenhOption = HisHeinBhytCFG.XML_EXPORT__TEN_BENH_OPTION;
                    ado.MaterialTypes = HisMaterialTypeCFG.DATA;
                    ado.HeinServiceTypeCodeNoTutorial = HisHeinBhytCFG.XML_EXPORT__HEIN_CODE_NO_TUTORIAL;
                    ado.XMLNumbers = HisHeinBhytCFG.XML_EXPORT__NUMBER;
                    ado.MaterialStent2Limit = HisHeinBhytCFG.XML_EXPORT__MATERIAL_STENT2_LIMIT_OPTION;
                    ado.IsTreatmentDayCount6556 = HisHeinBhytCFG.IS_TREATMENT_DAY_COUNT_6556;
                    ado.ListHeinMediOrg = HisMediOrgCFG.DATA;
                    ado.MaBacSiOption = HisHeinBhytCFG.MA_BAC_SI_EXAM_OPTION;
                    ado.ConfigData = Loader.CONFIGs;
                    ado.TotalIcdData = HisIcdCFG.DATA;
                    ado.TotalSericeData = HisServiceCFG.DATA_VIEW;
                    this.SetXmlCreatorConfig();
                    CreateXmlMain xmlCreator = new CreateXmlMain(ado);
                    string messageError = "";
                    MemoryStream memoryStream = xmlCreator.Run4210Plus(ref messageError);
                    memoryStream.Position = 0;
                    MemoryStream ehrMemoryStream = new MemoryStream();
                    memoryStream.CopyTo(ehrMemoryStream);
                    if (HisHeinApprovalCFG.IS_AUTO_EXPORT_XML && !string.IsNullOrWhiteSpace(HisHeinApprovalCFG.XML4210_FOLDER_PATH))
                    {
                        //Neu cau hinh khong xuat XML voi cac ho so ko co cong kham va ho so ko co cong kham thi ket thuc
                        if (!HisHeinApprovalCFG.NOT_AUTO_EXPORT_XML_NO_EXAM
                            || ado.ListSereServ.Exists(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH && o.IS_NO_EXECUTE == null && o.IS_EXPEND == null))
                        {
                            this.ProcessExport4210Bhyt(treatmentId, branch, memoryStream, heinApprovals[0].TREATMENT_CODE, heinApprovals[0].TDL_PATIENT_CODE, ref messageError);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(EhrCFG.XML4210_FOLDER_PATH))
                    {
                        this.ProcessExport4210Ehr(branch, ehrMemoryStream, heinApprovals[0].TREATMENT_CODE, heinApprovals[0].TDL_PATIENT_CODE, ref messageError);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void ProcessExport4210Bhyt(long treatmentId, HIS_BRANCH branch, MemoryStream memoryStream, string treatmentCode, string patientCode, ref string messageError)
        {
            string sql = "UPDATE HIS_TREATMENT SET XML4210_URL = '{0}', XML4210_RESULT = {1}, XML4210_DESC= '{2}' WHERE ID = {3}";
            string query = "";
            if (memoryStream == null)
            {
                LogSystem.Error("Tu dong xuat XML4210 that bai");
                query = String.Format(sql, "", IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_FAIL, messageError, treatmentId);
            }
            else
            {
                var fileName = string.Format("{0}___{1}___{2}.xml", Inventec.Common.DateTime.Get.Now().Value, treatmentCode, patientCode);

                FileUploadInfo fileUploadInfo = null;
                try
                {
                    string folderPath = string.Format("{0}\\{1}", HisHeinApprovalCFG.XML4210_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);
                    fileUploadInfo = FileUpload.UploadFile(Constant.APPLICATION_CODE, folderPath, memoryStream, fileName, true);
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }

                if (fileUploadInfo == null)
                {
                    LogSystem.Error("Tai file XML4210 len he thong FSS that bai");
                    query = String.Format(sql, fileUploadInfo.Url, IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_FAIL, MOS.MANAGER.Base.MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_UploadXml4210ThatBai, param.LanguageCode), treatmentId);
                }
                else
                {
                    //khong dung HisTreatmentUpdate(paramUpdate).Update(toUpdate, beforeUpdate) do Treatment co is_active = 0
                    query = String.Format(sql, fileUploadInfo.Url, IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_SUCCESS, "", treatmentId);
                    LogSystem.Info("Xuat xml4210 ho so dieu tri: " + treatmentCode + " thanh cong");
                }
            }

            if (!DAOWorker.SqlDAO.Execute(query))
            {
                LogSystem.Error("Cap nhat XML4210 URL cho HIS_TREATMENT that bai");
            }
        }

        private void ProcessExport4210Ehr(HIS_BRANCH branch, MemoryStream memoryStream, string treatmentCode, string patientCode, ref string messageError)
        {
            if (memoryStream == null)
            {
                LogSystem.Error("Tu dong xuat XML4210 EHR that bai");
            }
            else
            {
                var fileName = string.Format("{0}___{1}___{2}.xml", Inventec.Common.DateTime.Get.Now().Value, treatmentCode, patientCode);

                FileUploadInfo fileUploadInfo = null;
                try
                {
                    string folderPath = string.Format("{0}\\{1}", EhrCFG.XML4210_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);
                    fileUploadInfo = FileUpload.UploadFile(Constant.APPLICATION_CODE, folderPath, memoryStream, fileName, true);
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }

                if (fileUploadInfo == null)
                {
                    LogSystem.Error("Tai file XML4210 EHR len he thong FSS that bai");
                }
                else
                {
                    LogSystem.Info("Xuat xml4210 EHR ho so dieu tri: " + treatmentCode + " thanh cong");
                }
            }
        }

        private void SetXmlCreatorConfig()
        {
            try
            {
                if (!IS_SET_XML_CONFIG)
                {
                    GlobalConfigStore.ListIcdCode_Nds = HisHeinBhytCFG.BHYT_NDS_ICD_CODE__OTHER;
                    GlobalConfigStore.ListIcdCode_Nds_Te = HisHeinBhytCFG.BHYT_NDS_ICD_CODE__TE;

                    GlobalConfigStore.ListEmployees = new HisEmployeeGet().Get(new HisEmployeeFilterQuery());
                    GlobalConfigStore.PathSaveXml = HisHeinApprovalCFG.XML4210_FOLDER_PATH;
                    IS_SET_XML_CONFIG = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        //Lay branch theo cashier_room_id chu ko lay tu token (tranh truong hop chay thread bi mat thong tin token)
        private bool HasBranchInfo(long cashierRoomId, ref HIS_BRANCH branch)
        {
            try
            {
                V_HIS_CASHIER_ROOM cashierRoom = HisCashierRoomCFG.DATA != null ? HisCashierRoomCFG.DATA.FirstOrDefault(o => o.ID == cashierRoomId) : null;
                if (cashierRoom != null)
                {
                    branch = HisBranchCFG.DATA != null ? HisBranchCFG.DATA.FirstOrDefault(o => o.ID == cashierRoom.BRANCH_ID) : null;
                    if (branch == null)
                    {
                        LogSystem.Warn("Ko co thong tin chi nhanh theo cashier_room_id: " + cashierRoomId);
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }
    }
}
