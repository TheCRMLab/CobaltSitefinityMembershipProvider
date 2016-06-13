# CobaltSitefinityMembershipProvider
A library to enable SSO between Cobalt CRM Portals and Sitefinity CMS
Cobalt CRM Portals and Sitefinity SSO
1.	Configure API Service Configuration in CRM – Cobalt
a.	Open the default settings record Settings -> Settings (Cobalt Admin -> Settings > 2011)
b.	Click Integration Services in the Left hand navigation (Integration Services in Ribbon > 2011)
c.	Add a new Integration Service
i.	Assembly Name = Cobalt.Components.Crm.Integration
ii.	Class Name = Cobalt.Components.Crm.Integration.ApiService
iii.	Click Save
d.	Add a new Property
i.	Name = SecurityKey
ii.	Type = System.String
iii.	Value = [A secure key 10 – 12 characters]
iv.	Encrypt Value = True
v.	Click Save
e.	Now copy the Value after it’s been encrypted -This is the value that will be shared with the Sitefinity integrator and used in step 3 to authenticate to the API
2.	Build / Copy the CobaltSitefinityMembershipProvider.dll – Sitefinity Vendor
a.	For whatever version of Sitefinity being targeted update the dlls in the SitefinityReferences folder under the CobaltSitefinityMembershipProvider project. Note: The included references are for Sitefinity 6.1.
b.	Build the project to ensure the correct Sitefinity libraries are referenced.
c.	Copy the CobaltSitefinityMembershipProvider.dll and RestSharp.dll to the \bin directory of the sitefinity site. 
3.	Open the web.config in the root of the SF site – Sitefinity Vendor
a.	Inside the <appSettings> node add the following key / value nodes
i.	<add key="CobaltApiUrl" value="[The endpoint url is the root of the API site configured for the CRM org to be supplied by Cobalt (e.g. http://warealtorapi.ramcotest.com/)]" />
ii.	<add key="CobaltApiKey" value="[The security key is the encrypted value from step 1e above to be supplied by Cobalt. (e.g. 1YI7czOOd20rP1JK93VOdg==)]" />
b.	Replace the following:
      <roleManager enabled="false" />  
    <membership defaultProvider="Default"> 
      <providers>
        <clear /> 
        <add name="Default" type="Telerik.Sitefinity.Security.Data.SitefinityMembershipProvider, Telerik.Sitefinity" />
      </providers>
    </membership>
With:
  <roleManager enabled="true" defaultProvider="Cobalt">
      <providers>
        <clear />
        <add name="Cobalt" type="CobaltSitefinityMembershipProvider.RoleProvider" />
      </providers>
    </roleManager>

    <membership defaultProvider="Cobalt">
      <providers>
        <clear />
        <add name="Cobalt" type="CobaltSitefinityMembershipProvider.MembershipProvider" />
      </providers>
    </membership>

c.	Replace the following
           <federatedAuthentication>
        <wsFederation passiveRedirectEnabled="true" issuer="http://localhost" realm="http://localhost" requireHttps="false" />
        <cookieHandler requireSsl="false" />
      </federatedAuthentication>

With:
<federatedAuthentication>
        <wsFederation passiveRedirectEnabled="true" issuer="https://warealtorportal.ramcotest.com/Authentication/sts.ashx" realm="https://warealtor.ramcotest.com" requireHttps="true" />
        <cookieHandler requireSsl="true" />
      </federatedAuthentication>

d.	Add the following after the </security> closing tag inside the system.webServer node
    <rewrite>
      <rules>
        <rule name="Sitefinity STS Signout" stopProcessing="true">
            <match url="^sitefinity/signout$" />
            <conditions logicalGrouping="MatchAll" trackAllCaptures="false">
                <add input="{QUERY_STRING}" pattern="sts_signout=true" negate="true" />
            </conditions>
            <action type="Redirect" url="/Sitefinity/Signout?sts_signout=true" appendQueryString="true" redirectType="Temporary" />
        </rule>
      </rules>
    </rewrite>
