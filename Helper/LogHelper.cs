using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Helper
{
    internal class LogHelper
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    }
}
