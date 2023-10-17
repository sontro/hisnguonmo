using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.ApproveNotTaken
{
    class HisExpMestApproveNotTaken : BusinessBase
    {
        long index = 0;
        internal HisExpMestApproveNotTaken()
            : base()
        {

        }

        internal HisExpMestApproveNotTaken(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(ApproveNotTakenPresSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK stock = null;
                WorkPlaceSDO wp = null;
                List<HIS_EXP_MEST> expMests = null;
                List<HIS_EXP_MEST> arrgExamchildren = null;
                HisExpMestApproveNotTakenCheck checker = new HisExpMestApproveNotTakenCheck(param);
                HisMediStockCheck mediStockChecker = new HisMediStockCheck(param);
                valid = valid && checker.ValidData(data);
                valid = valid && mediStockChecker.VerifyId(data.MediStockId, ref stock);
                valid = valid && mediStockChecker.IsUnLock(stock);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref wp);
                valid = valid && checker.CheckWorkPlaceIsStock(data, wp);
                valid = valid && checker.CheckExpMest(data, ref expMests);
                valid = valid && checker.IsValidAndAddArrgExamChildren(expMests, ref arrgExamchildren); //AGGR_EXP_MEST_ID
                if (valid)
                {
                    List<HIS_EXP_MEST> listNotTaken = new List<HIS_EXP_MEST>();
                    List<string> sql = new List<string>();
                    List<object> paramSql = new List<object>();
                    List<string> messErrors = new List<string>();
                    foreach (var item in expMests)
                    {
                        string error = "";
                        this.HisProcessExpMest(item, arrgExamchildren, listNotTaken, ref sql, ref paramSql, ref error);
                        if (item.IS_NOT_TAKEN != Constant.IS_TRUE)
                        {
                            messErrors.Add(string.Format("{0} - {1}", item.EXP_MEST_CODE, error));
                        }
                    }

                    if (IsNotNullOrEmpty(sql))
                    {
                        if (!DAOWorker.SqlDAO.Execute(sql, paramSql.ToArray()))
                        {
                            LogSystem.Warn("Cap nhat IS_NOT_TAKEN_FAIL that bai");
                            Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => paramSql), paramSql));
                        }
                    }

                    if (IsNotNullOrEmpty(messErrors))
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatChuaDuocTichKhongLay, string.Join(". ", messErrors));
                    }

                    result = true;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisExpMest_DuyetKhongLayDonPhongKham, stock.MEDI_STOCK_NAME, String.Join(",", listNotTaken.Select(s => s.EXP_MEST_CODE).ToList())).Run();
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

        private void HisProcessExpMest(HIS_EXP_MEST expMest, List<HIS_EXP_MEST> arrgExamchildren, List<HIS_EXP_MEST> listNotTaken, ref List<string> sql, ref List<object> paramSql, ref string error)
        {
            try
            {
                CommonParam paramCommon = new CommonParam();
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                {
                    List<HIS_EXP_MEST> expChildren = IsNotNullOrEmpty(arrgExamchildren) ? arrgExamchildren.Where(o => o.AGGR_EXP_MEST_ID == expMest.ID).ToList() : null;
                    AggrExpMestProcessor expMestProcessor = new AggrExpMestProcessor(paramCommon);
                    if (!expMestProcessor.Run(expMest, expChildren))
                    {
                        LogSystem.Error("Khong Approve Not Taken duoc cho ExpMestCode: \n"
                            + expMest.EXP_MEST_CODE + LogUtil.TraceData("ExpMest", expMest)
                            + "\n" + LogUtil.TraceData("Param", paramCommon));
                        if (IsNotNullOrEmpty(paramCommon.Messages))
                        {
                            error = paramCommon.GetMessage();
                        }
                    }
                }
                else
                {
                    ExpMestProcessor expMestProcessor = new ExpMestProcessor(paramCommon);
                    if (!expMestProcessor.Run(expMest))
                    {
                        LogSystem.Error("Khong Approve Not Taken duoc cho ExpMestCode: \n"
                            + expMest.EXP_MEST_CODE + LogUtil.TraceData("ExpMest", expMest)
                            + "\n" + LogUtil.TraceData("Param", paramCommon));
                        if (IsNotNullOrEmpty(paramCommon.Messages))
                        {
                            error = paramCommon.GetMessage();
                        }
                    }
                }

                if (expMest.IS_NOT_TAKEN == Constant.IS_TRUE)
                    listNotTaken.Add(expMest);
                else
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

                    string queryTakenFall = string.Format("UPDATE HIS_EXP_MEST SET IS_NOT_TAKEN_FAIL = 1, NOT_TAKEN_DESC = :param{0} WHERE ID = :param{1}", index++, index++);
                    sql.Add(queryTakenFall);
                    paramSql.Add(desc.ToString());
                    paramSql.Add(expMest.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
