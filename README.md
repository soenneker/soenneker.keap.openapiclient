[![](https://img.shields.io/nuget/v/soenneker.keap.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.keap.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.keap.openapiclient/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.keap.openapiclient/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.keap.openapiclient/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.keap.openapiclient/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.keap.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.keap.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.keap.openapiclient/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.keap.openapiclient/actions/workflows/codeql.yml)

# Soenneker.Keap.OpenApiClient

A Kiota-generated client and model set for the Keap REST API.

## Install

```bash
dotnet add package Soenneker.Keap.OpenApiClient
```

## Create a client

```csharp
using System.Net.Http.Headers;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Keap.OpenApiClient;

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", accessToken);

var adapter = new HttpClientRequestAdapter(
    new AnonymousAuthenticationProvider(),
    httpClient: httpClient);

var client = new KeapOpenApiClient(adapter);
```

The generated client defaults to `https://api.infusionsoft.com/crm`. Set `adapter.BaseUrl` before constructing `KeapOpenApiClient` when targeting another compatible endpoint.

For dependency-injection registration, configuration-based authentication, and client caching, use [`Soenneker.Keap.OpenApiClientUtil`](https://www.nuget.org/packages/Soenneker.Keap.OpenApiClientUtil/) instead of constructing the Kiota adapter directly.

## List contacts

```csharp
using Soenneker.Keap.OpenApiClient.Models;

ListContactsResponse? page = await client.Rest.V2.Contacts.GetAsync(config =>
{
    config.QueryParameters.PageSize = 100;
    config.QueryParameters.Fields =
        "id,given_name,family_name,email_addresses";
}, cancellationToken);

foreach (Contact contact in page?.Contacts ?? [])
{
    Console.WriteLine($"{contact.Id}: {contact.GivenName} {contact.FamilyName}");
}

string? nextPageToken = page?.NextPageToken;
```

Request builders follow the API path: `client.Rest.V2.Contacts`, `client.Rest.V2.Companies`, `client.Rest.V2.Orders`, and so on. Item endpoints use indexers, for example `client.Rest.V2.Contacts[contactId]`.

Generated methods accept a request-configuration callback for query parameters and headers, followed by a cancellation token. Non-success API responses are surfaced through Kiota exceptions.

The source is generated from Keap's OpenAPI document. Extend behavior in separate partial files or a wrapper; edits to generated files can be replaced by the next client update.
