using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{

    /// <summary>
    /// Interface for dashboard service
    /// </summary>
    public interface IDashboardService
    {
        Task<DashboardStatsDTO> GetDashboardStatsAsync();
        Task<List<RecentActivityDTO>> GetRecentActivitiesAsync(int count);
        Task<List<PipelineOverviewDTO>> GetPipelineOverviewAsync();
        Task<object> GetHiringMetricsAsync(DateTime startDate, DateTime endDate);
    }
}
