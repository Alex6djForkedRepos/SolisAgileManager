using System.Net.Http.Json;
using System.Text.Json;
using SolisManager.Shared;
using SolisManager.Shared.Models;

namespace SolisManager.Client.Services;

public class ClientInverterService( HttpClient httpClient ) : IInverterService
{
    public SolisManagerState InverterState { get; private set; } = new();

    public async Task RefreshInverterState()
    {
        // Load the data from the server
        var state = await httpClient.GetFromJsonAsync<SolisManagerState?>("inverter/refreshinverterdata");
        if (state != null)
            InverterState = state;
    }

    public async Task OverrideSlotAction(ChangeSlotActionRequest request)
    {
        await httpClient.PostAsJsonAsync("inverter/overrideslotaction", request);
    }
    public async Task<List<HistoryEntry>> GetHistory()
    {
        var result = await httpClient.GetFromJsonAsync<List<HistoryEntry>>("inverter/history");
        if (result != null)
            return result;

        return [];
    }

    public async Task<SolisManagerConfig> GetConfig()
    {
        var result = await httpClient.GetFromJsonAsync<SolisManagerConfig>("inverter/getconfig");
        
        ArgumentNullException.ThrowIfNull(result);
        
        return result;
    }

    public async Task<ConfigSaveResponse> SaveConfig(SolisManagerConfig config)
    {
        // TODO - investigate why passing the object directly, rather than the json
        // as a queryparam, doesn't work. 
        var json = JsonSerializer.Serialize(config);
        var response = await httpClient.PostAsync($"inverter/saveconfig?configJson={json}", null);

        if (response.IsSuccessStatusCode)
        {
            var errResponse = await response.Content.ReadFromJsonAsync<ConfigSaveResponse?>();
            if( errResponse != null )
                return errResponse;
        }

        return new ConfigSaveResponse { Success = false, Message = "Unknown error saving config." };
    }

    public async Task ClearOverrides()
    {
        await httpClient.GetAsync("inverter/clearoverrides");
    }

    public async Task AdvanceSimulation()
    {
        await httpClient.GetAsync("inverter/advancesimulation");
    }

    public async Task ResetSimulation()
    {
        await httpClient.GetAsync("inverter/resetsimulation");
    }

    public async Task TestCharge()
    {
        await httpClient.GetAsync("inverter/testcharge");
    }
    
    public async Task ChargeBattery()
    {
        await httpClient.GetAsync("inverter/chargebattery");
    }

    public async Task DischargeBattery()
    {
        await httpClient.GetAsync("inverter/dischargebattery");
    }

    public async Task DumpAndChargeBattery()
    {
        await httpClient.GetAsync("inverter/dumpandchargebattery");
    }
    
    public async Task<NewVersionResponse> GetVersionInfo()
    {
        var result = await httpClient.GetFromJsonAsync<NewVersionResponse>("inverter/versioninfo");
        ArgumentNullException.ThrowIfNull(result);
        return result;
    }
}