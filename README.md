# NancyAzureADB2C

For a while now I have wanted to rethink the way I handle user authentication for my APIs. I am very fond of [NancyFX](http://nancyfx.org/ "NancyFX Project Home") and I base my APIs on NancyFX if at all posible. 

Microsoft Azure provides a hosted Active Directory, that for the first 50.000 users is free in the B2C version. The fact that it is an Active Directory isn't really important. What is important is that they can handle secure storing of passwords, signup flows, two factor authentication and not least OAuth login.

I started looking for examples of how to do this using NancyFx on .NET Core 2.0 using only out of the box Microsoft.AspNetCore functionality and the latest build of NancyFx for .Net Core (2.0.0-clinteastwood at the time of writing this). But I found no examples out there. So I asked for help in the [Nancy Slack Channel](https://nancyfx.slack.com). [Kristian Hellang](https://github.com/khellang) was quick to jump in and help. With a few pointers and some sample code that he cooked up with a crying baby on his arm I was up and running in no time.

Since there is no example out there, I have decided to document my findings with a little git repo. This sample shows how to build a NancyFX web API with Azure AD B2C using the ASP.Net Core JWT Bearer middleware.

## Set the stage
To begin with I will just set the stage for what I am actually doing here.

I want to create a REST API, that can serve as a backend for a client side javascript app running in a browser. To solve that problem I will make use of the OAuth2 Implicit Grant flow.

This repository does not contain a client. It only contains the REST API. I will explain how to use [Postman](https://www.getpostman.com/) to test out the flow.

You will need an Azure account to get this sample to work. All you need is a free account, since all this can be done in the free tier. 

## Set up your Azure AD
I used this sample as inspiration for my sample: [Azure AD B2C .NET Core Web Api Sample](https://github.com/Azure-Samples/active-directory-b2c-dotnetcore-webapi).
Follow steps 2 through 5 to set up your Azure AD. For me to get this to work and actually authenticate successfully I also had to add a published scope. I just added one called read:org. I also created a client secret under the Keys tab of the application, you will need that when requesting a token.

## Configure your application
Once you have gone through the steps described above you need to copy the file 'Sample appsettings.json' to a file called 'appsettings.json' and fill in the values that are missing.

## Test with PostMan
Once those configuration values have been entered you can test out authentication using Postman.
Open a tab in postman and issue a GET request to the random numbers API: https://localhost:44382/numbers. The project is configured to use IISExpress and a self signed SSL certificate. 

When you try to request the random numbers you will be met by a 401 - Unauthorized response. That is expected since you haven't provided a valid authentication token. 

Press the Authorization tab in Postman and chose OAuth2 as the type. Chose to add the authorization data to "Request headers". Then on your right is a button "Get New Access Token". Press that and fill in the details that match the AD you created. You can find more information about your AD here: https://login.microsoftonline.com/tfp/[Tenant name].onmicrosoft.com/[Policy_Name]/v2.0/.well-known/openid-configuration

| Field | Value |
|-------|-------|
|Token Name | AzureADB2C_JWT_Token|
|Grant Type | Implicit|
|Callback URL | Not really important for this sample, but this needs to be registered on the application in your Azure AD (Step 4 in the guide I linked to). I have used https://localhost:44340/signin-oidc although it doesn't exist in the API.|
|Auth URL | https://login.microsoftonline.com/te/[Tenant name].onmicrosoft.com/[Policy_Name]/oauth2/v2.0/authorize|
|Client ID | The GUID of your application in Azure AD (Step 4 in the guide I linked to)|
|Client Secret | The key created under the Keys tab of the application in Azure.|
|Scope | https://[Tenant name].onmicrosoft.com/[Application name]/[Scope name]|
|State | Blank|
|Client authentication | "Send as Basic Auth Header"|

Press the "Request Token" button and you should be asked to log in to your AD. If you haven't already created a user, you will be able to do so here. Once you have successfully authentication you will be presented with a Bearer token. Press the "Use Token" button and try your request again. You should now see an array of random numbers returned.

## Thank you
A big thank you to Kristian Hellang for helping out when I got stuck!
