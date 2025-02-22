
namespace Presentation.Dialogs
{
    public interface IMenuDialogs
    {
        Task CreateProjectOption();
        Task DeleteProjectOption();
        Task MenuOptions();
        Task UpdateProjectOption();
        Task ViewAllProjectsOption();
        Task ViewProjectOption();
        void CloseApplicationOption();
    }
}