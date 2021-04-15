using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Logic
{
    public class IdGenerator
    {
        public IdGenerator()
        { }

        public static string ID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
