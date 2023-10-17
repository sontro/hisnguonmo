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

namespace MOS.MANAGER.HisExpMest.Depa
{
    //Xuat hao phi (su dung) khoa phong
    partial class HisExpMestDepaCreate : BusinessBase
    {
        private HisExpMestAutoProcess hisExpMestAutoProcess;
        private HisExpMestCreate hisExpMestCreate;
        private HisExpMestMatyReqMaker hisExpMestMatyReqMaker;
        private HisExpMestMetyReqMaker hisExpMestMetyReqMaker;
        private HisExpMestBltyReqMaker hisExpMestBltyReqMaker;

        private HisExpMestResultSDO recentResultSDO;

        internal HisExpMestDepaCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestDepaCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestAutoProcess = new HisExpMestAutoProcess(param);
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.hisExpMestMatyReqMaker = new HisExpMestMatyReqMaker(param);
            this.hisExpMestMetyReqMaker = new HisExpMestMetyReqMaker(param);
            this.hisExpMestBltyReqMaker = new HisExpMestBltyReqMaker(param);
        }

        internal bool Create(HisExpMestDepaSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                HisExpMestDepaCheck checker = new HisExpMestDepaCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                bool valid = true;
                valid = valid && checker.IsAllowed(data);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ValidData(data);
                valid = valid && commonChecker.HasToExpMestReason(data.ExpMestReasonId);
                if (valid)
                {
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs = null;
                    List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs = null;
                    List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs = null;

                    //Tao exp_mest
                    expMest.MEDI_STOCK_ID = data.MediStockId;
                    expMest.REQ_ROOM_ID = data.ReqRoomId;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__HPKP; //hao phi khoa phong
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.DESCRIPTION = data.Description;
                    expMest.RECIPIENT = data.Recipient;
                    expMest.RECEIVING_PLACE = data.ReceivingPlace;
                    expMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;
                    expMest.REMEDY_COUNT = data.RemedyCount;

                    if (!this.hisExpMestCreate.Create(expMest))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_maty_req
                    if (!this.hisExpMestMatyReqMaker.Run(data.Materials, expMest, ref expMestMatyReqs))
                    {
                        throw new Exception("ExpMestMatyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_mety_req
                    if (!this.hisExpMestMetyReqMaker.Run(data.Medicines, expMest, ref expMestMetyReqs))
                    {
                        throw new Exception("ExpMestMetyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Tao exp_mest_blty_req
                    if (!this.hisExpMestBltyReqMaker.Run(data.Bloods, expMest, ref expMestBltyReqs))
                    {
                        throw new Exception("hisExpMestBltyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.ProcessAuto(expMest);

                    this.PassResult(expMest, expMestMatyReqs, expMestMetyReqs, expMestBltyReqs, ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    result = true;
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
        private void ProcessAuto(HIS_EXP_MEST expMest)
        {
            try
            {
                this.hisExpMestAutoProcess.Run(expMest, ref this.recentResultSDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> expMatyReqs, List<HIS_EXP_MEST_METY_REQ> expMetyReqs, List<HIS_EXP_MEST_BLTY_REQ> expBltyReqs, ref HisExpMestResultSDO resultData)
        {
            if (this.recentResultSDO!=null)
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
            }
        }

        private void RollBack()
        {
            this.hisExpMestCreate.RollbackData();
            this.hisExpMestMatyReqMaker.Rollback();
            this.hisExpMestMetyReqMaker.Rollback();
        }
    }
}
