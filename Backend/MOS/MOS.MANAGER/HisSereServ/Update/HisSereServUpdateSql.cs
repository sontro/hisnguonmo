using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using AutoMapper;
using MOS.SDO;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using MOS.OracleUDT;
using MOS.DAO.Sql;
using MOS.UTILITY;
using Inventec.Core;

namespace MOS.MANAGER.HisSereServ.Update
{
    class SereServResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisSereServ[] Data { get; set; }
    }

    class HisSereServUpdateSql : BusinessBase
    {
        private List<HIS_SERE_SERV> beforeSereServs = new List<HIS_SERE_SERV>();

        internal HisSereServUpdateSql()
            : base()
        {

        }

        internal HisSereServUpdateSql(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Run(List<HIS_SERE_SERV> listData)
        {
            return Run(listData, null);
        }

        internal bool Run(List<HIS_SERE_SERV> listData, List<HIS_SERE_SERV> befores)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServCheck checker = new HisSereServCheck(param);

                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);

                    if (!data.PARENT_ID.HasValue) //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERE_SERV, THisSereServ>();
                    List<THisSereServ> input = Mapper.Map<List<THisSereServ>>(listData);
                    THisSereServ[] sereServArray = new THisSereServ[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        sereServArray[i] = input[i];
                        sereServArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        sereServArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TSereServ tSereServ = new TSereServ();
                    tSereServ.SereServArray = sereServArray;

                    string storedSql = "PKG_UPDATE_SERE_SERV.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TSereServ>("P_SERE_SERV", "HIS_RS.T_SERE_SERV", tSereServ, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Update sere_serv that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisSereServ, HIS_SERE_SERV>();

                        SereServResultHolder rs = (SereServResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_SERE_SERV>>(rs.Data.ToList()) : null;
                        if (result && IsNotNullOrEmpty(befores))
                        {
                            beforeSereServs.AddRange(befores);
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
                SereServResultHolder rs = new SereServResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TSereServ sereServ = (TSereServ)parameters[0].Value;
                    rs.Data = sereServ.SereServArray;
                }
                resultHolder = rs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beforeSereServs))
            {
                if (!this.Run(this.beforeSereServs))
                {
                    LogSystem.Warn("Rollback sere_serv (khi update truc tiep qua package) that bai");
                }
                this.beforeSereServs = null;
            }
        }
    }
}
