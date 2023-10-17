using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Sql;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisService;
using MOS.OracleUDT;
using MOS.UTILITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisMaterialType
{
    class MaterialTypeResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMaterialType[] Data { get; set; }
        public THisService[] DataService { get; set; }
    }

    class HisMaterialTypeCreateSql : BusinessBase
    {
        private List<HIS_MATERIAL_TYPE> recentMaterialTypeDTOs = new List<HIS_MATERIAL_TYPE>();

        internal HisMaterialTypeCreateSql()
            : base()
        {

        }

        internal HisMaterialTypeCreateSql(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MATERIAL_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                HisMaterialTypeCheck checker = new HisMaterialTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                List<HIS_SERVICE> listService = new List<HIS_SERVICE>();
                foreach (HIS_MATERIAL_TYPE data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MATERIAL_TYPE_CODE, null);
                    valid = valid && serviceChecker.ExistsCode(data.MATERIAL_TYPE_CODE, null);

                    if (valid)
                    {
                        HIS_MATERIAL_TYPE parent = null;
                        data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT;
                        data.HIS_SERVICE.SERVICE_CODE = data.MATERIAL_TYPE_CODE;
                        data.HIS_SERVICE.SERVICE_NAME = data.MATERIAL_TYPE_NAME;
                        if (data.PARENT_ID.HasValue)
                        {
                            parent = new HisMaterialTypeGet().GetById(data.PARENT_ID.Value);
                            data.HIS_SERVICE.PARENT_ID = parent.SERVICE_ID;
                        }
                        data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                        data.HIS_SERVICE.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                        data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;


                        if (!data.IMP_UNIT_ID.HasValue)
                        {
                            data.IMP_UNIT_CONVERT_RATIO = null;
                        }

                        //Them theo thu tu nhu MaterialType de xu ly Store chinh xac
                        listService.Add(data.HIS_SERVICE);
                    }
                }
                if (valid)
                {
                    if (!this.ProcessSave(listData, listService))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMaterialType that bai." + LogUtil.TraceData("listData", listData));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool ProcessSave(List<HIS_MATERIAL_TYPE> listData, List<HIS_SERVICE> listService)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_MATERIAL_TYPE, THisMaterialType>();
                List<THisMaterialType> input = Mapper.Map<List<THisMaterialType>>(listData);
                Mapper.CreateMap<HIS_SERVICE, THisService>();
                List<THisService> services = Mapper.Map<List<THisService>>(listService);

                THisMaterialType[] materialTypeArray = new THisMaterialType[input.Count];
                for (int i = 0; i < input.Count; i++)
                {
                    materialTypeArray[i] = input[i];

                    materialTypeArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    materialTypeArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    materialTypeArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                    materialTypeArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                }

                THisService[] serviceArray = new THisService[services.Count];
                for (int i = 0; i < services.Count; i++)
                {
                    serviceArray[i] = services[i];

                    serviceArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    serviceArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    serviceArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                    serviceArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                }

                TMaterialType tMateialType = new TMaterialType();
                tMateialType.MaterialTypeArray = materialTypeArray;
                TService tService = new TService();
                tService.ServiceArray = serviceArray;

                string storedSql = "PKG_INSERT_MATERIAL_TYPE.PRO_EXECUTE";

                OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMaterialType>("P_MATERIAL_TYPE", "HIS_RS.T_MATERIAL_TYPE", tMateialType, ParameterDirection.InputOutput);
                OracleParameter param2 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TService>("P_SERVICE", "HIS_RS.T_SERVICE", tService, ParameterDirection.InputOutput);
                OracleParameter param3 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                object resultHolder = null;

                if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2, param3))
                {
                    throw new Exception("Insert materialType that bai" + LogUtil.TraceData("input", input));
                }

                if (resultHolder != null)
                {
                    Mapper.CreateMap<THisMaterialType, HIS_MATERIAL_TYPE>();

                    MaterialTypeResultHolder rs = (MaterialTypeResultHolder)resultHolder;
                    result = rs.IsSuccess;
                    listData = rs.Data != null ? Mapper.Map<List<HIS_MATERIAL_TYPE>>(rs.Data.ToList()) : null;
                    if (result)
                    {
                        this.recentMaterialTypeDTOs.AddRange(listData);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, OracleDataReader dataReader, params OracleParameter[] parameters)
        {
            try
            {
                MaterialTypeResultHolder rs = new MaterialTypeResultHolder();
                if (parameters[2] != null && parameters[2].Value != null && parameters[2].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[2].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMaterialType materialType = (TMaterialType)parameters[0].Value;
                    rs.Data = materialType.MaterialTypeArray;
                }
                if (rs.IsSuccess && parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    TService service = (TService)parameters[1].Value;
                    rs.DataService = service.ServiceArray;
                }
                resultHolder = rs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMaterialTypeDTOs))
                {
                    DAOWorker.HisMaterialTypeDAO.TruncateList(this.recentMaterialTypeDTOs);
                    new HisServiceTruncate(param).TruncateListId(this.recentMaterialTypeDTOs.Select(s => s.SERVICE_ID).ToList());
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
