using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ.Update;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.TruncateReq
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServUpdateSql hisSereServUpdateSql;

        internal SereServProcessor()
            : base()
        {
            this.hisSereServUpdateSql = new HisSereServUpdateSql(param);
        }

        internal SereServProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServUpdateSql = new HisSereServUpdateSql(param);
        }

        internal bool Run(List<HIS_SERVICE_REQ> lstServiceReqTruncate, List<HIS_SERE_SERV> lstSereServTruncate, List<HIS_SERE_SERV> lstAllSereServs, HIS_TREATMENT treatment)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(lstServiceReqTruncate))
                {
                    List<HIS_SERE_SERV> updates = new List<HIS_SERE_SERV>();
                    updates.AddRange(lstSereServTruncate);

                    var deleteSereServIds = lstSereServTruncate.Select(o => o.ID).ToList();
                    //deleteSsIds = deleteSereServIds;

                    //Bo dinh kem cac dich vu dinh kem
                    List<HIS_SERE_SERV> children = lstAllSereServs
                        .Where(o => o.PARENT_ID.HasValue && deleteSereServIds.Contains(o.PARENT_ID.Value))
                        .ToList();
                    if (IsNotNullOrEmpty(children))
                    {
                        updates.AddRange(children);
                    }

                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV>>(updates);

                    lstSereServTruncate.ForEach(o =>
                    {
                        o.MEDICINE_ID = null;
                        o.MATERIAL_ID = null;
                        o.BLOOD_ID = null;
                        o.EXP_MEST_MATERIAL_ID = null;
                        o.EXP_MEST_MEDICINE_ID = null;
                        o.IS_DELETE = Constant.IS_TRUE;
                        o.SERVICE_REQ_ID = null;
                    });

                    if (IsNotNullOrEmpty(children)) children.ForEach(o => o.PARENT_ID = null);

                    if (!this.hisSereServUpdateSql.Run(updates, beforeUpdates))
                    {
                        throw new Exception("Update HIS_SERE_SERV that bai");
                    }

                    List<string> sqls = new List<string>();
                    List<HIS_SERE_SERV> tests = lstSereServTruncate.Where(o => o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN).ToList();
                    if (IsNotNullOrEmpty(tests))
                    {
                        string sqlSereServTein = DAOWorker.SqlDAO.AddInClause(tests.Select(s => s.ID).ToList(), "UPDATE HIS_SERE_SERV_TEIN SET IS_DELETE = 1, TDL_TREATMENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                        sqls.Add(sqlSereServTein);
                    }

                    List<HIS_SERE_SERV> sieuAms = lstSereServTruncate.Where(o => o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA).ToList();
                    if (IsNotNullOrEmpty(sieuAms))
                    {
                        string sqlSereServSuin = DAOWorker.SqlDAO.AddInClause(sieuAms.Select(s => s.ID).ToList(), "UPDATE HIS_SERE_SERV_SUIN SET IS_DELETE = 1 WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                        sqls.Add(sqlSereServSuin);
                    }

                    List<HIS_SERE_SERV> phcns = lstSereServTruncate.Where(o => o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PHCN).ToList();
                    if (IsNotNullOrEmpty(phcns))
                    {
                        string sqlSereServReha = DAOWorker.SqlDAO.AddInClause(phcns.Select(s => s.ID).ToList(), "UPDATE HIS_SERE_SERV_REHA SET IS_DELETE = 1 WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                        sqls.Add(sqlSereServReha);
                    }

                    //pttt co the co voi cac loai dv nen ko check loai
                    string sqlSereServPttt = DAOWorker.SqlDAO.AddInClause(deleteSereServIds, "UPDATE HIS_SERE_SERV_PTTT SET IS_DELETE = 1, TDL_TREATMENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                    string sqlSereServFile = DAOWorker.SqlDAO.AddInClause(deleteSereServIds, "UPDATE HIS_SERE_SERV_FILE SET IS_DELETE = 1 WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                    string sqlSereServExt = DAOWorker.SqlDAO.AddInClause(deleteSereServIds, "UPDATE HIS_SERE_SERV_EXT SET IS_DELETE = 1, BED_LOG_ID = NULL, TDL_SERVICE_REQ_ID = NULL, TDL_TREATMENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                    sqls.Add(sqlSereServPttt);
                    sqls.Add(sqlSereServFile);
                    sqls.Add(sqlSereServExt);

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Update is_delete = 1 voi HIS_SERE_SERV_TEIN, HIS_SERE_SERV_SUIN, HIS_SERE_SERV_REHA, HIS_SERE_SERV_PTTT, HIS_SERE_SERV_FILE, HIS_SERE_SERV_EXT that bai.");
                    }
                    result = true;
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisSereServUpdateSql.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
