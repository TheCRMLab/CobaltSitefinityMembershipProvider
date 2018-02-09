Sitefinity / Cobalt Portal Components Single Signon

The steps below contain several variables that will need to be exchanged between
the Sitefinity Vendor and Cobalt. These values will be replaced in the steps
below with the value provided.

1.  [Cobalt Portal Url] – To be provided by Cobalt.

2.  [Administrator Role] – To be provided by Cobalt.

3.  [Sitefinity Site Url] – To be provided by Sitefinity vendor.

4.  [Sitefinity Security Token] - To be provided by Sitefinity vendor.

Steps to be performed by Cobalt

1.  Configure API Service Configuration in CRM

    1.  Open the default settings record Settings -\> Settings (Cobalt Admin -\>
        Settings \> 2011)

    2.  Click Integration Services in the Left hand navigation (Integration
        Services in Ribbon \> 2011)

    3.  Add a new Integration Service

        1.  Assembly Name = Cobalt.Components.Crm.Integration

        2.  Class Name = Cobalt.Components.Crm.Integration.ApiService

        3.  Click Save

    4.  Add a new Property

        1.  Name = SecurityKey

        2.  Type = System.String

        3.  Value = [A secure key 10 – 12 characters]

        4.  Encrypt Value = True

        5.  Click Save

    5.  Now copy the Value after it’s been encrypted -This is the value that
        will be shared with the Sitefinity integrator and used in step 3 to
        authenticate to the API

Steps to be performed by Sitefinity Vendor

1.  Build / Copy the CobaltSitefinityMembershipProvider.dll

    1.  Download Provider project from GitHub
        (https://github.com/TheCRMLab/CobaltSitefinityMembershipProvider)

    2.  For whatever version of Sitefinity being targeted update the dlls in the
        SitefinityReferences folder under the CobaltSitefinityMembershipProvider
        project. Note: The included references are for Sitefinity 6.1.

    3.  Build the project to ensure the correct Sitefinity libraries are
        referenced.

    4.  Copy the CobaltSitefinityMembershipProvider.dll and RestSharp.dll to the
        \\bin directory of the sitefinity site.

2.  Open the web.config in the root of the SF site

    1.  Inside the \<appSettings\> node add the following key / value nodes

        1.  \<add key="CobaltApiUrl" value="[The endpoint url is the root of the
            API site configured for the CRM org to be supplied by Cobalt (e.g.
            <http://warealtorapi.ramcotest.com/>)]" /\>

        2.  \<add key="CobaltApiKey" value="[The security key is the encrypted
            value from step 1e above to be supplied by Cobalt. (e.g.
            AaAaAAaAAAAAAAaAAA==)]" /\>

    2.  Replace the following:

>   \<roleManager enabled="false" /\>

>   \<membership defaultProvider="Default"\>

>   \<providers\>

>   \<clear /\>

>   \<add name="Default"
>   type="Telerik.Sitefinity.Security.Data.SitefinityMembershipProvider,
>   Telerik.Sitefinity" /\>

>   \</providers\>

>   \</membership\>

>   With:

>   \<roleManager enabled="true" defaultProvider="Cobalt"\>

>   \<providers\>

>   \<clear /\>

>   \<add name="Cobalt" type="CobaltSitefinityMembershipProvider.RoleProvider"
>   /\>

>   \</providers\>

>   \</roleManager\>

>   \<membership defaultProvider="Cobalt"\>

>   \<providers\>

>   \<clear /\>

>   \<add name="Cobalt"
>   type="CobaltSitefinityMembershipProvider.MembershipProvider" /\>

>   \</providers\>

>   \</membership\>

1.  Replace the following

>   \<federatedAuthentication\>

>   \<wsFederation passiveRedirectEnabled="true" issuer="http://localhost"
>   realm="http://localhost" requireHttps="false" /\>

>   \<cookieHandler requireSsl="false" /\>

>   \</federatedAuthentication\>

>   With:

>   \<federatedAuthentication\>

>   \<wsFederation passiveRedirectEnabled="true" issuer="[Cobalt Portal
>   Url]/Authentication/sts.ashx" realm="[Sitefinity Site Url]"
>   requireHttps="true" /\>

>   \<cookieHandler requireSsl="true" /\>

>   \</federatedAuthentication\>

1.  Add the following after the \</security\> closing tag inside the
    system.webServer node

>   \<rewrite\>

