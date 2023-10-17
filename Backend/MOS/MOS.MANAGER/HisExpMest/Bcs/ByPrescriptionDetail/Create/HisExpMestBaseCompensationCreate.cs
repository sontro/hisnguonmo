using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensation.Create
{
    class HisExpMestBaseCompensationCreate : BusinessBase
    {

        private ExpMestProcessor expMestProcessor;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;

        internal HisExpMestBaseCompensationCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBaseCompensationCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
        }

        internal bool Run(CabinetBaseCompensationSDO data, ref  List<HIS_EXP_MEST> resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_STOCK cabinetStock = null;
                HIS_MEDI_STOCK expStock = null;
                List<HIS_EXP_MEST> expMests = null;
                List<HIS_EXP_MEST_MEDICINE> expMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> expMaterials = null;
                List<HIS_EXP_MEST_METY_REQ> metyReqs = null;
                List<HIS_EXP_MEST_MATY_REQ> matyReqs = null;
                List<HIS_MEDI_STOCK_MATY> stockMatys = null;
                List<HIS_MEDI_STOCK_METY> stockMetys = null;
                WorkPlaceSDO workPlace = null;

                HisExpMestBaseCompensationCheck checker = new HisExpMestBaseCompensationCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisMediStockCheck stockChecker = new HisMediStockCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && stockChecker.VerifyId(data.CabinetMediStockId, ref cabinetStock);
                valid = valid && stockChecker.IsUnLock(cabinetStock);
                valid = valid && stockChecker.IsCabinetStock(cabinetStock);
                valid = valid && stockChecker.IsCabinetManageOptionPresDetail(cabinetStock);
                if (data.MediStockId.HasValue)
                {
                    valid = valid && stockChecker.VerifyId(data.MediStockId.Value, ref expStock);
                    valid = valid && stockChecker.IsUnLock(expStock);
                }
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtRoom(cabinetStock.ROOM_ID, workPlace.RoomId);
                valid = valid && checker.ValidData(data, ref stockMetys, ref stockMatys);
                valid = valid && checker.VerifyData(data, ref expMests, ref expMedicines, ref expMaterials, ref metyReqs, ref matyReqs);
                valid = valid && commonChecker.HasToExpMestReason(data.ExpMestReasonId);
                if (valid)
                {
                    Dictionary<HIS_EXP_MEST, ExpMestDetail> dicExpMest = new Dictionary<HIS_EXP_MEST, ExpMestDetail>();
                    this.PrepareData(data, stockMetys, stockMatys, workPlace, ref dicExpMest);

                    if (!this.expMestProcessor.Run(dicExpMest, ref expMests))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(dicExpMest, expMaterials, matyReqs))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(dicExpMest, expMedicines, metyReqs))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }

                    resultData = expMests;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuatBuCoSoTuTruc, this.GenEventLog(expMests)).Run();

                    this.ProcessAuto(expMests);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void ProcessAuto(List<HIS_EXP_MEST> expMests)
        {
            try
            {
                if (IsNotNullOrEmpty(expMests))
                {
                    foreach (var exp in expMests)
                    {
                        HisExpMestResultSDO recentResultSDO = null;
                        if (!new HisExpMestAutoProcess().Run(exp, ref recentResultSDO))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ChuaHoanThanh, exp.EXP_MEST_CODE);
                            continue;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PrepareData(CabinetBaseCompensationSDO data, List<HIS_MEDI_STOCK_METY> stockMetys, List<HIS_MEDI_STOCK_MATY> stockMatys, WorkPlaceSDO workPlace, ref Dictionary<HIS_EXP_MEST, ExpMestDetail> dicExpMest)
        {
            Dictionary<long, ExpMestDetail> dic = new Dictionary<long, ExpMestDetail>();
            if (IsNotNullOrEmpty(data.MedicineTypes))
            {
                foreach (BaseMedicineTypeSDO sdo in data.MedicineTypes)
                {
                    HIS_MEDI_STOCK_METY mety = stockMetys != null ? stockMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == sdo.MedicineTypeId && o.EXP_MEDI_STOCK_ID.HasValue) : null;
                    long mediStockId = 0;
                    if (mety != null)
                    {
                        mediStockId = mety.EXP_MEDI_STOCK_ID.Value;
                    }
                    else
                    {
                        mediStockId = data.MediStockId.Value;
                    }
                    ExpMestDetail dt = new ExpMestDetail();
                    if (dic.ContainsKey(mediStockId))
                    {
                        dt = dic[mediStockId];
                    }
                    else
                    {
                        dic[mediStockId] = dt;
                    }
                    if (dt.Medicines == null) dt.Medicines = new List<BaseMedicineTypeSDO>();
                    dt.Medicines.Add(sdo);

                }
            }

            if (IsNotNullOrEmpty(data.MaterialTypes))
            {
                foreach (BaseMaterialTypeSDO sdo in data.MaterialTypes)
                {
                    HIS_MEDI_STOCK_MATY mety = stockMatys != null ? stockMatys.FirstOrDefault(o => o.MATERIAL_TYPE_ID == sdo.MaterialTypeId && o.EXP_MEDI_STOCK_ID.HasValue) : null;
                    long mediStockId = 0;
                    if (mety != null)
                    {
                        mediStockId = mety.EXP_MEDI_STOCK_ID.Value;
                    }
                    else
                    {
                        mediStockId = data.MediStockId.Value;
                    }
                    ExpMestDetail dt = new ExpMestDetail();
                    if (dic.ContainsKey(mediStockId))
                    {
                        dt = dic[mediStockId];
                    }
                    else
                    {
                        dic[mediStockId] = dt;
                    }
                    if (dt.Materials == null) dt.Materials = new List<BaseMaterialTypeSDO>();
                    dt.Materials.Add(sdo);

                }
            }
            string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            foreach (var d in dic)
            {
                HIS_EXP_MEST exp = new HIS_EXP_MEST();
                exp.MEDI_STOCK_ID = d.Key;
                exp.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                exp.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS;
                exp.IMP_MEDI_STOCK_ID = data.CabinetMediStockId;
                exp.REQ_DEPARTMENT_ID = workPlace.DepartmentId;
                exp.REQ_LOGINNAME = loginname;
                exp.REQ_USER_TITLE = HisEmployeeUtil.GetTitle(loginname);
                exp.REQ_ROOM_ID = workPlace.RoomId;
                exp.REQ_USERNAME = username;
                exp.DESCRIPTION = data.Description;
                exp.BCS_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.BCS_TYPE__ID__PRES_DETAIL;
                exp.EXP_MEST_REASON_ID = data.ExpMestReasonId;
                dicExpMest[exp] = d.Value;
            }
        }

        private string GenEventLog(List<HIS_EXP_MEST> expMests)
        {
            string rs = "";
            try
            {
                List<string> logs = new List<string>();
                foreach (var exp in expMests)
                {
                    String.Format("{0}: {1}", EventLogUtil.SimpleEventKey.EXP_MEST_CODE, exp.EXP_MEST_CODE);
                }
                rs = String.Join(". ", logs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                rs = "";
            }
            return rs;
        }

        private void Rollback()
        {
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.expMestProcessor.Rollback();
        }
    }

    class ExpMestDetail
    {
        public List<BaseMedicineTypeSDO> Medicines { get; set; }
        public List<BaseMaterialTypeSDO> Materials { get; set; }
    }
}
