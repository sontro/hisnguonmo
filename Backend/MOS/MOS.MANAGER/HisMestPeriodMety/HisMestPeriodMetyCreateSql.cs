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

namespace MOS.MANAGER.HisMestPeriodMety
{
    class HisMestPeriodMetyCreateSql : BusinessBase
    {
        private List<HIS_MEST_PERIOD_METY> recentHisMestPeriodMetyDTOs = new List<HIS_MEST_PERIOD_METY>();

        internal HisMestPeriodMetyCreateSql()
            : base()
        {

        }

        internal HisMestPeriodMetyCreateSql(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MEST_PERIOD_METY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                if (valid)
                {
                    Mapper.CreateMap<HIS_MEST_PERIOD_METY, THisMestPeriodMety>();
                    List<THisMestPeriodMety> input = Mapper.Map<List<THisMestPeriodMety>>(listData);

                    THisMestPeriodMety[] mestPeriodMetyArray = new THisMestPeriodMety[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        mestPeriodMetyArray[i] = input[i];

                        mestPeriodMetyArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMetyArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        mestPeriodMetyArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        mestPeriodMetyArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TMestPeriodMety tMestPeriodMety = new TMestPeriodMety();
                    tMestPeriodMety.MestPeriodMetyArray = mestPeriodMetyArray;

                    string storedSql = "PKG_INSERT_MEST_PERIOD_METY.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMestPeriodMety>("P_MEST_PERIOD_METY", "HIS_RS.T_MEST_PERIOD_METY", tMestPeriodMety, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert HIS_MEST_PERIOD_METY that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisMestPeriodMety, HIS_MEST_PERIOD_METY>();

                        MestPeriodMetyResultHolder rs = (MestPeriodMetyResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_MEST_PERIOD_METY>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            recentHisMestPeriodMetyDTOs.AddRange(listData);
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
                MestPeriodMetyResultHolder rs = new MestPeriodMetyResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMestPeriodMety mestPeriodMety = (TMestPeriodMety)parameters[0].Value;
                    rs.Data = mestPeriodMety.MestPeriodMetyArray;
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
                if (IsNotNullOrEmpty(this.recentHisMestPeriodMetyDTOs))
                {
                    //Luu y:
                    //+ can cap nhat medicine_id, material_id, blood_id ve null de tranh truong hop fk khi nguoi dung xoa thuoc/vat tu/mau sau khi xoa sere_serv
                    List<long> ids = this.recentHisMestPeriodMetyDTOs.Select(o => o.ID).ToList();
                    if (!DAOWorker.SqlDAO.Execute(DAOWorker.SqlDAO.AddInClause(ids, "DELETE HIS_MEST_PERIOD_METY WHERE %IN_CLAUSE%", "ID")))
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

    class MestPeriodMetyResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMestPeriodMety[] Data { get; set; }
    }
}
