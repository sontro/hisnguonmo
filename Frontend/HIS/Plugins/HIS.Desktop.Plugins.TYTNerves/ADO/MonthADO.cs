using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.Nerves.ADO
{
    public class MonthADO
    {
        public long Id { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public MonthADO(long id, string month)
        {
            this.Id = id;
            this.Month = month;
        }

        public MonthADO(string monthWithYear)
        {
            try
            {
                this.Id = GetIDByMonth(monthWithYear);
                this.Month = GetMonthById(GetIDByMonth(monthWithYear));
                //this.Year = monthWithYear.Substring(0,4);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private long GetIDByMonth(string month)
        {
            long result = 0;
            try
            {
                switch (month)
                {
                    case "01":
                        result = 1;
                        break;
                    case "02":
                        result = 2;
                        break;
                    case "03":
                        result = 3;
                        break;
                    case "04":
                        result = 4;
                        break;
                    case "05":
                        result = 5;
                        break;
                    case "06":
                        result = 6;
                        break;
                    case "07":
                        result = 7;
                        break;
                    case "08":
                        result = 8;
                        break;
                    case "09":
                        result = 9;
                        break;
                    case "10":
                        result = 10;
                        break;
                    case "11":
                        result = 11;
                        break;
                    case "12":
                        result = 12;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private string GetMonthById(long id)
        {
            string result = "";
            try
            {
                switch (id)
                {
                    case 1:
                        result = "Tháng 01";
                        break;
                    case 2:
                        result = "Tháng 2";
                        break;
                    case 3:
                        result = "Tháng 3";
                        break;
                    case 4:
                        result = "Tháng 4";
                        break;
                    case 5:
                        result = "Tháng 5";
                        break;
                    case 6:
                        result = "Tháng 6";
                        break;
                    case 7:
                        result = "Tháng 7";
                        break;
                    case 8:
                        result = "Tháng 8";
                        break;
                    case 9:
                        result = "Tháng 9";
                        break;
                    case 10:
                        result = "Tháng 10";
                        break;
                    case 11:
                        result = "Tháng 11";
                        break;
                    case 12:
                        result = "Tháng 12";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
    }
}
