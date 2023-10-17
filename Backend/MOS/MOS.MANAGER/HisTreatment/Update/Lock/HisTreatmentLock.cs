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
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisHeinApproval;
using AutoMapper;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
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
using His.ExportXml.Base;

namespace MOS.MANAGER.HisTreatment.Lock
{
    class HisTreatmentLock : BusinessBase
    {
        class ThreadData
        {
            public long TreatmentId { get; set; }
            public long CashierRoomId { get; set; }
            public long ExecuteTime { get; set; }
            public string ExecuteLoginName { get; set; }
            public string ExecuteUserName { get; set; }
        }

        class EhrThreadData
        {
            public long TreatmentId { get; set; }
            public HIS_BRANCH Branch { get; set; }
        }

        internal HisTreatmentLock()
            : base()
        {

        }

        internal HisTreatmentLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool Run(HisTreatmentLockSDO sdo, ref HIS_TREATMENT resultData)
        {
            HIS_TREATMENT raw = null;
            HisTreatmentCheck checker = new HisTreatmentCheck(param);
            return checker.VerifyId(sdo.TreatmentId, ref raw) && this.Run(sdo, raw, null, ref resultData);
        }

        internal bool Run(HisTreatmentLockSDO sdo, HIS_TREATMENT treatment, long? cashierRoomId, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;

                WorkPlaceSDO workPlace = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                HisFinancePeriodCheck financePeriodChecker = new HisFinancePeriodCheck(param);
                HisTreatmentLockCheck lockChecker = new HisTreatmentLockCheck(param);

                valid = valid && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace);

                valid = valid && checker.IsUnLock(treatment);
                valid = valid && checker.IsPause(treatment);
                valid = valid && checker.IsUnLockHein(treatment);
                valid = valid && lockChecker.HasNoUnpaid(treatment);
                valid = valid && lockChecker.IsValidFeeLockTime(treatment, sdo.FeeLockTime);
                valid = valid && financePeriodChecker.HasNotFinancePeriod(workPlace.BranchId, sdo.FeeLockTime);
                valid = valid && lockChecker.CheckAllowLockBeforeApproveMediRecord(treatment);
                valid = valid && lockChecker.HasNoMissingInvoiceInfoMaterial(treatment.ID);
                valid = valid && lockChecker.IsValidSereServAmountTemp(treatment.ID);

