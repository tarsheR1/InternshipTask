using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.Application.Extensions
{
    public static class GuidExtension
    {
        public static Guid NewCombGuid(this Guid guid)
        {
            byte[] guidBytes = Guid.NewGuid().ToByteArray();
            byte[] timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            Buffer.BlockCopy(timestamp, 0, guidBytes, 10, 6);

            return new Guid(guidBytes);
        }
    }
}
