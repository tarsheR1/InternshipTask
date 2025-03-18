namespace UserManagementService.Core
{
    public class Services
    {
        public interface IGuidGenerator
        {
            Guid NewCombGuid();
        }

        public class GuidGenerator : IGuidGenerator
        {
            public Guid NewCombGuid()
            {
                byte[] guidBytes = Guid.NewGuid().ToByteArray();
                byte[] timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

                Buffer.BlockCopy(timestamp, 0, guidBytes, 10, 6);

                return new Guid(guidBytes);
            }
        }
    }
}
