using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace EWWebProcessor
{
    public class WebUtils
    {
        public static NameValueCollection ToNameValueCollection<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            var nameValueCollection = new NameValueCollection();

            foreach (var kvp in dict)
            {
                string value = null;
                if (kvp.Value != null)
                    value = kvp.Value.ToString();

                nameValueCollection.Add(kvp.Key.ToString(), value);
            }

            return nameValueCollection;
        }
    }
}
