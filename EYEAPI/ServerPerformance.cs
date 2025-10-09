 public class ServerPerformance
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int TemperatureC { get; set; }

        public double TotalRam { get; set; }
        public double FreeRam { get; set; }
        public double RamUsagePercentage = ((TotalRam -  FreeRam) / TotalRam ) * 100;
        public double TotalDisk { get; set; }
        public double FreeDisk { get; set; }
        public double DiskUsagePercentage ((TotalDisk - FreeDisk ) / TotalDisk ) * 100;
         
    }
