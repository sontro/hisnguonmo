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

namespace MOS.MANAGER.HisBcsMetyReqReq
{
    partial class HisBcsMetyReqReqCreate : BusinessBase
    {
        private List<HIS_BCS_METY_REQ_REQ> recentHisBcsMetyReqReqs = new List<HIS_BCS_METY_REQ_REQ>();

        internal HisBcsMetyReqReqCreate()
            : base()
        {

        }

        internal HisBcsMetyReqReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BCS_METY_REQ_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMetyReqReqCheck checker = new HisBcsMetyReqReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisBcsMetyReqReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMetyReqReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBcsMetyReqReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBcsMetyReqReqs.Add(data);
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

        internal bool CreateList(List<HIS_BCS_METY_REQ_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMetyReqReqCheck checker = new HisBcsMetyReqReqCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisBcsMetyReqReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBcsMetyReqReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBcsMetyReqReq that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisBcsMetyReqReqs.AddRange(listData);
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

        internal bool CreateListSql(List<HIS_BCS_METY_REQ_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMetyReqReqCheck checker = new HisBcsMetyReqReqCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    Mapper.CreateMap<HIS_BCS_METY_REQ_REQ, THisBcsMetyReqReq>();
                    List<THisBcsMetyReqReq> input = Mapper.Map<List<THisBcsMetyReqReq>>(listData);

                    THisBcsMetyReqReq[] expMestMetyArray = new THisBcsMetyReqReq[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        expMestMetyArray[i] = input[i];

                        expMestMetyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMetyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMetyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        expMestMetyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TBcsMetyReqReq tBcsMetyReqReq = new TBcsMetyReqReq();
                    tBcsMetyReqReq.BcsMetyReqReqArray = expMestMetyArray;

                    string storedSql = "PKG_INSERT_BCS_METY_REQ_REQ.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TBcsMetyReqReq>("P_BCS_METY_REQ_REQ", "HIS_RS.T_BCS_METY_REQ_REQ", tBcsMetyReqReq, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert exp_mest_maty_req that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisBcsMetyReqReq, HIS_BCS_METY_REQ_REQ>();

                        BcsMetyReqReqResultHolder rs = (BcsMetyReqReqResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        List<HIS_BCS_METY_REQ_REQ> datas = rs.Data != null ? Mapper.Map<List<HIS_BCS_METY_REQ_REQ>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            for (int i = 0; i < listData.Count; i++)
                            {
                                listData[i].ID = datas[i].ID;
                            }
                            this.recentHisBcsMetyReqReqs.AddRange(listData);
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
                BcsMetyReqReqResultHolder rs = new BcsMetyReqReqResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TBcsMetyReqReq expMestMetyReq = (TBcsMetyReqReq)parameters[0].Value;
                    rs.Data = expMestMetyReq.BcsMetyReqReqArray;
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
            if (IsNotNullOrEmpty(this.recentHisBcsMetyReqReqs))
            {
                if (!DAOWorker.HisBcsMetyReqReqDAO.TruncateList(this.recentHisBcsMetyReqReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisBcsMetyReqReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBcsMetyReqReqs", this.recentHisBcsMetyReqReqs));
                }
                this.recentHisBcsMetyReqReqs = null;
            }
        }
    }

    class BcsMetyReqReqResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisBcsMetyReqReq[] Data { get; set; }
    }
}
