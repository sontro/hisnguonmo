using AutoMapper;
using HIS.ERXConnect;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Common.Update;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Auto
{
    public class HisExpMestUploadErx : BusinessBase
    {
        private static bool IsRunning;

        const int MaxReq = 100;
        const string HinhThuc_NoiTru = "noitru";
        const string HinhThuc_NgoaiTru = "ngoaitru";
        const string Loai_co_ban = "c";
        const string Loai_huong_than = "h";
        const string Loai_gay_nghien = "n";
        const string TUTORIAL_DEFAULT = ".";

        readonly List<long> ServiceReqType = new List<long>() {
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK };

        public HisExpMestUploadErx()
            : base()
        {

        }

        public HisExpMestUploadErx(CommonParam param)
            : base(param)
        {

        }

        public void Run()
        {
            try
            {
                if (!IsNotNull(Config.HisExpMestCFG.ERX_CONFIG))
                {
                    LogSystem.Info("He thong khong cau hinh dia chi");
                    return;
                }

                if (Config.HisExpMestCFG.ERX_CONFIG.Split('|').Count() < 3)
                {
                    LogSystem.Info("Thong tin cau hinh he thong lien thong don thuoc khong dung");
                    LogSystem.Info(Config.HisExpMestCFG.ERX_CONFIG);
                    return;
                }

                if (IsRunning)
                {
                    LogSystem.Info("Tien trinh dang duoc chay khong cho phep khoi tao tien trinh khac");
                    return;
                }

                IsRunning = true;

                string sql = "SELECT * FROM HIS_SERVICE_REQ REQ WHERE SERVICE_REQ_TYPE_ID IN ({0}) AND NVL(IS_SENT_ERX,0) = 0 AND EXISTS (SELECT 1 FROM HIS_TREATMENT TREA WHERE TREA.ID = REQ.TREATMENT_ID AND TREA.OUT_TIME BETWEEN :PARAM1 AND :PARAM2)";
                sql = string.Format(sql, string.Join(",", GetServiceReqType()));
                long dateFrom = Convert.ToInt64(DateTime.Now.AddDays(-HisServiceReqCFG.INTEGRATE_SYSTEM_DAY_NUM_SYNC).ToString("yyyyMMdd") + "000000");
                long dateTo = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd") + "000000");

                List<HIS_SERVICE_REQ> serviceReq = MANAGER.Base.DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(sql, dateFrom, dateTo);

                if (IsNotNullOrEmpty(serviceReq))
                {
                    List<HIS_EMPLOYEE> listEmployee = Config.HisEmployeeCFG.DATA.Where(o => !string.IsNullOrWhiteSpace(o.ERX_LOGINNAME) && !string.IsNullOrWhiteSpace(o.ERX_PASSWORD)).ToList();

                    serviceReq = serviceReq.Where(o => listEmployee.Select(s => s.LOGINNAME).Contains(o.REQUEST_LOGINNAME)).ToList();
                    List<HIS_SERVICE_REQ> updateData = new List<HIS_SERVICE_REQ>();
                    ProcessSendToErx(serviceReq, ref updateData);
                    if (IsNotNullOrEmpty(updateData))
                    {
                        List<HIS_SERVICE_REQ> resultData = null;
                        if (!new HisServiceReqUpdateSentErx(param).Run(updateData, ref resultData))
                        {
                            Inventec.Common.Logging.LogSystem.Info("___listExpCode: " + string.Join(",", updateData.Select(s => s.SERVICE_REQ_CODE).Distinct()));
                            throw new Exception("Cap nhat trang thai lien thong don thuoc that bai");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            IsRunning = false;
        }

        ///lọc các loại xuất theo cấu hình
        private List<long> GetServiceReqType()
        {
            List<long> result = new List<long>();
            try
            {
                result.AddRange(ServiceReqType);

                if (IsNotNull(Config.HisExpMestCFG.TYPE_DO_NOT_ALLOW_SENDING))
                {
                    string[] typeCode = Config.HisExpMestCFG.TYPE_DO_NOT_ALLOW_SENDING.Split(',');
                    foreach (var code in typeCode)
                    {
                        var reqType = Config.HisServiceReqTypeCFG.DATA.FirstOrDefault(o => o.SERVICE_REQ_TYPE_CODE.ToLower() == code.ToLower());
                        if (IsNotNull(reqType))
                        {
                            switch (reqType.ID)
                            {
                                case IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT:
                                    result.Remove(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT);
                                    break;
                                case IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK:
                                    result.Remove(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK);
                                    break;
                                case IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT:
                                    result.Remove(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = ServiceReqType;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private void ProcessSendToErx(List<HIS_SERVICE_REQ> serviceReq, ref List<HIS_SERVICE_REQ> updateData)
        {
            try
            {
                if (!IsNotNullOrEmpty(serviceReq)) return;

                List<HIS_TREATMENT> listTreatment = null;
                List<HIS_DHST> listDhst = null;
                List<HIS_SERVICE_REQ_METY> listReqMety = null;
                List<V_HIS_SERE_SERV_2> listSereServ = null;

                LoadDataForProcess(serviceReq, ref listTreatment, ref listDhst, ref listReqMety, ref listSereServ);

                var cfg = Config.HisExpMestCFG.ERX_CONFIG.Split('|');

                foreach (var branch in Config.HisBranchCFG.DATA)
                {
                    if (String.IsNullOrWhiteSpace(branch.HEIN_MEDI_ORG_CODE))
                        continue;

                    var listDepartment = Config.HisDepartmentCFG.DATA.Where(o => o.BRANCH_ID == branch.ID).ToList();
                    if (!IsNotNullOrEmpty(listDepartment))
                        continue;

                    DataInput sendData = new DataInput();
                    sendData.Url = cfg[0];
                    sendData.HospitalLoginname = cfg[1];
                    sendData.HospitalPassword = cfg[2];
                    sendData.MediOrgCode = branch.HEIN_MEDI_ORG_CODE;
                    sendData.ListDhst = listDhst;
                    sendData.ListEmplyee = Config.HisEmployeeCFG.DATA;
                    sendData.ListServiceReq = serviceReq.Where(o => listDepartment.Select(s => s.ID).Contains(o.REQUEST_DEPARTMENT_ID)).ToList();
                    sendData.ListMedicineType = Config.HisMedicineTypeCFG.DATA;
                    sendData.ListSereServs = listSereServ;
                    sendData.ListReqMety = listReqMety;
                    sendData.ListServiceUnit = Config.HisServiceUnitCFG.DATA;
                    sendData.ListTreatment = listTreatment;

                    var dataResult = new HIS.ERXConnect.ERXConnectProcessor().SendPrescription(sendData);

                    if (dataResult != null && dataResult.Datas != null && dataResult.Datas.Count > 0)
                    {
                        var groupByServiceReqCode = dataResult.Datas.GroupBy(o => o.ServiceReqCode);
                        foreach (var data in groupByServiceReqCode)
                        {
                            var sere = serviceReq.FirstOrDefault(o => o.SERVICE_REQ_CODE == data.Key);
                            if (sere != null)
                            {
                                if (IsNotNullOrEmpty(data.ToList()))
                                {
                                    if (!data.ToList().Exists(o => o.Success == false))
                                    {
                                        updateData.Add(new HIS_SERVICE_REQ() { ID = sere.ID, IS_SENT_ERX = 1 });
                                    }
                                    else
                                    {
                                        string message = string.Join(",", data.ToList().Select(o => string.Join(",", o.ErrorMessage)));
                                        updateData.Add(new HIS_SERVICE_REQ() { ID = sere.ID, IS_SENT_ERX = 2, ERX_DESC = message });
                                    }
                                }
                            }
                        }
                    }

                    if (dataResult != null && dataResult.Messages != null && dataResult.Messages.Count > 0)
                    {
                        Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("___HisExpMestUploadErx__ErrorMessage:", dataResult.Messages));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataForProcess(List<HIS_SERVICE_REQ> serviceReq, ref List<HIS_TREATMENT> listTreatment, ref List<HIS_DHST> listDhst, ref List<HIS_SERVICE_REQ_METY> listReqMety, ref List<V_HIS_SERE_SERV_2> listSereServ)
        {
            try
            {
                if (IsNotNullOrEmpty(serviceReq))
                {
                    listTreatment = new List<HIS_TREATMENT>();
                    listDhst = new List<HIS_DHST>();
                    listReqMety = new List<HIS_SERVICE_REQ_METY>();
                    listSereServ = new List<V_HIS_SERE_SERV_2>();

                    List<long> listTreatmentId = serviceReq.Select(s => s.TREATMENT_ID).Distinct().ToList();

                    int skip = 0;
                    while (listTreatmentId.Count - skip > 0)
                    {
                        var listIds = listTreatmentId.Skip(skip).Take(MaxReq).ToList();
                        skip += MaxReq;

                        CommonParam param = new CommonParam();
                        HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                        filter.IDs = listIds;
                        var treatment = new HisTreatmentGet(param).Get(filter);
                        if (treatment != null && treatment.Count > 0)
                        {
                            listTreatment.AddRange(treatment);
                        }

                        HisDhstFilterQuery filterDhst = new HisDhstFilterQuery();
                        filterDhst.TREATMENT_IDs = listIds;
                        var dhst = new HisDhstGet(param).Get(filterDhst);
                        if (dhst != null && dhst.Count > 0)
                        {
                            dhst = dhst.Where(o => o.WEIGHT.HasValue).ToList();
                            if (dhst != null && dhst.Count > 0)
                            {
                                listDhst.AddRange(dhst);
                            }
                        }
                    }

                    List<long> serviceReqIds = serviceReq.Select(s => s.ID).ToList();
                    skip = 0;
                    while (serviceReqIds.Count - skip > 0)
                    {
                        var listIds = serviceReqIds.Skip(skip).Take(MaxReq).ToList();
                        skip += MaxReq;

                        CommonParam param = new CommonParam();

                        HisSereServView2FilterQuery ssFilter = new HisSereServView2FilterQuery();
                        ssFilter.SERVICE_REQ_IDs = listIds;
                        var sereServ = new HisSereServGet(param).GetView2(ssFilter);
                        if (sereServ != null && sereServ.Count > 0)
                        {
                            listSereServ.AddRange(sereServ);
                        }

                        HisServiceReqMetyFilterQuery metyFilter = new HisServiceReqMetyFilterQuery();
                        metyFilter.SERVICE_REQ_IDs = listIds;
                        var metys = new HisServiceReqMetyGet(param).Get(metyFilter);
                        if (metys != null && metys.Count > 0)
                        {
                            listReqMety.AddRange(metys);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
