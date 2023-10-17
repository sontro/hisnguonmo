using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.AutoCreate
{
    public class HisMediStockPeriodAutoCreate : BusinessBase
    {
        private static bool IsRunning;
        private long toTime;

        public HisMediStockPeriodAutoCreate()
            : base()
        {
        }

        public HisMediStockPeriodAutoCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        public void Run()
        {
            try
            {
                if (!Config.HisMediStockCFG.IS_MEDI_STOCK_PERIOD_AUTO_CREATE)
                {
                    LogSystem.Info("He thong khong cau hinh tu dong chot ky");
                    return;
                }

                if (IsRunning)
                {
                    LogSystem.Info("Tien trinh dang duoc chay khong cho phep khoi tao tien trinh khac");
                    return;
                }

                IsRunning = true;

                if (DateTime.Now.Day == 1)
                {
                    toTime = long.Parse(DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "235959");

                    var filter = new HisMediStock.HisMediStockFilterQuery();
                    filter.IS_ACTIVE = 1;
                    List<HIS_MEDI_STOCK> listMediStock = new HisMediStock.HisMediStockGet().Get(filter);
                    if (IsNotNullOrEmpty(listMediStock))
                    {
                        HisMediStockPeriodFilterQuery periodfilter = new HisMediStockPeriodFilterQuery();
                        periodfilter.MEDI_STOCK_IDs = listMediStock.Select(s => s.ID).ToList();
                        periodfilter.TO_TIME_FROM = long.Parse(DateTime.Now.ToString("yyyyMMdd") + "000000");
                        var mediStockPeriod = new HisMediStockPeriodGet().Get(periodfilter);
                        //lấy danh sách kho chưa có chốt kỳ.
                        List<HIS_MEDI_STOCK> listMediStockCreatePeriod = new List<HIS_MEDI_STOCK>();
                        if (IsNotNullOrEmpty(mediStockPeriod))
                        {
                            listMediStockCreatePeriod = listMediStock.Where(o => mediStockPeriod.Select(s => s.MEDI_STOCK_ID).Contains(o.ID)).ToList();
                        }
                        else
                        {
                            listMediStockCreatePeriod.AddRange(listMediStock);
                        }

                        List<string> errorMediStock = new List<string>();
                        listMediStockCreatePeriod = listMediStockCreatePeriod.OrderBy(o => o.ID).ToList();
                        foreach (var item in listMediStockCreatePeriod)
                        {
                            HisMediStockPeriodFilterQuery perFilter = new HisMediStockPeriodFilterQuery();
                            perFilter.MEDI_STOCK_ID = item.ID;
                            perFilter.TO_TIME = toTime;
                            perFilter.IS_AUTO_PERIOD = true;
                            perFilter.MEDI_STOCK_PERIOD_NAME = string.Format("Kỳ tháng {0}/{1}", toTime.ToString().Substring(4, 2), toTime.ToString().Substring(0, 4));
                            List<HIS_MEDI_STOCK_PERIOD> periodeds = new HisMediStockPeriodGet().Get(perFilter);
                            if (IsNotNullOrEmpty(periodeds))
                            {
                                LogSystem.Warn(string.Format("Da ton tai tu dong chot ky cho kho: {0}", item.MEDI_STOCK_NAME));
                                continue;
                            }

                            HIS_MEDI_STOCK_PERIOD resultData = null;
                            HIS_MEDI_STOCK_PERIOD createData = new HIS_MEDI_STOCK_PERIOD();
                            createData.MEDI_STOCK_ID = item.ID;
                            createData.MEDI_STOCK_PERIOD_NAME = string.Format("Kỳ tháng {0}/{1}", toTime.ToString().Substring(4, 2), toTime.ToString().Substring(0, 4));
                            createData.TO_TIME = toTime;
                            createData.IS_AUTO_PERIOD = Constant.IS_TRUE;
                            if (!new HisMediStockPeriodCreate().Create(createData, ref resultData, true))
                            {
                                errorMediStock.Add(item.MEDI_STOCK_NAME);
                            }
                        }

                        if (IsNotNullOrEmpty(errorMediStock))
                        {
                            new EventLogGenerator(EventLog.Enum.TuDongChotKy)
                                .Detail(LogCommonUtil.GetEventLogContent(EventLog.Enum.CacKhoThatBai))
                                .PrepareDetailList(errorMediStock)
                                .Run();
                        }
                        else
                        {
                            new EventLogGenerator(EventLog.Enum.TuDongChotKy)
                                .Detail(LogCommonUtil.GetEventLogContent(EventLog.Enum.ThanhCong))
                                .Run();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            IsRunning = false;
        }
    }
}
