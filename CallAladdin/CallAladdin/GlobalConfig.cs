using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin
{
    /// <summary>
    /// Define all global configs using this.
    /// </summary>
    public class GlobalConfig
    {
        private static GlobalConfig instance;
        //private bool usePasswordless;
        private ConcurrentDictionary<string, object> dictionary;

        //public bool UsePasswordless
        //{
        //    get { return usePasswordless; }
        //}


        public static GlobalConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalConfig();
                }
                return instance;
            }
        }

        private GlobalConfig()
        {
            //usePasswordless = false;
            dictionary = new ConcurrentDictionary<string, object>();
        }

        public object GetByKey(string key)
        {
            object result = null;

            if (this.dictionary != null)
            {
                this.dictionary.TryGetValue(key, out result);
            }

            return result;
        }

        public void SetValueByKey(string key, object value)
        {
            if (this.dictionary != null)
            {
                //this.dictionary.GetOrAdd(key, value);
                this.dictionary.AddOrUpdate(key, value, (s,o) =>
                {
                    return value;
                });
            }
        }
    }
}
