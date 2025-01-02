using CommunityToolkit.Mvvm.Input;
using Viamus.Fast.Sharp.Dispatcher.Viewer.Models;

namespace Viamus.Fast.Sharp.Dispatcher.Viewer.PageModels;

public interface IProjectTaskPageModel
{
    IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
    bool IsBusy { get; }
}