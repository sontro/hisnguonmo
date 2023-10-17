using Inventec.VoiceCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Utility
{
    public class VoiceCommandData
    {
        public static bool isUseVoiceCommand = false;
        public static VoiceCommandProcessor voiceCommandProcessor;
        public static System.Windows.Forms.Timer timerVoiceCommand;

    }
}
