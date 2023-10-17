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

namespace MOS.MANAGER.HisMestPeriodBlood
{
    class HisMestPeriodBloodCreateSql : BusinessBase
    {
        private List<HIS_MEST_PERIOD_BLOOD> recentHisMestPeriodBloods = new List<HIS_MEST_PERIOD_BLOOD>();

        internal HisMestPeriodBloodCreateSql()
            : base()
        {

        }

        internal HisMestPeriodBloodCreateSql(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MEST_PERIOD_BLOOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                if (valid)
                {
                    Mapper.CreateMap<HIS_MEST_PERIOD_BLOOD, THisMestPeriodBlood>();
                    List<THisMestPeriodBlood> input = Mapper.Map<List<THisMestPeriodBlood>>(listData);

                    THisMestPeriodBlood[] mestPeriodMatyArray = new THisMestPeriodBlood[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        mestPeriodMatyArray[i] = input[i];

                        mestPeriodMatyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMatyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMatyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        mestPeriodMatyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TMestPeriodBlood tMestPeriodBlood = new TMestPeriodBlood();
                    tMestPeriodBlood.MestPeriodBloodArray = mestPeriodMatyArray;

                    string storedSql = "PKG_INSERT_MEST_PERIOD_BLOOD.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMestPeriodBlood>("P_MEST_PERIOD_BLOOD", "HIS_RS.T_MEST_PERIOD_BLOOD", tMestPeriodBlood, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert HIS_MEST_PERIOD_BLOOD that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisMestPeriodBlood, HIS_MEST_PERIOD_BLOOD>();

                        MestPeriodBloodResultHolder rs = (MestPeriodBloodResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_MEST_PERIOD_BLOOD>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            recentHisMestPeriodBloods.AddRange(listData);
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
                MestPeriodBloodResultHolder rs = new MestPeriodBloodResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMestPeriodBlood sereServ = (TMestPeriodBlood)parameters[0].Value;
                    rs.Data = sereServ.MestPeriodBloodArray;
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
                if (IsNotNullOrEmpty(this.recentHisMestPeriodBloods))
                {
                    //Luu y:
                    //+ can cap nhat medicine_id, Bloodrial_id, blood_id ve null de tranh truong hop fk khi nguoi dung xoa thuoc/vat tu/mau sau khi xoa sere_serv
                    List<long> ids = this.recentHisMestPeriodBloods.Select(o => o.ID).ToList();
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

    class MestPeriodBloodResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMestPeriodBlood[] Data { get; set; }
    }
}
