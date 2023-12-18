using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafe_Bulochka
{
    internal class Helper
    {
        public static DataBases.BulochkaContext db = new DataBases.BulochkaContext();
        public static DataBases.User usersession;
        public static DataBases.Order order;
    }
}
