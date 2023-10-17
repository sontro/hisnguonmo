using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common.Delete;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisRationSchedule;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServFile;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisSereServReha;
using MOS.MANAGER.HisSereServSuin;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisServiceReq.Pacs;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTransReq.CreateByService;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Common.Truncate
{
    class HisServiceReqTruncate : BusinessBase
    {
        private HisServiceReqUpdate hisChildrenServiceReqUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServUpdateSql hisSereServUpdateSql;
        private HisSereServUpdate hisSereServUpdateExam;

        private HisSereServTeinDelete hisSereServTeinDelete;
        private HisSereServFileDelete hisSereServFileDelete;
        private HisSereServSuinDelete hisSereServSuinDelete;
        private HisSereServPtttDelete hisSereServPtttDelete;
        private HisSereServRehaDelete hisSereServRehaDelete;
        private HisSereServExtDelete hisSereServExtDelete;
        private HisTreatmentUpdate hisTreatmentUpdate;

        private HisSereServExtProcessor hisSereServExtProcessor;

        internal HisServiceReqTruncate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServUpdateSql = new HisSereServUpdateSql(param);
            this.hisChildrenServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServTeinDelete = new HisSereServTeinDelete(param);
            this.hisSereServFileDelete = new HisSereServFileDelete(param);
            this.hisSereServSuinDelete = new HisSereServSuinDelete(param);
            this.hisSereServPtttDelete = new HisSereServPtttDelete(param);
            this.hisSereServRehaDelete = new HisSereServRehaDelete(param);
            this.hisSereServExtDelete = new HisSereServExtDelete(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisSereServUpdateExam = new HisSereServUpdate(param);
            this.hisSereServExtProcessor = new HisSereServExtProcessor(param);
        }

        internal bool Truncate(HisServiceReqSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HIS_TREATMENT hisTreatment = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisServiceReqTruncateCheck truncateChecker = new HisServiceReqTruncateCheck(param);
                HIS_SERVICE_REQ raw = null;
                HIS_EXP_MEST expMest = null;
                List<HIS_SERE_SERV> allSereServsOfTreatment = null;
                List<HIS_SERE_SERV> sereServsToDelete = null;
                List<HIS_SERVICE_REQ> serviceReqFollows = null;
                List<HIS_EXP_MEST> expMestFollows = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && (data.Id.HasValue && data.RequestRoomId > 0);
                valid = valid && checker.VerifyId(data.Id.Value, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && truncateChecker.IsAllow(data, raw);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                //valid = valid && checker.HasNoSaleExpMest(raw);
                valid = valid && checker.IsNotAprovedSurgeryRemuneration(raw);
                valid = valid && truncateChecker.IsAllowStatusForDelete(raw);
                valid = valid && treatmentChecker.IsUnLock(raw.TREATMENT_ID, ref hisTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                valid = valid && truncateChecker.IsValidPrescription(raw, ref expMest);
                valid = valid && truncateChecker.IsValidSereServ(raw, ref allSereServsOfTreatment, ref sereServsToDelete);
                valid = valid && truncateChecker.IsValidServiceFollow(raw, allSereServsOfTreatment, ref serviceReqFollows, ref sereServsToDelete, ref expMestFollows);
                valid = valid && checker.HasNoTempBed(sereServsToDelete);

                if (valid)
                {
                    List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByTreatmentId(raw.TREATMENT_ID);
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    HIS_TREATMENT treatmentBefore = Mapper.Map<HIS_TREATMENT>(hisTreatment); //phuc vu rollback
                    List<long> deleteSereServIds = new List<long>();
                    List<long> deletedExpMestMaterialIds = IsNotNullOrEmpty(sereServsToDelete) ? sereServsToDelete.Where(o => o.EXP_MEST_MATERIAL_ID.HasValue).Select(o => o.EXP_MEST_MATERIAL_ID.Value).ToList() : null;
                    List<long> parentIdsOfDeleted = IsNotNullOrEmpty(sereServsToDelete) ? sereServsToDelete.Where(o => o.PARENT_ID.HasValue).Select(o => o.PARENT_ID.Value).ToList() : null;

                    this.ProcessSereServ(allSereServsOfTreatment, sereServsToDelete, raw, hisTreatment, ref deleteSereServIds);
                    this.hisSereServExtProcessor.Run(raw, deletedExpMestMaterialIds, parentIdsOfDeleted);//Can xu ly truoc khi xoa du lieu phieu xuat
                    this.ProcessDetail(data, raw, serviceReqs, sereServsToDelete, expMest, hisTreatment, expMestFollows);
                    this.ProcessTreatment(raw, treatmentBefore, hisTreatment, serviceReqs);
                    this.ProcessOther(raw, deleteSereServIds);

                    //chuyen sang dung "delete" (update is_delete = 1) de tranh anh huong hieu nang
                    //do xoa thi DB phai quet toan bo de danh lai index --> I/O se rat lon
                    List<HIS_SERVICE_REQ> reqDeleteList = new List<HIS_SERVICE_REQ>() { raw };
                    if (IsNotNullOrEmpty(serviceReqFollows))
                    {
                        reqDeleteList.AddRange(serviceReqFollows);
                    }

                    if (DAOWorker.HisServiceReqDAO.DeleteList(reqDeleteList))
                    {
                        if (raw.IS_SENT_EXT == Constant.IS_TRUE)
                        {
                            string sql = string.Format("UPDATE HIS_SERVICE_REQ SET IS_UPDATED_EXT = 1 WHERE ID = {0}", raw.ID);
                            if (!DAOWorker.SqlDAO.Execute(sql))
                            {
                                LogSystem.Warn(string.Format("Update truong is_update_ext sau khi xoa his_service_req id ({0}) that bai", raw.ID));
                            }
                        }
                        this.ProcessorRationSachedule(raw, serviceReqs);
                       
                        result = true;
                        string expMestCode = expMest != null ? expMest.EXP_MEST_CODE : null;
                        new EventLogGenerator(EventLog.Enum.HisServiceReq_XoaYLenh)
                            .TreatmentCode(raw.TDL_TREATMENT_CODE)
                            .ServiceReqCode(raw.SERVICE_REQ_CODE)
                            .ExpMestCode(expMestCode)
                            .Run();
                    }
                    if (!new HisTransReqCreateByService(param).Run(hisTreatment, null, workPlace))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                        Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param));
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessTreatment(HIS_SERVICE_REQ raw, HIS_TREATMENT treatmentBefore, HIS_TREATMENT hisTreatment, List<HIS_SERVICE_REQ> serviceReqs)
        {
            if (raw != null && hisTreatment != null)
            {
                //Kiem tra xem trong cac y lenh con lai, co y lenh nao chua icd giong voi icd cua y lenh bi xoa khong
                //Neu khong thi thuc hien remove khoi ho so dieu tri
                List<HIS_SERVICE_REQ> remains = serviceReqs != null ? serviceReqs.Where(o => o.ID != raw.ID).ToList() : null;
                if (IsNotNullOrEmpty(remains))
                {
                    string icdCode = CommonUtil.ToUpper(raw.ICD_CODE);
                    string icdName = raw.ICD_NAME;

                    if (raw.ICD_SUB_CODE != null)
                    {
                        icdCode = icdCode + ";" + CommonUtil.ToUpper(raw.ICD_SUB_CODE);
                    }

                    if (raw.ICD_TEXT != null)
                    {
                        icdName = icdName + ";" + raw.ICD_TEXT;
                    }

                    //Duyet cac y lenh con lai
                    //Loai cac icd co trong cac y lenh nay, ra khoi danh sach ICD cua y lenh bi xoa
                    //==> icd con lai sau khi duyet chinh la ICD can phai remove khoi ho so dieu tri
                    foreach (HIS_SERVICE_REQ req in remains)
                    {
                        icdCode = HisIcdUtil.Remove(icdCode, CommonUtil.ToUpper(req.ICD_CODE));
                        icdCode = HisIcdUtil.RemoveInList(icdCode, CommonUtil.ToUpper(req.ICD_SUB_CODE));
                        icdName = HisIcdUtil.Remove(icdName, req.ICD_NAME);
                        icdName = HisIcdUtil.RemoveInList(icdName, req.ICD_TEXT);
                    }

                    //Chi remove o benh phu, tranh bi loi mat ICD benh chinh
                    hisTreatment.ICD_SUB_CODE = HisIcdUtil.RemoveInList(CommonUtil.ToUpper(hisTreatment.ICD_SUB_CODE), icdCode);
                    hisTreatment.ICD_TEXT = HisIcdUtil.RemoveInList(hisTreatment.ICD_TEXT, icdName);

                    if (ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(treatmentBefore, hisTreatment))
                    {
                        if (!this.hisTreatmentUpdate.Update(hisTreatment, treatmentBefore))
                        {
                            throw new Exception("Cap nhat ICD cua treatment that bai");
                        }
                    }
                }
            }
        }

        private void ProcessSereServ(List<HIS_SERE_SERV> allSereServsOfTreatment, List<HIS_SERE_SERV> sereServsToDelete, HIS_SERVICE_REQ raw, HIS_TREATMENT treatment, ref List<long> deleteSsIds)
        {
            if (IsNotNullOrEmpty(allSereServsOfTreatment))
            {
                if (IsNotNullOrEmpty(sereServsToDelete))
                {
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> listBeforeUpdate = Mapper.Map<List<HIS_SERE_SERV>>(allSereServsOfTreatment);

                    //set lai cac truong nay ve null tranh truong hop nguoi dung thuc hien xoa phieu nhap (xoa medicine/material/blood)
                    //==> se ko xoa duoc vi bi FK (do sere_serv ko xoa tren DB ma chi update is_delete = 1)
                    sereServsToDelete.ForEach(o =>
                    {
                        o.MEDICINE_ID = null;
                        o.MATERIAL_ID = null;
                        o.BLOOD_ID = null;
                        o.EXP_MEST_MATERIAL_ID = null;
                        o.EXP_MEST_MEDICINE_ID = null;
                        o.IS_DELETE = Constant.IS_TRUE;
                        o.SERVICE_REQ_ID = null;
                    });

                    var deleteSereServIds = sereServsToDelete.Select(o => o.ID).ToList();
                    deleteSsIds = deleteSereServIds;

                    //Bo dinh kem cac dich vu dinh kem
                    List<HIS_SERE_SERV> children = allSereServsOfTreatment
                        .Where(o => o.PARENT_ID.HasValue && deleteSereServIds.Contains(o.PARENT_ID.Value))
                        .ToList();
                    if (IsNotNullOrEmpty(children))
                    {
                        children.ForEach(o => o.PARENT_ID = null);
                    }

                    //- Trong truong hop danh sach xoa co stent thi thuc hien gan lai gia cho cac stent con lai
                    // (Do theo BHYT co quy dinh ve cach tinh gia cua stent thu 2)
                    //- Trong truong hop danh sach xoa co cong kham thi thuc hien tinh lai gia cho cac dv kham con lai
                    // (do chinh sach gia co quy dinh ve cach tinh gia cong kham theo luot chi dinh)
                    if (sereServsToDelete.Exists(t => HisMaterialTypeCFG.IsStentByServiceId(t.SERVICE_ID)
                        || t.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH))
                    {
                        List<long> materialIds = allSereServsOfTreatment != null ? allSereServsOfTreatment
                            .Where(o => o.MATERIAL_ID.HasValue && HisMaterialTypeCFG.IsStentByServiceId(o.SERVICE_ID)).Select(o => o.MATERIAL_ID.Value).ToList() : null;

                        HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, materialIds);

                        foreach (HIS_SERE_SERV s in allSereServsOfTreatment)
                        {
                            if (!deleteSereServIds.Contains(s.ID) &&
                                (HisMaterialTypeCFG.IsStentByServiceId(s.SERVICE_ID) || s.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH))
                            {
                                priceAdder.AddPrice(s, allSereServsOfTreatment, s.TDL_INTRUCTION_TIME, s.TDL_EXECUTE_BRANCH_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_EXECUTE_ROOM_ID);
                            }
                        }
                    }

                    //Cap nhat ti le BHYT cho sere_serv
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);

                    if (this.hisSereServUpdateHein.Update(allSereServsOfTreatment))
                    {
                        List<HIS_SERE_SERV> changes = null;
                        List<HIS_SERE_SERV> oldOfChanges = null;

                        HisSereServUtil.GetChangeRecord(listBeforeUpdate, allSereServsOfTreatment, ref changes, ref oldOfChanges);

                        if (IsNotNullOrEmpty(changes))
                        {
                            //Neu co ban ghi thay doi gia tri thi thuc hien cap nhat
                            if (!this.hisSereServUpdateSql.Run(changes, oldOfChanges))
                            {
                                throw new Exception("Update HIS_SERE_SERV that bai");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("hisSereServUpdateHein. Ket thuc nghiep vu.");
                    }

                    List<string> sqls = new List<string>();

                    if (sereServsToDelete.Exists(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN))
                    {
                        string sqlSereServTein = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_SERE_SERV_TEIN SET IS_DELETE = 1, TDL_TREATMENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                        sqls.Add(sqlSereServTein);
                    }
                    if (sereServsToDelete.Exists(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA))
                    {
                        string sqlSereServSuin = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_SERE_SERV_SUIN SET IS_DELETE = 1 WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                        sqls.Add(sqlSereServSuin);
                    }
                    if (sereServsToDelete.Exists(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__PHCN))
                    {
                        string sqlSereServReha = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_SERE_SERV_REHA SET IS_DELETE = 1 WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                        sqls.Add(sqlSereServReha);
                    }

                    //pttt co the co voi cac loai dv nen ko check loai
                    string sqlSereServPttt = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_SERE_SERV_PTTT SET IS_DELETE = 1, TDL_TREATMENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                    string sqlSereServFile = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_SERE_SERV_FILE SET IS_DELETE = 1 WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                    string sqlSereServExt = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_SERE_SERV_EXT SET IS_DELETE = 1, BED_LOG_ID = NULL, TDL_SERVICE_REQ_ID = NULL, TDL_TREATMENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                    sqls.Add(sqlSereServPttt);
                    sqls.Add(sqlSereServFile);
                    sqls.Add(sqlSereServExt);

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Update is_delete = 1 voi HIS_SERE_SERV_TEIN, HIS_SERE_SERV_SUIN, HIS_SERE_SERV_REHA, HIS_SERE_SERV_PTTT, HIS_SERE_SERV_FILE, HIS_SERE_SERV_EXT that bai.");
                    }
                }
            }
        }

        private void ProcessOther(HIS_SERVICE_REQ serviceReq, List<long> deleteSsIds)
        {
            List<string> sqls = new List<string>();

            if (IsNotNullOrEmpty(deleteSsIds))
            {
                string sqlMedicine = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_EXP_MEST_MEDICINE SET SERE_SERV_PARENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_PARENT_ID");
                string sqlMaterial = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_EXP_MEST_MATERIAL SET SERE_SERV_PARENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_PARENT_ID");
                string sqlBlood = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_EXP_MEST_BLOOD SET SERE_SERV_PARENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_PARENT_ID");
                string sqlBltyReq = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_EXP_MEST_BLTY_REQ SET SERE_SERV_PARENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_PARENT_ID");

                sqls.Add(sqlMedicine);
                sqls.Add(sqlMaterial);
                sqls.Add(sqlBlood);
                sqls.Add(sqlBltyReq);
            }
            if (serviceReq.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G)
            {
                string sqlBedLog = string.Format("UPDATE HIS_BED_LOG SET SERVICE_REQ_ID = NULL WHERE SERVICE_REQ_ID = {0}", serviceReq.ID);
                sqls.Add(sqlBedLog);
            }

            string sqlSereServBill = string.Format("UPDATE HIS_SERE_SERV_BILL SET TDL_SERVICE_REQ_ID = NULL WHERE TDL_SERVICE_REQ_ID = {0}", serviceReq.ID);
            string sqlSereServDeposit = string.Format("UPDATE HIS_SERE_SERV_DEPOSIT SET TDL_SERVICE_REQ_ID = NULL WHERE TDL_SERVICE_REQ_ID = {0}", serviceReq.ID);
            string sqlSeseDepoRepay = string.Format("UPDATE HIS_SESE_DEPO_REPAY SET TDL_SERVICE_REQ_ID = NULL WHERE TDL_SERVICE_REQ_ID = {0}", serviceReq.ID);
            string sqlExamSereDire = string.Format("DELETE HIS_EXAM_SERE_DIRE WHERE SERVICE_REQ_ID = {0}", serviceReq.ID);

            sqls.Add(sqlSereServBill);
            sqls.Add(sqlSereServDeposit);
            sqls.Add(sqlSeseDepoRepay);
            sqls.Add(sqlExamSereDire);

            if (!DAOWorker.SqlDAO.Execute(sqls))
            {
                throw new Exception("Update TDL_SERVICE_REQ_ID cua HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_BLOOD,  HIS_EXP_MEST_BLTY_REQ, HIS_SERE_SERV_BILL, HIS_SERE_SERV_DEPOSIT, HIS_SESE_DEPO_REPAY, HIS_EXAM_SERE_DIRE that bai.");
            }
        }

        private void ProcessHeinSereServ(HIS_SERVICE_REQ data, List<HIS_SERE_SERV> sereServs, HIS_TREATMENT hisTreatment)
        {
            if (IsNotNullOrEmpty(sereServs))
            {
                //Lúc xóa serviceReq Oracle tự động set null Service_req_id tu dong cap nhat thong tin gia 
                //va bao hiem cua cac sere_serv trong treatment (ko can validate treatment, vi da validate o tren)
                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, hisTreatment, false);
                if (!this.hisSereServUpdateHein.UpdateDb())
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisPatientTypeAlter_CapNhatGiaVaThongTinBaoHiemChoDichVuThatBai);
                    throw new Exception();
                }
            }
        }

        /// <summary>
        /// Xoa du lieu chi tiet don thuoc, he thong tich hop LIS, PACS
        /// Luu y, ham nay co the goi truoc hoac sau khi tao cac du lieu chi tiet. Vi the, can check du lieu chi tiet co khac null khong thi moi thuc hien xoa
        /// </summary>
        /// <param name="data"></param>
        private void ProcessDetail(HisServiceReqSDO data, HIS_SERVICE_REQ raw, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> sereServs, HIS_EXP_MEST expMest, HIS_TREATMENT hisTreatment, List<HIS_EXP_MEST> expMestFollow)
        {
            if (HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(raw.SERVICE_REQ_TYPE_ID))
            {
                List<string> sqls = new List<string>();
                sqls.Add(string.Format("DELETE FROM HIS_SERVICE_REQ_MATY WHERE SERVICE_REQ_ID = {0}", raw.ID));
                sqls.Add(string.Format("DELETE FROM HIS_SERVICE_REQ_METY WHERE SERVICE_REQ_ID = {0}", raw.ID));

                //Neu la don CLS thi cap nhat ca HIS_SERE_SERV_EXT
                if (raw.PRESCRIPTION_TYPE_ID == (short)PrescriptionType.SUBCLINICAL)
                {
                    sqls.Add(string.Format("UPDATE HIS_SERE_SERV_EXT SET SUBCLINICAL_PRES_ID = NULL WHERE SUBCLINICAL_PRES_ID = {0}", raw.ID));
                }

                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Xoa du lieu HIS_SERVICE_REQ_MATY, HIS_SERVICE_REQ_METY, update HIS_SERE_SERV_EXT that bai.");
                }

                if (IsNotNull(expMest))
                {
                    HisExpMestSDO sdo = new HisExpMestSDO();
                    sdo.ExpMestId = expMest.ID;
                    sdo.ReqRoomId = data.RequestRoomId;
                    if (!new HisExpMestTruncate(param).Truncate(sdo, true))
                    {
                        throw new Exception("Xoa du lieu bang chi tiet that bai.");
                    }
                }
            }
            else if (raw.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
            {
                this.ProcessExam(raw, serviceReqs, hisTreatment);
            }
            else if (raw.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
            {
                if (!new HisServiceReqTestTruncate(param).Truncate(raw, sereServs))
                {
                    throw new Exception("Xoa du lieu bang chi tiet that bai.");
                }
            }
            else if (HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(raw.SERVICE_REQ_TYPE_ID) || raw.ALLOW_SEND_PACS == Constant.IS_TRUE)
            {
                if (!new HisServiceReqPacsTruncate(param).Truncate(hisTreatment, raw, sereServs))
                {
                    throw new Exception("Xoa du lieu bang chi tiet that bai.");
                }
            }
            else if (IsNotNullOrEmpty(sereServs) && sereServs.Exists(o => o.SERVICE_REQ_ID != raw.ID))//xoa dich vu di kem
            {
                List<HIS_SERVICE_REQ> reqDelete = serviceReqs != null ? serviceReqs.Where(o => sereServs.Exists(e => e.SERVICE_REQ_ID != raw.ID && e.SERVICE_REQ_ID == o.ID)).ToList() : null;
                if (IsNotNullOrEmpty(reqDelete))
                {
                    foreach (var req in reqDelete)
                    {
                        List<HIS_SERE_SERV> listSereServ = sereServs.Where(o => o.SERVICE_REQ_ID == req.ID).ToList();
                        if (req.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                        {
                            if (!new HisServiceReqTestTruncate(param).Truncate(req, listSereServ))
                            {
                                throw new Exception("Xoa du lieu bang chi tiet that bai.");
                            }
                        }
                        else if (HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(req.SERVICE_REQ_TYPE_ID) || raw.ALLOW_SEND_PACS == Constant.IS_TRUE)
                        {
                            if (!new HisServiceReqPacsTruncate(param).Truncate(hisTreatment, req, listSereServ))
                            {
                                throw new Exception("Xoa du lieu bang chi tiet that bai.");
                            }
                        }
                    }
                }
            }

            if (IsNotNullOrEmpty(expMestFollow))
            {
                foreach (var exp in expMestFollow)
                {
                    HIS_SERVICE_REQ req = serviceReqs != null ? serviceReqs.FirstOrDefault(o => o.ID == exp.SERVICE_REQ_ID) : null;
                    if (IsNotNull(req))
                    {
                        List<string> sqls = new List<string>();
                        sqls.Add(string.Format("DELETE FROM HIS_SERVICE_REQ_MATY WHERE SERVICE_REQ_ID = {0}", req.ID));
                        sqls.Add(string.Format("DELETE FROM HIS_SERVICE_REQ_METY WHERE SERVICE_REQ_ID = {0}", req.ID));

                        //Neu la don CLS thi cap nhat ca HIS_SERE_SERV_EXT
                        if (req.PRESCRIPTION_TYPE_ID == (short)PrescriptionType.SUBCLINICAL)
                        {
                            sqls.Add(string.Format("UPDATE HIS_SERE_SERV_EXT SET SUBCLINICAL_PRES_ID = NULL WHERE SUBCLINICAL_PRES_ID = {0}", req.ID));
                        }

                        if (!DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("Xoa du lieu HIS_SERVICE_REQ_MATY, HIS_SERVICE_REQ_METY, update HIS_SERE_SERV_EXT that bai.");
                        }
                    }

                    HisExpMestSDO sdo = new HisExpMestSDO();
                    sdo.ExpMestId = exp.ID;
                    sdo.ReqRoomId = data.RequestRoomId;
                    if (!new HisExpMestTruncate(param).Truncate(sdo, true))
                    {
                        throw new Exception("Xoa du lieu bang chi tiet that bai.");
                    }
                }
            }
        }

        private void ProcessExam(HIS_SERVICE_REQ raw, List<HIS_SERVICE_REQ> serviceReqs, HIS_TREATMENT treatment)
        {
            try
            {
                if (raw.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                {
                    List<HIS_SERVICE_REQ> befores = new List<HIS_SERVICE_REQ>();
                    List<HIS_SERVICE_REQ> toUpdates = new List<HIS_SERVICE_REQ>();

                    //Cap nhat thong tin previous_service_req_id
                    List<HIS_SERVICE_REQ> nexts = new HisServiceReqGet().GetByPreviousId(raw.ID);
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    if (IsNotNullOrEmpty(nexts))
                    {
                        var tmp = Mapper.Map<List<HIS_SERVICE_REQ>>(nexts);
                        befores.AddRange(tmp);
                        nexts.ForEach(o => o.PREVIOUS_SERVICE_REQ_ID = null);
                        toUpdates.AddRange(nexts);
                    }

                    //Neu dv kham bi xoa la "kham chinh" thi chuyen "kham chinh" sang y lenh kham truoc do 
                    // hoac y lenh co thoi gian chi dinh nho nhat
                    //- Neu co previous_service_req_id thi lay theo previous_service_req_id
                    //- Neu ko co thi lay service_req co thoi gian y lenh nho nhat
                    if (raw.IS_MAIN_EXAM == Constant.IS_TRUE)
                    {
                        HIS_SERVICE_REQ previous = null;

                        if (raw.PREVIOUS_SERVICE_REQ_ID.HasValue)
                        {
                            previous = serviceReqs != null ? serviceReqs.Where(o => o.ID == raw.PREVIOUS_SERVICE_REQ_ID.Value).FirstOrDefault() : null;
                        }
                        else
                        {
                            previous = serviceReqs != null ? serviceReqs
                                .Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH
                                    && o.ID != raw.ID && o.IS_NO_EXECUTE != Constant.IS_TRUE)
                                    .OrderBy(t => t.INTRUCTION_TIME).FirstOrDefault() : null;
                        }

                        if (previous != null)
                        {
                            //Kiem tra xem prev co nam trong d/s update da co chua, neu co roi thi chi set lai truong
                            //is_main_exam, neu chua co thi set lai truong is_main_exam va bo sung vao d/s update
                            var prev = toUpdates.Where(t => t.ID == previous.ID).FirstOrDefault();
                            if (prev != null)
                            {
                                prev.IS_MAIN_EXAM = Constant.IS_TRUE;
                                List<HIS_SERE_SERV> ss = new HisSereServGet().GetByServiceReqId(prev.ID);
                            }
                            else
                            {
                                var tmp = Mapper.Map<HIS_SERVICE_REQ>(previous);
                                befores.Add(tmp);

                                previous.IS_MAIN_EXAM = Constant.IS_TRUE;
                                toUpdates.Add(previous);

                                List<HIS_SERE_SERV> ss = new HisSereServGet().GetByServiceReqId(previous.ID);
                            }

                            //Cap nhat lai ICD cua treatment theo dv kham chinh
                            string preIcdCode = previous.ICD_CODE != null ? previous.ICD_CODE.ToUpper() : null;
                            treatment.ICD_SUB_CODE = treatment.ICD_SUB_CODE != null ? treatment.ICD_SUB_CODE.ToUpper() : null;
                            treatment.ICD_CODE = preIcdCode;
                            treatment.ICD_NAME = previous.ICD_NAME;
                            treatment.ICD_SUB_CODE = HisIcdUtil.Remove(treatment.ICD_SUB_CODE, preIcdCode);
                            treatment.ICD_TEXT = HisIcdUtil.Remove(treatment.ICD_TEXT, previous.ICD_NAME);
                        }
                    }

                    if (IsNotNullOrEmpty(toUpdates) && !this.hisServiceReqUpdate.UpdateList(toUpdates, befores))
                    {
                        throw new Exception("Update is_main_exam/previous_sevice_req_id that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void ProcessorRationSachedule(HIS_SERVICE_REQ raw, List<HIS_SERVICE_REQ> serviceReqs)
        {
            try
            {
                if (raw.RATION_SCHEDULE_ID != null)
                {
                    HIS_RATION_SCHEDULE RationChedule = new HisRationScheduleGet().GetById(raw.RATION_SCHEDULE_ID ?? 0);
                    if (RationChedule != null && IsNotNullOrEmpty(serviceReqs))
                    {
                        if (raw.INTRUCTION_DATE == RationChedule.LAST_ASSIGN_DATE)
                        {
                            List<HIS_SERVICE_REQ> serviceReqRationSches = serviceReqs.Where(p => p.RATION_SCHEDULE_ID == raw.RATION_SCHEDULE_ID && p.ID != raw.ID).ToList();
                            long? maxIntructionTime = null;
                            if (IsNotNullOrEmpty(serviceReqRationSches))
                            {
                                maxIntructionTime = serviceReqRationSches.Max(o => o.INTRUCTION_DATE);
                            }
                            string query = "UPDATE HIS_RATION_SCHEDULE SET LAST_ASSIGN_DATE =:param1 WHERE ID =:param2";
                            if (!DAOWorker.SqlDAO.Execute(query, maxIntructionTime, RationChedule.ID))
                            {
                                LogSystem.Warn(string.Format("Update truong LAST_ASSIGN_DATE trong HIS_RATION_SCHEDULE sau khi xoa his_service_req that bai"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }

        internal void Rollback()
        {
            this.hisSereServExtProcessor.Rollback();
            this.hisSereServUpdateSql.Rollback();
            this.hisChildrenServiceReqUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
            this.hisSereServUpdateExam.RollbackData();
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
        }
    }
}
