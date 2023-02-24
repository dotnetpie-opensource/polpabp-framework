using System;
using Volo.Abp.Application.Dtos;

namespace PolpAbp.Framework.Auditing.Dto
{
    public class GetAuditLogsInput : PagedAndSortedResultRequestDto 
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string UserName { get; set; }

        public string ServiceName { get; set; }

        public string MethodName { get; set; }

        public string BrowserInfo { get; set; }

        public bool? HasException { get; set; }

        public int? MinExecutionDuration { get; set; }

        public int? MaxExecutionDuration { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "ExecutionTime DESC";
            }

            Sorting = Sorting.ReplaceSorting(s =>
            {
	            if (s.IndexOf("UserName", StringComparison.OrdinalIgnoreCase) >= 0)
	            {
		            s = "User." + s;
	            }
	            else
	            {
		            s = "AuditLog." + s;
	            }

	            return s;
            });
        }
    }
}
