\# cGeoIP - Simple GeoIP Library for .NET

A lightweight .NET library that resolves IP addresses to countries and displays corresponding flags.

\## Features

\- \*\*IP to Country Lookup\*\* - Convert any IP to country name and code

\- \*\*Flag Images\*\* - Includes 248 country flags with clean padding

\- \*\*Local Network Detection\*\* - Automatically detects localhost and private IPs

\- \*\*Single DLL\*\* - No external dependencies, thanks to Costura.Fody

\- \*\*.NET 4.8\*\* - Compatible with older projects



\## Usage



```csharp

use cGeoIP
var geo = new cGeoMain();

string ipInfo = geo.GetIpInf("8.8.8.8");
// Returns: "8.8.8.8:United States:US"



string ipInfo = geo.GetIpInf("192.168.1.1");
// Returns: "192.168.1.1:LocalNetwork:XY"

// Use with ListView
listView1.SmallImageList = geo.cImageList;
listView1.LargeImageList = geo.cImageList;

```


\## Database

Includes DB-IP Lite country database (dbip-country-lite-2025-11.mmdb). (can be updated)
Source: https://db-ip.com/db/download/ip-to-country-lite

Note: GeoLite2 Country .mmdb database works too, just a little larger on size

