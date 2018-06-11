using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Helper
{
    public class Auth
    {
        public static bool UsePasswordless()
        {
            bool usePasswordless = false;

            try
            {
                usePasswordless = (bool)GlobalConfig.Instance.GetByKey("use_passwordless");
            }
            catch (Exception)
            {

            }

            return usePasswordless;
        }
    }
}
