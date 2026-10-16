using ClientBlazor.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClientBlazor.ViewModels;

public partial class ListViewModel<TListDto, TDetailDto>(IApiService<TListDto, TDetailDto> service) : ObservableObject
    where TListDto : class
    where TDetailDto : class
{
    [ObservableProperty]
    private bool _isLoading = true;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private IEnumerable<TListDto> _items = [];

    public async Task LoadDataAsync()
    {
        IsLoading = true;
        ErrorMessage = null;

        try
        {
            Items = await service.GetAllAsync();
        }
        catch (HttpRequestException)
        {
            ErrorMessage = "Impossible de joindre l'API.";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
