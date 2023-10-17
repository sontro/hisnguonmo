using His.Bhyt.ExportXml;
using His.Bhyt.ExportXml.Base;
using Inventec.Common.Logging;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisDebate;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisTracking;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.Finish
{
    public class AutoExportXml4210Collinear : BusinessBase
    {
        private static bool IS_SET_XML_CONFIG = false;

        public void Run(long treatmentId, string treatmentCode, string patientCode, HIS_BRANCH branch, HIS_PATIENT_TYPE_ALTER pta)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(HisTreatmentCFG.XML4210_COLLINEAR_FOLDER_PATH)
                    && pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    string folderPath = string.Format("{0}\\{1}", HisTreatmentCFG.XML4210_COLLINEAR_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);
                    HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == pta.TREATMENT_TYPE_ID).FirstOrDefault();

                    List<HIS_DHST> dhsts = new HisDhstGet().GetByTreatmentId(treatmentId);
                    V_HIS_HEIN_APPROVAL heinApproval = new V_HIS_HEIN_APPROVAL();
                    heinApproval.ADDRESS = pta.ADDRESS;
                    heinApproval.BRANCH_ID = branch.ID;
                    heinApproval.HAS_BIRTH_CERTIFICATE = pta.HAS_BIRTH_CERTIFICATE;
                    heinApproval.HEIN_CARD_FROM_TIME = pta.HEIN_CARD_FROM_TIME.Value;
                    heinApproval.HEIN_CARD_NUMBER = pta.HEIN_CARD_NUMBER;
                    heinApproval.HEIN_CARD_TO_TIME = pta.HEIN_CARD_TO_TIME.Value;
                    heinApproval.HEIN_MEDI_ORG_CODE = pta.HEIN_MEDI_ORG_CODE;
                    heinApproval.HEIN_MEDI_ORG_NAME = pta.HEIN_MEDI_ORG_NAME;
                    heinApproval.HEIN_TREATMENT_TYPE_CODE = treatmentType.HEIN_TREATMENT_TYPE_CODE;
                    heinApproval.JOIN_5_YEAR = pta.JOIN_5_YEAR;
                    heinApproval.LEVEL_CODE = pta.LEVEL_CODE;
                    heinApproval.LIVE_AREA_CODE = pta.LIVE_AREA_CODE;
                    heinApproval.PAID_6_MONTH = pta.PAID_6_MONTH;
                    heinApproval.PATIENT_ID = pta.TDL_PATIENT_ID;
                    heinApproval.RIGHT_ROUTE_CODE = pta.RIGHT_ROUTE_CODE;
                    heinApproval.RIGHT_ROUTE_TYPE_CODE = pta.RIGHT_ROUTE_TYPE_CODE;
                    heinApproval.TREATMENT_TYPE_ID = pta.TREATMENT_TYPE_ID;

                    InputADO ado = new InputADO();
                    ado.HeinApprovals = new List<V_HIS_HEIN_APPROVAL>() { heinApproval };

                    HisSereServView2FilterQuery filter = new HisSereServView2FilterQuery();
                    filter.TREATMENT_ID = treatmentId;
                    filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                    ado.ListSereServ = new HisSereServGet().GetView2(filter);
                    ado.Treatment = new HisTreatmentGet().GetView3ById(treatmentId);
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
                    MemoryStream memoryStream = xmlCreator.Run4210PlusCollinear(ref messageError);

                    string sql = "UPDATE HIS_TREATMENT SET COLLINEAR_XML4210_URL = '{0}', COLLINEAR_XML4210_RESULT = {1}, COLLINEAR_XML4210_DESC= '{2}' WHERE ID = {3}";
                    string query = "";
                    if (memoryStream == null)
                    {
                        LogSystem.Error("Tu dong xuat XML thong tuyen that bai");
                        query = String.Format(sql, "", IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_FAIL, messageError, treatmentId);
                    }
                    else
                    {
                        var fileName = string.Format("{0}___{1}___{2}.xml", Inventec.Common.DateTime.Get.Now().Value, treatmentCode, patientCode);

                        FileUploadInfo fileUploadInfo = null;
                        try
                        {
                            fileUploadInfo = FileUpload.UploadFile(Constant.APPLICATION_CODE, folderPath, memoryStream, fileName, true);
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Error(ex);
                        }

                        if (fileUploadInfo == null)
                        {
                            LogSystem.Error("Tai file XML4210 thong tuyen len he thong FSS that bai");
                            query = String.Format(sql, fileUploadInfo.Url, IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_FAIL, MOS.MANAGER.Base.MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_UploadXml4210ThatBai, param.LanguageCode), treatmentId);
                        }
                        else
                        {
                            //khong dung HisTreatmentUpdate(paramUpdate).Update(toUpdate, beforeUpdate) do Treatment co is_active = 0
                            query = String.Format(sql, fileUploadInfo.Url, IMSys.DbConfig.HIS_RS.HIS_TREATMENT.XML4210_RESULT__CREATE_FILE_SUCCESS, "", treatmentId);
                            LogSystem.Info("Xuat XML thong tuyen ho so dieu tri: " + treatmentCode + " thanh cong");
                        }
                    }

                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Error("Cap nhat XML4210 URL cho HIS_TREATMENT that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
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
    }
}
