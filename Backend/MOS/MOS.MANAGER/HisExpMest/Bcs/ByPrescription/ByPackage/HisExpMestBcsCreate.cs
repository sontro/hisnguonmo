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
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisExpMest.Common;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.Bcs
{
    class HisExpMestBcsCreate : BusinessBase
    {
        private HIS_EXP_MEST recentHisExpMest;
        private HisExpMestResultSDO recentResultSDO;

        private HisExpMestCreate hisExpMestCreate;
        //private HisExpMestMatyReqMaker hisExpMestMatyReqMaker;
        //private HisExpMestMetyReqMaker hisExpMestMetyReqMaker;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;

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
            //this.hisExpMestMatyReqMaker = new HisExpMestMatyReqMaker(param);
            //this.hisExpMestMetyReqMaker = new HisExpMestMetyReqMaker(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
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
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                List<HIS_EXP_MEST_METY_REQ> metyReqs = null;
                List<HIS_EXP_MEST_MATY_REQ> matyReqs = null;
                HisExpMestBcsCheck checker = new HisExpMestBcsCheck(param);
                HisMediStockCheck stockChecker = new HisMediStockCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data);
                valid = valid && checker.ValidExpMest(data, ref hisExpMestDtts, ref hisExpMestBcss, ref materials, ref medicines, ref matyReqs, ref metyReqs);
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
                    if (!this.materialProcessor.Run(data, expMest, materials, matyReqs, ref expMestMatyReqs))
                    {
                        throw new Exception("materialProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_mety_req
                    if (!this.medicineProcessor.Run(data, expMest, medicines, metyReqs, ref expMestMetyReqs))
                    {
                        throw new Exception("medicineProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!IsNotNullOrEmpty(expMestMatyReqs) && !IsNotNullOrEmpty(expMestMetyReqs))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong co chi tiet xuat expMestMatyReqs va expMestMetyReqs = null");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(expMest, expMestMatyReqs, expMestMetyReqs, ref resultData);
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    this.ProcessAuto();

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
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.hisExpMestCreate.RollbackData();
        }
    }
}
