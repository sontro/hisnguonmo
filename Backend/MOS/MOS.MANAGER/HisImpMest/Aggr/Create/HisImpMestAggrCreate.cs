using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr
{
    class HisImpMestAggrCreate : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;
        private OddMedicineProcessor oddMedicineProcessor;
        private OddMaterialProcessor oddMaterialProcessor;
        private DeleteImpMestProcessor deleteImpMestProcessor;
        //private HisImpMestAutoProcess hisImpMestAutoProcess;
        private List<HisImpMestCreate> hisImpMestCreates = new List<HisImpMestCreate>();

        private List<HIS_IMP_MEST> childHisImpMests;
        private List<HIS_IMP_MEST> recentHisImpMestAggrs;

        internal HisImpMestAggrCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestAggrCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
            this.oddMedicineProcessor = new OddMedicineProcessor(param);
            this.oddMaterialProcessor = new OddMaterialProcessor(param);
            this.deleteImpMestProcessor = new DeleteImpMestProcessor(param);
            //this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        internal bool AggrCreate(HisImpMestAggrSDO data, ref List<V_HIS_IMP_MEST> resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;

                bool valid = true;
                List<HIS_IMP_MEST_MEDICINE> allImpMedicines = null;
                List<HIS_IMP_MEST_MEDICINE> deleteImpMedicines = null;
                List<HIS_IMP_MEST_MATERIAL> allImpMaterials = null;
                List<HIS_IMP_MEST_MATERIAL> deleteImpMaterials = null;
                HisImpMestAggrCheck checker = new HisImpMestAggrCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && IsNotNullOrEmpty(data.ImpMestIds);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsValid(data, ref this.childHisImpMests);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<long> idDeletes = new List<long>();

                    if (!this.oddMedicineProcessor.Process(data, this.childHisImpMests, ref allImpMedicines, ref deleteImpMedicines))
                    {
                        throw new Exception("oddMedicineProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.oddMaterialProcessor.Process(data, this.childHisImpMests, ref allImpMaterials, ref deleteImpMaterials))
                    {
                        throw new Exception("oddMaterialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.deleteImpMestProcessor.Run(allImpMedicines, allImpMaterials, deleteImpMedicines, deleteImpMaterials, ref idDeletes, ref sqls))
                    {
                        throw new Exception("deleteImpMestProcessor. Ket thuc nghiep vu");
                    }

                    this.childHisImpMests = this.childHisImpMests != null ? this.childHisImpMests.Where(o => idDeletes == null || !idDeletes.Contains(o.ID)).ToList() : null;

                    this.ProcessHisImpMest(data, workPlace);

                    this.ProcessChildHisImpMest();

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(ref resultData);

                    HisAggrImpMestLog.Run(this.recentHisImpMestAggrs, this.childHisImpMests, LibraryEventLog.EventLog.Enum.HisImpMest_TongHopPhieuTra);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessHisImpMest(HisImpMestAggrSDO data, WorkPlaceSDO workPlace)
        {
            if (IsNotNullOrEmpty(this.childHisImpMests))
            {
                List<HIS_IMP_MEST> aggrImpMests = new List<HIS_IMP_MEST>();
                long createYear = DateTime.Now.Year;

                //Neu co cau hinh danh so thu tu theo loai don thi phai tach phieu tra theo ca loai don
                if (HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK)
                {
                    var groups = this.childHisImpMests.GroupBy(o => new { o.MEDI_STOCK_ID, o.SPECIAL_MEDICINE_TYPE }).ToList();
                    foreach (var group in groups)
                    {
                        HIS_IMP_MEST aggrImpMest = new HIS_IMP_MEST();
                        aggrImpMest.MEDI_STOCK_ID = group.Key.MEDI_STOCK_ID;
                        aggrImpMest.REQ_ROOM_ID = data.RequestRoomId;
                        aggrImpMest.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
                        aggrImpMest.SPECIAL_MEDICINE_TYPE = group.Key.SPECIAL_MEDICINE_TYPE;
                        aggrImpMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                        aggrImpMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT;
                        //Danh STT tra thuoc dac biet
                        aggrImpMest.SPECIAL_MEDICINE_NUM_ORDER = this.GetNextSpeciaMedicineTypeNumOrder(aggrImpMest.SPECIAL_MEDICINE_TYPE, aggrImpMest.REQ_DEPARTMENT_ID.Value, createYear, aggrImpMest.MEDI_STOCK_ID);

                        HisImpMestCreate impMestCreate = new HisImpMestCreate(param);
                        if (!impMestCreate.Create(aggrImpMest))
                        {
                            throw new Exception("Tao phieu nhap tong hop that bai. Rollback du lieu");
                        }
                        this.hisImpMestCreates.Add(impMestCreate);
                        List<HIS_IMP_MEST> listSub = group.ToList();
                        listSub.ForEach(o =>
                        {
                            o.AGGR_IMP_MEST_ID = aggrImpMest.ID;
                            o.TDL_AGGR_IMP_MEST_CODE = aggrImpMest.IMP_MEST_CODE;
                        });
                        aggrImpMests.Add(aggrImpMest);
                    }
                }
                else
                {
                    var groups = this.childHisImpMests.GroupBy(o => o.MEDI_STOCK_ID).ToList();
                    foreach (var group in groups)
                    {
                        HIS_IMP_MEST aggrImpMest = new HIS_IMP_MEST();
                        aggrImpMest.MEDI_STOCK_ID = group.Key;
                        aggrImpMest.REQ_ROOM_ID = data.RequestRoomId;
                        aggrImpMest.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
                        aggrImpMest.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
                        aggrImpMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                        aggrImpMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT;
                        HisImpMestCreate impMestCreate = new HisImpMestCreate(param);
                        if (!impMestCreate.Create(aggrImpMest))
                        {
                            throw new Exception("Tao phieu nhap tong hop that bai. Rollback du lieu");
                        }
                        this.hisImpMestCreates.Add(impMestCreate);
                        List<HIS_IMP_MEST> listSub = group.ToList();
                        listSub.ForEach(o =>
                        {
                            o.AGGR_IMP_MEST_ID = aggrImpMest.ID;
                            o.TDL_AGGR_IMP_MEST_CODE = aggrImpMest.IMP_MEST_CODE;
                        });
                        aggrImpMests.Add(aggrImpMest);
                    }
                }
                this.recentHisImpMestAggrs = aggrImpMests;
            }
            else
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_CacThuocSeBiTraVeKhoLeKhongTaoDuocPhieuTongHop);
                throw new Exception("So luong thuoc vat tu tong hop bang so luong thuoc vat tu xu ly le (xuat sang kho khac).");
            }
        }

        /// <summary>
        /// Danh STT xuat thuoc theo khoa doi voi phieu linh thuoc dac biet (thuoc gay nghien/huong than, thuoc doc)
        /// </summary>
        /// <param name="medicineSpecialType"></param>
        /// <param name="requestDepartmentId"></param>
        /// <param name="createYear"></param>
        /// <returns></returns>
        private long? GetNextSpeciaMedicineTypeNumOrder(long? medicineSpecialType, long requestDepartmentId, long createYear, long mediStockId)
        {
            if (!medicineSpecialType.HasValue)
            {
                return null;
            }
            else
            {
                string sql = "SELECT MAX(SPECIAL_MEDICINE_NUM_ORDER) FROM HIS_IMP_MEST WHERE SPECIAL_MEDICINE_TYPE = :param1 AND REQ_DEPARTMENT_ID = :param2 AND VIR_CREATE_YEAR = :param3 AND IMP_MEST_TYPE_ID = :param4 AND MEDI_STOCK_ID = :param5";
                long? maxNumOrder = DAOWorker.SqlDAO.GetSqlSingle<long?>(sql, medicineSpecialType.Value, requestDepartmentId, createYear, IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT, mediStockId);
                return maxNumOrder.HasValue ? maxNumOrder.Value + 1 : 1;
            }
        }

        private void ProcessChildHisImpMest()
        {
            if (IsNotNullOrEmpty(this.childHisImpMests))
            {
                if (!this.hisImpMestUpdate.UpdateList(this.childHisImpMests))
                {
                    throw new Exception("Khong update duoc cac phieu con. Rollback du lieu");
                }
            }
        }

        private void PassResult(ref List<V_HIS_IMP_MEST> resultData)
        {
            resultData = new HisImpMestGet().GetViewByIds(this.recentHisImpMestAggrs.Select(s => s.ID).ToList());
        }

        internal void RollbackData()
        {
            this.hisImpMestUpdate.RollbackData();

            if (this.hisImpMestCreates != null && this.hisImpMestCreates.Count > 0)
            {
                foreach (var create in this.hisImpMestCreates)
                {
                    create.RollbackData();
                }
            }
            this.oddMedicineProcessor.RollbackData();
            this.oddMaterialProcessor.RollbackData();
        }
    }
}
