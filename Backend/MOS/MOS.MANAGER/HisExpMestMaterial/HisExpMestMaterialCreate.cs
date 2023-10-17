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

namespace MOS.MANAGER.HisExpMestMaterial
{
    class ExpMestMaterialResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisExpMestMaterial[] Data { get; set; }
    }

    class HisExpMestMaterialCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_MATERIAL> recentHisExpMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();

        internal HisExpMestMaterialCreate()
            : base()
        {

        }

        internal HisExpMestMaterialCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_MATERIAL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMaterialCheck checker = new HisExpMestMaterialCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    if (!data.SERE_SERV_PARENT_ID.HasValue)
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }

                    StentUtil.SetStentOrder(data);

                    if (!DAOWorker.HisExpMestMaterialDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestMaterial that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestMaterials.Add(data);
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

        internal bool CreateList(List<HIS_EXP_MEST_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMaterialCheck checker = new HisExpMestMaterialCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);

                    if (!data.SERE_SERV_PARENT_ID.HasValue) //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    StentUtil.SetStentOrder(listData);

                    if (!DAOWorker.HisExpMestMaterialDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMaterial_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestMaterial that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpMestMaterials.AddRange(listData);
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

        internal bool CreateListSql(List<HIS_EXP_MEST_MATERIAL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMaterialCheck checker = new HisExpMestMaterialCheck(param);

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);

                    if (!data.SERE_SERV_PARENT_ID.HasValue) //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    StentUtil.SetStentOrder(listData);

                    Mapper.CreateMap<HIS_EXP_MEST_MATERIAL, THisExpMestMaterial>();
                    List<THisExpMestMaterial> input = Mapper.Map<List<THisExpMestMaterial>>(listData);

                    THisExpMestMaterial[] expMestMaterialArray = new THisExpMestMaterial[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        expMestMaterialArray[i] = input[i];

                        expMestMaterialArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMaterialArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMaterialArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        expMestMaterialArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TExpMestMaterial tExpMestMaterial = new TExpMestMaterial();
                    tExpMestMaterial.ExpMestMaterialArray = expMestMaterialArray;

                    string storedSql = "PKG_INSERT_EXP_MEST_MATERIAL.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TExpMestMaterial>("P_EXP_MEST_MATERIAL", "HIS_RS.T_EXP_MEST_MATERIAL", tExpMestMaterial, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert exp_mest_material that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisExpMestMaterial, HIS_EXP_MEST_MATERIAL>();

                        ExpMestMaterialResultHolder rs = (ExpMestMaterialResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        List<HIS_EXP_MEST_MATERIAL> datas = rs.Data != null ? Mapper.Map<List<HIS_EXP_MEST_MATERIAL>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            for (int i = 0; i < listData.Count; i++)
                            {
                                listData[i].ID = datas[i].ID;
                            }
                            this.recentHisExpMestMaterials.AddRange(listData);
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
                ExpMestMaterialResultHolder rs = new ExpMestMaterialResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TExpMestMaterial expMestMaterial = (TExpMestMaterial)parameters[0].Value;
                    rs.Data = expMestMaterial.ExpMestMaterialArray;
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
            if (IsNotNullOrEmpty(this.recentHisExpMestMaterials))
            {
                //su dung "delete" (cap nhat is_delete --> 1) de tranh hieu nang khi xoa vao bang du lieu lon
                if (!DAOWorker.HisExpMestMaterialDAO.DeleteList(this.recentHisExpMestMaterials))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestMaterial that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestMaterials", this.recentHisExpMestMaterials));
                }
                else
                {
                    this.recentHisExpMestMaterials = null;
                }
            }
        }
    }
}
