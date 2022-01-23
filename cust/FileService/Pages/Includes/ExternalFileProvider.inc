<px:PXSmartPanel runat="server" CreateOnDemand="True" Key="ExternalFiles" AutoRepaint="True" ID="pnlExternalFiles" Caption="Caption" CaptionVisible="True" AutoReload="True" LoadOnDemand="True" Width="50%">
    <px:PXGrid ID="frmExternalFiles" runat="server" DataSourceID="ds" Width="100%" Height="100%" SkinID="Inquire">
        <Levels>
            <px:PXGridLevel DataMember="ExternalFiles">
                <Columns>
                    <px:PXGridColumn DataField="FileName" LinkCommand="ActionRedirectToFile" />
                    <px:PXGridColumn DataField="Path" />
                </Columns>
            </px:PXGridLevel>
        </Levels>
        <AutoSize Enabled="True" Container="Parent"/>
        <ActionBar>
            <CustomItems>
                <px:PXToolBarButton>
                    <AutoCallBack Command="ActionUploadFile" Target="ds"/>
                </px:PXToolBarButton>
                <px:PXToolBarButton>
                    <AutoCallBack Command="ActionDownloadFile" Target="ds"/>
                </px:PXToolBarButton>
            </CustomItems>
        </ActionBar>
    </px:PXGrid>
    <px:PXPanel ID="pnlExternalFilesBtn" runat="server" SkinID="Buttons">
        <px:PXButton ID="btnOKExternalFiles" runat="server" DialogResult="OK" Text="OK">
            <AutoCallBack Target="frmExternalFiles" Command="Save"/>
        </px:PXButton>
        <px:PXButton ID="btnCancelExternalFiles" runat="server" DialogResult="Cancel" Text="Cancel"/>
    </px:PXPanel>
</px:PXSmartPanel>

<px:PXUploadDialog ID="pnlNewRev" runat="server" Height="120px" Style="position: static" Width="560px" Caption="File Upload" Key="FileServicePreferences" SessionKey="LekkerExternalFileProvider" AutoSaveFile="false" RenderCheckIn="false" />