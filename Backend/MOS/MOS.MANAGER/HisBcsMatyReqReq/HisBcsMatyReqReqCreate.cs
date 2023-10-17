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

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    partial class HisBcsMatyReqReqCreate : BusinessBase
    {
        private List<HIS_BCS_MATY_REQ_REQ> recentHisBcsMatyReqReqs = new List<HIS_BCS_MATY_REQ_REQ>();

        internal HisBcsMatyReqReqCreate()
            : base()
        {

        }

        internal HisBcsMatyReqReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BCS_MATY_REQ_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMatyReqReqCheck checker = new HisBcsMatyReqReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisBcsMatyReqReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMatyReqReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBcsMatyReqReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBcsMatyReqReqs.Add(data);
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

        internal bool CreateList(List<HIS_BCS_MATY_REQ_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMatyReqReqCheck checker = new HisBcsMatyReqReqCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBcsMatyReqReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMatyReqReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBcsMatyReqReq that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBcsMatyReqReqs.AddRange(listData);
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

        internal bool CreateListSql(List<HIS_BCS_MATY_REQ_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMatyReqReqCheck checker = new HisBcsMatyReqReqCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    Mapper.CreateMap<HIS_BCS_MATY_REQ_REQ, THisBcsMatyReqReq>();
                    List<THisBcsMatyReqReq> input = Mapper.Map<List<THisBcsMatyReqReq>>(listData);

                    THisBcsMatyReqReq[] expMestMatyArray = new THisBcsMatyReqReq[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        expMestMatyArray[i] = input[i];

                        expMestMatyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMatyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMatyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        expMestMatyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TBcsMatyReqReq tBcsMatyReqReq = new TBcsMatyReqReq();
                    tBcsMatyReqReq.BcsMatyReqReqArray = expMestMatyArray;

                    string storedSql = "PKG_INSERT_BCS_MATY_REQ_REQ.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TBcsMatyReqReq>("P_BCS_MATY_REQ_REQ", "HIS_RS.T_BCS_MATY_REQ_REQ", tBcsMatyReqReq, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert exp_mest_maty_req that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisBcsMatyReqReq, HIS_BCS_MATY_REQ_REQ>();

                        BcsMatyReqReqResultHolder rs = (BcsMatyReqReqResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        List<HIS_BCS_MATY_REQ_REQ> datas = rs.Data != null ? Mapper.Map<List<HIS_BCS_MATY_REQ_REQ>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            for (int i = 0; i < listData.Count; i++)
                            {
                                listData[i].ID = datas[i].ID;
                            }
                            this.recentHisBcsMatyReqReqs.AddRange(listData);
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
                BcsMatyReqReqResultHolder rs = new BcsMatyReqReqResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TBcsMatyReqReq expMestMatyReq = (TBcsMatyReqReq)parameters[0].Value;
                    rs.Data = expMestMatyReq.BcsMatyReqReqArray;
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
            if (IsNotNullOrEmpty(this.recentHisBcsMatyReqReqs))
            {
                if (!DAOWorker.HisBcsMatyReqReqDAO.TruncateList(this.recentHisBcsMatyReqReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisBcsMatyReqReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBcsMatyReqReqs", this.recentHisBcsMatyReqReqs));
                }
                this.recentHisBcsMatyReqReqs = null;
            }
        }
    }

    class BcsMatyReqReqResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisBcsMatyReqReq[] Data { get; set; }
    }
}
