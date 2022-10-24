using System.ComponentModel.DataAnnotations;

namespace ClosetItemApp.Api
{
    public class ClosetItem
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string ItemType { get; set; }
        public string Color { get; set; }
    }
}