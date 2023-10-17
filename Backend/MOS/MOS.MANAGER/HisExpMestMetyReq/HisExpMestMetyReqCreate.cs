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

namespace MOS.MANAGER.HisExpMestMetyReq
{
    partial class HisExpMestMetyReqCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_METY_REQ> recentHisExpMestMetyReqs = new List<HIS_EXP_MEST_METY_REQ>();

        internal HisExpMestMetyReqCreate()
            : base()
        {

        }

        internal HisExpMestMetyReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_METY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMetyReqCheck checker = new HisExpMestMetyReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisExpMestMetyReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMetyReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestMetyReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestMetyReqs.Add(data);
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

        internal bool CreateList(List<HIS_EXP_MEST_METY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMetyReqCheck checker = new HisExpMestMetyReqCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestMetyReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMetyReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestMetyReq that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpMestMetyReqs.AddRange(listData);
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

        internal bool CreateListSql(List<HIS_EXP_MEST_METY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMetyReqCheck checker = new HisExpMestMetyReqCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST_METY_REQ, THisExpMestMetyReq>();
                    List<THisExpMestMetyReq> input = Mapper.Map<List<THisExpMestMetyReq>>(listData);

                    THisExpMestMetyReq[] expMestMetyArray = new THisExpMestMetyReq[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        expMestMetyArray[i] = input[i];

                        expMestMetyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMetyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMetyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        expMestMetyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TExpMestMetyReq tExpMestMetyReq = new TExpMestMetyReq();
                    tExpMestMetyReq.ExpMestMetyReqArray = expMestMetyArray;

                    string storedSql = "PKG_INSERT_EXP_MEST_METY_REQ.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TExpMestMetyReq>("P_EXP_MEST_METY_REQ", "HIS_RS.T_EXP_MEST_METY_REQ", tExpMestMetyReq, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert exp_mest_maty_req that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisExpMestMetyReq, HIS_EXP_MEST_METY_REQ>();

                        ExpMestMetyReqResultHolder rs = (ExpMestMetyReqResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        List<HIS_EXP_MEST_METY_REQ> datas = rs.Data != null ? Mapper.Map<List<HIS_EXP_MEST_METY_REQ>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            for (int i = 0; i < listData.Count; i++)
                            {
                                listData[i].ID = datas[i].ID;
                            }
                            this.recentHisExpMestMetyReqs.AddRange(listData);
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
                ExpMestMetyReqResultHolder rs = new ExpMestMetyReqResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TExpMestMetyReq expMestMetyReq = (TExpMestMetyReq)parameters[0].Value;
                    rs.Data = expMestMetyReq.ExpMestMetyReqArray;
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
            if (IsNotNullOrEmpty(this.recentHisExpMestMetyReqs))
            {
                if (!new HisExpMestMetyReqTruncate(param).TruncateList(this.recentHisExpMestMetyReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestMetyReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpMestMetyReqs", this.recentHisExpMestMetyReqs));
                }
            }
        }
    }

    class ExpMestMetyReqResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisExpMestMetyReq[] Data { get; set; }
    }

}
