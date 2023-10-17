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
    class OtherProcessor : BusinessBase
    {
        internal OtherProcessor()
            : base()
        {

        }

        internal OtherProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(List<HIS_SERVICE_REQ> lstServiceReqTruncate, List<HIS_SERE_SERV> lstSereServTruncate)
        {
            bool result = false;
            try
            {
                List<string> sqls = new List<string>();

                if (IsNotNullOrEmpty(lstSereServTruncate))
                {
                    List<long> deleteSsIds = lstSereServTruncate.Select(s => s.ID).ToList();
                    string sqlMedicine = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_EXP_MEST_MEDICINE SET SERE_SERV_PARENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_PARENT_ID");
                    string sqlMaterial = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_EXP_MEST_MATERIAL SET SERE_SERV_PARENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_PARENT_ID");
                    string sqlBlood = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_EXP_MEST_BLOOD SET SERE_SERV_PARENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_PARENT_ID");
                    string sqlBltyReq = DAOWorker.SqlDAO.AddInClause(deleteSsIds, "UPDATE HIS_EXP_MEST_BLTY_REQ SET SERE_SERV_PARENT_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_PARENT_ID");

                    sqls.Add(sqlMedicine);
                    sqls.Add(sqlMaterial);
                    sqls.Add(sqlBlood);
                    sqls.Add(sqlBltyReq);
                }

                List<HIS_SERVICE_REQ> beds = lstServiceReqTruncate != null ? lstServiceReqTruncate.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__G).ToList() : null;
                if (IsNotNullOrEmpty(beds))
                {
                    string sqlBedLog = DAOWorker.SqlDAO.AddInClause(beds.Select(s => s.ID).ToList(), "UPDATE HIS_BED_LOG SET SERVICE_REQ_ID = NULL WHERE %IN_CLAUSE% ", "SERVICE_REQ_ID");
                    sqls.Add(sqlBedLog);
                }

                if (IsNotNullOrEmpty(lstServiceReqTruncate))
                {
                    List<long> deleteReqIds = lstServiceReqTruncate.Select(s => s.ID).ToList();
                    string sqlSereServBill = DAOWorker.SqlDAO.AddInClause(deleteReqIds, "UPDATE HIS_SERE_SERV_BILL SET TDL_SERVICE_REQ_ID = NULL WHERE %IN_CLAUSE% ", "TDL_SERVICE_REQ_ID");
                    string sqlSereServDeposit = DAOWorker.SqlDAO.AddInClause(deleteReqIds, "UPDATE HIS_SERE_SERV_DEPOSIT SET TDL_SERVICE_REQ_ID = NULL WHERE %IN_CLAUSE% ", "TDL_SERVICE_REQ_ID");
                    string sqlSeseDepoRepay = DAOWorker.SqlDAO.AddInClause(deleteReqIds, "UPDATE HIS_SESE_DEPO_REPAY SET TDL_SERVICE_REQ_ID = NULL WHERE %IN_CLAUSE% ", "TDL_SERVICE_REQ_ID");
                    string sqlExamSereDire = DAOWorker.SqlDAO.AddInClause(deleteReqIds, "DELETE HIS_EXAM_SERE_DIRE WHERE %IN_CLAUSE% ", "SERVICE_REQ_ID");

                    sqls.Add(sqlSereServBill);
                    sqls.Add(sqlSereServDeposit);
                    sqls.Add(sqlSeseDepoRepay);
                    sqls.Add(sqlExamSereDire);
                }

                if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Update TDL_SERVICE_REQ_ID cua HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_BLOOD,  HIS_EXP_MEST_BLTY_REQ, HIS_SERE_SERV_BILL, HIS_SERE_SERV_DEPOSIT, HIS_SESE_DEPO_REPAY, HIS_EXAM_SERE_DIRE that bai.");
                }

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
