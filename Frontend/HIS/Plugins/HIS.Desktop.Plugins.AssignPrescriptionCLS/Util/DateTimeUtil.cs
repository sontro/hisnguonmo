using System;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.Util
{
    public class DateTimeUtil
    {
        public static long TimeMinus(long? timeIn, long? timeOut)
        {
            long result = 0;
            try
            {
                if (!timeIn.HasValue)
                    return 0;
                if (!timeOut.HasValue)
                    return 0;
                if (timeIn > timeOut)
                    return result;

                DateTime dtIn = TimeNumberToSystemDateTime(timeIn.Value) ?? DateTime.Now;
                DateTime dtOut = TimeNumberToSystemDateTime(timeOut.Value) ?? DateTime.Now;
                result = (int)((TimeSpan)(dtOut.Date - dtIn.Date)).TotalDays;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        private static System.DateTime? TimeNumberToSystemDateTime(long time)
        {
            System.DateTime? result = null;
            try
            {
                if (time > 0)
                {
                    result = System.DateTime.ParseExact(time.ToString(), "yyyyMMddHHmmss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
