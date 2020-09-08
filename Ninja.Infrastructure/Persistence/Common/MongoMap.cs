using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Infrastructure.Persistence.Common
{
    public static class MongoMap
    {
        public static void Configure()
        {
            UserMap.Config();
        }
    }
}
