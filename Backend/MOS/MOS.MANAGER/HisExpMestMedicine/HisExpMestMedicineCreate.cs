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

namespace MOS.MANAGER.HisExpMestMedicine
{
    class ExpMestMedicineResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisExpMestMedicine[] Data { get; set; }
    }
    class HisExpMestMedicineCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_MEDICINE> recentHisExpMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();

        internal HisExpMestMedicineCreate()
            : base()
        {

        }

        internal HisExpMestMedicineCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                //trong truong hop ke don thi lay theo thong tin do nguoi dung nhap
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                data.APPROVAL_LOGINNAME = string.IsNullOrWhiteSpace(data.APPROVAL_LOGINNAME) ? loginName : data.APPROVAL_LOGINNAME;
                data.APPROVAL_USERNAME = string.IsNullOrWhiteSpace(data.APPROVAL_USERNAME) ? userName : data.APPROVAL_USERNAME;
                data.APPROVAL_TIME = data.APPROVAL_TIME <= 0 ? Inventec.Common.DateTime.Get.Now().Value : data.APPROVAL_TIME;
                //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                if (!data.SERE_SERV_PARENT_ID.HasValue)
                {
                    data.IS_OUT_PARENT_FEE = null;
                }

                HisExpMestMedicineCheck checker = new HisExpMestMedicineCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisExpMestMedicineDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestMedicine that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestMedicines.Add(data);
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

        internal bool CreateList(List<HIS_EXP_MEST_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMedicineCheck checker = new HisExpMestMedicineCheck(param);
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                long approvalTime = Inventec.Common.DateTime.Get.Now().Value;

                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);

                    //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    if (!data.SERE_SERV_PARENT_ID.HasValue)
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    if (!DAOWorker.HisExpMestMedicineDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestMedicine_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestMedicine that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisExpMestMedicines.AddRange(listData);
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


        internal bool CreateListSql(List<HIS_EXP_MEST_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMedicineCheck checker = new HisExpMestMedicineCheck(param);

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
                    Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, THisExpMestMedicine>();
                    List<THisExpMestMedicine> input = Mapper.Map<List<THisExpMestMedicine>>(listData);

                    THisExpMestMedicine[] expMestMedicineArray = new THisExpMestMedicine[input.Count];
                    for (int i = 0; i < input.Count; i++)
                    {
                        expMestMedicineArray[i] = input[i];

                        expMestMedicineArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMedicineArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        expMestMedicineArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                        expMestMedicineArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                    }

                    TExpMestMedicine tExpMestMedicine = new TExpMestMedicine();
                    tExpMestMedicine.ExpMestMedicineArray = expMestMedicineArray;

                    string storedSql = "PKG_INSERT_EXP_MEST_MEDICINE.PRO_EXECUTE";

                    OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TExpMestMedicine>("P_EXP_MEST_MEDICINE", "HIS_RS.T_EXP_MEST_MEDICINE", tExpMestMedicine, ParameterDirection.InputOutput);
                    OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    object resultHolder = null;

                    if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                    {
                        throw new Exception("Insert exp_mest_medicine that bai" + LogUtil.TraceData("input", input));
                    }

                    if (resultHolder != null)
                    {
                        Mapper.CreateMap<THisExpMestMedicine, HIS_EXP_MEST_MEDICINE>();

                        ExpMestMedicineResultHolder rs = (ExpMestMedicineResultHolder)resultHolder;
                        result = rs.IsSuccess;
                        List<HIS_EXP_MEST_MEDICINE> datas = rs.Data != null ? Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(rs.Data.ToList()) : null;
                        if (result)
                        {
                            for (int i = 0; i < listData.Count; i++)
                            {
                                listData[i].ID = datas[i].ID;
                            }
                            this.recentHisExpMestMedicines.AddRange(listData);
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
                ExpMestMedicineResultHolder rs = new ExpMestMedicineResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TExpMestMedicine expMestMedicine = (TExpMestMedicine)parameters[0].Value;
                    rs.Data = expMestMedicine.ExpMestMedicineArray;
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
            if (IsNotNullOrEmpty(this.recentHisExpMestMedicines))
            {
                //su dung "delete" (cap nhat is_delete --> 1) de tranh hieu nang khi xoa vao bang du lieu lon
                if (!DAOWorker.HisExpMestMedicineDAO.DeleteList(this.recentHisExpMestMedicines))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestMedicine that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestMedicines", this.recentHisExpMestMedicines));
                }
                else
                {
                    this.recentHisExpMestMedicines = null;
                }
            }
        }
    }
}
