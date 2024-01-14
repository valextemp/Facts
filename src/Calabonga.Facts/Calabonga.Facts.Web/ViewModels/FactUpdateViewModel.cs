using Calabonga.Facts.Web.Data;

namespace Calabonga.Facts.Web.ViewModels
{
    public class FactUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
