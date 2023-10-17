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

namespace MOS.MANAGER.HisBcsMetyReqDt
{
    partial class HisBcsMetyReqDtCreate : BusinessBase
    {
        private List<HIS_BCS_METY_REQ_DT> recentHisBcsMetyReqDts = new List<HIS_BCS_METY_REQ_DT>();

        internal HisBcsMetyReqDtCreate()
            : base()
        {

        }

        internal HisBcsMetyReqDtCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BCS_METY_REQ_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMetyReqDtCheck checker = new HisBcsMetyReqDtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisBcsMetyReqDtDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMetyReqDt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBcsMetyReqDt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBcsMetyReqDts.Add(data);
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

        internal bool CreateList(List<HIS_BCS_METY_REQ_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMetyReqDtCheck checker = new HisBcsMetyReqDtCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBcsMetyReqDtDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMetyReqDt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBcsMetyReqDt that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBcsMetyReqDts.AddRange(listData);
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

        internal bool CreateListSql(List<HIS_BCS_METY_REQ_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMetyReqDtCheck checker = new HisBcsMetyReqDtCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    Mapper.CreateMap<HIS_BCS_METY_REQ_DT, THisBcsMetyReqDt>();
                    List<THisBcsMetyReqDt> input = Mapper.Map<List<THisBcsMetyReqDt>>(listData);

                    THisBcsMetyReqDt[] expMestMatyArray = new THisBcsMetyReqDt[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        expMestMatyArray[i] = input[i];

                        expMestMatyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMatyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMatyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        expMestMatyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TBcsMetyReqDt tBcsMetyReqDt = new TBcsMetyReqDt();
                    tBcsMetyReqDt.BcsMetyReqDtArray = expMestMatyArray;

                    string storedSql = "PKG_INSERT_BCS_METY_REQ_DT.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TBcsMetyReqDt>("P_BCS_METY_REQ_DT", "HIS_RS.T_BCS_METY_REQ_DT", tBcsMetyReqDt, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert exp_mest_maty_req that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisBcsMetyReqDt, HIS_BCS_METY_REQ_DT>();

                        BcsMetyReqDtResultHolder rs = (BcsMetyReqDtResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        List<HIS_BCS_METY_REQ_DT> datas = rs.Data != null ? Mapper.Map<List<HIS_BCS_METY_REQ_DT>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            for (int i = 0; i < listData.Count; i++)
                            {
                                listData[i].ID = datas[i].ID;
                            }
                            this.recentHisBcsMetyReqDts.AddRange(listData);
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
                BcsMetyReqDtResultHolder rs = new BcsMetyReqDtResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TBcsMetyReqDt expMestMatyReq = (TBcsMetyReqDt)parameters[0].Value;
                    rs.Data = expMestMatyReq.BcsMetyReqDtArray;
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
            if (IsNotNullOrEmpty(this.recentHisBcsMetyReqDts))
            {
                if (!DAOWorker.HisBcsMetyReqDtDAO.TruncateList(this.recentHisBcsMetyReqDts))
                {
                    LogSystem.Warn("Rollback du lieu HisBcsMetyReqDt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBcsMetyReqDts", this.recentHisBcsMetyReqDts));
                }
                this.recentHisBcsMetyReqDts = null;
            }
        }
    }

    class BcsMetyReqDtResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisBcsMetyReqDt[] Data { get; set; }
    }
}
