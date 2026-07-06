using System;
using System.Text;
using System.Security.Cryptography;
using System.Management;

namespace Kinovea.Root
{
    public static class MachineHelper
    {
        private static string _machineId;

        public static string GetMachineUniqueId()
        {
            if (!string.IsNullOrEmpty(_machineId))
                return _machineId;

            try
            {
                string cpuId = GetWmiInfo("Win32_Processor", "ProcessorId");
                string boardSn = GetWmiInfo("Win32_BaseBoard", "SerialNumber");
                string raw = $"{cpuId}_{boardSn}";

                using var sha = SHA256.Create();
                byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append($"{b:X2}");

                _machineId = sb.ToString();
            }
            catch
            {
                _machineId = Guid.NewGuid().ToString("N");
            }

            return _machineId;
        }

        private static string GetWmiInfo(string table, string field)
        {
            using var searcher = new ManagementObjectSearcher($"SELECT {field} FROM {table}");
            foreach (var mObj in searcher.Get())
            {
                var val = mObj[field];
                return val?.ToString() ?? "";
            }
            return "";
        }
    }
}