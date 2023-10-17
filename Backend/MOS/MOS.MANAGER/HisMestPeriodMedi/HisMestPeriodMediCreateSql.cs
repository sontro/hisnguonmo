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

namespace MOS.MANAGER.HisMestPeriodMedi
{
    class HisMestPeriodMediCreateSql : BusinessBase
    {
        private List<HIS_MEST_PERIOD_MEDI> recentHisMestPeriodMediDTOs = new List<HIS_MEST_PERIOD_MEDI>();

        internal HisMestPeriodMediCreateSql()
            : base()
        {

        }

        internal HisMestPeriodMediCreateSql(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MEST_PERIOD_MEDI> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                if (valid)
                {
                    Mapper.CreateMap<HIS_MEST_PERIOD_MEDI, THisMestPeriodMedi>();
                    List<THisMestPeriodMedi> input = Mapper.Map<List<THisMestPeriodMedi>>(listData);

                    THisMestPeriodMedi[] mestPeriodMediArray = new THisMestPeriodMedi[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        mestPeriodMediArray[i] = input[i];

                        mestPeriodMediArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMediArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMediArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        mestPeriodMediArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TMestPeriodMedi tMestPeriodMedi = new TMestPeriodMedi();
                    tMestPeriodMedi.MestPeriodMediArray = mestPeriodMediArray;

                    string storedSql = "PKG_INSERT_MEST_PERIOD_MEDI.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMestPeriodMedi>("P_MEST_PERIOD_MEDI", "HIS_RS.T_MEST_PERIOD_MEDI", tMestPeriodMedi, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert HIS_MEST_PERIOD_MEDI that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisMestPeriodMedi, HIS_MEST_PERIOD_MEDI>();

                        MestPeriodMediResultHolder rs = (MestPeriodMediResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_MEST_PERIOD_MEDI>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            recentHisMestPeriodMediDTOs.AddRange(listData);
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
                MestPeriodMediResultHolder rs = new MestPeriodMediResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMestPeriodMedi sereServ = (TMestPeriodMedi)parameters[0].Value;
                    rs.Data = sereServ.MestPeriodMediArray;
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
                if (IsNotNullOrEmpty(this.recentHisMestPeriodMediDTOs))
                {
                    //Luu y:
                    //+ can cap nhat medicine_id, Medirial_id, blood_id ve null de tranh truong hop fk khi nguoi dung xoa thuoc/vat tu/mau sau khi xoa sere_serv
                    List<long> ids = this.recentHisMestPeriodMediDTOs.Select(o => o.ID).ToList();
                    if (!DAOWorker.SqlDAO.Execute(DAOWorker.SqlDAO.AddInClause(ids, "DELETE HIS_MEST_PERIOD_MEDI WHERE %IN_CLAUSE%", "ID")))
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

    class MestPeriodMediResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMestPeriodMedi[] Data { get; set; }
    }
}
