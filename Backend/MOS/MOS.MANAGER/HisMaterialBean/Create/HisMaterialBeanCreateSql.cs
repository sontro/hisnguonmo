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

namespace MOS.MANAGER.HisMaterialBean
{
    class MaterialBeanResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMaterialBean[] Data { get; set; }
    }

    class HisMaterialBeanCreateSql : BusinessBase
    {
        private List<HIS_MATERIAL_BEAN> recentHisMaterialBeans = new List<HIS_MATERIAL_BEAN>();

        internal HisMaterialBeanCreateSql()
            : base()
        {

        }

        internal HisMaterialBeanCreateSql(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MATERIAL_BEAN> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMaterialBeanCheck checker = new HisMaterialBeanCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {

                    Mapper.CreateMap<HIS_MATERIAL_BEAN, THisMaterialBean>();
                    List<THisMaterialBean> input = Mapper.Map<List<THisMaterialBean>>(listData);
                    THisMaterialBean[] sereServArray = new THisMaterialBean[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        sereServArray[i] = input[i];
                        sereServArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        sereServArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        sereServArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        sereServArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TMaterialBean tMaterialBean = new TMaterialBean();
                    tMaterialBean.MaterialBeanArray = sereServArray;

                    string storedSql = "PKG_INSERT_MATERIAL_BEAN.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMaterialBean>("P_MATERIAL_BEAN", "HIS_RS.T_MATERIAL_BEAN", tMaterialBean, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert material_bean that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisMaterialBean, HIS_MATERIAL_BEAN>();

                        MaterialBeanResultHolder rs = (MaterialBeanResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        listData = rs.Data != null ? Mapper.Map<List<HIS_MATERIAL_BEAN>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            recentHisMaterialBeans.AddRange(listData);
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
                MaterialBeanResultHolder rs = new MaterialBeanResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMaterialBean sereServ = (TMaterialBean)parameters[0].Value;
                    rs.Data = sereServ.MaterialBeanArray;
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
                if (IsNotNullOrEmpty(this.recentHisMaterialBeans))
                {
                    List<long> ids = this.recentHisMaterialBeans.Select(o => o.ID).ToList();
                    List<string> sqls = new List<string>();
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1 WHERE %IN_CLAUSE%", "ID"));
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(ids, "DELETE HIS_MATERIAL_BEAN WHERE %IN_CLAUSE%", "ID"));
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
