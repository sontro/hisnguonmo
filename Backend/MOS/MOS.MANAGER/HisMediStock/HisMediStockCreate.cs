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
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMediStock
{
    class HisMediStockCreate : BusinessBase
    {
        private HisMediStockImtyCreate hisMediStockImtyCreate;
        private HisMediStockExtyCreate hisMediStockExtyCreate;
        private HisRoomCreate hisRoomCreate;

        private List<HIS_MEDI_STOCK> recentHisMediStocks = new List<HIS_MEDI_STOCK>();

        internal HisMediStockCreate()
            : base()
        {
            this.Init();
        }

        internal HisMediStockCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMediStockExtyCreate = new HisMediStockExtyCreate(param);
            this.hisMediStockImtyCreate = new HisMediStockImtyCreate(param);
            this.hisRoomCreate = new HisRoomCreate(param);
        }

        internal bool Create(HisMediStockSDO data)
        {
            bool result = false;
            try
            {
                if (data != null)
                {
                    data.HisRoom.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__KHO;

                    if (!this.hisRoomCreate.Create(data.HisRoom))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    data.HisMediStock.ROOM_ID = data.HisRoom.ID;
                    if (!this.Create(data.HisMediStock))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (IsNotNullOrEmpty(data.HisMediStockExtys))
                    {
                        data.HisMediStockExtys.ForEach(o => o.MEDI_STOCK_ID = data.HisMediStock.ID);
                        if (!hisMediStockExtyCreate.CreateList(data.HisMediStockExtys))
                        {
                            result = false;
                            throw new Exception("Khong tao duoc HisMediStockExtys. Rollback du lieu ket thuc nghiep vu " + LogUtil.TraceData(LogUtil.GetMemberName(() => data.HisRoom), data.HisRoom));
                        }
                    }

                    if (IsNotNullOrEmpty(data.HisMediStockImtys))
                    {
                        data.HisMediStockImtys.ForEach(o => o.MEDI_STOCK_ID = data.HisMediStock.ID);
                        if (!hisMediStockImtyCreate.CreateList(data.HisMediStockImtys))
                        {
                            result = false;
                            throw new Exception("Khong tao duoc HisMediStockImtys. Rollback du lieu ket thuc nghiep vu " + LogUtil.TraceData(LogUtil.GetMemberName(() => data.HisRoom), data.HisRoom));
                        }
                    }
                    result = true;
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

        internal bool CreateList(List<HisMediStockSDO> listData)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(listData))
                {
                    return false;
                }

                List<HIS_ROOM> listRoom = listData.Select(s => s.HisRoom).ToList();
                List<HIS_MEDI_STOCK> listStock = listData.Select(s => s.HisMediStock).ToList();
                listRoom.ForEach(o => o.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__KHO);

                if (!this.hisRoomCreate.CreateList(listRoom))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }

                listData.ForEach(o => o.HisMediStock.ROOM_ID = o.HisRoom.ID);

                if (!this.CreateList(listStock))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }

                List<HIS_MEDI_STOCK_IMTY> mediStockImtys = new List<HIS_MEDI_STOCK_IMTY>();
                List<HIS_MEDI_STOCK_EXTY> mediStockExtys = new List<HIS_MEDI_STOCK_EXTY>();

                foreach (HisMediStockSDO data in listData)
                {
                    if (IsNotNullOrEmpty(data.HisMediStockExtys))
                    {
                        data.HisMediStockExtys.ForEach(o => o.MEDI_STOCK_ID = data.HisMediStock.ID);
                        mediStockExtys.AddRange(data.HisMediStockExtys);
                    }

                    if (IsNotNullOrEmpty(data.HisMediStockImtys))
                    {
                        data.HisMediStockImtys.ForEach(o => o.MEDI_STOCK_ID = data.HisMediStock.ID);
                        mediStockImtys.AddRange(data.HisMediStockImtys);
                    }
                }

                if (IsNotNullOrEmpty(mediStockExtys))
                {
                    if (!hisMediStockExtyCreate.CreateList(mediStockExtys))
                    {
                        result = false;
                        throw new Exception("Khong tao duoc HisMediStockExtys. Rollback du lieu ket thuc nghiep vu ");
                    }
                }

                if (IsNotNullOrEmpty(mediStockImtys))
                {
                    if (!hisMediStockImtyCreate.CreateList(mediStockImtys))
                    {
                        result = false;
                        throw new Exception("Khong tao duoc HisMediStockImtys. Rollback du lieu ket thuc nghiep vu ");
                    }
                }
                result = true;
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

        private bool Create(HIS_MEDI_STOCK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockCheck checker = new HisMediStockCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEDI_STOCK_CODE, null);
                if (valid)
                {
                    result = DAOWorker.HisMediStockDAO.Create(data);
                    if (result)
                    {
                        this.recentHisMediStocks.Add(data);
                    }
                    else
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMediStockExty_ThemMoiThatBai);
                    }
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

        private bool CreateList(List<HIS_MEDI_STOCK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockCheck checker = new HisMediStockCheck(param);
                valid = valid && IsNotNullOrEmpty(listData);
                foreach (HIS_MEDI_STOCK data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDI_STOCK_CODE, null);
                }
                if (valid)
                {
                    result = DAOWorker.HisMediStockDAO.CreateList(listData);
                    if (result)
                    {
                        this.recentHisMediStocks.AddRange(listData);
                    }
                    else
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisMediStockExty_ThemMoiThatBai);
                    }
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
            if (IsNotNullOrEmpty(this.recentHisMediStocks))
            {
                if (!DAOWorker.HisMediStockDAO.TruncateList(this.recentHisMediStocks))
                {
                    LogSystem.Warn("Khong Rollback Truncate duoc HisMediStock. Kiem tra lai du liue");
                }
            }
            this.hisRoomCreate.Rollback();
        }
    }
}
