using System;
using System.Collections.Generic;
using System.Text;

namespace Ninja.Domain.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public CollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}