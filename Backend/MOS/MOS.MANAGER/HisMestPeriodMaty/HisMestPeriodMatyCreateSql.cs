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

namespace MOS.MANAGER.HisMestPeriodMaty
{
    class HisMestPeriodMatyCreateSql : BusinessBase
    {
        private List<HIS_MEST_PERIOD_MATY> recentHisMestPeriodMaty = new List<HIS_MEST_PERIOD_MATY>();

        internal HisMestPeriodMatyCreateSql()
            : base()
        {

        }

        internal HisMestPeriodMatyCreateSql(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MEST_PERIOD_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                if (valid)
                {
                    Mapper.CreateMap<HIS_MEST_PERIOD_MATY, THisMestPeriodMaty>();
                    List<THisMestPeriodMaty> input = Mapper.Map<List<THisMestPeriodMaty>>(listData);

                    THisMestPeriodMaty[] mestPeriodMatyArray = new THisMestPeriodMaty[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        mestPeriodMatyArray[i] = input[i];

                        mestPeriodMatyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMatyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMatyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        mestPeriodMatyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TMestPeriodMaty tMestPeriodMaty = new TMestPeriodMaty();
                    tMestPeriodMaty.MestPeriodMatyArray = mestPeriodMatyArray;

                    string storedSql = "PKG_INSERT_MEST_PERIOD_MATY.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMestPeriodMaty>("P_MEST_PERIOD_MATY", "HIS_RS.T_MEST_PERIOD_MATY", tMestPeriodMaty, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert HIS_MEST_PERIOD_MATY that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisMestPeriodMaty, HIS_MEST_PERIOD_MATY>();

                        MestPeriodMatyResultHolder rs = (MestPeriodMatyResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_MEST_PERIOD_MATY>>(rs.Data.ToList()) : null;
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
                MestPeriodMatyResultHolder rs = new MestPeriodMatyResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMestPeriodMaty mestPeriodMaty = (TMestPeriodMaty)parameters[0].Value;
                    rs.Data = mestPeriodMaty.MestPeriodMatyArray;
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

    class MestPeriodMatyResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMestPeriodMaty[] Data { get; set; }
    }
}