>   \<rules\>

>   \<rule name="Sitefinity STS Signout" stopProcessing="true"\>

>   \<match url="\^sitefinity/signout\$" /\>

>   \<conditions logicalGrouping="MatchAll" trackAllCaptures="false"\>

>   \<add input="{QUERY_STRING}" pattern="sts_signout=true" negate="true" /\>

>   \</conditions\>

>   \<action type="Redirect" url="/Sitefinity/Signout?sts_signout=true"
>   appendQueryString="true" redirectType="Temporary" /\>

>   \</rule\>

>   \</rules\>

>   \</rewrite\>

1.  Save and close the web.config

2.  Open the Security.config in the App_Data\\Sitefinity\\Configuration
    directory

    1.  Find the following node

>   \<securityConfig xmlns:config="urn:telerik:sitefinity:configuration"
>   xmlns:type="urn:telerik:sitefinity:configuration:type"
>   config:version="6.1.4700.0"
>   authCookieName=".SFAUTH-sfsandbox.websiteurl.com"
>   rolesCookieName=".SFROLES-sfsandbox.ramcotest.com"
>   validationKey="[Variable]" decryptionKey="[Variable]"\>

1.  Add the defaultBackedRoleProvider and defaultBackendMembershipProvider e.g.:

>   \<securityConfig xmlns:config="urn:telerik:sitefinity:configuration"
>   xmlns:type="urn:telerik:sitefinity:configuration:type"
>   config:version="6.1.4700.0" authCookieName="[Variable]"
>   rolesCookieName=".SFROLES-sfsandbox.websiteurl.com"
>   validationKey="[Variable]" decryptionKey="[Variable]"
>   **defaultBackendRoleProvider="Cobalt"
>   defaultBackendMembershipProvider="Cobalt"**\>

1.  Replace the following

>   \<securityTokenIssuers\>

>   \<add key="[Sitefinity Security Token]" encoding="Hexadecimal"
>   membershipProvider="Default" realm="http://localhost" /\>

>   \</securityTokenIssuers\>

>   \<relyingParties\>

>   \<add key="[Sitefinity Security Token]" encoding="Hexadecimal"
>   realm="http://localhost" /\>

>   \</relyingParties\>

1.  With the following:

>   \<securityTokenIssuers\>

>   \<add key="\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*" encoding="Hexadecimal"

>   membershipProvider="Cobalt" realm="[Cobalt Portal
>   Url]/Authentication/sts.ashx" /\>

>   \</securityTokenIssuers\>

>   \<relyingParties\>

>   \<add key="\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*" encoding="Hexadecimal"

>   realm="[Sitefinity Site Url]" /\>

>   \</relyingParties\>

1.  Replace the following

>   \<membershipProviders\>

>   \<config:link name="OpenAccessMembership37Provider"
>   path="migrationModuleConfig/membershipProviders/OpenAccessMembership37Provider"
>   module="Migration" /\>

>   \</membershipProviders\>

1.  With the following (replace Content Administrators with the administrator
    role configured in CRM to be supplied by Cobalt):

>   \<roleProviders\>

>   \<add title="Cobalt" description="Cobalt"
>   type="CobaltSitefinityMembershipProvider.SitefinityRoleDataProvider,
>   CobaltSitefinityMembershipProvider" enabled="True" name="Cobalt" /\>

>   \</roleProviders\>

>   \<membershipProviders\>

>   \<config:link name="OpenAccessMembership37Provider"
>   path="migrationModuleConfig/membershipProviders/OpenAccessMembership37Provider"
>   module="Migration" /\>

>   \<add title="Cobalt" description="Cobalt"
>   type="CobaltSitefinityMembershipProvider.SitefinityMembershipDataProvider,
>   CobaltSitefinityMembershipProvider" enabled="True" name="Cobalt" /\>

>   \</membershipProviders\>

>   \<administrativeRoles\>

>   \<role roleProvider="Cobalt" roleName="[Administrator Role]" /\>

>   \</administrativeRoles\>

1.  Save and close the Security.config

2.  Go to [Sitefinity Site Url]/Sitefinity (e.g. )

    1.  Verify you get the portal login prompt.

![](media/8c16717e98c1ad6f55f807614682c4ef.png)

1.  Verify you can login as a user in the Content Administrators role specified
    in 4f to be supplied by Cobalt and you are redirected to the Sitefinity
    backend.
