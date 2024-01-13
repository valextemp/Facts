using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Facts.Web.Data
{
    /// <summary>
    /// Notification Entity
    /// </summary>
    public class Notification:Auditable
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsCompleted { get; set; }
        public string AddressFrom { get; set; }
        public string AddessTo { get; set; }
    }
}
