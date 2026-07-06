namespace Kinovea.Root
{
    public static class ClientSession
    {
        public static string MachineId { get; set; }
        public static string AuthToken { get; set; }
        public static string UserName { get; set; }
        public static string UserId { get; set; }

        public static bool IsLogined => !string.IsNullOrEmpty(AuthToken);

        public static void Clear()
        {
            MachineId = null;
            AuthToken = null;
            UserName = null;
            UserId = null;
        }
    }
}