# CobaltSitefinityMembershipProvider
A library to enable SSO between Cobalt CRM Portals and Sitefinity CMS



Below are the steps to configure Sitefinity / Cobalt Portal Components Single Signon

The steps below contain several variables that will need to be exchanged between the Sitefinity Vendor and Cobalt. These values will be replaced in the steps below with the value provided.
1.	[Cobalt Portal Url] – To be provided by Cobalt.
2.	[Administrator Role] – To be provided by Cobalt.
3.	[Sitefinity Site Url] – To be provided by Sitefinity vendor.
4.	[Sitefinity Security Token] - To be provided by Sitefinity vendor.

Steps to be performed by Cobalt
1.	Configure API Service Configuration in CRM
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

Steps to be performed by Sitefinity Vendor
1.	Build / Copy the CobaltSitefinityMembershipProvider.dll
	a.	Download Provider project from GitHub (https://github.com/TheCRMLab/CobaltSitefinityMembershipProvider)
	b.	For whatever version of Sitefinity being targeted update the dlls in the SitefinityReferences folder under the CobaltSitefinityMembershipProvider project. Note: The included references are for Sitefinity 6.1.
	c.	Build the project to ensure the correct Sitefinity libraries are referenced.
	d.	Copy the CobaltSitefinityMembershipProvider.dll and RestSharp.dll to the \bin directory of the sitefinity site. 
2.	Open the web.config in the root of the SF site
	a.	Inside the <appSettings> node add the following key / value nodes
		i.	<add key="CobaltApiUrl" value="[The endpoint url is the root of the API site configured for the CRM org to be supplied by Cobalt (e.g. http://warealtorapi.ramcotest.com/)]" />
		ii.	<add key="CobaltApiKey" value="[The security key is the encrypted value from step 1e above to be supplied by Cobalt. (e.g. AaAaAAaAAAAAAAaAAA==)]" />
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
        <wsFederation passiveRedirectEnabled="true" issuer="[Cobalt Portal Url]/Authentication/sts.ashx" realm="[Sitefinity Site Url]" requireHttps="true" />
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

3.	Open the Security.config in the App_Data\Sitefinity\Configuration directory
a.	Find the following node

<securityConfig xmlns:config="urn:telerik:sitefinity:configuration" xmlns:type="urn:telerik:sitefinity:configuration:type" config:version="6.1.4700.0" authCookieName=".SFAUTH-sfsandbox.websiteurl.com" rolesCookieName=".SFROLES-sfsandbox.ramcotest.com" validationKey="[Variable]" decryptionKey="[Variable]"/>

b.	Add the defaultBackedRoleProvider and defaultBackendMembershipProvider e.g.:
<securityConfig xmlns:config="urn:telerik:sitefinity:configuration" xmlns:type="urn:telerik:sitefinity:configuration:type" config:version="6.1.4700.0" authCookieName="[Variable]" rolesCookieName=".SFROLES-sfsandbox.websiteurl.com" validationKey="[Variable]" decryptionKey="[Variable]" defaultBackendRoleProvider="Cobalt" defaultBackendMembershipProvider="Cobalt"/>

c.	Replace the following

	<securityTokenIssuers>
		<add key="[Sitefinity Security Token]" encoding="Hexadecimal" membershipProvider="Default" realm="http://localhost" />
	</securityTokenIssuers>
	<relyingParties>
		<add key="[Sitefinity Security Token]" encoding="Hexadecimal" realm="http://localhost" />
	</relyingParties>
d.	With the following:

	<securityTokenIssuers>
		<add key="***************" encoding="Hexadecimal" 
membershipProvider="Cobalt" realm="[Cobalt Portal Url]/Authentication/sts.ashx" />
	</securityTokenIssuers>
	<relyingParties>
		<add key="********************" encoding="Hexadecimal" 
realm="[Sitefinity Site Url]" />
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
		<role roleProvider="Cobalt" roleName="[Administrator Role]" />
	</administrativeRoles>
g.	Save and close the Security.config
4.	Go to [Sitefinity Site Url]/Sitefinity (e.g. http://sitefinity.cobalt.net /Sitefinity)
a.	Verify you get the portal login prompt.

 
b.	Verify you can login as a user in the Content Administrators role specified in 4f to be supplied by Cobalt and you are redirected to the Sitefinity backend.

