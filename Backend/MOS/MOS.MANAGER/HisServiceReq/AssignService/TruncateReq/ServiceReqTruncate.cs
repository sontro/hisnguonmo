using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.TruncateReq
{
    class ServiceReqTruncate : BusinessBase
    {
        private SereServProcessor sereServProcessor;
        private SereServExtProcessor sereServExtProcessor;
        private DetailProcessor detailProcessor;
        private OtherProcessor otherProcessor;


        private List<HIS_SERVICE_REQ> recentServiceReqs = null;

        internal ServiceReqTruncate()
            : base()
        {
            this.Init();
        }

        internal ServiceReqTruncate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.sereServProcessor = new SereServProcessor(param);
            this.sereServExtProcessor = new SereServExtProcessor(param);
            this.detailProcessor = new DetailProcessor(param);
            this.otherProcessor = new OtherProcessor(param);
        }

        internal bool Run(List<HIS_SERVICE_REQ> lstServiceReqTruncate, List<HIS_SERE_SERV> lstSereServTruncate, List<HIS_SERE_SERV> allSereServs, HIS_TREATMENT treatment, bool notVerify = false)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(lstServiceReqTruncate))
                {
                    bool valid = true;
                    List<HIS_SERVICE_REQ> beforeUpdates = null;
                    AutoMapper.Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    beforeUpdates = AutoMapper.Mapper.Map<List<HIS_SERVICE_REQ>>(lstServiceReqTruncate);

                    ServiceReqTruncateCheck checker = new ServiceReqTruncateCheck(param);
                    valid = valid && (notVerify || checker.Verify(lstServiceReqTruncate, lstSereServTruncate, treatment));
                    if (valid)
                    {
                        if (!this.sereServProcessor.Run(lstServiceReqTruncate, lstSereServTruncate, allSereServs, treatment))
                        {
                            throw new Exception("sereServProcessor. Ket thuc nghiep vu");
                        }

                        if (!this.sereServExtProcessor.Run(lstServiceReqTruncate, lstSereServTruncate))
                        {
                            throw new Exception("sereServExtProcessor. Ket thuc nghiep vu");
                        }

                        if (!this.detailProcessor.Run(lstServiceReqTruncate, lstSereServTruncate, treatment))
                        {
                            throw new Exception("detailProcessor. Ket thuc nghiep vu");
                        }

                        if (!this.otherProcessor.Run(lstServiceReqTruncate, lstSereServTruncate))
                        {
                            throw new Exception("otherProcessor. Ket thuc nghiep vu");
                        }

                        string sql = DAOWorker.SqlDAO.AddInClause(lstServiceReqTruncate.Select(s => s.ID).ToList(), "UPDATE HIS_SERVICE_REQ SET IS_DELETE = 1 WHERE %IN_CLAUSE% ", "ID");
                        if (!DAOWorker.SqlDAO.Execute(sql))
                        {
                            throw new Exception("Xoa HIS_SERVICE_REQ that bai");
                        }
                        this.recentServiceReqs = beforeUpdates;
                        result = true;
                    }
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
                if (IsNotNullOrEmpty(this.recentServiceReqs))
                {
                    List<string> sqls = new List<string>();
                    foreach (HIS_SERVICE_REQ item in this.recentServiceReqs)
                    {
                        StringBuilder sb = new StringBuilder()
                        .Append("UPDATE HIS_SERVICE_REQ SET IS_DELETE = 0");
                        if (item.TRACKING_ID.HasValue)
                        {
                            sb.Append(String.Format(", TRACKING_ID = {0}", item.TRACKING_ID.Value));
                        }
                        if (item.EKIP_PLAN_ID.HasValue)
                        {
                            sb.Append(String.Format(", EKIP_PLAN_ID = {0}", item.EKIP_PLAN_ID.Value));
                        }
                        if (item.EXECUTE_GROUP_ID.HasValue)
                        {
                            sb.Append(String.Format(", EXECUTE_GROUP_ID = {0}", item.EXECUTE_GROUP_ID.Value));
                        }
                        if (item.EXE_SERVICE_MODULE_ID.HasValue)
                        {
                            sb.Append(String.Format(", EXE_SERVICE_MODULE_ID = {0}", item.EXE_SERVICE_MODULE_ID.Value));
                        }
                        if (item.EXP_MEST_TEMPLATE_ID.HasValue)
                        {
                            sb.Append(String.Format(", EXP_MEST_TEMPLATE_ID = {0}", item.EXP_MEST_TEMPLATE_ID.Value));
                        }
                        if (item.HEALTH_EXAM_RANK_ID.HasValue)
                        {
                            sb.Append(String.Format(", HEALTH_EXAM_RANK_ID = {0}", item.HEALTH_EXAM_RANK_ID.Value));
                        }

                        sb.Append(String.Format(" WHERE ID = {0}", item.ID));
                        sqls.Add(sb.ToString());
                    }

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        LogSystem.Error("Rollback HIS_SERVICE_REQ that bai, Kiem tra lai du lieu");
                    }
                }
                this.sereServExtProcessor.Rollback();
                this.sereServProcessor.Rollback();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
