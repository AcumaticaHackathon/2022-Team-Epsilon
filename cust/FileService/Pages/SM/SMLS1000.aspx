<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="SMLS1000.aspx.cs" Inherits="Page_SMLS1000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FileService.Acu.FileServicesPreferencesMaint"
        PrimaryView="Preferences"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Preferences" Width="100%" Height="100px" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"/>
            <px:PXSelector runat="server" ID="slPluginType" DataField="PluginType"/>
        </Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXTab runat="server" Width="100%">
		<Items>
            <px:PXTabItem Text="Mappping">
				<Template>
                    <px:PXGrid ID="gridMapping" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Inquire" AllowAutoHide="false">
                        <Levels>
                            <px:PXGridLevel DataMember="Mappings">
                                <Columns>
                                    <px:PXGridColumn DataField="Active" Type="CheckBox" TextAlign="Center"/>
                                    <px:PXGridColumn DataField="Entity"/>
                                    <px:PXGridColumn DataField="Mapping"/>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                        <ActionBar >
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
            <px:PXTabItem Text="Settings">
                <Template>
                    <px:PXGrid ID="gridSettings" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Inquire" AllowAutoHide="false">
                        <Levels>
                            <px:PXGridLevel DataMember="Mappings">
                                <Columns>
                                    <px:PXGridColumn DataField="Active" Type="CheckBox" TextAlign="Center"/>
                                    <px:PXGridColumn DataField="Entity"/>
                                    <px:PXGridColumn DataField="Mapping"/>
                                </Columns>
                            </px:PXGridLevel>
                        </Levels>
                        <AutoSize Container="Window" Enabled="True" MinHeight="150" />
                        <ActionBar >
                        </ActionBar>
                    </px:PXGrid>
                </Template>
            </px:PXTabItem>
        </Items>
        <AutoSize Enabled="True" Container="Window"/>
    </px:PXTab>
</asp:Content>

