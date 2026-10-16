using ClientBlazor.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClientBlazor.ViewModels;

public partial class DetailViewModel<TListDto, TDetailDto>(IApiService<TListDto, TDetailDto> service) : ObservableObject
    where TListDto : class
    where TDetailDto : class
{
    [ObservableProperty]
    private bool _isLoading = true;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private TDetailDto? _item;

    public async Task LoadDataAsync(int id)
    {
        IsLoading = true;
        ErrorMessage = null;

        try
        {
            Item = await service.GetByIdAsync(id);
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
