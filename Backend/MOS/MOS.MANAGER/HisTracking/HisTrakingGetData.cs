using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCare;
using MOS.MANAGER.HisCareDetail;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestBlood;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServRation;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReqMaty;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTracking
{
    public class HisTrakingGetData : BusinessBase
    {
        internal HisTrakingGetData()
            : base()
        {

        }

        internal HisTrakingGetData(CommonParam paramGet)
            : base(paramGet)
        {

        }

        private HIS_TREATMENT Treatment { get; set; }
        private List<HIS_SERVICE_REQ> ServiceRq { get; set; }
        private List<HIS_DHST> Dhst { get; set; }
        private List<V_HIS_SERE_SERV_RATION> vSSRation { get; set; }
        private List<V_HIS_TREATMENT_BED_ROOM> vTreatBedRoom { get; set; }
        private List<HIS_EXP_MEST> ExpMests { get; set; }
        private List<V_HIS_IMP_MEST_2> vImpMests2 { get; set; }
        private List<HIS_EXP_MEST_MEDICINE> ExpMestMedicines { get; set; }
        private List<HIS_EXP_MEST_MATERIAL> ExpMestMaterials { get; set; }
        private List<V_HIS_IMP_MEST_MEDICINE> vImpMestMedicines { get; set; }
        private List<V_HIS_IMP_MEST_MATERIAL> vImpMestMaterials { get; set; }
        private List<V_HIS_IMP_MEST_BLOOD> vImpMestBloods { get; set; }
        private List<HIS_SERVICE_REQ_METY> ServiceReqMetys { get; set; }
        private List<HIS_SERVICE_REQ_MATY> ServiceReqMatys { get; set; }
        private List<V_HIS_EXP_MEST_BLTY_REQ_2> vExpMestBityReqs2 { get; set; }
        private List<HIS_SERE_SERV> SereServs { get; set; }
        private List<HIS_SERE_SERV_EXT> SereServExts { get; set; }
        private List<HIS_CARE> HisCares { get; set; }
        private List<V_HIS_CARE_DETAIL> CareDetails { get; set; }
        bool? includeMaterial = null;
        int dem = 500;

        private void LoadDataTreatment(TrackingDataInputSDO data)
        {
            Treatment = new HIS_TREATMENT();
            try
            {
                Treatment = new HisTreatmentGet().GetById(data.TreatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataServiceRq(TrackingDataInputSDO data)
        {
            ServiceRq = new List<HIS_SERVICE_REQ>();
            try
            {
                ServiceRq = new HisServiceReqGet().GetByTreatmentId(data.TreatmentId);
                int skip = 0;
                List<Task> taslAll = new List<Task>();

                if (IsNotNullOrEmpty(ServiceRq))
                {
                    while (ServiceRq.Count - skip > 0)
                    {
                        var listSub = ServiceRq.Skip(skip).Take(dem).ToList();
                        skip += dem;
                        List<long> srId = listSub.Select(s => s.ID).ToList();

                        if (IsNotNullOrEmpty(srId))
                        {
                            Task tsDatavExpMests = Task.Factory.StartNew((object obj) =>
                            {
                                LoadDatavExpMests((List<long>)obj);
                            }, srId);
                            taslAll.Add(tsDatavExpMests);

                            Task tsDataServiceReqMetys = Task.Factory.StartNew((object obj) =>
                            {
                                LoadDataServiceReqMetys((List<long>)obj);
                            }, srId);
                            taslAll.Add(tsDataServiceReqMetys);

                            Task tsDataServiceReqMatys = Task.Factory.StartNew((object obj) =>
                            {
                                LoadDataServiceReqMatys((List<long>)obj);
                            }, srId);
                            taslAll.Add(tsDataServiceReqMatys);

                            Task tsDataSereServs = Task.Factory.StartNew((object obj) =>
                            {
                                LoadDataSereServs((List<long>)obj);
                            }, srId);
                            taslAll.Add(tsDataSereServs);
                        }
                    }
                    Task.WaitAll(taslAll.ToArray());
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataSereServs(List<long> srId)
        {
            try
            {
                List<HIS_SERE_SERV> lstSereServs = new List<HIS_SERE_SERV>();
                lstSereServs = new HisSereServGet().GetByServiceReqIds(srId);

                if (IsNotNullOrEmpty(lstSereServs))
                {
                    List<Task> taslAll = new List<Task>();
                    int skip = 0;
                    while (lstSereServs.Count - skip > 0)
                    {
                        var listSub = lstSereServs.Skip(skip).Take(dem).ToList();
                        skip += dem;
                        List<long> svIds = listSub.Select(s => s.SERVICE_ID).ToList();
                        List<V_HIS_SERVICE> vService = HisServiceCFG.DATA_VIEW.Where(o => svIds.Exists(p => p == o.ID)).ToList();
                        if (IsNotNullOrEmpty(vService))
                        {
                            vService = vService.Where(o => o.IS_NOT_SHOW_TRACKING == 1).ToList();
                            if (IsNotNullOrEmpty(vService))
                            {
                                listSub = listSub.Where(o => !vService.Exists(p => p.ID == o.SERVICE_ID)).ToList();
                            }

                            if (IsNotNullOrEmpty(listSub))
                            {
                                List<long> ssIds = listSub.Select(o => o.ID).ToList();
                                Task tsDataSereServExts = Task.Factory.StartNew((object obj) =>
                                {
                                    LoadDataSereServExts((List<long>)obj);
                                }, ssIds);
                                taslAll.Add(tsDataSereServExts);
                            }
                        }
                        if (IsNotNullOrEmpty(listSub))
                        {
                            SereServs.AddRange(listSub);
                        }
                    }
                    Task.WaitAll(taslAll.ToArray());
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataSereServExts(List<long> ssIds)
        {
            try
            {
                List<HIS_SERE_SERV_EXT> listSereServExts = new HisSereServExtGet().GetBySereServIds(ssIds);
                if (IsNotNullOrEmpty(listSereServExts))
                {
                    SereServExts.AddRange(listSereServExts);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataServiceReqMatys(List<long> srId)
        {
            try
            {
                List<HIS_SERVICE_REQ_MATY> listServiceReqMatys = new HisServiceReqMatyGet().GetByServiceReqIds(srId);
                if (IsNotNullOrEmpty(listServiceReqMatys))
                {
                    ServiceReqMatys.AddRange(listServiceReqMatys);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataServiceReqMetys(List<long> srId)
        {
            try
            {
                List<HIS_SERVICE_REQ_METY> listServiceReqMetys = new HisServiceReqMetyGet().GetByServiceReqIds(srId);
                if (IsNotNullOrEmpty(listServiceReqMetys))
                {
                    ServiceReqMetys.AddRange(listServiceReqMetys);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDatavExpMests(List<long> srId)
        {
            try
            {
                List<HIS_EXP_MEST> listExpMests = new HisExpMestGet().GetByServiceReqIds(srId);
                if (IsNotNullOrEmpty(listExpMests))
                {
                    ExpMests.AddRange(listExpMests);
                    List<Task> taslAll = new List<Task>();
                    int skip = 0;
                    while (listExpMests.Count - skip > 0)
                    {
                        var listSub = listExpMests.Skip(skip).Take(dem).ToList();
                        skip += dem;
                        List<long> expMestIds = listSub.Select(s => s.ID).ToList();

                        Task tsDataExpMestMedicines = Task.Factory.StartNew((object obj) =>
                        {
                            LoadDataExpMestMedicines((List<long>)obj);
                        }, expMestIds);
                        taslAll.Add(tsDataExpMestMedicines);

                        if (includeMaterial != null && includeMaterial == true)
                        {
                            Task tsDataExpMestMaterials = Task.Factory.StartNew((object obj) =>
                            {
                                LoadDataExpMestMaterials((List<long>)obj);
                            }, expMestIds);
                            taslAll.Add(tsDataExpMestMaterials);
                        }
                    }
                    Task.WaitAll(taslAll.ToArray());
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataExpMestMaterials(List<long> expMestIds)
        {
            try
            {
                List<HIS_EXP_MEST_MATERIAL> listExpMestMaterials = new HisExpMestMaterialGet().GetByExpMestIds(expMestIds);
                if (IsNotNullOrEmpty(listExpMestMaterials))
                {
                    ExpMestMaterials.AddRange(listExpMestMaterials);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataExpMestMedicines(List<long> expMestIds)
        {
            try
            {
                List<HIS_EXP_MEST_MEDICINE> listExpMestMedicines = new HisExpMestMedicineGet().GetByExpMestIds(expMestIds);
                if (IsNotNullOrEmpty(listExpMestMedicines))
                {
                    ExpMestMedicines.AddRange(listExpMestMedicines);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataDhst(TrackingDataInputSDO data)
        {
            Dhst = new List<HIS_DHST>();
            try
            {
                Dhst = new HisDhstGet().GetByTreatmentId(data.TreatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDatavSSRation(TrackingDataInputSDO data)
        {
            vSSRation = new List<V_HIS_SERE_SERV_RATION>();
            try
            {
                vSSRation = new HisSereServRationGet().GetViewByTreatmentId(data.TreatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataVTreatBedRoom(TrackingDataInputSDO data)
        {
            vTreatBedRoom = new List<V_HIS_TREATMENT_BED_ROOM>();
            try
            {
                vTreatBedRoom = new HisTreatmentBedRoomGet().GetByViewTreatmentId(data.TreatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDatavImpMests2(TrackingDataInputSDO data)
        {
            vImpMests2 = new List<V_HIS_IMP_MEST_2>();
            try
            {
                if (data.IncludeMoveBackMediMate != null && data.IncludeMoveBackMediMate == true)
                {
                    List<long> impMestType = new List<long>{
                            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL,
                            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL,
                            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DMTL,
                            IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DONKTL
                        };

                    vImpMests2 = new HisImpMestGet().GetView2ByTreatmentId(data.TreatmentId);
                    vImpMests2 = vImpMests2.Where(o => impMestType.Exists(p => p == o.IMP_MEST_TYPE_ID)).ToList();

                    if (IsNotNullOrEmpty(vImpMests2))
                    {
                        List<long> vImpMests2Ids = vImpMests2.Select(o => o.ID).ToList();

                        if (IsNotNullOrEmpty(vImpMests2Ids))
                        {
                            List<Task> taslAll = new List<Task>();
                            Task tsDatavImpMestMedicines = Task.Factory.StartNew((object obj) =>
                            {
                                LoadDatavImpMestMedicines((List<long>)obj);
                            }, vImpMests2Ids);
                            taslAll.Add(tsDatavImpMestMedicines);

                            Task tsDatavImpMestMaterials = Task.Factory.StartNew((object obj) =>
                            {
                                LoadDatavImpMestMaterials((List<long>)obj);
                            }, vImpMests2Ids);
                            taslAll.Add(tsDatavImpMestMaterials);

                            Task tsDatavImpMestBloods = Task.Factory.StartNew((object obj) =>
                            {
                                LoadDatavImpMestBloods((List<long>)obj);
                            }, vImpMests2Ids);
                            taslAll.Add(tsDatavImpMestBloods);

                            Task.WaitAll(taslAll.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDatavImpMestBloods(List<long> vImpMests2Ids)
        {
            vImpMestBloods = new List<V_HIS_IMP_MEST_BLOOD>();
            try
            {
                vImpMestBloods = new HisImpMestBloodGet().GetViewByImpMestIds(vImpMests2Ids);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDatavImpMestMaterials(List<long> vImpMests2Ids)
        {
            vImpMestMaterials = new List<V_HIS_IMP_MEST_MATERIAL>();
            try
            {
                vImpMestMaterials = new HisImpMestMaterialGet().GetViewByImpMestIds(vImpMests2Ids);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDatavImpMestMedicines(List<long> vImpMests2Ids)
        {
            vImpMestMedicines = new List<V_HIS_IMP_MEST_MEDICINE>();
            try
            {
                vImpMestMedicines = new HisImpMestMedicineGet().GetViewByImpMestIds(vImpMests2Ids);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDatavExpMestBityReqs2(TrackingDataInputSDO data)
        {
            vExpMestBityReqs2 = new List<V_HIS_EXP_MEST_BLTY_REQ_2>();
            try
            {
                if (data.IncludeBloodPres != null && data.IncludeBloodPres == true)
                {
                    vExpMestBityReqs2 = new HisExpMestBltyReqGet().GetView2TdlTreatmentId(data.TreatmentId);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataHisCares(TrackingDataInputSDO data)
        {
            HisCares = new List<HIS_CARE>();
            try
            {
                HisCares = new HisCareGet().GetByTreatmentId(data.TreatmentId);
                List<Task> taslAll = new List<Task>();
                Task tsDataCareDetails = Task.Factory.StartNew((object obj) =>
                {
                    LoadDataCareDetails((List<HIS_CARE>)obj);
                }, HisCares);
                taslAll.Add(tsDataCareDetails);
                Task.WaitAll(taslAll.ToArray());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDataCareDetails(List<HIS_CARE> HisCares)
        {
            CareDetails = new List<V_HIS_CARE_DETAIL>();
            try
            {
                List<long> careIds = IsNotNullOrEmpty(HisCares) ? HisCares.Select(o => o.ID).ToList() : null;
                if (IsNotNullOrEmpty(careIds))
                    CareDetails = new HisCareDetailGet().GetViewByCareIds(careIds);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal bool Run(TrackingDataInputSDO data, ref HisTrackingDataSDO resultData)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    ServiceRq = new List<HIS_SERVICE_REQ>();
                    ExpMests = new List<HIS_EXP_MEST>();
                    vImpMests2 = new List<V_HIS_IMP_MEST_2>();
                    ExpMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                    ExpMestMaterials = new List<HIS_EXP_MEST_MATERIAL>();
                    vImpMestMedicines = new List<V_HIS_IMP_MEST_MEDICINE>();
                    vImpMestMaterials = new List<V_HIS_IMP_MEST_MATERIAL>();
                    vImpMestBloods = new List<V_HIS_IMP_MEST_BLOOD>();
                    ServiceReqMetys = new List<HIS_SERVICE_REQ_METY>();
                    ServiceReqMatys = new List<HIS_SERVICE_REQ_MATY>();
                    vExpMestBityReqs2 = new List<V_HIS_EXP_MEST_BLTY_REQ_2>();
                    SereServs = new List<HIS_SERE_SERV>();
                    SereServExts = new List<HIS_SERE_SERV_EXT>();

                    includeMaterial = data.IncludeMaterial;
                    resultData = new HisTrackingDataSDO();

                    List<Task> taslAll = new List<Task>();
                    Task tsDataTreatment = Task.Factory.StartNew((object obj) =>
                    {
                        LoadDataTreatment((TrackingDataInputSDO)obj);
                    }, data);
                    taslAll.Add(tsDataTreatment);

                    Task tsDataServiceRq = Task.Factory.StartNew((object obj) =>
                    {
                        LoadDataServiceRq((TrackingDataInputSDO)obj);
                    }, data);
                    taslAll.Add(tsDataServiceRq);

                    Task tsDataDhst = Task.Factory.StartNew((object obj) =>
                    {
                        LoadDataDhst((TrackingDataInputSDO)obj);
                    }, data);
                    taslAll.Add(tsDataDhst);

                    Task tsDatavSSRation = Task.Factory.StartNew((object obj) =>
                    {
                        LoadDatavSSRation((TrackingDataInputSDO)obj);
                    }, data);
                    taslAll.Add(tsDatavSSRation);

                    Task tsDatavVTreatBedRoom = Task.Factory.StartNew((object obj) =>
                    {
                        LoadDataVTreatBedRoom((TrackingDataInputSDO)obj);
                    }, data);
                    taslAll.Add(tsDatavVTreatBedRoom);

                    Task tsDatavImpMests2 = Task.Factory.StartNew((object obj) =>
                    {
                        LoadDatavImpMests2((TrackingDataInputSDO)obj);
                    }, data);
                    taslAll.Add(tsDatavImpMests2);

                    Task tsDatavHisCares = Task.Factory.StartNew((object obj) =>
                    {
                        LoadDataHisCares((TrackingDataInputSDO)obj);
                    }, data);
                    taslAll.Add(tsDatavHisCares);

                    Task tsDatavExpMestBityReqs2 = Task.Factory.StartNew((object obj) =>
                    {
                        LoadDatavExpMestBityReqs2((TrackingDataInputSDO)obj);
                    }, data);
                    taslAll.Add(tsDatavExpMestBityReqs2);

                    Task.WaitAll(taslAll.ToArray());

                    resultData.ServiceReqs = ServiceRq;
                    resultData.Treatment = Treatment;
                    resultData.HisDHSTs = Dhst;
                    resultData.vSereServRations = vSSRation;
                    resultData.TreatmentBedRooms = vTreatBedRoom;
                    resultData.ExpMests = ExpMests;
                    resultData.ServiceReqMetys = ServiceReqMetys;
                    resultData.ServiceReqMatys = ServiceReqMatys;
                    resultData.SereServs = SereServs;
                    resultData.SereServExts = SereServExts;
                    resultData.ExpMestMedicines = ExpMestMedicines;
                    resultData.ExpMestMaterials = ExpMestMaterials;
                    resultData.vImpMests2 = vImpMests2;
                    resultData.vImpMestMedicines = vImpMestMedicines;
                    resultData.vImpMestMaterials = vImpMestMaterials;
                    resultData.vImpMestBloods = vImpMestBloods;
                    resultData.vExpMestBityReqs2 = vExpMestBityReqs2;
                    resultData.HisCares = HisCares;
                    resultData.CareDetails = CareDetails;

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
