using System;

namespace Helper.RandomHelper
{
    public class RandomHelper
    {
        public static string RandomPass(int size = 8)
        {            
            return Guid.NewGuid().ToString().Replace("-","").ToLower().Substring(0, size);
        }
    }
}
