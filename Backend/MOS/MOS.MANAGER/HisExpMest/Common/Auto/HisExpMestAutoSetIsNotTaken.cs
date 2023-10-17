using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestType;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Auto
{
    public class HisExpMestAutoSetIsNotTaken : BusinessBase
    {
        private static bool IsRunning;

        public HisExpMestAutoSetIsNotTaken()
            : base()
        {

        }

        public HisExpMestAutoSetIsNotTaken(CommonParam param)
            : base(param)
        {

        }

        public void Run()
        {
            try
            {
                if (IsRunning)
                {
                    LogSystem.Info("Tien trinh dang duoc chay khong cho phep khoi tao tien trinh khac");
                    return;
                }

                IsRunning = true;

                List<long> listValidTypes = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK,
                };

                if (Config.HisExpMestCFG.NOT_TAKEN_BY_DAYs <= 0)
                {
                    LogSystem.Info("He thong khong cau hinh thoi gian khong lay");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Config.HisExpMestCFG.EXP_MEST_TYPE_CODES_WHEN_AUTO_SET_IS_NOT_TAKEN))
                {
                    LogSystem.Info("Chua khai bao cac ma loai phieu xuat xu ly tu dong tich khong lay");
                    return;
                }

                List<HIS_EXP_MEST_TYPE> expMestTypes = null;
                List<string> typeCodes = Config.HisExpMestCFG.EXP_MEST_TYPE_CODES_WHEN_AUTO_SET_IS_NOT_TAKEN.Split(',').ToList();
                typeCodes = typeCodes != null && typeCodes.Count > 0 ? typeCodes.Where(o => !string.IsNullOrWhiteSpace(o)).ToList() : null;
                if (typeCodes != null && typeCodes.Count > 0)
                {
                    HisExpMestTypeFilterQuery filter = new HisExpMestTypeFilterQuery();
                    filter.EXP_MEST_TYPE_CODEs = typeCodes;
                    expMestTypes = new HisExpMestTypeGet().Get(filter);
                    expMestTypes = expMestTypes != null && expMestTypes.Count > 0 ? expMestTypes.Where(o => listValidTypes.Contains(o.ID)).ToList() : null;
                }

                if (expMestTypes == null || expMestTypes.Count <= 0)
                {
                    LogSystem.Info("Cac loai phieu xuat duoc khai bao khong hop le: " + Config.HisExpMestCFG.EXP_MEST_TYPE_CODES_WHEN_AUTO_SET_IS_NOT_TAKEN);
                    return;
                }

                List<HIS_EXP_MEST> listRawData = new List<HIS_EXP_MEST>();
                DateTime date = DateTime.Now.AddDays(-Config.HisExpMestCFG.NOT_TAKEN_BY_DAYs);

                string query = "SELECT * FROM HIS_EXP_MEST EM WHERE 1=1";
                query += string.Format(" AND EM.EXP_MEST_TYPE_ID IN ({0}) ", string.Join(", ", expMestTypes.Select(o => o.ID).ToList()));
                query += " AND EM.EXP_MEST_STT_ID = " + IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                query += string.Format(" AND (EM.TDL_INTRUCTION_DATE < {0} OR (EM.EXP_MEST_TYPE_ID = 15 AND EM.CREATE_TIME < {0}))", date.ToString("yyyyMMddHHmmss"));
                query += " AND EM.AGGR_EXP_MEST_ID IS NULL";
                query += " AND (EM.IS_NOT_TAKEN IS NULL OR EM.IS_NOT_TAKEN <> 1)";
                query += " AND (EM.IS_NOT_TAKEN_FAIL IS NULL OR EM.IS_NOT_TAKEN_FAIL <> 1)";
                query += " AND EXISTS (SELECT 1 FROM HIS_TREATMENT WHERE EM.TDL_TREATMENT_ID = ID AND IS_ACTIVE = 1 AND (IS_PAUSE IS NULL OR IS_PAUSE <> 1))";
                query += " AND ((EM.EXP_MEST_TYPE_ID = 15) OR (EXISTS (SELECT 1 FROM HIS_EXP_MEST_MEDICINE WHERE EM.ID = EXP_MEST_ID) AND NOT EXISTS (SELECT 1 FROM HIS_EXP_MEST_MEDICINE WHERE EM.ID = EXP_MEST_ID AND IS_EXPORT = 1)) OR (EXISTS (SELECT 1 FROM HIS_EXP_MEST_MATERIAL WHERE EM.ID = EXP_MEST_ID) AND NOT EXISTS (SELECT 1 FROM HIS_EXP_MEST_MATERIAL WHERE EM.ID = EXP_MEST_ID AND IS_EXPORT = 1)))";

                LogSystem.Info("Query is taken:" + query);

                listRawData = DAOWorker.SqlDAO.GetSql<HIS_EXP_MEST>(query);
                if (IsNotNullOrEmpty(listRawData))
                {
                    List<long> serviceReqIds = listRawData.Select(s => s.SERVICE_REQ_ID ?? 0).Distinct().ToList();
                    List<long> serviceReqIdsPays = ProcessCheckBill(serviceReqIds);

                    if (IsNotNullOrEmpty(serviceReqIdsPays))//Nếu phiếu xuất đã tồn tài biên lai (thanh toán, tạm ứng dịch vụ) thì bỏ quá không xử lý phiếu này.
                    {
                        ProcessUpdateNotTakenFail(serviceReqIdsPays);
                        listRawData = listRawData.Where(o => !serviceReqIdsPays.Contains(o.SERVICE_REQ_ID ?? 0)).ToList();
                    }

                    if (IsNotNullOrEmpty(listRawData))
                    {
                        var arrgExamIds = listRawData
                        .Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                        .Select(s => s.ID).ToList();
                        List<HIS_EXP_MEST> arrgExamchildren = null;
                        if (IsNotNullOrEmpty(arrgExamIds))
                        {
                            HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                            filter.AGGR_EXP_MEST_IDs = arrgExamIds;
                            arrgExamchildren = new HisExpMestGet().Get(filter);
                        }

                        foreach (var expMest in listRawData)
                        {
                            CommonParam paramCommon = new CommonParam();
                            if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                            {
                                List<HIS_EXP_MEST> arrgchildren = IsNotNullOrEmpty(arrgExamchildren) ? arrgExamchildren.Where(o => o.AGGR_EXP_MEST_ID == expMest.ID).ToList() : null;

                                Common.ApproveNotTaken.AggrExpMestProcessor expMestProcessor = new ApproveNotTaken.AggrExpMestProcessor(paramCommon);
                                if (!expMestProcessor.Run(expMest, arrgchildren))
                                {
                                    LogSystem.Warn("Khong set Not Taken duoc cho ExpMestCode: " + expMest.EXP_MEST_CODE + LogUtil.TraceData("ExpMest", expMest));
                                }
                            }
                            else
                            {
                                Common.ApproveNotTaken.ExpMestProcessor expMestProcessor = new ApproveNotTaken.ExpMestProcessor(paramCommon);
                                if (!expMestProcessor.Run(expMest))
                                {
                                    LogSystem.Warn("Khong set Not Taken duoc cho ExpMestCode: " + expMest.EXP_MEST_CODE + LogUtil.TraceData("ExpMest", expMest));
                                }
                            }

                            if (expMest.IS_NOT_TAKEN != Constant.IS_TRUE)
                            {
                                StringBuilder desc = new StringBuilder();
                                if (IsNotNullOrEmpty(paramCommon.BugCodes))
                                {
                                    desc.AppendFormat("({0})", paramCommon.GetBugCode());
                                }
                                if (IsNotNullOrEmpty(paramCommon.Messages))
                                {
                                    desc.Append(paramCommon.GetMessage());
                                }

                                string queryTakenFall = "UPDATE HIS_EXP_MEST SET IS_NOT_TAKEN_FAIL = 1, NOT_TAKEN_DESC = :param1 WHERE ID = :param2";
                                if (!DAOWorker.SqlDAO.Execute(queryTakenFall, desc.ToString(), expMest.ID))
                                {
                                    LogSystem.Warn("Cap nhat IS_NOT_TAKEN_FAIL that bai");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            IsRunning = false;
        }

        private void ProcessUpdateNotTakenFail(List<long> serviceReqIdsPays)
        {
            try
            {
                if (IsNotNullOrEmpty(serviceReqIdsPays))
                {
                    string query = "UPDATE HIS_EXP_MEST SET IS_NOT_TAKEN_FAIL = 1, NOT_TAKEN_DESC = 'Phiếu xuất đã tồn tại biên lai' WHERE %IN_CLAUSE%";
                    query = new MOS.DAO.Sql.SqlDAO().AddInClause(serviceReqIdsPays, query, "SERVICE_REQ_ID");
                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Warn("Cap nhat IS_NOT_TAKEN_FAIL that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private List<long> ProcessCheckBill(List<long> serviceReqIds)
        {
            List<long> result = null;
            try
            {
                if (IsNotNullOrEmpty(serviceReqIds))
                {
                    string query = "SELECT ID FROM HIS_SERVICE_REQ REQ WHERE %IN_CLAUSE%";
                    query = new MOS.DAO.Sql.SqlDAO().AddInClause(serviceReqIds, query, "ID");
                    query += " AND EXISTS(SELECT 1 FROM HIS_SERE_SERV SS WHERE REQ.ID = SS.SERVICE_REQ_ID AND (EXISTS (SELECT 1 FROM HIS_SERE_SERV_BILL SSB WHERE SSB.SERE_SERV_ID = SS.ID AND (SSB.IS_CANCEL IS NULL OR SSB.IS_CANCEL <> 1)) OR EXISTS (SELECT 1 FROM HIS_SERE_SERV_DEPOSIT SSD WHERE SSD.SERE_SERV_ID = SS.ID AND (SSD.IS_CANCEL IS NULL OR SSD.IS_CANCEL <> 1))))";
                    result = DAOWorker.SqlDAO.GetSql<long>(query);
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
