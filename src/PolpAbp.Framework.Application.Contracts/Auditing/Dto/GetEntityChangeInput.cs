using System;
using Volo.Abp.Application.Dtos;

namespace PolpAbp.Framework.Auditing.Dto
{
    public class GetEntityChangeInput : PagedAndSortedResultRequestDto 
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserName { get; set; }

        public string EntityTypeFullName { get; set; }

        [Obsolete("Do not use it")]
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ChangeTime DESC";
            }

            Sorting = Sorting.ReplaceSorting(s =>
            {
                if (s.IndexOf("UserName", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    s = "User." + s;
                }
                else
                {
                    s = "EntityChange." + s;
                }

                return s;
            });
        }
    }

    public class GetEntityTypeChangeInput : PagedAndSortedResultRequestDto
    {
        public string EntityTypeFullName { get; set; }

        public string EntityId { get; set; }

        [Obsolete("Do not use it")]
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ChangeTime DESC";
            }

            Sorting = Sorting.ReplaceSorting(s =>
            {
                if (s.IndexOf("UserName", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    s = "User." + s;
                }
                else
                {
                    s = "EntityChange." + s;
                }

                return s;
            });
        }
    }
}
