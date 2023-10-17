using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMediStock;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisImpMest.UpdateStatus;
using MOS.MANAGER.HisMediStockImty;
using AutoMapper;
using MOS.MANAGER.HisImpMest.Import;
using MOS.MANAGER.HisImpMest.UnImport;

namespace MOS.MANAGER.HisImpMest
{
    class HisImpMestAutoProcess : BusinessBase
    {
        private HisImpMestCancelImport hisImpMestCancelImport;
        private HisImpMestImport hisImpMestImport;
        private HisImpMestUpdateStatus hisImpMestUpdateStatus;

        internal HisImpMestAutoProcess()
            : base()
        {
            this.Init();
        }

        internal HisImpMestAutoProcess(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestImport = new HisImpMestImport(param);
            this.hisImpMestUpdateStatus = new HisImpMestUpdateStatus(param);
            this.hisImpMestCancelImport = new HisImpMestCancelImport(param);
        }

        internal void Run(long id)
        {
            try
            {
                HIS_IMP_MEST data = new HisImpMestGet().GetById(id);
                this.Run(data);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
        }

        internal void Run(HIS_IMP_MEST data, long? expMediStockId = null)
        {
            try
            {
                HIS_MEDI_STOCK_IMTY mediStockImty = HisMediStockImtyCFG.DATA != null ?
                    HisMediStockImtyCFG.DATA.Where(o => o.MEDI_STOCK_ID == data.MEDI_STOCK_ID && o.IMP_MEST_TYPE_ID == data.IMP_MEST_TYPE_ID).FirstOrDefault() : null;

                if (new HisImpMestCheck().IsAutoStockTransfer(data, expMediStockId)
                    || (mediStockImty != null && data != null))
                {
                    //Tu dong thuc duyet voi phieu xuat dang o trang thai yeu cau
                    if (data.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST)
                    {
                        this.AutoApprove(data, mediStockImty, expMediStockId);
                    }
                    //Tu dong thuc xuat voi nhung phieu xuat duoc duyet
                    else if (data.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL)
                    {
                        this.AutoImport(data, mediStockImty, expMediStockId);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void AutoImport(HIS_IMP_MEST data, HIS_MEDI_STOCK_IMTY mediStockImty, long? expMediStockId)
        {
            try
            {
                //khong tu dong voi phieu tong hop tra
                if (data.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT)
                {
                    //Neu kho cho phep tu dong thuc xuat thi thuc hien xuat
                    if (new HisImpMestCheck().IsAutoStockTransfer(data, expMediStockId)
                        || (mediStockImty != null && mediStockImty.IS_AUTO_EXECUTE == MOS.UTILITY.Constant.IS_TRUE))
                    {
                        HIS_IMP_MEST resultData = null;

                        if (!this.hisImpMestImport.Import(data, true, ref resultData))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TuDongNhapThatBai);
                            LogSystem.Warn("Tu dong thuc nhap that bai." + LogUtil.TraceData("data", data));
                        }
                        else
                        {
                            //update lai thong tin cua doi tuong du lieu truyen vao
                            data.IMP_MEST_STT_ID = resultData.IMP_MEST_STT_ID;
                            data.IMP_LOGINNAME = resultData.IMP_LOGINNAME;
                            data.IMP_TIME = resultData.IMP_TIME;
                            data.IMP_USERNAME = resultData.IMP_USERNAME;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void AutoApprove(HIS_IMP_MEST data, HIS_MEDI_STOCK_IMTY mediStockImty, long? expMediStockId)
        {
            try
            {
                //Neu kho cho phep tu dong duyet hoac tu dong xuat thi thuc hien duyet phieu xuat
                //Luu y: ko tu dong xuat voi loai la xuat mat mat
                //Tien hanh tu dong duyet
                if (new HisImpMestCheck().IsAutoStockTransfer(data, expMediStockId)
                    || (mediStockImty.IS_AUTO_APPROVE == MOS.UTILITY.Constant.IS_TRUE && data.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL))
                {
                    Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                    HIS_IMP_MEST update = Mapper.Map<HIS_IMP_MEST>(data);
                    update.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL;
                    HIS_IMP_MEST resultData = null;
                    if (!this.hisImpMestUpdateStatus.UpdateStatus(update, true, ref resultData))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TuDongDuyetThatBai);
                        LogSystem.Error("Tu dong duyet phieu nhap that bai." + LogUtil.TraceData("data", data));
                    }
                    else
                    {
                        data.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL;
                        data.APPROVAL_LOGINNAME = resultData.APPROVAL_LOGINNAME;
                        data.APPROVAL_TIME = resultData.APPROVAL_TIME;
                        data.APPROVAL_USERNAME = resultData.APPROVAL_USERNAME;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void Rollback()
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
