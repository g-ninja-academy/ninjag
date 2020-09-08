using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Infrastructure
{
    public class NinjaDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}