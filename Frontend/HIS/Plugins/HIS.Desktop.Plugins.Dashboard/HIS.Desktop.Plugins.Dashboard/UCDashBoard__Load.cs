using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using MOS.SDO;
using DevExpress.XtraCharts;
using System.Globalization;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.Plugins.Dashboard.Base;
using HIS.Desktop.Plugins.Dashboard.ADO;

namespace HIS.Desktop.Plugins.Dashboard
{
    public partial class UCDashBoard : UserControl
    {

        private List<HipoFinanceReportADO> FilterDataDateTime()
        {
            List<HipoFinanceReportADO> list = new List<HipoFinanceReportADO>();
            try
            {
                switch (cboFilter.SelectedIndex)
                {
                    case FilterCFG.NGAY:
                        list = DataByDay();
                        break;
                    case FilterCFG.TUAN:
                        list = DataByWeek();
                        break;
                    case FilterCFG.THANG:
                        list = DataByMonth();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                list = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return list;
        }

        private List<HipoFinanceReportADO> DataByDay()
        {
            List<HipoFinanceReportADO> result = new List<HipoFinanceReportADO>();
            try
            {
                if (ucFilterByDay != null)
                {
                    HipoFinanceReportADO hipoFinanceReportADO = new HipoFinanceReportADO();
                    DateTime dateTimePos = ucFilterByDay.dtFrom.DateTime;
                    hipoFinanceReportADO.Name = Inventec.Common.DateTime.Convert.SystemDateTimeToDateString(dateTimePos);
                    hipoFinanceReportADO.listTimeStr = this.GetListFirstLastTimeString(dateTimePos, dateTimePos);
                    result.Add(hipoFinanceReportADO);
                    while (DateTime.Compare(dateTimePos.Date, ucFilterByDay.dtTo.DateTime.Date) < 0)
                    {
                        hipoFinanceReportADO = new HipoFinanceReportADO();
                        this.PlusTime(ref dateTimePos, ucFilterByDay.dtTo.DateTime);
                        hipoFinanceReportADO.Name = Inventec.Common.DateTime.Convert.SystemDateTimeToDateString(dateTimePos);
                        hipoFinanceReportADO.listTimeStr = this.GetListFirstLastTimeString(dateTimePos, dateTimePos);
                        result.Add(hipoFinanceReportADO);
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private List<HipoFinanceReportADO> DataByWeek()
        {
            List<HipoFinanceReportADO> result = new List<HipoFinanceReportADO>();
            try
            {
                
                if (ucFilterByWeek != null)
                {
                    List<Week> listWeek = ucFilterByWeek.listWeek;
                    if (listWeek != null && listWeek.Count > 0)
                    {
                        int weekNumFrom = Inventec.Common.TypeConvert.Parse.ToInt32(ucFilterByWeek.cboWeekFrom.EditValue.ToString());
                        int weekNumTo = Inventec.Common.TypeConvert.Parse.ToInt32(ucFilterByWeek.cboWeekTo.EditValue.ToString());

                        for (int i = weekNumFrom; i <= weekNumTo; i++)
                        {
                            Week week = listWeek.FirstOrDefault(o => o.WeekNum == i);
                            HipoFinanceReportADO hipoFinanceReportADO = new HipoFinanceReportADO();
                            hipoFinanceReportADO.Name = "Tuần " + week.WeekNum;
                            hipoFinanceReportADO.listTimeStr = this.GetListFirstLastTimeString(week.WeekStart, week.WeekEnd);
                            result.Add(hipoFinanceReportADO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private List<HipoFinanceReportADO> DataByMonth()
        {
            List<HipoFinanceReportADO> result = new List<HipoFinanceReportADO>();
            try
            {

                if (ucFilterByMonth != null)
                {
                    List<Month> listMonth = ucFilterByMonth.listMonth;
                    if (listMonth != null && listMonth.Count > 0)
                    {
                        int monthNumFrom = Inventec.Common.TypeConvert.Parse.ToInt32(ucFilterByMonth.cboMonthFrom.EditValue.ToString());
                        int monthNumTo = Inventec.Common.TypeConvert.Parse.ToInt32(ucFilterByMonth.cboMonthTo.EditValue.ToString());

                        for (int i = monthNumFrom; i <= monthNumTo; i++)
                        {
                            Month month = listMonth.FirstOrDefault(o => o.MonthNum == i);
                            HipoFinanceReportADO hipoFinanceReportADO = new HipoFinanceReportADO();
                            hipoFinanceReportADO.Name = "Tháng " + month.MonthNum;
                            hipoFinanceReportADO.listTimeStr = this.GetListFirstLastTimeString(month.MonthStart, month.MonthEnd);
                            result.Add(hipoFinanceReportADO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
