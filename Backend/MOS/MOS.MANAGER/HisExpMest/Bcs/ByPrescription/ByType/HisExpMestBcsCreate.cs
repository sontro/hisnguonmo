using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.MANAGER.HisExpMest.Common;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms
{
    class HisExpMestBcsCreate : BusinessBase
    {
        private HIS_EXP_MEST recentHisExpMest;
        private HisExpMestResultSDO recentResultSDO;

        private HisExpMestCreate hisExpMestCreate;
        private HisExpMestMatyReqMaker hisExpMestMatyReqMaker;
        private HisExpMestMetyReqMaker hisExpMestMetyReqMaker;
        //private HisExpMestUpdate hisExpMestUpdate;

        private HisExpMestAutoProcess hisExpMestAutoProcess;

        internal HisExpMestBcsCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestBcsCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.hisExpMestMatyReqMaker = new HisExpMestMatyReqMaker(param);
            this.hisExpMestMetyReqMaker = new HisExpMestMetyReqMaker(param);
            //this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisExpMestAutoProcess = new HisExpMestAutoProcess(param);
        }

        internal bool Create(HisExpMestBcsSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_EXP_MEST> hisExpMestDtts = null;
                List<HIS_EXP_MEST> hisExpMestBcss = null;
                HisExpMestBcsCheck checker = new HisExpMestBcsCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data);
                valid = valid && checker.ValidExpMest(data, ref hisExpMestDtts, ref hisExpMestBcss);
                valid = valid && commonChecker.HasToExpMestReason(data.ExpMestReasonId);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs = null;
                    List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs = null;
                    //Tao exp_mest
                    expMest.MEDI_STOCK_ID = data.MediStockId;
                    expMest.REQ_ROOM_ID = data.ReqRoomId;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS; //xuat Bu co so
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.IMP_MEDI_STOCK_ID = data.ImpMediStockId;
                    expMest.DESCRIPTION = data.Description;
                    expMest.BCS_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.BCS_TYPE__ID__PRES;
                    expMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;

                    List<ExpMedicineTypeSDO> expMedicineTypeSdos = this.MakeExpMedicineTypeSDO(data);
                    List<ExpMaterialTypeSDO> expMaterialTypeSdos = this.MakeExpMaterialTypeSDO(data);

                    if (!IsNotNullOrEmpty(expMaterialTypeSdos) && !IsNotNullOrEmpty(expMedicineTypeSdos))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong co chi tiet xuat expMaterialTypeSdos va expMedicineTypeSdos = null");
                    }

                    if (!this.hisExpMestCreate.Create(expMest))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    this.recentHisExpMest = expMest;

                    if (IsNotNullOrEmpty(hisExpMestDtts))
                    {
                        string sql1 = String.Format("UPDATE HIS_EXP_MEST SET XBTT_EXP_MEST_ID = {0}, TDL_XBTT_EXP_MEST_CODE = '{1}' WHERE %IN_CLAUSE% ", this.recentHisExpMest.ID, this.recentHisExpMest.EXP_MEST_CODE);
                        sql1 = DAOWorker.SqlDAO.AddInClause(hisExpMestDtts.Select(s => s.ID).ToList(), sql1, "ID");
                        sqls.Add(sql1);
                    }

                    if (IsNotNullOrEmpty(hisExpMestBcss))
                    {
                        string sql2 = String.Format("UPDATE HIS_EXP_MEST SET XBTT_EXP_MEST_ID = {0}, TDL_XBTT_EXP_MEST_CODE = '{1}' WHERE %IN_CLAUSE% ", this.recentHisExpMest.ID, this.recentHisExpMest.EXP_MEST_CODE);
                        sql2 = DAOWorker.SqlDAO.AddInClause(hisExpMestBcss.Select(s => s.ID).ToList(), sql2, "ID");
                        sqls.Add(sql2);
                    }

                    //Tao exp_mest_maty_req
                    if (!this.hisExpMestMatyReqMaker.Run(expMaterialTypeSdos, expMest, ref expMestMatyReqs))
                    {
                        throw new Exception("ExpMestMatyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_mety_req
                    if (!this.hisExpMestMetyReqMaker.Run(expMedicineTypeSdos, expMest, ref expMestMetyReqs))
                    {
                        throw new Exception("ExpMestMetyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    this.ProcessAuto();

                    this.PassResult(expMest, expMestMatyReqs, expMestMetyReqs, ref resultData);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollBack();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private List<ExpMedicineTypeSDO> MakeExpMedicineTypeSDO(HisExpMestBcsSDO data)
        {
            List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines = null;
            List<HIS_EXP_MEST_METY_REQ> hisExpMestReqs = null;

            if (IsNotNullOrEmpty(data.ExpMestDttIds))
            {
                hisExpMestMedicines = new HisExpMestMedicineGet().GetExportedByExpMestIds(data.ExpMestDttIds);
            }

            if (IsNotNullOrEmpty(data.ExpMestBcsIds))
            {
                hisExpMestReqs = new HisExpMestMetyReqGet().GetByExpMestIds(data.ExpMestBcsIds);
            }

            Dictionary<long, ExpMedicineTypeSDO> dicMedicineType = new Dictionary<long, ExpMedicineTypeSDO>();
            if (IsNotNullOrEmpty(hisExpMestMedicines))
            {
                var Groups = hisExpMestMedicines.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToList();
                foreach (var group in Groups)
                {
                    List<HIS_EXP_MEST_MEDICINE> listByGroup = group.ToList();
                    ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                    sdo.Amount = listByGroup.Sum(s => s.AMOUNT);
                    sdo.MedicineTypeId = group.Key.Value;
                    dicMedicineType[group.Key.Value] = sdo;
                }
            }
            if (IsNotNullOrEmpty(hisExpMestReqs))
            {
                var Groups = hisExpMestReqs.GroupBy(g => g.MEDICINE_TYPE_ID).ToList();
                foreach (var group in Groups)
                {
                    List<HIS_EXP_MEST_METY_REQ> listByGroup = group.ToList();
                    decimal amount = listByGroup.Sum(s => (s.AMOUNT - (s.DD_AMOUNT ?? 0)));
                    if (amount > 0)
                    {
                        ExpMedicineTypeSDO sdo = null;
                        if (dicMedicineType.ContainsKey(group.Key))
                        {
                            sdo = dicMedicineType[group.Key];
                        }
                        else
                        {
                            sdo = new ExpMedicineTypeSDO();
                            sdo.MedicineTypeId = group.Key;
                            dicMedicineType[group.Key] = sdo;
                        }

                        sdo.Amount += amount;
                    }
                }
            }

            return dicMedicineType.Select(s => s.Value).ToList();
        }

        private List<ExpMaterialTypeSDO> MakeExpMaterialTypeSDO(HisExpMestBcsSDO data)
        {
            List<HIS_EXP_MEST_MATERIAL> hisExpMestMaterials = null;
            List<HIS_EXP_MEST_MATY_REQ> hisExpMestReqs = null;

            if (IsNotNullOrEmpty(data.ExpMestDttIds))
            {
                hisExpMestMaterials = new HisExpMestMaterialGet().GetExportedByExpMestIds(data.ExpMestDttIds);
            }

            if (IsNotNullOrEmpty(data.ExpMestBcsIds))
            {
                hisExpMestReqs = new HisExpMestMatyReqGet().GetByExpMestIds(data.ExpMestBcsIds);
            }

            Dictionary<long, ExpMaterialTypeSDO> dicMaterialType = new Dictionary<long, ExpMaterialTypeSDO>();
            if (IsNotNullOrEmpty(hisExpMestMaterials))
            {
                var Groups = hisExpMestMaterials.GroupBy(g => g.TDL_MATERIAL_TYPE_ID).ToList();
                foreach (var group in Groups)
                {
                    List<HIS_EXP_MEST_MATERIAL> listByGroup = group.ToList();
                    ExpMaterialTypeSDO sdo = new ExpMaterialTypeSDO();
                    sdo.Amount = listByGroup.Sum(s => s.AMOUNT);
                    sdo.MaterialTypeId = group.Key.Value;
                    dicMaterialType[group.Key.Value] = sdo;
                }
            }

            if (IsNotNullOrEmpty(hisExpMestReqs))
            {
                var Groups = hisExpMestReqs.GroupBy(g => g.MATERIAL_TYPE_ID).ToList();
                foreach (var group in Groups)
                {
                    List<HIS_EXP_MEST_MATY_REQ> listByGroup = group.ToList();
                    decimal amount = listByGroup.Sum(s => (s.AMOUNT - (s.DD_AMOUNT ?? 0)));
                    if (amount > 0)
                    {
                        ExpMaterialTypeSDO sdo = null;
                        if (dicMaterialType.ContainsKey(group.Key))
                        {
                            sdo = dicMaterialType[group.Key];
                        }
                        else
                        {
                            sdo = new ExpMaterialTypeSDO();
                            sdo.MaterialTypeId = group.Key;
                            dicMaterialType[group.Key] = sdo;
                        }
                        sdo.Amount += amount;
                    }
                }
            }

            return dicMaterialType.Select(s => s.Value).ToList();
        }

        /// <summary>
        /// Tu dong phe duyet, thuc xuat trong truong hop kho co cau hinh tu dong
        /// </summary>
        private void ProcessAuto()
        {
            try
            {
                this.hisExpMestAutoProcess.Run(this.recentHisExpMest, ref this.recentResultSDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> expMatyReqs, List<HIS_EXP_MEST_METY_REQ> expMetyReqs, ref HisExpMestResultSDO resultData)
        {
            if (this.recentResultSDO != null)
            {
                resultData = this.recentResultSDO;
            }
            else
            {
                resultData = new HisExpMestResultSDO();
                resultData.ExpMest = expMest;
                resultData.ExpMatyReqs = expMatyReqs;
                resultData.ExpMetyReqs = expMetyReqs;
            }
        }

        private void RollBack()
        {
            this.hisExpMestMatyReqMaker.Rollback();
            this.hisExpMestMetyReqMaker.Rollback();
            this.hisExpMestCreate.RollbackData();
        }
    }
}
