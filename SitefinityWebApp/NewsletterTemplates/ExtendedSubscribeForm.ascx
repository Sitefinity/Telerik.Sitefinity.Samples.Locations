<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExtendedSubscribeForm.ascx.cs" Inherits="NewsletterTemplates_ExtendedSubscribeForm" %>
<%@ Register TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>


<fieldset id="formFieldset" runat="server" class="sfnewsletterForm sfSubscribe">
    <sitefinity:SitefinityLabel id="widgetTitle" runat="server" WrapperTagName="h2" HideIfNoText="true" CssClass="sfnewsletterTitle" />
    <sitefinity:SitefinityLabel id="widgetDescription" runat="server" WrapperTagName="p" HideIfNoText="true" CssClass="sfnewsletterDescription" />
    <sitefinity:Message ID="messageControl" runat="server" FadeDuration="3000" />
    <ol class="sfnewsletterFieldsList">
        <li class="sfnewsletterField">
            <asp:Label ID="emailAddressLabel" runat="server" Text='<%$Resources:NewslettersResources, EmailAddress %>' AssociatedControlID="emailAddress" CssClass="sfTxtLbl" />
            <asp:TextBox ID="emailAddress" runat="server" CssClass="sfTxt" />
            <asp:RequiredFieldValidator ID="emailValidator" runat="server" ControlToValidate="emailAddress" ValidationGroup="subscribeForm" CssClass="sfErrorWrp" Display="Dynamic">
                <strong class="sfError"><asp:Literal runat="server" ID="lEmailIsRequired" Text='<%$Resources:NewslettersResources, EmailIsRequired %>' /></strong>
            </asp:RequiredFieldValidator>
        </li>
        <li class="sfnewsletterField">
            <asp:Label ID="firstNameLabel" runat="server" Text='<%$Resources:NewslettersResources, FirstNamePublicForm %>' AssociatedControlID="firstName" CssClass="sfTxtLbl" />
            <asp:TextBox ID="firstName" runat="server" CssClass="sfTxt" />
        </li>
        <li class="sfnewsletterField">
            <asp:Label ID="lastNameLabel" runat="server" Text='<%$Resources:NewslettersResources, LastNamePublicForm %>' AssociatedControlID="lastName" CssClass="sfTxtLbl" />
            <asp:TextBox ID="lastName" runat="server" CssClass="sfTxt" />
        </li>
        <li class="sfnewsletterField">
            <asp:Label ID="isCompanyLabel" runat="server" Text="Is company?" AssociatedControlID="isCompany" CssClass="sfTxtLbl" />
            <asp:CheckBox ID="isCompany" runat="server" />
        </li>
        <li class="sfnewsletterField">
            <asp:Label ID="companyNameLabel" runat="server" Text="Company name" AssociatedControlID="companyName" CssClass="sfTxtLbl" />
            <asp:TextBox ID="companyName" runat="server" CssClass="sfTxt" />
        </li>
    </ol>
    <div class="sfnewsletterSubmitBtnWrp">
        <!-- obsolete subscribe button -->
        <asp:Button ID="subscribeButton" 
                    runat="server" 
                    Text='<%$Resources:NewslettersResources, SubscribeToList %>' 
                    ValidationGroup="subscribeForm" 
                    CssClass="sfnewsletterSubmitBtn"
                    Visible="false" />
        <!-- end obsolete subscribe button -->
        <asp:Button ID="newSubscribeButton" 
                    runat="server" 
                    Text='<%$Resources:NewslettersResources, SubscribeToList %>' 
                    ValidationGroup="subscribeForm" 
                    CssClass="sfnewsletterSubmitBtn" />
    </div>
</fieldset>

<asp:Panel ID="selectListInstructionPanel" runat="server">
    <asp:Literal ID="pleaseSelectList" runat="server" Text='<%$Resources:NewslettersResources, ClickEditAndSelectList %>' />
</asp:Panel>