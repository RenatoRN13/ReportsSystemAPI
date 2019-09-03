using System;

namespace ReportsSystemApi.Domain.Entities {
    public class Log {
        public int id { get; set; }
        public string acao { get; set; }
        public DateTime data { get; set; }
        public Usuario usuario { get; set; }
        
    }
}