using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Threading;

namespace NoSleep
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;

            SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
           
            new System.Threading.AutoResetEvent(false).WaitOne();
        }

        static void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
             SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_USER_PRESENT = 0x00000004
        }
    }
}
