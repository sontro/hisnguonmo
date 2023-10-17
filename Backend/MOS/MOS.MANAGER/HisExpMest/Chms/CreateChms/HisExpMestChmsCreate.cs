using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;

namespace MOS.MANAGER.HisExpMest.Chms.CreateChms
{
    //Xuat chuyen kho
    partial class HisExpMestChmsCreate : BusinessBase
    {
        private HIS_EXP_MEST recentHisExpMest;
        private HisExpMestResultSDO recentResultSDO;

        private HisExpMestCreate hisExpMestCreate;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private HisExpMestBltyReqMaker hisExpMestBltyReqMaker;

        private HisExpMestAutoProcess hisExpMestAutoProcess;

        internal HisExpMestChmsCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestChmsCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestAutoProcess = new HisExpMestAutoProcess(param);
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.hisExpMestBltyReqMaker = new HisExpMestBltyReqMaker(param);
        }

        internal bool Create(HisExpMestChmsSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestChmsCheck checker = new HisExpMestChmsCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ValidData(data);
                valid = valid && checker.IsAllowed(data);
                valid = valid && commonChecker.HasToExpMestReason(data.ExpMestReasonId);
                if (valid)
                {
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs = null;
                    List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs = null;
                    List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs = null;
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                    List<string> sqls = new List<string>();

                    bool isByPackage = (IsNotNullOrEmpty(data.ExpMaterialSdos) || IsNotNullOrEmpty(data.ExpMedicineSdos));
                    //Tao exp_mest
                    expMest.MEDI_STOCK_ID = data.MediStockId;
                    expMest.REQ_ROOM_ID = data.ReqRoomId;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK; //xuat chuyen kho
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.IMP_MEDI_STOCK_ID = data.ImpMediStockId;
                    expMest.DESCRIPTION = data.Description;
                    expMest.RECIPIENT = data.Recipient;
                    expMest.RECEIVING_PLACE = data.ReceivingPlace;
                    expMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;

                    if (isByPackage)
                    {
                        expMest.IS_REQUEST_BY_PACKAGE = Constant.IS_TRUE;
                    }
                    else
                    {
                        expMest.IS_REQUEST_BY_PACKAGE = null;
                    }
                    if (!this.hisExpMestCreate.Create(expMest))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.recentHisExpMest = expMest;

                    //Tao exp_mest_maty_req
                    if (!this.materialProcessor.Run(data, expMest, ref expMestMatyReqs, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("materialProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_mety_req
                    if (!this.medicineProcessor.Run(data, expMest, ref expMestMetyReqs, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_blty_req
                    if (!this.hisExpMestBltyReqMaker.Run(data.Bloods, expMest, ref expMestBltyReqs))
                    {
                        throw new Exception("hisExpMestBltyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(expMest, expMestMatyReqs, expMestMetyReqs, expMestBltyReqs, expMestMedicines, expMestMaterials, ref resultData);
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    if (this.IsProcessAuto(data))
                    {
                        this.ProcessAuto();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
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

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> expMatyReqs, List<HIS_EXP_MEST_METY_REQ> expMetyReqs, List<HIS_EXP_MEST_BLTY_REQ> expBltyReqs, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, ref HisExpMestResultSDO resultData)
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
                resultData.ExpBltyReqs = expBltyReqs;
                resultData.ExpMedicines = medicines;
                resultData.ExpMaterials = materials;
            }
        }

        private bool IsProcessAuto(HisExpMestChmsSDO data)
        {
            try
            {
                if (!IsNotNullOrEmpty(data.Materials)) return true;
                if (HisMaterialTypeCFG.DATA.Any(o => o.IS_REUSABLE == Constant.IS_TRUE && data.Materials.Any(a => a.MaterialTypeId == o.ID)))
                {
                    LogSystem.Warn("Co Yeu cau vat tu tai su dung. Khong xu ly tu dong duyet");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        private void RollBack()
        {
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.hisExpMestBltyReqMaker.Rollback();
            this.hisExpMestCreate.RollbackData();
        }
    }
}
