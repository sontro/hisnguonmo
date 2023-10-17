using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMediStockExty;
using MOS.MANAGER.HisMediStockImty;
using MOS.MANAGER.HisRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStock
{
    class HisMediStockUpdate : BusinessBase
    {
        private HisMediStockImtyCreate hisMediStockImtyCreate;
        private HisMediStockExtyCreate hisMediStockExtyCreate;
        private HisMediStockImtyTruncate hisMediStockImtyTruncate;
        private HisMediStockExtyTruncate hisMediStockExtyTruncate;
        private HisRoomUpdate hisRoomUpdate;

        internal HisMediStockUpdate()
            : base()
        {
            this.Init();
        }

        internal HisMediStockUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMediStockExtyCreate = new HisMediStockExtyCreate(param);
            this.hisMediStockExtyTruncate = new HisMediStockExtyTruncate(param);
            this.hisMediStockImtyCreate = new HisMediStockImtyCreate(param);
            this.hisMediStockImtyTruncate = new HisMediStockImtyTruncate(param);
            this.hisRoomUpdate = new HisRoomUpdate(param);
        }

        internal bool Update(HisMediStockSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    //backup du lieu de phuc vu rollback
                    Mapper.CreateMap<HIS_ROOM, HIS_ROOM>();
                    HIS_ROOM beforeUpdate = Mapper.Map<HIS_ROOM>(data.HisRoom);

                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__KHO;

                    if (!this.hisRoomUpdate.Update(data.HisRoom))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    if (!this.hisMediStockExtyTruncate.TruncateByMediStockId(data.HisMediStock.ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.hisMediStockImtyTruncate.TruncateByMediStockId(data.HisMediStock.ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(data.HisMediStockExtys))
                    {
                        data.HisMediStockExtys.ForEach(o => 
                            {
                                o.MEDI_STOCK_ID = data.HisMediStock.ID;
                                o.ID = 0;
                            });
                        if (!hisMediStockExtyCreate.CreateList(data.HisMediStockExtys))
                        {
                            result = false;
                            throw new Exception("Khong tao duoc HisMediStockExtys. Rollback du lieu ket thuc nghiep vu " + LogUtil.TraceData(LogUtil.GetMemberName(() => data.HisRoom), data.HisRoom));
                        }
                    }

                    if (IsNotNullOrEmpty(data.HisMediStockImtys))
                    {
                        data.HisMediStockImtys.ForEach(o =>
                        {
                            o.MEDI_STOCK_ID = data.HisMediStock.ID;
                            o.ID = 0;
                        });
                        if (!hisMediStockImtyCreate.CreateList(data.HisMediStockImtys))
                        {
                            result = false;
                            throw new Exception("Khong tao duoc HisMediStockImtys. Rollback du lieu ket thuc nghiep vu " + LogUtil.TraceData(LogUtil.GetMemberName(() => data.HisRoom), data.HisRoom));
                        }
                    }
                    data.HisMediStock.ROOM_ID = data.HisRoom.ID;
                    result = this.Update(data.HisMediStock);

                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool Update(HIS_MEDI_STOCK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockCheck checker = new HisMediStockCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.MEDI_STOCK_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMediStockDAO.Update(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            this.hisMediStockExtyCreate.RollbackData();
            this.hisMediStockImtyCreate.RollbackData();
            this.hisRoomUpdate.Rollback();
        }
    }
}
