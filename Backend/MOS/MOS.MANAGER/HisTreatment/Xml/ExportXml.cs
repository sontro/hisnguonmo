using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisTracking;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisHeinApproval;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisServiceReq;
using MOS.UTILITY;
using MOS.MANAGER.HisDebate;
using His.Bhyt.ExportXml;
using Inventec.Fss.Utility;
using Inventec.Fss.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Xml
{
    public class ExportXml : BusinessBase
    {
        private static bool IS_SET_XML_CONFIG = false;

        public bool ExportXML4210(long treatmentId, long branchId)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(HisHeinApprovalCFG.XML4210_FOLDER_PATH))
                {
                    HisHeinApprovalViewFilterQuery filter = new HisHeinApprovalViewFilterQuery();
                    filter.TREATMENT_ID = treatmentId;
                    List<V_HIS_HEIN_APPROVAL> heinApprovals = new HisHeinApprovalGet().GetView(filter);

                    HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == branchId);
                    string folderPath = string.Format("{0}\\{1}", HisHeinApprovalCFG.XML4210_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);

                    heinApprovals = heinApprovals != null ? heinApprovals.Where(o => HisHeinApprovalCFG.HEIN_CARD_NUMBER_PREFIX_RESTRICTS == null || !HisHeinApprovalCFG.HEIN_CARD_NUMBER_PREFIX_RESTRICTS.Any(a => !string.IsNullOrWhiteSpace(a) && o.HEIN_CARD_NUMBER.StartsWith(a))).ToList() : null;

                    if (!IsNotNullOrEmpty(heinApprovals))
                    {
                        LogSystem.Warn("Ko ton tai du lieu duyet BHYT nao cho phep xuat tu dong");
                        return false;
                    }
                    List<long> heinApprovalIds = heinApprovals.Select(s => s.ID).ToList();

                    His.Bhyt.ExportXml.Base.InputADO ado = new His.Bhyt.ExportXml.Base.InputADO();
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
                    //Neu cau hinh khong xuat XML voi cac ho so ko co cong kham va ho so ko co cong kham thi ket thuc
                    if (!HisHeinApprovalCFG.NOT_AUTO_EXPORT_XML_NO_EXAM
                        || ado.ListSereServ.Exists(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH && o.IS_NO_EXECUTE == null && o.IS_EXPEND == null))
                    {
                        result = this.ProcessExport4210Bhyt(treatmentId, branch, memoryStream, heinApprovals[0].TREATMENT_CODE, heinApprovals[0].TDL_PATIENT_CODE, ref messageError);
                    }

                }
                else
                {
                    LogSystem.Error("Thu muc luu file XML4210 tren server chua duoc cau hinh");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return result;
        }

        private bool ProcessExport4210Bhyt(long treatmentId, HIS_BRANCH branch, MemoryStream memoryStream, string treatmentCode, string patientCode, ref string messageError)
        {
            bool result = false;
            try
            {
                string sql = "UPDATE HIS_TREATMENT SET XML4210_URL = '{0}', XML4210_RESULT = {1}, XML4210_DESC= '{2}' WHERE ID = {3}";
                string query = "";
                if (memoryStream == null)
                {
                    LogSystem.Error("Xuat xml4210 ho so dieu tri: " + treatmentCode + " that bai");
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
                        LogSystem.Error(ex);
                    }

                    if (fileUploadInfo == null)
                    {
                        LogSystem.Error("Tai file XML4210 ho so dieu tri: " + treatmentCode + " len he thong FSS that bai");
                        query = String.Format(sql, fileUploadInfo.Url, IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_FAIL, MOS.MANAGER.Base.MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_UploadXml4210ThatBai, param.LanguageCode), treatmentId);
                    }
                    else
                    {
                        query = String.Format(sql, fileUploadInfo.Url, IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_SUCCESS, "", treatmentId);
                        LogSystem.Info("Xuat xml4210 ho so dieu tri: " + treatmentCode + " thanh cong");
                        result = true;
                    }
                }

                if (!DAOWorker.SqlDAO.Execute(query))
                {
                    LogSystem.Error("Cap nhat XML4210 URL cho ho so dieu tri: " + treatmentCode + " that bai");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return result;
        }

        private void SetXmlCreatorConfig()
        {
            try
            {
                if (!IS_SET_XML_CONFIG)
                {
                    His.Bhyt.ExportXml.Base.GlobalConfigStore.ListIcdCode_Nds = HisHeinBhytCFG.BHYT_NDS_ICD_CODE__OTHER;
                    His.Bhyt.ExportXml.Base.GlobalConfigStore.ListIcdCode_Nds_Te = HisHeinBhytCFG.BHYT_NDS_ICD_CODE__TE;

                    His.Bhyt.ExportXml.Base.GlobalConfigStore.ListEmployees = new HisEmployeeGet().Get(new HisEmployeeFilterQuery());
                    His.Bhyt.ExportXml.Base.GlobalConfigStore.PathSaveXml = HisHeinApprovalCFG.XML4210_FOLDER_PATH;
                    IS_SET_XML_CONFIG = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public bool FhirExportXML4210(long treatmentId, long branchId)
        {
            bool result = false;
            try
            {
                HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == branchId);

                HisPatientTypeAlterViewFilterQuery filter = new HisPatientTypeAlterViewFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                List<V_HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlterGet().GetView(filter);
                patientTypeAlters = IsNotNullOrEmpty(patientTypeAlters) ? patientTypeAlters.OrderByDescending(o => o.LOG_TIME).ToList() : null;


                HisServiceReqFilterQuery srFilter = new HisServiceReqFilterQuery();
                srFilter.TREATMENT_ID = treatmentId;
                srFilter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(srFilter);

                List<HIS_DHST> dhsts = new HisDhstGet().GetByTreatmentId(treatmentId);

                His.ExportXml.Base.InputADO ado = new His.ExportXml.Base.InputADO();

                ado.Treatment = new HisTreatmentGet().GetView3ById(treatmentId);
                ado.LastPatientTypeAlter = IsNotNullOrEmpty(patientTypeAlters) ? patientTypeAlters[0] : null;
                ado.ListPatientTypeAlter = IsNotNullOrEmpty(patientTypeAlters) ? patientTypeAlters : null;
                List<V_HIS_SERE_SERV_2> sereServs = new HisSereServGet().GetView2ByTreatmentId(treatmentId);
                ado.ListSereServ = sereServs != null ? sereServs.Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList() : null;
                ado.Branch = branch;
                ado.SereServTeins = new HisSereServTeinGet().GetViewByTreatmentId(treatmentId);
                ado.Trackings = new HisTrackingGet().GetByTreatmentId(treatmentId);
                ado.SereServPttts = new HisSereServPtttGet().GetViewByTreatmentId(treatmentId);
                ado.BedLogs = new HisBedLogGet().GetViewByTreatmentId(treatmentId);
                List<long> ekipIds = ado.ListSereServ != null ? ado.ListSereServ.Where(o => o.EKIP_ID.HasValue).Select(o => o.EKIP_ID.Value).ToList() : null;
                ado.EkipUsers = new HisEkipUserGet().GetByEkipIds(ekipIds);
                ado.MaterialPackageOption = HisHeinBhytCFG.CALC_MATERIAL_PACKAGE_PRICE_OPTION.ToString();
                ado.MaterialPriceOriginalOption = HisHeinBhytCFG.XML__4210__MATERIAL_PRICE_OPTION;
                ado.MaterialTypes = HisMaterialTypeCFG.DATA;
                ado.ListHeinMediOrg = HisMediOrgCFG.DATA;
                ado.ConfigData = Loader.CONFIGs;
                ado.TotalIcdData = HisIcdCFG.DATA;
                ado.TotalSericeData = HisServiceCFG.DATA_VIEW;
                ado.ListDebate = new HisDebateGet().GetByTreatmentId(treatmentId);
                ado.ListPatientType = HisPatientTypeCFG.DATA;

                if (IsNotNullOrEmpty(dhsts))
                {
                    var dhst = dhsts.OrderByDescending(o => o.EXECUTE_TIME).ThenByDescending(o => o.ID).FirstOrDefault(o => o.WEIGHT.HasValue);
                    if (IsNotNull(dhst))
                    {
                        ado.Dhst = dhst;
                    }
                    else
                    {
                        ado.Dhst = dhsts.OrderByDescending(o => o.EXECUTE_TIME).ThenByDescending(o => o.ID).FirstOrDefault();
                    }
                }
                else
                {
                    ado.Dhst = null;
                }

                if (IsNotNullOrEmpty(serviceReqs))
                {
                    HIS_SERVICE_REQ mainServiceReq = serviceReqs.FirstOrDefault(o => o.IS_MAIN_EXAM == Constant.IS_TRUE);
                    if (IsNotNull(mainServiceReq))
                    {
                        ado.ExamServiceReq = mainServiceReq;
                    }
                    else
                    {
                        HIS_SERVICE_REQ serviceReq = serviceReqs.OrderBy(o => o.START_TIME).FirstOrDefault();
                        foreach (var sr in serviceReqs)
                        {
                            if (string.IsNullOrWhiteSpace(serviceReq.EXECUTE_LOGINNAME) && !string.IsNullOrWhiteSpace(sr.EXECUTE_LOGINNAME))
                            {
                                serviceReq.EXECUTE_LOGINNAME = sr.EXECUTE_LOGINNAME;
                                serviceReq.EXECUTE_USERNAME = sr.EXECUTE_USERNAME;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.ICD_CODE) && !string.IsNullOrWhiteSpace(sr.ICD_CODE))
                            {
                                serviceReq.ICD_CODE = sr.ICD_CODE;
                                serviceReq.ICD_NAME = sr.ICD_NAME;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.ICD_SUB_CODE) && !string.IsNullOrWhiteSpace(sr.ICD_SUB_CODE))
                            {
                                serviceReq.ICD_SUB_CODE = sr.ICD_SUB_CODE;
                                serviceReq.ICD_TEXT = sr.ICD_TEXT;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.PATHOLOGICAL_HISTORY) && !string.IsNullOrWhiteSpace(sr.PATHOLOGICAL_HISTORY))
                            {
                                serviceReq.PATHOLOGICAL_HISTORY = sr.PATHOLOGICAL_HISTORY;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.PATHOLOGICAL_HISTORY_FAMILY) && !string.IsNullOrWhiteSpace(sr.PATHOLOGICAL_HISTORY_FAMILY))
                            {
                                serviceReq.PATHOLOGICAL_HISTORY_FAMILY = sr.PATHOLOGICAL_HISTORY_FAMILY;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.TREATMENT_INSTRUCTION) && !string.IsNullOrWhiteSpace(sr.TREATMENT_INSTRUCTION))
                            {
                                serviceReq.TREATMENT_INSTRUCTION = sr.TREATMENT_INSTRUCTION;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.PART_EXAM) && !string.IsNullOrWhiteSpace(sr.PART_EXAM))
                            {
                                serviceReq.PART_EXAM = sr.PART_EXAM;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.FULL_EXAM) && !string.IsNullOrWhiteSpace(sr.FULL_EXAM))
                            {
                                serviceReq.FULL_EXAM = sr.FULL_EXAM;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.HOSPITALIZATION_REASON) && !string.IsNullOrWhiteSpace(sr.HOSPITALIZATION_REASON))
                            {
                                serviceReq.HOSPITALIZATION_REASON = sr.HOSPITALIZATION_REASON;
                            }
                            if (string.IsNullOrWhiteSpace(serviceReq.PROVISIONAL_DIAGNOSIS) && !string.IsNullOrWhiteSpace(sr.PROVISIONAL_DIAGNOSIS))
                            {
                                serviceReq.PROVISIONAL_DIAGNOSIS = sr.PROVISIONAL_DIAGNOSIS;
                            }
                        }
                    }
                }
                else
                {
                    ado.ExamServiceReq = null;
                }
                

                His.ExportXml.CreateXmlMain xmlMain = new His.ExportXml.CreateXmlMain(ado);

                var memoryStream = xmlMain.RunFhir4210Plus();

                if (memoryStream == null)
                {
                    LogSystem.Error("Tu dong xuat xml4210 Fhir ho so dieu tri: " + ado.Treatment.TREATMENT_CODE + " that bai");
                }
                else
                {
                    var fileName = string.Format("{0}___{1}.xml", Inventec.Common.DateTime.Get.Now().Value, ado.Treatment.TREATMENT_CODE);

                    FileUploadInfo fileUploadInfo = null;
                    try
                    {
                        string folderPath = string.Format("{0}\\{1}", FhirCFG.FHIR_XML4210_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);
                        fileUploadInfo = FileUpload.UploadFile(Constant.APPLICATION_CODE, folderPath, memoryStream, fileName, true);
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(ex);
                    }

                    if (fileUploadInfo == null)
                    {
                        LogSystem.Error("Tai file XML4210 Fhir ho so dieu tri: " + ado.Treatment.TREATMENT_CODE + " len he thong FSS that bai");
                    }
                    else
                    {
                        LogSystem.Info("Xuat xml4210 Fhir ho so dieu tri: " + ado.Treatment.TREATMENT_CODE + " thanh cong");
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return result;
        }
    }
}
