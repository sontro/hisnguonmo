using AutoMapper;
using His.Bhyt.ExportXml;
using His.Bhyt.ExportXml.Base;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisCashierRoom;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.HisFinancePeriod;

namespace MOS.MANAGER.HisHeinApproval
{
    partial class HisHeinApprovalCreate : BusinessBase
    {
        private static bool IS_SET_XML_CONFIG = false;

        private List<HIS_HEIN_APPROVAL> recentHisHeinApprovals = new List<HIS_HEIN_APPROVAL>();

        private HisTreatmentLockHein hisTreatmentLockHein;
        private HisSereServUpdate hisSereServUpdate;

        internal HisHeinApprovalCreate()
            : base()
        {
            this.Init();
        }

        internal HisHeinApprovalCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisTreatmentLockHein = new HisTreatmentLockHein(param);
        }

        internal bool Create(HIS_HEIN_APPROVAL data, HIS_TREATMENT treatment)
        {
            V_HIS_HEIN_APPROVAL resultData = null;
            return this.Create(data, treatment, false, ref resultData);
        }

        internal bool Create(HIS_HEIN_APPROVAL data, ref V_HIS_HEIN_APPROVAL resultData)
        {
            try
            {
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                if (data != null && checker.VerifyId(data.TREATMENT_ID, ref treatment))
                {
                    return this.Create(data, treatment, true, ref resultData);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private bool Create(HIS_HEIN_APPROVAL data, HIS_TREATMENT treatment, bool isValidate, ref V_HIS_HEIN_APPROVAL resultData)
        {
            bool result = false;
            try
            {
                //Trong truong hop truyen vao thong tin user (khi chay thread tu dong duyet) 
                //thi ko lay thong tin theo token
                if (string.IsNullOrWhiteSpace(data.EXECUTE_LOGINNAME))
                {
                    data.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                }
                if (string.IsNullOrWhiteSpace(data.EXECUTE_USERNAME))
                {
                    data.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                }

                //Lay branch theo cashier_room_id chu ko lay tu token (tranh truong hop chay thread bi mat thong tin token)
                HIS_BRANCH branch = this.GetByCashierRoomId(data.CASHIER_ROOM_ID);

                HIS_PATIENT_TYPE_ALTER currentPta = new HisPatientTypeAlterGet().GetLastByTreatmentId(data.TREATMENT_ID);
                if (currentPta != null)
                {
                    data.TREATMENT_TYPE_ID = currentPta.TREATMENT_TYPE_ID;
                    data.JOIN_5_YEAR_TIME = currentPta.JOIN_5_YEAR_TIME;
                }

                //Ho so dieu tri phai duoc duyet khoa tai chinh (is_active = 0)
                //va chua duoc duyet khoa BHYT (is_lock_hein = 0)
                //thi moi cho phep them du lieu hein_approval
                HisHeinApprovalCheck checker = new HisHeinApprovalCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck(param);

                bool valid = true;

                if (isValidate)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExisted(data, data.TREATMENT_ID);
                    valid = valid && treatmentChecker.IsLock(treatment);
                    valid = valid && treatmentChecker.IsUnLockHein(treatment);
                    valid = valid && checker.IsValidExecuteTime(treatment.FEE_LOCK_TIME.Value, data.EXECUTE_TIME.Value);
                    valid = valid && financePeriodChecker.HasNotFinancePeriod(branch.ID, data.EXECUTE_TIME.Value);
                }

                if (valid)
                {
                    if (!DAOWorker.HisHeinApprovalDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHeinApproval_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHeinApproval that bai." + LogUtil.TraceData("data", data));
                    }

                    this.recentHisHeinApprovals.Add(data);

                    List<HIS_SERE_SERV> hisSereServs = null;

                    this.ProcessHisSereServ(data, ref hisSereServs);
                    this.ProcessAutoLockHein(treatment, hisSereServs);
                    this.ProcessAutoExportXml(branch, data);
                    this.ParseResultData(data, ref resultData);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessAutoLockHein(HIS_TREATMENT hisTreatment, List<HIS_SERE_SERV> hisSereServs)
        {
            if (HisTreatmentCFG.AUTO_LOCK_AFTER_HEIN_APPROVAL)
            {
                List<HIS_PATIENT_TYPE_ALTER> pt = this.GetDistinctBySereServ(hisSereServs);
                List<HIS_HEIN_APPROVAL> heinApproval = new HisHeinApprovalGet().GetByTreatmentId(hisTreatment.ID);
                //Neu da duyet het thi moi thuc hien tu dong khoa ho so
                if (pt.Count == heinApproval.Count)
                {
                    HIS_TREATMENT resultData = null;
                    if (!this.hisTreatmentLockHein.LockHein(hisTreatment, ref resultData))
                    {
                        LogSystem.Warn("Tu dong khoa ho so dieu tri that bai." + LogUtil.TraceData("hisTreatment", hisTreatment));
                    }
                }
            }
        }

        private void ProcessAutoExportXml(HIS_BRANCH branch, HIS_HEIN_APPROVAL data)
        {
            try
            {
                //Neu co cau hinh thu muc xuat XML917 thi moi thuc hien xuat
                if (HisHeinApprovalCFG.IS_AUTO_EXPORT_XML && !string.IsNullOrWhiteSpace(HisHeinApprovalCFG.XML_FOLDER_PATH))
                {
                    if (HisHeinApprovalCFG.HEIN_CARD_NUMBER_PREFIX_RESTRICTS != null && HisHeinApprovalCFG.HEIN_CARD_NUMBER_PREFIX_RESTRICTS.Any(a => data.HEIN_CARD_NUMBER.StartsWith(a)))
                    {
                        return;
                    }
                    string folderPath = string.Format("{0}\\{1}", HisHeinApprovalCFG.XML_FOLDER_PATH, branch.HEIN_MEDI_ORG_CODE);
                    List<HIS_DHST> dhsts = new HisDhstGet().GetByTreatmentId(data.TREATMENT_ID);
                    V_HIS_HEIN_APPROVAL vHeinApproval = new HisHeinApprovalGet().GetViewById(data.ID);
                    InputADO ado = new InputADO();
                    ado.HeinApproval = vHeinApproval;
                    ado.ListSereServ = new HisSereServGet().GetView2ByHeinApprovalId(data.ID);
                    ado.Treatment = new HisTreatmentGet().GetView3ById(data.TREATMENT_ID);
                    ado.Dhst = IsNotNullOrEmpty(dhsts) ? dhsts.OrderByDescending(o => o.ID).FirstOrDefault(o => o.WEIGHT.HasValue) ?? dhsts.OrderByDescending(o => o.ID).FirstOrDefault() : null;
                    ado.Branch = branch;

                    this.SetXmlCreatorConfig();
                    CreateXmlMain xmlCreator = new CreateXmlMain(ado);
                    MemoryStream memoryStream = xmlCreator.Run917Plus();

                    if (memoryStream == null)
                    {
                        LogSystem.Error("Tu dong xuat XML917 that bai");
                    }
                    else
                    {
                        var fileName = string.Format("{0}___{1}_{2}_{3}.xml", Inventec.Common.DateTime.Get.Now().Value, data.HEIN_CARD_NUMBER, ado.Treatment.TREATMENT_CODE, data.HEIN_APPROVAL_CODE);

                        FileUploadInfo fileUploadInfo = FileUpload.UploadFile(MOS.UTILITY.Constant.APPLICATION_CODE, folderPath, memoryStream, fileName, true);

                        if (fileUploadInfo != null)
                        {
                            Mapper.CreateMap<HIS_HEIN_APPROVAL, HIS_HEIN_APPROVAL>();
                            HIS_HEIN_APPROVAL beforeUpdate = Mapper.Map<HIS_HEIN_APPROVAL>(data);
                            HIS_HEIN_APPROVAL toUpdate = Mapper.Map<HIS_HEIN_APPROVAL>(data);
                            toUpdate.XML_URL = fileUploadInfo.Url;
                            if (!new HisHeinApprovalUpdate().Update(toUpdate, beforeUpdate))
                            {
                                LogSystem.Error("Cap nhat XML917 URL cho HIS_HEIN_APPROVAL that bai");
                            }
                        }
                        else
                        {
                            LogSystem.Error("Tai file XML917 len he thong FSS that bai");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void ProcessHisSereServ(HIS_HEIN_APPROVAL data, ref List<HIS_SERE_SERV> hisSereServs)
        {
            HisSereServFilterQuery filter = new HisSereServFilterQuery();
            filter.TREATMENT_ID = data.TREATMENT_ID;
            filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
            hisSereServs = new HisSereServGet().Get(filter);

            List<HIS_SERE_SERV> sereServs = this.GetByHeinApprovalData(data, hisSereServs);

            if (IsNotNullOrEmpty(sereServs))
            {
                string str = String.Format("UPDATE HIS_SERE_SERV SET HEIN_APPROVAL_ID = {0}", data.ID) + " WHERE %IN_CLAUSE% ";
                string sql = DAOWorker.SqlDAO.AddInClause(sereServs.Select(s => s.ID).ToList(), str, "ID");
                if (!DAOWorker.SqlDAO.Execute(sql))
                {
                    throw new Exception("Rollback data. Ket thuc nghiep vu.");
                }
            }
            else
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisHeinApproval_KhongCoDichVuTuongUng);
                throw new Exception("Khong co dich vu." + LogUtil.TraceData("data", data));
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
                    GlobalConfigStore.PathSaveXml = HisHeinApprovalCFG.XML_FOLDER_PATH;
                    IS_SET_XML_CONFIG = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ParseResultData(HIS_HEIN_APPROVAL data, ref V_HIS_HEIN_APPROVAL resultData)
        {
            if (data != null)
            {
                resultData = new HisHeinApprovalGet().GetViewById(data.ID);
            }
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisHeinApprovals))
            {
                if (!DAOWorker.HisHeinApprovalDAO.TruncateList(this.recentHisHeinApprovals))
                {
                    LogSystem.Warn("Rollback du lieu HisHeinApproval that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHeinApprovals", this.recentHisHeinApprovals));
                }
            }
            this.hisTreatmentLockHein.RollbackData();
        }

        private List<HIS_SERE_SERV> GetByHeinApprovalData(HIS_HEIN_APPROVAL heinApproval, List<HIS_SERE_SERV> hisSereServs)
        {
            if (IsNotNullOrEmpty(hisSereServs))
            {
                List<HIS_SERE_SERV> result = new List<HIS_SERE_SERV>();
                foreach (HIS_SERE_SERV vHisSereServ in hisSereServs)
                {
                    if (!string.IsNullOrWhiteSpace(vHisSereServ.JSON_PATIENT_TYPE_ALTER))
                    {
                        BhytServiceRequestData bhytServiceRequestData = new BhytServiceRequestData(vHisSereServ.JSON_PATIENT_TYPE_ALTER, null);
                        if (bhytServiceRequestData != null
                            && this.IsProperPatientTypeData(heinApproval, bhytServiceRequestData.PatientTypeData))
                        {
                            result.Add(vHisSereServ);
                        }
                    }
                }
                return result;
            }
            return null;
        }

        private bool IsProperPatientTypeData(HIS_HEIN_APPROVAL heinApproval, BhytPatientTypeData patientData)
        {
            return IsNotNullOrEmpty(heinApproval.HEIN_CARD_NUMBER)
                && heinApproval.HEIN_CARD_NUMBER.Equals(patientData.HEIN_CARD_NUMBER)
                && IsNotNullOrEmpty(heinApproval.HEIN_MEDI_ORG_CODE)
                && heinApproval.HEIN_MEDI_ORG_CODE.Equals(patientData.HEIN_MEDI_ORG_CODE)
                && IsNotNullOrEmpty(heinApproval.LEVEL_CODE)
                && heinApproval.LEVEL_CODE.Equals(patientData.LEVEL_CODE)
                && IsNotNullOrEmpty(heinApproval.RIGHT_ROUTE_CODE)
                && heinApproval.RIGHT_ROUTE_CODE.Equals(patientData.RIGHT_ROUTE_CODE)
                && IsNotNullOrEmpty(heinApproval.JOIN_5_YEAR)
                && heinApproval.JOIN_5_YEAR.Equals(patientData.JOIN_5_YEAR)
                && IsNotNullOrEmpty(heinApproval.PAID_6_MONTH)
                && heinApproval.PAID_6_MONTH.Equals(patientData.PAID_6_MONTH)
                && (!IsNotNullOrEmpty(heinApproval.LIVE_AREA_CODE) || (IsNotNullOrEmpty(heinApproval.LIVE_AREA_CODE) && heinApproval.LIVE_AREA_CODE.Equals(patientData.LIVE_AREA_CODE)));
        }

        private List<HIS_PATIENT_TYPE_ALTER> GetDistinctBySereServ(List<HIS_SERE_SERV> hisSereServs)
        {
            List<HIS_PATIENT_TYPE_ALTER> lst = new List<HIS_PATIENT_TYPE_ALTER>();
            if (IsNotNullOrEmpty(hisSereServs))
            {
                Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, HIS_PATIENT_TYPE_ALTER>();
                foreach (HIS_SERE_SERV vHisSereServ in hisSereServs)
                {
                    if (!string.IsNullOrWhiteSpace(vHisSereServ.JSON_PATIENT_TYPE_ALTER))
                    {
                        BhytServiceRequestData bhytServiceRequestData = new BhytServiceRequestData(vHisSereServ.JSON_PATIENT_TYPE_ALTER, null);
                        if (bhytServiceRequestData != null)
                        {
                            HIS_PATIENT_TYPE_ALTER pt = Mapper.Map<HIS_PATIENT_TYPE_ALTER>(bhytServiceRequestData.PatientTypeData);
                            lst.Add(pt);
                        }
                    }
                }
            }
            return IsNotNullOrEmpty(lst) ? new HisPatientTypeAlterGet().GetDistinct(lst) : null;
        }

        //Lay branch theo cashier_room_id chu ko lay tu token (tranh truong hop chay thread bi mat thong tin token)
        private HIS_BRANCH GetByCashierRoomId(long cashierRoomId)
        {
            try
            {
                V_HIS_CASHIER_ROOM cashierRoom = HisCashierRoomCFG.DATA != null ? HisCashierRoomCFG.DATA.FirstOrDefault(o => o.ID == cashierRoomId) : null;
                if (cashierRoom != null)
                {
                    return (HisBranchCFG.DATA != null ? HisBranchCFG.DATA.FirstOrDefault(o => o.ID == cashierRoom.BRANCH_ID) : null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }
    }
}
