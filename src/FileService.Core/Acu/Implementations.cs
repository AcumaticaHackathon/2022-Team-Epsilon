
using PX.Data.WorkflowAPI;
using PX.Objects.PM;

namespace FileService.Acu
{
    public class PMProjectFileExtension : FileGraphExtension<ProjectEntry, PMProject, PMProject.noteID>
    {
        public override void Configure(PXScreenConfiguration configuration)
        {
            var context = configuration.GetScreenConfigurationContext<ProjectEntry, PMProject>();
            context.ActionDefinitions.CreateNew(nameof(ActionDownloadFile), config=>config);
            context.ActionDefinitions.CreateNew(nameof(ActionRedirectToFile), config => config);
            context.ActionDefinitions.CreateNew(nameof(ActionUploadFile), config => config);
        }
    }
}