                if (valid)
                {
                    //Neu ben ngoai ko truyen vao cashier_room_id thi lay thong tin tu workplace
                    cashierRoomId = !cashierRoomId.HasValue ? workPlace.CashierRoomId : cashierRoomId;

                    if (!cashierRoomId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                        return false;
                    }

                    treatment.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                    treatment.FEE_LOCK_TIME = sdo.FeeLockTime;
                    treatment.FEE_LOCK_ROOM_ID = workPlace.RoomId;
                    treatment.FEE_LOCK_DEPARTMENT_ID = workPlace.DepartmentId;
                    treatment.FEE_LOCK_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    treatment.FEE_LOCK_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    if (DAOWorker.HisTreatmentDAO.Update(treatment))
                    {
                        result = true;
                        resultData = treatment;

                        new EventLogGenerator(EventLog.Enum.HisTreatment_DuyetKhoaVienPhi)
                            .TreatmentCode(treatment.TREATMENT_CODE).Run();

                        this.HeinApprovalThreadInit(treatment, cashierRoomId.Value);

                        this.ExportEhr4210XmlThreadInit(treatment);
                        this.ProcessSendDataToDbXML(resultData);
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

        private void HeinApprovalThreadInit(HIS_TREATMENT treatment, long? cashierRoomId)
        {
            try
            {
                List<long> outPatientTypeIds = new List<long>(){
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU,
                    IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                };

                //neu co cau hinh tu dong duyet ho so BHYT sau khi thuc hien khoa vien phi
                if (treatment.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE
                    && cashierRoomId.HasValue
                    && (HisTreatmentCFG.AUTO_HEIN_APPROVAL_AFTER_FEE_LOCK_OPTION == HisTreatmentCFG.AutoHeinApprovalAfterFeeLockOption.ALL ||
                        (HisTreatmentCFG.AUTO_HEIN_APPROVAL_AFTER_FEE_LOCK_OPTION == HisTreatmentCFG.AutoHeinApprovalAfterFeeLockOption.OUT_PATIENT
                        && outPatientTypeIds.Contains(treatment.TDL_TREATMENT_TYPE_ID.Value)))
                    && this.HasBhyt(treatment.ID)
                    )
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.HeinApprovalThreadProcess));
                    thread.Priority = ThreadPriority.Normal;
                    ThreadData threadData = new ThreadData();
                    threadData.CashierRoomId = cashierRoomId.Value;
                    threadData.TreatmentId = treatment.ID;
                    threadData.ExecuteLoginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    threadData.ExecuteUserName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    threadData.ExecuteTime = treatment.FEE_LOCK_TIME.HasValue ? treatment.FEE_LOCK_TIME.Value : Inventec.Common.DateTime.Get.Now().Value;
                    thread.Start(threadData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh xu ly duyet BHYT that bai", ex);
            }
        }

        private void ExportEhr4210XmlThreadInit(HIS_TREATMENT treatment)
        {
            try
            {
                //Neu co cau hinh tu dong xuat XML phuc vu EHR
                //--> tu dong xuat XML4210 voi doi tuong vien phi (con doi tuong BHYT thi se xuat sau khi duyet BHYT)
                if (!string.IsNullOrWhiteSpace(EhrCFG.XML4210_FOLDER_PATH) && treatment.TDL_PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ExportXML4210));
                    thread.Priority = ThreadPriority.Lowest;
                    EhrThreadData threadData = new EhrThreadData();
                    threadData.TreatmentId = treatment.ID;
                    threadData.Branch = new TokenManager().GetBranch();
                    thread.Start(threadData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh xu ly duyet BHYT that bai", ex);
            }
        }

        //Tien trinh xu ly de tao thong tin duyet ho so BHYT
        private void HeinApprovalThreadProcess(object data)
        {
            try
            {
                ThreadData threadData = (ThreadData)data;
                if (!new HisTreatmentHeinApproval(param).Run(threadData.TreatmentId, threadData.ExecuteTime, threadData.CashierRoomId, threadData.ExecuteLoginName, threadData.ExecuteUserName))
                {
                    LogSystem.Warn("Tu dong duyet khoa BHYT that bai: " + param.GetMessage());
                };
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private bool HasBhyt(long treatmentId)
        {
            try
            {
                string sql = string.Format("SELECT COUNT(1) FROM HIS_SERE_SERV WHERE IS_NO_EXECUTE IS NULL AND PATIENT_TYPE_ID = {0} AND TDL_TREATMENT_ID = {1} AND IS_DELETE = 0 AND SERVICE_REQ_ID IS NOT NULL", HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT, treatmentId);
                int count = DAOWorker.SqlDAO.GetSqlSingle<int>(sql);
                return count > 0;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        private void ExportXML4210(object threadData)
        {
            EhrThreadData data = (EhrThreadData)threadData;
            InputADO ado = new InputADO();
            ado.Treatment = new HisTreatmentGet().GetView3ById(data.TreatmentId);
            ado.LastPatientTypeAlter = new HisPatientTypeAlterGet().GetViewLastByTreatmentId(data.TreatmentId);

            List<V_HIS_SERE_SERV_2> sereServs = new HisSereServGet().GetView2ByTreatmentId(data.TreatmentId);
            ado.ListSereServ = sereServs != null ? sereServs.Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT).ToList() : null;
            ado.Branch = data.Branch;
            List<HIS_DHST> dhsts = new HisDhstGet().GetByTreatmentId(data.TreatmentId);
            ado.Dhst = dhsts != null ? dhsts.OrderByDescending(o => o.EXECUTE_TIME).ThenByDescending(o => o.ID).FirstOrDefault() : null;
            ado.SereServTeins = new HisSereServTeinGet().GetViewByTreatmentId(data.TreatmentId);
            ado.Trackings = new HisTrackingGet().GetByTreatmentId(data.TreatmentId);
            ado.SereServPttts = new HisSereServPtttGet().GetViewByTreatmentId(data.TreatmentId);
            ado.BedLogs = new HisBedLogGet().GetViewByTreatmentId(data.TreatmentId);

            List<long> ekipIds = ado.ListSereServ != null ? ado.ListSereServ.Where(o => o.EKIP_ID.HasValue).Select(o => o.EKIP_ID.Value).ToList() : null;
            ado.EkipUsers = new HisEkipUserGet().GetByEkipIds(ekipIds);

            ado.MaterialPackageOption = HisHeinBhytCFG.CALC_MATERIAL_PACKAGE_PRICE_OPTION.ToString();
            ado.MaterialPriceOriginalOption = HisHeinBhytCFG.XML__4210__MATERIAL_PRICE_OPTION;

            His.ExportXml.CreateXmlMain xmlMain = new His.ExportXml.CreateXmlMain(ado);

            var memoryStream = xmlMain.Run4210Plus();

            if (memoryStream == null)
            {
                LogSystem.Error("Tu dong xuat XML4210 EHR that bai");
            }
            else
            {
                var fileName = string.Format("{0}___{1}.xml", Inventec.Common.DateTime.Get.Now().Value, ado.Treatment.TREATMENT_CODE);

                FileUploadInfo fileUploadInfo = null;
                try
                {
                    string folderPath = string.Format("{0}\\{1}", EhrCFG.XML4210_FOLDER_PATH, data.Branch.HEIN_MEDI_ORG_CODE);
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
                    LogSystem.Info("Xuat xml4210 EHR ho so dieu tri: " + ado.Treatment.TREATMENT_CODE + " thanh cong");
                }
            }
        }

        private void ProcessSendDataToDbXML(HIS_TREATMENT resultData)
        {
            try
            {
                System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew((object data) => new HisTreatmentLockSendDataXml().Run((HIS_TREATMENT)data), resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
