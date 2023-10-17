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
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMestPeriodBlty
{
    class HisMestPeriodBltyCreateSql : BusinessBase
    {
        private List<HIS_MEST_PERIOD_BLTY> recentHisMestPeriodBltys = new List<HIS_MEST_PERIOD_BLTY>();

        internal HisMestPeriodBltyCreateSql()
            : base()
        {

        }

        internal HisMestPeriodBltyCreateSql(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MEST_PERIOD_BLTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                if (valid)
                {
                    Mapper.CreateMap<HIS_MEST_PERIOD_BLTY, THisMestPeriodBlty>();
                    List<THisMestPeriodBlty> input = Mapper.Map<List<THisMestPeriodBlty>>(listData);

                    THisMestPeriodBlty[] mestPeriodBltyArray = new THisMestPeriodBlty[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        mestPeriodBltyArray[i] = input[i];

                        mestPeriodBltyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodBltyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodBltyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        mestPeriodBltyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TMestPeriodBlty tMestPeriodBlty = new TMestPeriodBlty();
                    tMestPeriodBlty.MestPeriodBltyArray = mestPeriodBltyArray;

                    string storedSql = "PKG_INSERT_MEST_PERIOD_BLTY.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMestPeriodBlty>("P_MEST_PERIOD_BLTY", "HIS_RS.T_MEST_PERIOD_BLTY", tMestPeriodBlty, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert HIS_MEST_PERIOD_BLTY that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisMestPeriodBlty, HIS_MEST_PERIOD_BLTY>();

                        MestPeriodBltyResultHolder rs = (MestPeriodBltyResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_MEST_PERIOD_BLTY>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            recentHisMestPeriodBltys.AddRange(listData);
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
                MestPeriodBltyResultHolder rs = new MestPeriodBltyResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMestPeriodBlty mestPeriodBlty = (TMestPeriodBlty)parameters[0].Value;
                    rs.Data = mestPeriodBlty.MestPeriodBltyArray;
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
            try
            {
                if (IsNotNullOrEmpty(this.recentHisMestPeriodBltys))
                {
                    //Luu y:
                    //+ can cap nhat medicine_id, material_id, blood_id ve null de tranh truong hop fk khi nguoi dung xoa thuoc/vat tu/mau sau khi xoa sere_serv
                    List<long> ids = this.recentHisMestPeriodBltys.Select(o => o.ID).ToList();
                    if (!DAOWorker.SqlDAO.Execute(DAOWorker.SqlDAO.AddInClause(ids, "DELETE HIS_MEST_PERIOD_BLTY WHERE %IN_CLAUSE%", "ID")))
                    {
                        LogSystem.Error("Rollback du lieu that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }

    class MestPeriodBltyResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMestPeriodBlty[] Data { get; set; }
    }
}
