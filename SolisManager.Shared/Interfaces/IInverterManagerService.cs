using SolisManager.Shared.Models;

namespace SolisManager.Shared.Interfaces;

public interface IInverterManagerService
{
    public SolisManagerState InverterState { get; }
    public Task RefreshInverterState();
    public Task<List<HistoryEntry>> GetHistory();
    public Task<SolisManagerConfig> GetConfig();
    public Task<ConfigSaveResponse> SaveConfig(SolisManagerConfig config);

    Task<TariffComparison> GetTariffComparisonData(string tariffA, string tariffB);


    public Task OverrideSlotAction(ChangeSlotActionRequest change);
    public Task TestCharge();
    public Task ChargeBattery();
    public Task DischargeBattery();
    public Task DumpAndChargeBattery();
    public Task ClearManualOverrides();
    public Task AdvanceSimulation();
    public Task ResetSimulation();
    public Task<NewVersionResponse> GetVersionInfo();

    Task<OctopusProductResponse?> GetOctopusProducts();
    Task<OctopusTariffResponse?> GetOctopusTariffs(string product);

}

public interface IInverterRefreshService
{
    public Task UpdateInverterState();
    public Task RefreshAgileRates();
    public Task RefreshTariff();
    public Task UpdateInverterTime();
}