using AutoMapper;
using Inventec.Common.Logging;
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

namespace MOS.MANAGER.HisMestPeriodMate
{
    class HisMestPeriodMateCreateSql : BusinessBase
    {
        private List<HIS_MEST_PERIOD_MATE> recentHisMestPeriodMaty = new List<HIS_MEST_PERIOD_MATE>();

        internal HisMestPeriodMateCreateSql()
            : base()
        {

        }

        internal HisMestPeriodMateCreateSql(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MEST_PERIOD_MATE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                if (valid)
                {
                    Mapper.CreateMap<HIS_MEST_PERIOD_MATE, THisMestPeriodMate>();
                    List<THisMestPeriodMate> input = Mapper.Map<List<THisMestPeriodMate>>(listData);

                    THisMestPeriodMate[] mestPeriodMatyArray = new THisMestPeriodMate[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        mestPeriodMatyArray[i] = input[i];

                        mestPeriodMatyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMatyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMatyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        mestPeriodMatyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TMestPeriodMate tMestPeriodMaty = new TMestPeriodMate();
                    tMestPeriodMaty.MestPeriodMatyArray = mestPeriodMatyArray;

                    string storedSql = "PKG_INSERT_MEST_PERIOD_MATE.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMestPeriodMate>("P_MEST_PERIOD_MATE", "HIS_RS.T_MEST_PERIOD_MATE", tMestPeriodMaty, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert HIS_MEST_PERIOD_MATE that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisMestPeriodMate, HIS_MEST_PERIOD_MATE>();

                        MestPeriodMateResultHolder rs = (MestPeriodMateResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_MEST_PERIOD_MATE>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            recentHisMestPeriodMaty.AddRange(listData);
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
                MestPeriodMateResultHolder rs = new MestPeriodMateResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMestPeriodMate sereServ = (TMestPeriodMate)parameters[0].Value;
                    rs.Data = sereServ.MestPeriodMatyArray;
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
                if (IsNotNullOrEmpty(this.recentHisMestPeriodMaty))
                {
                    //Luu y:
                    //+ can cap nhat medicine_id, material_id, blood_id ve null de tranh truong hop fk khi nguoi dung xoa thuoc/vat tu/mau sau khi xoa sere_serv
                    List<long> ids = this.recentHisMestPeriodMaty.Select(o => o.ID).ToList();
                    if (!DAOWorker.SqlDAO.Execute(DAOWorker.SqlDAO.AddInClause(ids, "DELETE HIS_MEST_PERIOD_MATY WHERE %IN_CLAUSE%", "ID")))
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

    class MestPeriodMateResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMestPeriodMate[] Data { get; set; }
    }
}
