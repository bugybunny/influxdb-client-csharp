using System.Reflection;
using InfluxDB.Client.Core.Flux.Domain;

namespace InfluxDB.Client.Core.Flux.Internal
{
    public interface IFluxResultMapper
    {
        /// <summary>
        /// Converts flux record to object specified by Type.
        /// </summary>
        /// <param name="fluxRecord">Flux record</param>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <returns>Converted entity.</returns>
        T ConvertToEntity<T>(FluxRecord fluxRecord);

        /// <summary>
        /// Check if specified property is Timestamp or not. 
        /// </summary>
        /// <param name="propertyInfo">property</param>
        /// <returns>True if property is Timestamp otherwise false</returns>
        bool IsTimestamp(PropertyInfo propertyInfo);
        
        /// <summary>
        /// Get name of property that will be use as a tag name or field name in InfluxDB.
        /// </summary>
        /// <param name="propertyInfo">property</param>
        /// <returns>Returns name of field or tag in InfluxDB</returns>
        string GetColumnName(PropertyInfo propertyInfo);
    }
}