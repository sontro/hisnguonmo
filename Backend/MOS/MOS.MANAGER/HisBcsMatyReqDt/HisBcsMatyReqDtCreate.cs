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

namespace MOS.MANAGER.HisBcsMatyReqDt
{
    partial class HisBcsMatyReqDtCreate : BusinessBase
    {
        private List<HIS_BCS_MATY_REQ_DT> recentHisBcsMatyReqDts = new List<HIS_BCS_MATY_REQ_DT>();

        internal HisBcsMatyReqDtCreate()
            : base()
        {

        }

        internal HisBcsMatyReqDtCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BCS_MATY_REQ_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMatyReqDtCheck checker = new HisBcsMatyReqDtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisBcsMatyReqDtDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMatyReqDt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBcsMatyReqDt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBcsMatyReqDts.Add(data);
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

        internal bool CreateList(List<HIS_BCS_MATY_REQ_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMatyReqDtCheck checker = new HisBcsMatyReqDtCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBcsMatyReqDtDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMatyReqDt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBcsMatyReqDt that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBcsMatyReqDts.AddRange(listData);
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

        internal bool CreateListSql(List<HIS_BCS_MATY_REQ_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMatyReqDtCheck checker = new HisBcsMatyReqDtCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    Mapper.CreateMap<HIS_BCS_MATY_REQ_DT, THisBcsMatyReqDt>();
                    List<THisBcsMatyReqDt> input = Mapper.Map<List<THisBcsMatyReqDt>>(listData);

                    THisBcsMatyReqDt[] expMestMatyArray = new THisBcsMatyReqDt[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        expMestMatyArray[i] = input[i];

                        expMestMatyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMatyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMatyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        expMestMatyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TBcsMatyReqDt tBcsMatyReqDt = new TBcsMatyReqDt();
                    tBcsMatyReqDt.BcsMatyReqDtArray = expMestMatyArray;

                    string storedSql = "PKG_INSERT_BCS_MATY_REQ_DT.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TBcsMatyReqDt>("P_BCS_MATY_REQ_DT", "HIS_RS.T_BCS_MATY_REQ_DT", tBcsMatyReqDt, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert exp_mest_maty_req that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisBcsMatyReqDt, HIS_BCS_MATY_REQ_DT>();

                        BcsMatyReqDtResultHolder rs = (BcsMatyReqDtResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        List<HIS_BCS_MATY_REQ_DT> datas = rs.Data != null ? Mapper.Map<List<HIS_BCS_MATY_REQ_DT>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            for (int i = 0; i < listData.Count; i++)
                            {
                                listData[i].ID = datas[i].ID;
                            }
                            this.recentHisBcsMatyReqDts.AddRange(listData);
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
                BcsMatyReqDtResultHolder rs = new BcsMatyReqDtResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TBcsMatyReqDt expMestMatyReq = (TBcsMatyReqDt)parameters[0].Value;
                    rs.Data = expMestMatyReq.BcsMatyReqDtArray;
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
            if (IsNotNullOrEmpty(this.recentHisBcsMatyReqDts))
            {
                if (!DAOWorker.HisBcsMatyReqDtDAO.TruncateList(this.recentHisBcsMatyReqDts))
                {
                    LogSystem.Warn("Rollback du lieu HisBcsMatyReqDt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBcsMatyReqDts", this.recentHisBcsMatyReqDts));
                }
                this.recentHisBcsMatyReqDts = null;
            }
        }
    }
    class BcsMatyReqDtResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisBcsMatyReqDt[] Data { get; set; }
    }

}
