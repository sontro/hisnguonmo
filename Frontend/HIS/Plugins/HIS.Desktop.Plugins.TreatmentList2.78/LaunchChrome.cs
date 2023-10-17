using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentList
{
    class LaunchChrome
    {
        internal LaunchChrome() { }
        internal void Launch(string maThe, string successUrl, string apiDomain)
        {
            var chromePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            var found = File.Exists(chromePath);
            if (!found)
            {
                chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                found = File.Exists(chromePath);
            }

            if (!found)
            {
                using (var openFileDialog = new OpenFileDialog { Filter = "Chrome browser|chrome.exe", CheckPathExists = true, Title = "Hãy chọn trình duyệt chrome" })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        chromePath = openFileDialog.FileName;
                        found = true;
                    }
                }
            }

            if (!found)
                return;

            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => maThe), maThe) + "__" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => successUrl), successUrl) + "__" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => apiDomain), apiDomain));

            var secretKey = GetSecretKey(maThe);
            var clearText = maThe + "\n" + secretKey + "\n" + successUrl;
            var p = Convert.ToBase64String(Encoding.UTF8.GetBytes(clearText));

            Process.Start(chromePath, "-incognito " + apiDomain + "/redirect2.html?p=" + p);
        }

        string GetSecretKey(string maThe)
        {
            // return "abcdef";
            // {time}.salt.{hash({mã thẻ}.{time}.{private})}
            var tick = DateTime.Now.Ticks.ToString();
            var sha256 = SHA256.Create();
            var privateString = "VietSens_Siten_2019";
            var salt = new Random().Next().ToString();
            return tick + "." + salt + "." + Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(maThe + "." + tick + "." + salt + "." + privateString)));
        }
    }
}
