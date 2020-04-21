using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Utility.Locker
{
    public static class LockExtension
    {
        public static void Lock(this string key, LockWrapper.LockDelegate action)
        {
            using (var lockWrapper = new LockWrapper(key))
            {
                lockWrapper.Lock(action);
            }
        }

        public static object Lock(this string key, Func<object> action)
        {
            using (var lockWrapper = new LockWrapper(key))
            {
                return lockWrapper.Lock(() => action);
            }
        }
    }
}
