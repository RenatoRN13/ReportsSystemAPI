namespace ReportsSystemApi.Domain.Entities {
    public class Vinculo {
        public int id { get; set; }
        public int IES { get; set; }
        public int orgao { get; set; }
        public Usuario usuario { get; set; }
    }
}