using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Sql;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.OracleUDT;
using MOS.UTILITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMatyReq
{
    partial class HisExpMestMatyReqCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_MATY_REQ> recentHisExpMestMatyReqs = new List<HIS_EXP_MEST_MATY_REQ>();

        internal HisExpMestMatyReqCreate()
            : base()
        {

        }

        internal HisExpMestMatyReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_MATY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMatyReqCheck checker = new HisExpMestMatyReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisExpMestMatyReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMatyReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestMatyReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestMatyReqs.Add(data);
                    result = true;
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

        internal bool CreateList(List<HIS_EXP_MEST_MATY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMatyReqCheck checker = new HisExpMestMatyReqCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestMatyReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMatyReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestMatyReq that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpMestMatyReqs.AddRange(listData);
                    result = true;
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

        internal bool CreateListSql(List<HIS_EXP_MEST_MATY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMatyReqCheck checker = new HisExpMestMatyReqCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST_MATY_REQ, THisExpMestMatyReq>();
                    List<THisExpMestMatyReq> input = Mapper.Map<List<THisExpMestMatyReq>>(listData);

                    THisExpMestMatyReq[] expMestMatyArray = new THisExpMestMatyReq[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        expMestMatyArray[i] = input[i];

                        expMestMatyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMatyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMatyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        expMestMatyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TExpMestMatyReq tExpMestMatyReq = new TExpMestMatyReq();
                    tExpMestMatyReq.ExpMestMatyReqArray = expMestMatyArray;

                    string storedSql = "PKG_INSERT_EXP_MEST_MATY_REQ.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TExpMestMatyReq>("P_EXP_MEST_MATY_REQ", "HIS_RS.T_EXP_MEST_MATY_REQ", tExpMestMatyReq, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert exp_mest_maty_req that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisExpMestMatyReq, HIS_EXP_MEST_MATY_REQ>();

                        ExpMestMatyReqResultHolder rs = (ExpMestMatyReqResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        List<HIS_EXP_MEST_MATY_REQ> datas = rs.Data != null ? Mapper.Map<List<HIS_EXP_MEST_MATY_REQ>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            for (int i = 0; i < listData.Count; i++)
                            {
                                listData[i].ID = datas[i].ID;
                            }
                            this.recentHisExpMestMatyReqs.AddRange(listData);
                        }
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

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, OracleDataReader dataReader, params OracleParameter[] parameters)
        {
            try
            {
                ExpMestMatyReqResultHolder rs = new ExpMestMatyReqResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TExpMestMatyReq expMestMatyReq = (TExpMestMatyReq)parameters[0].Value;
                    rs.Data = expMestMatyReq.ExpMestMatyReqArray;
                }
                resultHolder = rs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisExpMestMatyReqs))
            {
                if (!new HisExpMestMatyReqTruncate(param).TruncateList(this.recentHisExpMestMatyReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestMatyReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpMestMatyReqs", this.recentHisExpMestMatyReqs));
                }
            }
        }
    }

    class ExpMestMatyReqResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisExpMestMatyReq[] Data { get; set; }
    }

}
