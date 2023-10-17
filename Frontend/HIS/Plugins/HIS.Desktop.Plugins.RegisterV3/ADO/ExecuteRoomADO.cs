using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterV3.ADO
{
    public class ExecuteRoomADO
    {
        public string EXECUTE_ROOM_NAME_1 { get; set; }
        public long TOTAL_OPEN_1 { get; set; }
        public long TOTAL_TODAY_1 { get; set; }
        public long MAX_BY_DAY_1 { get; set; }
        public string AMOUNT_1 { get; set; }

        public string EXECUTE_ROOM_NAME_2 { get; set; }
        public long TOTAL_OPEN_2 { get; set; }
        public long TOTAL_TODAY_2 { get; set; }
        public long MAX_BY_DAY_2 { get; set; }
        public string AMOUNT_2 { get; set; }

        public string EXECUTE_ROOM_NAME_3 { get; set; }
        public long TOTAL_OPEN_3 { get; set; }
        public long TOTAL_TODAY_3 { get; set; }
        public long MAX_BY_DAY_3 { get; set; }
        public string AMOUNT_3 { get; set; }

        public string EXECUTE_ROOM_NAME_4 { get; set; }
        public long TOTAL_OPEN_4 { get; set; }
        public long TOTAL_TODAY_4 { get; set; }
        public long MAX_BY_DAY_4 { get; set; }
        public string AMOUNT_4 { get; set; }

        public string EXECUTE_ROOM_NAME_5 { get; set; }
        public long TOTAL_OPEN_5 { get; set; }
        public long TOTAL_TODAY_5 { get; set; }
        public long MAX_BY_DAY_5 { get; set; }
        public string AMOUNT_5 { get; set; }

        public void SetValueRoom(V_HIS_EXECUTE_ROOM_1 data, int num)
        {
            try
            {
                if (data != null)
                {
                    if (num == 1)
                    {
                        this.EXECUTE_ROOM_NAME_1 = data.EXECUTE_ROOM_NAME;
                        this.TOTAL_OPEN_1 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ ?? 0);
                        this.TOTAL_TODAY_1 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ ?? 0);
                        this.MAX_BY_DAY_1 = data.MAX_REQUEST_BY_DAY ?? 0;
                        this.AMOUNT_1 = this.TOTAL_OPEN_1 + "/" + this.TOTAL_TODAY_1 + "(" + this.MAX_BY_DAY_1 + ")";
                    }
                    else if (num == 2)
                    {
                        this.EXECUTE_ROOM_NAME_2 = data.EXECUTE_ROOM_NAME;
                        this.TOTAL_OPEN_2 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ ?? 0);
                        this.TOTAL_TODAY_2 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ ?? 0);
                        this.MAX_BY_DAY_2 = data.MAX_REQUEST_BY_DAY ?? 0;
                        this.AMOUNT_2 = this.TOTAL_OPEN_2 + "/" + this.TOTAL_TODAY_2 + "(" + this.MAX_BY_DAY_2 + ")";
                    }
                    else if (num == 3)
                    {
                        this.EXECUTE_ROOM_NAME_3 = data.EXECUTE_ROOM_NAME;
                        this.TOTAL_OPEN_3 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ ?? 0);
                        this.TOTAL_TODAY_3 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ ?? 0);
                        this.MAX_BY_DAY_3 = data.MAX_REQUEST_BY_DAY ?? 0;
                        this.AMOUNT_3 = this.TOTAL_OPEN_3 + "/" + this.TOTAL_TODAY_3 + "(" + this.MAX_BY_DAY_3 + ")";
                    }
                    else if (num == 4)
                    {
                        this.EXECUTE_ROOM_NAME_4 = data.EXECUTE_ROOM_NAME;
                        this.TOTAL_OPEN_4 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ ?? 0);
                        this.TOTAL_TODAY_4 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ ?? 0);
                        this.MAX_BY_DAY_4 = data.MAX_REQUEST_BY_DAY ?? 0;
                        this.AMOUNT_4 = this.TOTAL_OPEN_4 + "/" + this.TOTAL_TODAY_4 + "(" + this.MAX_BY_DAY_4 + ")";
                    }
                    else if (num == 5)
                    {
                        this.EXECUTE_ROOM_NAME_5 = data.EXECUTE_ROOM_NAME;
                        this.TOTAL_OPEN_5 = Convert.ToInt64(data.TOTAL_OPEN_SERVICE_REQ ?? 0);
                        this.TOTAL_TODAY_5 = Convert.ToInt64(data.TOTAL_TODAY_SERVICE_REQ ?? 0);
                        this.MAX_BY_DAY_5 = data.MAX_REQUEST_BY_DAY ?? 0;
                        this.AMOUNT_5 = this.TOTAL_OPEN_5 + "/" + this.TOTAL_TODAY_5 + "(" + this.MAX_BY_DAY_5 + ")";
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