e.	Save and close the web.config

4.	Open the Security.config in the App_Data\Sitefinity\Configuration directory
a.	Find the following node
<securityConfig xmlns:config="urn:telerik:sitefinity:configuration" xmlns:type="urn:telerik:sitefinity:configuration:type" config:version="6.1.4700.0" authCookieName=".SFAUTH-sfsandbox.ramcotest.com" rolesCookieName=".SFROLES-sfsandbox.ramcotest.com" validationKey="[Variable]" decryptionKey="[Variable]">
b.	Add the defaultBackedRoleProvider and defaultBackendMembershipProvider e.g.:
<securityConfig xmlns:config="urn:telerik:sitefinity:configuration" xmlns:type="urn:telerik:sitefinity:configuration:type" config:version="6.1.4700.0" authCookieName="[Variable]" rolesCookieName=".SFROLES-sfsandbox.ramcotest.com" validationKey="[Variable]" decryptionKey="[Variable]" defaultBackendRoleProvider="Cobalt" defaultBackendMembershipProvider="Cobalt">

c.	Replace the following

	<securityTokenIssuers>
		<add key="EE0FF302FE1A91B4E1AB025B726EBAD7589F4F0F8C54A3A94D7A20B0F7D22E50" encoding="Hexadecimal" membershipProvider="Default" realm="http://localhost" />
	</securityTokenIssuers>
	<relyingParties>
		<add key="EE0FF302FE1A91B4E1AB025B726EBAD7589F4F0F8C54A3A94D7A20B0F7D22E50" encoding="Hexadecimal" realm="http://localhost" />
	</relyingParties>
d.	With the following (replace warealtorportal.ramcotest.com with portal url to be supplied by Cobalt):

	<securityTokenIssuers>
		<add key="CD29559E6EDC312272976AC43F7E921C5766D7063DAF6D177F3EEDEB1802FABE" encoding="Hexadecimal" 
membershipProvider="Cobalt" realm="https://warealtorportal.ramcotest.com/Authentication/sts.ashx" />
	</securityTokenIssuers>
	<relyingParties>
		<add key="CD29559E6EDC312272976AC43F7E921C5766D7063DAF6D177F3EEDEB1802FABE" encoding="Hexadecimal" 
realm="https://warealtor.ramcotest.com" />
	</relyingParties>


e.	Replace the following
	<membershipProviders>
		<config:link name="OpenAccessMembership37Provider" path="migrationModuleConfig/membershipProviders/OpenAccessMembership37Provider" module="Migration" />
	</membershipProviders>
f.	With the following (replace Content Administrators with the administrator role configured in CRM to be supplied by Cobalt):
		<roleProviders>
		<add title="Cobalt" description="Cobalt" type="CobaltSitefinityMembershipProvider.SitefinityRoleDataProvider, CobaltSitefinityMembershipProvider" enabled="True" name="Cobalt" />
	</roleProviders>
	<membershipProviders>
		<config:link name="OpenAccessMembership37Provider" path="migrationModuleConfig/membershipProviders/OpenAccessMembership37Provider" module="Migration" />
		<add title="Cobalt" description="Cobalt" type="CobaltSitefinityMembershipProvider.SitefinityMembershipDataProvider, CobaltSitefinityMembershipProvider" enabled="True" name="Cobalt" />
	</membershipProviders>
	<administrativeRoles>
		<role roleProvider="Cobalt" roleName="Content Administrators" />
	</administrativeRoles>
g.	Save and close the Security.config
5.	Go to [site url]/Sitefinity (e.g. http://warealtor.ramcotest.com/Sitefinity)
a.	Verify you get the portal login prompt.

 
b.	Verify you can login as a user in the Content Administrators role specified in 4f to be supplied by Cobalt and you are redirected to the Sitefinity backend.
