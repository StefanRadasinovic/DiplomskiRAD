using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DiplomskiRAD.Enums;

namespace DiplomskiRAD.Models
{
    public class Order
    {
        public Guid Id { get; set; }    

        public double OrderAmount { get; set; }

        public double TotalPrice { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus OrderStatus {  get; set; }

        [JsonIgnore]
        public List<Equipment> Equipments { get; set; }

        [JsonIgnore]
        public List<Motorcycle> Motorcycles { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
