#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

using PX.Data.WorkflowAPI;
using PX.Objects.PO;

namespace FileService.Acu.Implementation
{
    public class POEntryFileExtension : FileGraphExtension<POOrderEntry, POOrder, POOrder.noteID>
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
}
