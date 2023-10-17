using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DAO.Sql;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMedicineTypeAcin;
using MOS.MANAGER.HisService;
using MOS.OracleUDT;
using MOS.UTILITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisMedicineType
{
    class MedicineTypeResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisMedicineType[] Data { get; set; }
        public THisService[] DataService { get; set; }
    }

    class HisMedicineTypeCreateSql : BusinessBase
    {
        private List<HIS_MEDICINE_TYPE> recentMedicineTypeDTOs = new List<HIS_MEDICINE_TYPE>();

        internal HisMedicineTypeCreateSql()
            : base()
        {

        }

        internal HisMedicineTypeCreateSql(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                HisMedicineTypeCheck checker = new HisMedicineTypeCheck(param);
                HisServiceCheck serviceChecker = new HisServiceCheck(param);
                List<HIS_SERVICE> listService = new List<HIS_SERVICE>();
                foreach (HIS_MEDICINE_TYPE data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDICINE_TYPE_CODE, null);
                    valid = valid && serviceChecker.ExistsCode(data.MEDICINE_TYPE_CODE, null);
                    if (valid)
                    {
                        HIS_MEDICINE_TYPE parent = null;
                        //luu du thua du lieu
                        data.HIS_SERVICE.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC;
                        data.HIS_SERVICE.SERVICE_CODE = data.MEDICINE_TYPE_CODE;
                        data.HIS_SERVICE.SERVICE_NAME = data.MEDICINE_TYPE_NAME;
                        data.HIS_SERVICE.NUM_ORDER = data.NUM_ORDER;
                        if (data.PARENT_ID.HasValue)
                        {
                            parent = new HisMedicineTypeGet().GetById(data.PARENT_ID.Value);
                            data.HIS_SERVICE.PARENT_ID = parent.SERVICE_ID;
                        }
                        data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE; //ban ghi moi tao ra luon la "la'"
                        data.HIS_SERVICE.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                        data.TDL_SERVICE_UNIT_ID = data.HIS_SERVICE.SERVICE_UNIT_ID;

                        if (!data.IMP_UNIT_ID.HasValue)
                        {
                            data.IMP_UNIT_CONVERT_RATIO = null;
                        }
                        //Them theo thu tu nhu MedicineType de xu ly Store chinh xac
                        listService.Add(data.HIS_SERVICE);
                    }
                }

                if (valid)
                {
                    if (!this.ProcessSave(listData, listService))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicineType that bai." + LogUtil.TraceData("listData", listData));
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

        private bool ProcessSave(List<HIS_MEDICINE_TYPE> listData, List<HIS_SERVICE> listService)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_MEDICINE_TYPE, THisMedicineType>();
                List<THisMedicineType> input = Mapper.Map<List<THisMedicineType>>(listData);
                Mapper.CreateMap<HIS_SERVICE, THisService>();
                List<THisService> services = Mapper.Map<List<THisService>>(listService);

                THisMedicineType[] medicineTypeArray = new THisMedicineType[input.Count];
                for (int i = 0; i < input.Count; i++)
                {
                    medicineTypeArray[i] = input[i];

                    medicineTypeArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    medicineTypeArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    medicineTypeArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                    medicineTypeArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
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

                TMedicineType tMedicineType = new TMedicineType();
                tMedicineType.MedicineTypeArray = medicineTypeArray;
                TService tService = new TService();
                tService.ServiceArray = serviceArray;

                string storedSql = "PKG_INSERT_MEDICINE_TYPE.PRO_EXECUTE";

                OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TMedicineType>("P_MEDICINE_TYPE", "HIS_RS.T_MEDICINE_TYPE", tMedicineType, ParameterDirection.InputOutput);
                OracleParameter param2 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TService>("P_SERVICE", "HIS_RS.T_SERVICE", tService, ParameterDirection.InputOutput);
                OracleParameter param3 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                object resultHolder = null;

                if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2, param3))
                {
                    throw new Exception("Insert medicineType that bai" + LogUtil.TraceData("input", input));
                }

                if (resultHolder != null)
                {
                    Mapper.CreateMap<THisMedicineType, HIS_MEDICINE_TYPE>();

                    MedicineTypeResultHolder rs = (MedicineTypeResultHolder)resultHolder;
                    result = rs.IsSuccess;
                    listData = rs.Data != null ? Mapper.Map<List<HIS_MEDICINE_TYPE>>(rs.Data.ToList()) : null;
                    if (result)
                    {
                        this.recentMedicineTypeDTOs.AddRange(listData);
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
                MedicineTypeResultHolder rs = new MedicineTypeResultHolder();
                if (parameters[2] != null && parameters[2].Value != null && parameters[2].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[2].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TMedicineType medicineType = (TMedicineType)parameters[0].Value;
                    rs.Data = medicineType.MedicineTypeArray;
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
                if (IsNotNullOrEmpty(this.recentMedicineTypeDTOs))
                {
                    DAOWorker.HisMedicineTypeDAO.TruncateList(this.recentMedicineTypeDTOs);
                    new HisServiceTruncate(param).TruncateListId(this.recentMedicineTypeDTOs.Select(s => s.SERVICE_ID).ToList());
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
