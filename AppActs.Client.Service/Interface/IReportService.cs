using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model;
using AppActs.Client.Model.Enum;
using AppActs.Model.Enum;
using AppActs.Model;
using MongoDB.Bson;

namespace AppActs.Client.Service.Interface
{
    public interface IReportService
    {
        /// <summary>
        /// Comparing Application vs Application ( A v A )
        /// </summary>
        GraphWithTabularCompare GetGraphApplications(Guid graphGuid, Guid applicationId,
            IEnumerable<Guid> applicationIdsCompare, DateTime dateStart, DateTime dateEnd);

        GraphWithTabularCompare<ApplicationMeta, Guid> GetGraphWithApplicationCompare(Guid graphGuid,
            Guid applicationId, DateTime dateStart, DateTime dateEnd);

        /// <summary>
        /// Comparing Application, Platform vs Platform A ( P v P )
        /// </summary>
        GraphWithTabularCompare GetGraphPlatform(Guid graphGuid, Guid applicationId,
            IEnumerable<PlatformType> platformTypes, DateTime dateStart, DateTime dateEnd);

        /// <summary>
        /// Gets the graph.
        /// </summary>
        GraphWithTabularCompare<Platform, PlatformType> GetGraphWithPlatformCompare(Guid graphGuid, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd);

        /// <summary>
        /// Comparing platform application A (V v V)
        /// </summary>
        GraphWithTabularCompare GetGraphVersions(Guid graphGuid, Guid applicationId, IEnumerable<string> versions, 
            DateTime dateStart, DateTime dateEnd);

        /// <summary>
        /// Gets the graph.
        /// </summary>
        GraphWithTabularCompare<string, string> GetGraphWithVersionsCompare(Guid graphGuid, Guid applicationId, 
            DateTime dateStart, DateTime dateEnd);

        /// <summary>
        /// Getting statistics for platform and application P A
        /// </summary>
        DataWithInfo GetGraphWithInfo(Guid graphGuid, Guid applicationId, DateTime dateStart, DateTime dateEnd);

        object GetDetail(Guid detailGuid, Guid applicationId, DateTime dateStart, 
            DateTime dateEnd, string detailId);

        IEnumerable<ReportDefinition> GetReportDefinitions();
    }
}
