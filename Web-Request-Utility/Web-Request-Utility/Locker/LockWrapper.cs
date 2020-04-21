using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Utility.Locker
{
    /// <summary>
    /// 封装锁
    /// 静态LockPool, 锁池, 通过Key来区分不同的锁, dispose释放对应key的锁
    /// 相同key初始化会累加锁count, 每次dispose count 减一, 当count=0时remove
    /// 
    /// </summary>
    public class LockWrapper : IDisposable
    {
        private readonly object _locker = new object();
        private static readonly Dictionary<string, Locker> LockPool = new Dictionary<string, Locker>();

        public delegate T LockDelegate<T>();
        public delegate void LockDelegate();

        private string LockKey { get; set; }

        public LockWrapper(string key)
        {
            LockKey = key;
            lock (_locker)
            {
                if (LockPool.ContainsKey(key))
                {
                    LockPool[key].Count++;
                }
                else
                {
                    LockPool[key] = new Locker(key);
                }
            }
        }

        public void Lock(LockDelegate action)
        {
            lock (LockPool[LockKey])
            {
                action.Invoke();
            }
        }

        public T Lock<T>(LockDelegate<T> action)
        {
            lock (LockPool[LockKey])
            {
                return action.Invoke();
            }
        }

        public void Dispose()
        {
            lock (_locker)
            {
                if (LockPool.ContainsKey(LockKey))
                {
                    LockPool[LockKey].Count--;

                    if (LockPool[LockKey].Count == 0)
                    {
                        LockPool.Remove(LockKey);
                    }
                }
            }
        }
    }

    public class Locker
    {
        public Locker(string key)
        {
            Key = key;
            Count = 1;
        }

        public string Key { get; set; }

        public int Count { get; set; }
    }
}
