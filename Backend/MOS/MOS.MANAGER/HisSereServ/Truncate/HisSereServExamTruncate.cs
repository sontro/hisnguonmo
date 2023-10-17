using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Truncate
{
    class HisSereServExamTruncate : BusinessBase
    {
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisSereServUpdateSql hisSereServUpdateSql;

        internal HisSereServExamTruncate()
            : base()
        {
            this.hisSereServUpdateSql = new HisSereServUpdateSql(param);
        }

        internal HisSereServExamTruncate(CommonParam param)
            : base(param)
        {
            this.hisSereServUpdateSql = new HisSereServUpdateSql(param);
        }

        internal bool Run(long sereServId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERE_SERV raw = null;
                HIS_SERVICE_REQ req = null;
                HIS_TREATMENT treat = null;
                HisSereServExamTruncateCheck checker = new HisSereServExamTruncateCheck(param);
                HisSereServCheck commonChecker = new HisSereServCheck(param);
                HisServiceReqCheck reqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);

                valid = valid && commonChecker.VerifyId(sereServId, ref raw);
                valid = valid && commonChecker.IsUnLock(raw);
                valid = valid && reqChecker.VerifyId(raw.SERVICE_REQ_ID ?? 0, ref req);
                valid = valid && reqChecker.IsUnLock(req);
                valid = valid && reqChecker.IsNotFinished(req);
                valid = valid && checker.IsExam(raw);
                valid = valid && checker.IsCreatorOrAdmin(req);
                valid = valid && reqChecker.IsAllowNotChoiceService(req);
                valid = valid && treatChecker.IsUnLock(req.TREATMENT_ID, ref treat);
                valid = valid && treatChecker.IsUnTemporaryLock(treat);
                valid = valid && treatChecker.IsUnpause(treat);
                valid = valid && treatChecker.IsUnLockHein(treat);
                valid = valid && commonChecker.HasNoInvoice(raw);//Chi cho phep xoa doi voi cac sere_serv chua co invoice
                valid = valid && commonChecker.HasNoBill(raw);//Chi cho phep xoa doi voi cac sere_serv chua co invoice
                valid = valid && commonChecker.HasNoHeinApproval(raw); //da duyet ho so Bao hiem thi ko cho phep sua
                valid = valid && commonChecker.HasNoDeposit(raw.ID, false);
                valid = valid && commonChecker.HasNoDebt(new List<long>() { raw.ID });
                if (valid)
                {
                    List<HIS_SERE_SERV> allSereServs = new HisSereServGet().GetByTreatmentId(treat.ID);
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> listBeforeUpdate = Mapper.Map<List<HIS_SERE_SERV>>(allSereServs);
                    HIS_SERE_SERV delete = allSereServs.FirstOrDefault(o => o.ID == raw.ID);
                    //set lai cac truong nay ve null tranh truong hop nguoi dung thuc hien xoa phieu nhap (xoa medicine/material/blood)
                    //==> se ko xoa duoc vi bi FK (do sere_serv ko xoa tren DB ma chi update is_delete = 1)
                    delete.MEDICINE_ID = null;
                    delete.MATERIAL_ID = null;
                    delete.BLOOD_ID = null;
                    delete.EXP_MEST_MATERIAL_ID = null;
                    delete.EXP_MEST_MEDICINE_ID = null;
                    delete.IS_DELETE = Constant.IS_TRUE;
                    delete.SERVICE_REQ_ID = null;

                    //Bo dinh kem cac dich vu dinh kem
                    List<HIS_SERE_SERV> children = allSereServs
                        .Where(o => o.PARENT_ID.HasValue && o.PARENT_ID.Value == delete.ID)
                        .ToList();

                    if (IsNotNullOrEmpty(children))
                    {
                        children.ForEach(o => o.PARENT_ID = null);
                    }

                    //- Trong truong hop danh sach xoa co stent thi thuc hien gan lai gia cho cac stent con lai
                    // (Do theo BHYT co quy dinh ve cach tinh gia cua stent thu 2)
                    //- Trong truong hop danh sach xoa co cong kham thi thuc hien tinh lai gia cho cac dv kham con lai
                    // (do chinh sach gia co quy dinh ve cach tinh gia cong kham theo luot chi dinh)

                    List<long> materialIds = allSereServs != null ? allSereServs
                        .Where(o => o.MATERIAL_ID.HasValue && HisMaterialTypeCFG.IsStentByServiceId(o.SERVICE_ID)).Select(o => o.MATERIAL_ID.Value).ToList() : null;

                    HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treat, null, materialIds);

                    foreach (HIS_SERE_SERV s in allSereServs)
                    {
                        if (s.ID != delete.ID &&
                            (HisMaterialTypeCFG.IsStentByServiceId(s.SERVICE_ID) || s.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH))
                        {
                            priceAdder.AddPrice(s, allSereServs, s.TDL_INTRUCTION_TIME, s.TDL_EXECUTE_BRANCH_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_EXECUTE_ROOM_ID);
                        }
                    }

                    //Cap nhat ti le BHYT cho sere_serv
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treat, false);

                    if (this.hisSereServUpdateHein.Update(allSereServs))
                    {
                        List<HIS_SERE_SERV> changes = null;
                        List<HIS_SERE_SERV> oldOfChanges = null;

                        HisSereServUtil.GetChangeRecord(listBeforeUpdate, allSereServs, ref changes, ref oldOfChanges);

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


                    //pttt co the co voi cac loai dv nen ko check loai
                    string sqlSereServPttt = String.Format("UPDATE HIS_SERE_SERV_PTTT SET IS_DELETE = 1, TDL_TREATMENT_ID = NULL WHERE SERE_SERV_ID = {0}", delete.ID);
                    string sqlSereServFile = String.Format("UPDATE HIS_SERE_SERV_FILE SET IS_DELETE = 1 WHERE SERE_SERV_ID = {0} ", delete.ID);
                    string sqlSereServExt = String.Format("UPDATE HIS_SERE_SERV_EXT SET IS_DELETE = 1, BED_LOG_ID = NULL, TDL_SERVICE_REQ_ID = NULL, TDL_TREATMENT_ID = NULL WHERE SERE_SERV_ID = {0} ", delete.ID);
                    string sqlserviceReq = String.Format("UPDATE HIS_SERVICE_REQ SET TDL_SERVICE_IDS = NULL WHERE ID = {0}", req.ID);

                    sqls.Add(sqlSereServPttt);
                    sqls.Add(sqlSereServFile);
                    sqls.Add(sqlSereServExt);
                    sqls.Add(sqlserviceReq);

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Update is_delete = 1 voi HIS_SERE_SERV_PTTT, HIS_SERE_SERV_FILE, HIS_SERE_SERV_EXT that bai.");
                    }

                    new EventLogGenerator(EventLog.Enum.HisSereServ_XoaDichVuKham, string.Format("{0} - {1}", raw.TDL_SERVICE_CODE, raw.TDL_SERVICE_NAME))
                            .TreatmentCode(req.TDL_TREATMENT_CODE)
                            .ServiceReqCode(req.SERVICE_REQ_CODE)
                            .Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                this.Rollback();
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisSereServUpdateSql.Rollback();
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
        }
    }
}
