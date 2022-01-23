#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

using PX.Data.WorkflowAPI;
using PX.Objects.PM;
using PX.Objects.PO;

namespace FileService.Acu.Implementation
{
    public class PMProjectFileExtension : FileGraphExtension<ProjectEntry, PMProject, PMProject.noteID>
    {
        public override void Configure(PXScreenConfiguration configuration)
        {
            var context = configuration.GetScreenConfigurationContext<POOrderEntry, POOrder>();
            context.UpdateScreenConfigurationFor(def =>
            {
                def.WithActions(actions =>
                {
                    actions.AddNew(nameof(ActionOpenFilesWindow));
                    actions.AddNew(nameof(ActionDownloadFile));
                    actions.AddNew(nameof(ActionRedirectToFile));
                    actions.AddNew(nameof(ActionUploadFile));
                });
                def.UpdateDefaultFlow(d =>
                {
                    d.WithFlowStates(states =>
                    {
                        states.Update<ProjectStatus.planned>(flowState =>
                        {
                            return flowState
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow), config => config.IsDuplicatedInToolbar());
                                    actions.Add(nameof(ActionDownloadFile));
                                    actions.Add(nameof(ActionRedirectToFile));
                                    actions.Add(nameof(ActionUploadFile));
                                });
                        });
                        states.Update<ProjectStatus.active>(flowState =>
                        {
                            return flowState
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow), config => config.IsDuplicatedInToolbar());
                                    actions.Add(nameof(ActionDownloadFile));
                                    actions.Add(nameof(ActionRedirectToFile));
                                    actions.Add(nameof(ActionUploadFile));
                                });
                        });
                        states.Update<ProjectStatus.completed>(flowState =>
                        {
                            return flowState
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow), config => config.IsDuplicatedInToolbar());
                                    actions.Add(nameof(ActionDownloadFile));
                                    actions.Add(nameof(ActionRedirectToFile));
                                    actions.Add(nameof(ActionUploadFile));
                                });
                        });
                        states.Update<ProjectStatus.cancelled>(flowState =>
                        {
                            return flowState
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow), config => config.IsDuplicatedInToolbar());
                                    actions.Add(nameof(ActionDownloadFile));
                                    actions.Add(nameof(ActionRedirectToFile));
                                    actions.Add(nameof(ActionUploadFile));
                                });
                        });
                        states.Update<ProjectStatus.onHold>(flowState =>
                        {
                            return flowState
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow), config => config.IsDuplicatedInToolbar());
                                    actions.Add(nameof(ActionDownloadFile));
                                    actions.Add(nameof(ActionRedirectToFile));
                                    actions.Add(nameof(ActionUploadFile));
                                });
                        });
                    });
                    return d;
                });
                return def;
            });
        }
    }
}