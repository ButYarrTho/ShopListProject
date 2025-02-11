using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoppingListTracker.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual List<Item> Items { get; set; } = new(); // one to many 
    }
}
