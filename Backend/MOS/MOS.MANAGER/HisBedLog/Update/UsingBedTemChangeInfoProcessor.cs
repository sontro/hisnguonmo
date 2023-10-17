using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBedLog.Update
{
    class UsingBedTemChangeInfoProcessor : BusinessBase
    {
        private HisSereServPackage37 hisSereServPackage37;
        private HisSereServPackageBirth hisSereServPackageBirth;
        private HisSereServPackagePttm hisSereServPackagePttm;

        internal UsingBedTemChangeInfoProcessor()
            : base()
        {
            this.Init();
        }

        internal UsingBedTemChangeInfoProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
        }

        internal bool Run(HIS_TREATMENT treatment, HIS_BED_LOG newBedLog, WorkPlaceSDO workPlace, ref Dictionary<HIS_SERVICE_REQ, HIS_SERE_SERV> dicUpdateSRSS, ref List<HIS_SERE_SERV> beforeUpdateSS, ref List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            bool result = false;
            try
            {
                if (IsNotNull(treatment))
                {
                    List<HIS_SERVICE_REQ> serviceReqs = null;
                    List<HIS_SERE_SERV> sereServs = null;
                    if (dicUpdateSRSS != null && dicUpdateSRSS.Count > 0)
                    {
                        serviceReqs = dicUpdateSRSS.Keys.ToList();
                        sereServs = dicUpdateSRSS.Values.ToList();
                    }
                    else
                    {
                        string srSql = "SELECT * FROM HIS_SERVICE_REQ WHERE IS_DELETE = 0 AND BED_LOG_ID =: param1";
                        serviceReqs = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(srSql, newBedLog.ID);
                        if (IsNotNullOrEmpty(serviceReqs))
                        {
                            string ssSql = "SELECT * FROM HIS_SERE_SERV WHERE IS_DELETE = 0 AND %IN_CLAUSE%";
                            ssSql = DAOWorker.SqlDAO.AddInClause(serviceReqs.Select(s => s.ID).ToList(), ssSql, "SERVICE_REQ_ID");
                            sereServs = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(ssSql);
                            beforeUpdateSS.AddRange(sereServs);
                        }
                    }

                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        foreach (var s in sereServs)
                        {
                            s.SERVICE_ID = newBedLog.BED_SERVICE_TYPE_ID.Value;
                            s.PATIENT_TYPE_ID = newBedLog.PATIENT_TYPE_ID.Value;
                            s.SHARE_COUNT = newBedLog.SHARE_COUNT;
                        }

                        ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                        HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
                        this.hisSereServPackage37 = new HisSereServPackage37(param, treatment.ID, workPlace.RoomId, workPlace.DepartmentId, sereServs);
                        this.hisSereServPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, sereServs);
                        this.hisSereServPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, sereServs);
                        HisSereServSetInfo addInfo = new HisSereServSetInfo(param, ptas);

                        foreach (HIS_SERVICE_REQ sr in serviceReqs)
                        {
                            List<HIS_SERE_SERV> ss = sereServs.Where(o => o.SERVICE_REQ_ID.HasValue && o.SERVICE_REQ_ID.Value == sr.ID).ToList();
                            if (ss != null && ss.Count > 0)
                            {
                                foreach (HIS_SERE_SERV sereServ in ss)
                                {
                                    HisSereServUtil.SetTdl(sereServ, sr);

                                    if (!addInfo.AddInfo(sereServ))
                                    {
                                        throw new Exception("Ket thuc nghiep vu, rollback du lieu");
                                    }
                                    if (!priceAdder.AddPrice(sereServ, sereServ.TDL_INTRUCTION_TIME, sereServ.TDL_EXECUTE_BRANCH_ID, sereServ.TDL_REQUEST_ROOM_ID, sereServ.TDL_REQUEST_DEPARTMENT_ID, sereServ.TDL_EXECUTE_ROOM_ID))
                                    {
                                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                                    }
                                    dicUpdateSRSS[sr] = sereServ;
                                }
                                //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                                this.hisSereServPackage37.Apply3Day7Day(ss, sr.INTRUCTION_TIME);
                                //Xu ly de ap dung goi de
                                this.hisSereServPackageBirth.Run(ss, ss[0].PARENT_ID);
                                //Xu ly de ap dung goi phau thuat tham my
                                this.hisSereServPackagePttm.Run(ss, ss[0].PARENT_ID, sr.INTRUCTION_TIME);
                            }
                        }
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            //if (this.hisSereServUpdateHein != null)
            //{
            //    this.hisSereServUpdateHein.RollbackData();
            //}
        }
    }
}
