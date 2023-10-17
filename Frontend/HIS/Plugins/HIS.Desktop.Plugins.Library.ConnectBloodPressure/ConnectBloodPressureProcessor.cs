using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Library.ConnectBloodPressure
{
    public class ConnectBloodPressureProcessor
    {
        public static bool OpenAppBloodPressure()
        {
            try
            {
                if (IsProcessOpen("Blood.Pressure.Monitor"))
                {
                    return true;
                }
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Application.StartupPath + @"\Integrate\BloodPressure\Blood.Pressure.Monitor.exe";
                Process.Start(startInfo);
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return false;
        }

        private static bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool KillAppBloodPressure()
        {
            try
            {
                var processBloodPressure = System.Diagnostics.Process.GetProcesses().Where(o => o.ProcessName.Contains("Blood.Pressure.Monitor")).ToList();
                if (processBloodPressure != null && processBloodPressure.Count() > 0)
                {
                    for (int i = 0; i < processBloodPressure.Count(); i++)
                    {
                        try
                        {
                            processBloodPressure[i].Kill();
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Debug(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        public static HIS_DHST GetData()
        {
            HIS_DHST result = null;
            try
            {
                if (!OpenAppBloodPressure())
                {
                    MessageBox.Show("Lỗi mở ứng dụng kết nối máy đo.");
                    return result;
                }

                ConnectService.ServiceConnectClient client = new ConnectService.ServiceConnectClient();
                string data = client.GetData();
                if (!String.IsNullOrWhiteSpace(data))
                {
                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
                    var receivedData = Newtonsoft.Json.JsonConvert.DeserializeObject<BloodPressureRessultADO>(data);
                    if (receivedData != null)
                    {
                        if (String.IsNullOrWhiteSpace(receivedData.Message))
                        {
                            result = new HIS_DHST();
                            if (!String.IsNullOrWhiteSpace(receivedData.Systolic))
                            {
                                result.BLOOD_PRESSURE_MAX = Inventec.Common.TypeConvert.Parse.ToInt64(receivedData.Systolic);
                            }

                            if (!String.IsNullOrWhiteSpace(receivedData.Diastolic))
                            {
                                result.BLOOD_PRESSURE_MIN = Inventec.Common.TypeConvert.Parse.ToInt64(receivedData.Diastolic);
                            }

                            if (!String.IsNullOrWhiteSpace(receivedData.Time))
                            {
                                result.EXECUTE_TIME = Inventec.Common.TypeConvert.Parse.ToInt64(receivedData.Time);
                            }

                            if (!String.IsNullOrWhiteSpace(receivedData.Pulse))
                            {
                                result.PULSE = Inventec.Common.TypeConvert.Parse.ToInt64(receivedData.Pulse);
                            }
                        }
                        else
                        {
                            MessageBox.Show(receivedData.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Xử lý thất bại. Không nhận được kết quả");
                    }
                }
                else
                {
                    MessageBox.Show("Xử lý thất bại. Không nhận được kết quả");
                }
            }
            catch (Exception ex)
            {
                KillAppBloodPressure();
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
