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

namespace MOS.MANAGER.HisMedicineBean
{
    class MedicineBeanResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMedicineBean[] Data { get; set; }
    }

    class HisMedicineBeanCreateSql : BusinessBase
    {
        private List<HIS_MEDICINE_BEAN> recentHisMedicineBeans = new List<HIS_MEDICINE_BEAN>();

        internal HisMedicineBeanCreateSql()
            : base()
        {

        }

        internal HisMedicineBeanCreateSql(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MEDICINE_BEAN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineBeanCheck checker = new HisMedicineBeanCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {

                    Mapper.CreateMap<HIS_MEDICINE_BEAN, THisMedicineBean>();
                    List<THisMedicineBean> input = Mapper.Map<List<THisMedicineBean>>(listData);
                    THisMedicineBean[] sereServArray = new THisMedicineBean[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        sereServArray[i] = input[i];
                        sereServArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        sereServArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        sereServArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        sereServArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TMedicineBean tMedicineBean = new TMedicineBean();
                    tMedicineBean.MedicineBeanArray = sereServArray;

                    string storedSql = "PKG_INSERT_MEDICINE_BEAN.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMedicineBean>("P_MEDICINE_BEAN", "HIS_RS.T_MEDICINE_BEAN", tMedicineBean, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert medicine_bean that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisMedicineBean, HIS_MEDICINE_BEAN>();

                        MedicineBeanResultHolder rs = (MedicineBeanResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_MEDICINE_BEAN>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            recentHisMedicineBeans.AddRange(listData);
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
                MedicineBeanResultHolder rs = new MedicineBeanResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMedicineBean sereServ = (TMedicineBean)parameters[0].Value;
                    rs.Data = sereServ.MedicineBeanArray;
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
                if (IsNotNullOrEmpty(this.recentHisMedicineBeans))
                {
                    List<long> ids = this.recentHisMedicineBeans.Select(o => o.ID).ToList();
                    List<string> sqls = new List<string>();
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1 WHERE %IN_CLAUSE%", "ID"));
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(ids, "DELETE HIS_MEDICINE_BEAN WHERE %IN_CLAUSE%", "ID"));
                    if (!DAOWorker.SqlDAO.Execute(sqls))
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
}
