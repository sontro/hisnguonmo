using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.LibraryHein.Bhyt.HeinHasBirthCertificate;

namespace MOS.MANAGER.HisTreatment.Update.Unfinish
{
    class HisTreatmentUnfinish : BusinessBase
    {
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal HisTreatmentUnfinish()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentUnfinish(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Unfinish(long treatmentId)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT hisTreatment = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisTreatmentUnfinishCheck unfinishChecker = new HisTreatmentUnfinishCheck(param);
                bool valid = treatmentChecker.IsUnLock(treatmentId, ref hisTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                valid = valid && treatmentChecker.IsValidDepartment(treatmentId);
                valid = valid && unfinishChecker.IsNotStored(hisTreatment);
                valid = valid && unfinishChecker.HasNoTreatmentDebt(treatmentId);
                valid = valid && unfinishChecker.IsAllow(hisTreatment.TDL_TREATMENT_TYPE_ID);

                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(hisTreatment);//clone phuc vu rollback

                    long? mediRecordId = null;

                    hisTreatment.IS_PAUSE = null;
                    hisTreatment.IS_YDT_UPLOAD = null;
                    //IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL
                    if (!HisTreatmentCFG.DONOT_CLEAR_OUT_TIME_WHEN_REOPENING)
                    {
                        hisTreatment.OUT_TIME = null;
                    }
                    if (!HisTreatmentCFG.IS_KEEPING_STORE_CODE)
                    {
                        mediRecordId = hisTreatment.MEDI_RECORD_ID;

                        if (hisTreatment.MEDI_RECORD_ID.HasValue)
                        {
                            //chi update voi truong hop su dung nghiep vu moi (co tao ra his_medi_record)
                            //do xoa thong tin luu tru his_medi_record nen can xoa thong tin kho luu tru va thoi gian luu tru
                            hisTreatment.STORE_CODE = null;
                            hisTreatment.STORE_TIME = null;
                            hisTreatment.DATA_STORE_ID = null;
                        }

                        hisTreatment.MEDI_RECORD_ID = null;
                    }

                    if (this.hisTreatmentUpdate.Update(hisTreatment, beforeUpdate))
                    {
                        List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                        List<HIS_SERE_SERV> existsSereServs = null;

                        this.ProcessRecalcExamSereServ(hisTreatment, ref ptas, ref existsSereServs);

                        List<string> sqls = new List<string>();

                        this.ProcessMediRecord(mediRecordId, ref sqls);

                        this.ProcessPatientTypeAlter(beforeUpdate, ptas, existsSereServs, ref sqls);

                        this.ProcessTransReq(beforeUpdate, ref sqls);

                        if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("Xu ly that bai ");
                        }

                        result = true;

                        new EventLogGenerator(EventLog.Enum.HisTreatment_HuyKetThucDieuTri)
                                .TreatmentCode(hisTreatment.TREATMENT_CODE).Run();

                        new Util.HisTreatmentUploadEmr().Run(hisTreatment.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                this.RollBack();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessTransReq(HIS_TREATMENT treatment, ref List<string> sqls)
        {
            if (sqls == null)
            {
                sqls = new List<string>();
            }
            string sqlUpdate = string.Format("UPDATE HIS_TRANS_REQ TRAN SET TRANS_REQ_STT_ID = {0} WHERE TRAN.TREATMENT_ID = {1} AND TRAN.TRANS_REQ_TYPE = {2} AND TRAN.TRANS_REQ_STT_ID = {3} ", IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__CANCEL, treatment.ID, IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_TYPE.ID__BY_TREATMENT, IMSys.DbConfig.HIS_RS.HIS_TRANS_REQ_STT.ID__REQUEST);
            sqls.Add(sqlUpdate);
        }

        /// <summary>
        /// Xu ly de cap nhat lai han cua the BHYT tam cap cho tre em
        /// (Do khi ket thuc dieu tri thi xu ly cap nhat han the = ngay ra vien)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="patientTypeAlters"></param>
        /// <param name="existsSereServs"></param>
        /// <param name="sqls"></param>
        private void ProcessPatientTypeAlter(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters, List<HIS_SERE_SERV> existsSereServs, ref List<string> sqls)
        {
            if (sqls == null)
            {
                sqls = new List<string>();
            }

            List<HIS_PATIENT_TYPE_ALTER> ptas = patientTypeAlters != null ? patientTypeAlters.Where(t => t.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && t.HAS_BIRTH_CERTIFICATE == HeinHasBirthCertificateCode.TRUE).ToList() : null;

            if (IsNotNullOrEmpty(ptas))
            {
                string sqlUpdatePta = string.Format("UPDATE HIS_PATIENT_TYPE_ALTER P SET HEIN_CARD_TO_TIME = HEIN_CARD_TO_TIME_TMP, HEIN_CARD_TO_TIME_TMP = NULL WHERE P.TREATMENT_ID = {0} AND P.HAS_BIRTH_CERTIFICATE = '{1}' AND P.PATIENT_TYPE_ID = {2} AND HEIN_CARD_TO_TIME_TMP IS NOT NULL ", treatment.ID, HeinHasBirthCertificateCode.TRUE, HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT);
                sqls.Add(sqlUpdatePta);

                if (IsNotNullOrEmpty(existsSereServs)
                && existsSereServs.Exists(t => t.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                {
                    foreach (HIS_PATIENT_TYPE_ALTER p in ptas)
                    {
                        if (p.HEIN_CARD_TO_TIME.HasValue && p.HEIN_CARD_TO_TIME_TMP.HasValue)
                        {
                            string newStr = string.Format("'\"HEIN_CARD_TO_TIME\":{0}'", p.HEIN_CARD_TO_TIME_TMP);
                            string oldStr = string.Format("'\"HEIN_CARD_TO_TIME\":{0}'", p.HEIN_CARD_TO_TIME.Value);
                            string sqlUpdateSs = string.Format("UPDATE HIS_SERE_SERV SET JSON_PATIENT_TYPE_ALTER = REPLACE (JSON_PATIENT_TYPE_ALTER, {0}, {1}) WHERE TDL_TREATMENT_ID = {2} AND HEIN_CARD_NUMBER IS NOT NULL AND JSON_PATIENT_TYPE_ALTER LIKE '%\"ID\":{3}%'", oldStr, newStr, treatment.ID, p.ID);
                            sqls.Add(sqlUpdateSs);
                        }
                    }
                }
            }
        }

        private void ProcessMediRecord(long? mediRecordId, ref List<string> sqls)
        {
            if (mediRecordId.HasValue)
            {
                List<HIS_TREATMENT> treatments = new HisTreatmentGet().GetByMediRecordId(mediRecordId.Value);

                if (!IsNotNullOrEmpty(treatments))
                {
                    string deleteMediRecord = string.Format("DELETE FROM HIS_MEDI_RECORD WHERE ID = {0}", mediRecordId.Value);
                    sqls.Add(deleteMediRecord);
                }
            }
        }

        /// <summary>
        /// Tinh tai cac thong tin lien quan den gia cua ho so
        /// (do co nghiep vu cap nhat lai gia tien dich vu kham khi huy ket thuc dieu tri)
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="ptas"></param>
        /// <param name="existsSereServs"></param>
        private void ProcessRecalcExamSereServ(HIS_TREATMENT treatment, ref List<HIS_PATIENT_TYPE_ALTER> ptas, ref List<HIS_SERE_SERV> existsSereServs)
        {
            if (treatment != null)
            {
                ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                existsSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);

                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                List<HIS_SERE_SERV> olds = Mapper.Map<List<HIS_SERE_SERV>>(existsSereServs);

                if (IsNotNullOrEmpty(ptas) && IsNotNullOrEmpty(existsSereServs))
                {
                    List<HIS_SERE_SERV> exams = existsSereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH).ToList();

                    //Chi check cac dich vu kham
                    if (IsNotNullOrEmpty(exams))
                    {
                        HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);

                        foreach (HIS_SERE_SERV s in exams)
                        {
                            priceAdder.AddPrice(s, existsSereServs, s.TDL_INTRUCTION_TIME, s.TDL_EXECUTE_BRANCH_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_EXECUTE_ROOM_ID);
                        }

                        this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false);
                        if (!this.hisSereServUpdateHein.UpdateDb(olds, existsSereServs))
                        {
                            throw new Exception("Cap nhat du lieu sere_serv that bai. Rollback du lieu");
                        }
                    }

                }
            }
        }

        private void RollBack()
        {
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}
