using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin
{
    public class GlobalConfig
    {
        private static GlobalConfig instance;
        private bool usePasswordless;

        public bool UsePasswordless
        {
            get { return usePasswordless; }
        }


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

        //Define call global configs here
        private GlobalConfig()
        {
            usePasswordless = true;
        }

    }
}
