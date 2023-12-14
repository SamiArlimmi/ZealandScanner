using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ZealandIdScanner.Models
{
    public class Lokaler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LokaleId { get; set; }
        public string Navn { get; set; }

        [ForeignKey("Sensor")]
        public int SensorId { get; set; }

        public Lokaler(string navn)
        {
            Navn = navn;
        }

        public Lokaler()
        {

        }

        public override string ToString()
        {
            return $"{{{nameof(LokaleId)}={LokaleId.ToString()}, {nameof(Navn)}={Navn}}}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Lokaler lokaler &&
                   LokaleId == lokaler.LokaleId &&
                   Navn == lokaler.Navn;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
