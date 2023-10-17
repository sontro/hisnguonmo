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

namespace MOS.MANAGER.HisService
{
    class ServiceResultHolder
    {
        public bool IsSuccess { get; set; }
        public THisService[] DataService { get; set; }
    }

    class HisServiceCreateSql : BusinessBase
    {
        private List<HIS_SERVICE> recentHisServiceDTOs = new List<HIS_SERVICE>();

        internal HisServiceCreateSql()
            : base()
        {

        }

        internal HisServiceCreateSql(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Run(List<HIS_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(listData);
                HisServiceCheck checker = new HisServiceCheck(param);
                List<HIS_SERVICE> listParent = new List<HIS_SERVICE>();
                foreach (HIS_SERVICE data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsValidData(data);
                }

                if (valid && this.ProcessSave(listData))
                {
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

        private bool ProcessSave(List<HIS_SERVICE> listService)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_SERVICE, THisService>();
                List<THisService> services = Mapper.Map<List<THisService>>(listService);

                THisService[] serviceArray = new THisService[services.Count];
                for (int i = 0; i < services.Count; i++)
                {
                    serviceArray[i] = services[i];

                    serviceArray[i].CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    serviceArray[i].MODIFIER = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    serviceArray[i].APP_CREATOR = Constant.APPLICATION_CODE;
                    serviceArray[i].APP_MODIFIER = Constant.APPLICATION_CODE;
                }

                TService tService = new TService();
                tService.ServiceArray = serviceArray;

                string storedSql = "PKG_INSERT_SERVICE.PRO_EXECUTE";

                OracleParameter param1 = SqlExecuteUnmanaged.CreateCustomTypeArrayInputParameter<TService>("P_SERVICE", "HIS_RS.T_SERVICE", tService, ParameterDirection.InputOutput);
                OracleParameter param2 = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                object resultHolder = null;

                if (!DAOWorker.SqlDAO.ExecuteStoredUnmanaged(OutputHandler, ref resultHolder, storedSql, param1, param2))
                {
                    throw new Exception("Insert service that bai" + LogUtil.TraceData("services", services));
                }

                if (resultHolder != null)
                {
                    Mapper.CreateMap<THisService, HIS_SERVICE>();

                    ServiceResultHolder rs = (ServiceResultHolder)resultHolder;
                    result = rs.IsSuccess;
                    listService = rs.DataService != null ? Mapper.Map<List<HIS_SERVICE>>(rs.DataService.ToList()) : null;
                    if (result)
                    {
                        this.recentHisServiceDTOs.AddRange(listService);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        //Xu ly ket qua tra ve khi goi procedure
        private void OutputHandler(ref object resultHolder, OracleDataReader dataReader, params OracleParameter[] parameters)
        {
            try
            {
                ServiceResultHolder rs = new ServiceResultHolder();
                if (parameters[1] != null && parameters[1].Value != null && parameters[1].Value != DBNull.Value)
                {
                    rs.IsSuccess = Int32.Parse(parameters[1].Value.ToString()) == Constant.IS_TRUE;
                }

                if (rs.IsSuccess && parameters[0] != null && parameters[0].Value != null && parameters[0].Value != DBNull.Value)
                {
                    TService service = (TService)parameters[0].Value;
                    rs.DataService = service.ServiceArray;
                }
                resultHolder = rs;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
