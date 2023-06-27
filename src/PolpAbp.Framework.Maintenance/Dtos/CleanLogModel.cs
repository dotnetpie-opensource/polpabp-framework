using System.ComponentModel.DataAnnotations;

namespace PolpAbp.Framework.Maintenance.Dtos
{
    public class CleanLogModel : OperationGuard
    {
        [Required]
        public DateTime LastCreatedUtc { get; set; }
    }
}
