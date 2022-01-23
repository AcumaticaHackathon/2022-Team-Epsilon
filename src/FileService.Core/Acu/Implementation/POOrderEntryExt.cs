
using PX.Data.WorkflowAPI;
using PX.Objects.PM;
using PX.Objects.PO;

namespace FileService.Acu.Implementation
{
    public class POEntryFileExtension : FileGraphExtension1<POOrderEntry, POOrder, POOrder.noteID>
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
                }).UpdateDefaultFlow(d =>
                {
                    d.WithFlowStates(states =>
                    {
                        states.Update<POOrderStatus.hold>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow));
                                });
                        });
                        states.Update<POOrderStatus.pendingPrint>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow));
                                });
                        });
                        states.Update<POOrderStatus.pendingEmail>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow));
                                });
                        });
                        states.Update<POOrderStatus.open>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow));
                                });
                        });
                        states.Update<POOrderStatus.completed>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow));
                                });
                        });
                        states.Update<POOrderStatus.cancelled>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow));
                                });
                        });
                        states.Update<POOrderStatus.closed>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow));
                                });
                        });
                    });
                    return d;
                });
                return def;
            });
        }
    }

    public class POEntryFileExtension2 : FileGraphExtension2<POOrderEntry, POOrder, POLine.noteID>
    {
        public override void Configure(PXScreenConfiguration configuration)
        {
            var context = configuration.GetScreenConfigurationContext<POOrderEntry, POOrder>();
            context.UpdateScreenConfigurationFor(def =>
            {
                def.WithActions(actions =>
                {
                    actions.AddNew(nameof(ActionOpenFilesWindow2));
                    actions.AddNew(nameof(ActionDownloadFile2));
                    actions.AddNew(nameof(ActionRedirectToFile2));
                    actions.AddNew(nameof(ActionUploadFile2));
                }).UpdateDefaultFlow(d =>
                {
                    d.WithFlowStates(states =>
                    {
                        states.Update<POOrderStatus.hold>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow2));
                                });
                        });
                        states.Update<POOrderStatus.pendingPrint>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow2));
                                });
                        });
                        states.Update<POOrderStatus.pendingEmail>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow2));
                                });
                        });
                        states.Update<POOrderStatus.open>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow2));
                                });
                        });
                        states.Update<POOrderStatus.completed>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow2));
                                });
                        });
                        states.Update<POOrderStatus.cancelled>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow2));
                                });
                        });
                        states.Update<POOrderStatus.closed>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow2));
                                });
                        });
                    });
                    return d;
                });
                return def;
            });
        }
    }

    public class POEntryFileExtension3 : FileGraphExtension3<POOrderEntry, POOrder, PMChangeOrder.noteID>
    {
        public override void Configure(PXScreenConfiguration configuration)
        {
            var context = configuration.GetScreenConfigurationContext<POOrderEntry, POOrder>();
            context.UpdateScreenConfigurationFor(def =>
            {
                def.WithActions(actions =>
                {
                    actions.AddNew(nameof(ActionOpenFilesWindow3));
                    actions.AddNew(nameof(ActionDownloadFile3));
                    actions.AddNew(nameof(ActionRedirectToFile3));
                    actions.AddNew(nameof(ActionUploadFile3));
                }).UpdateDefaultFlow(d =>
                {
                    d.WithFlowStates(states =>
                    {
                        states.Update<POOrderStatus.hold>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow3));
                                });
                        });
                        states.Update<POOrderStatus.pendingPrint>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow3));
                                });
                        });
                        states.Update<POOrderStatus.pendingEmail>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow3));
                                });
                        });
                        states.Update<POOrderStatus.open>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow3));
                                });
                        });
                        states.Update<POOrderStatus.completed>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow3));
                                });
                        });
                        states.Update<POOrderStatus.cancelled>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow3));
                                });
                        });
                        states.Update<POOrderStatus.closed>(state =>
                        {
                            return state
                                .WithActions(actions =>
                                {
                                    actions.Add(nameof(ActionOpenFilesWindow3));
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
