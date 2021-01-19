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
    }
}