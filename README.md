# cGeoIP - Simple GeoIP Library for .NET

Lightweight .NET library that resolves IP addresses to countries and displays corresponding flags.

## Features

- Convert any IP to country name and code
- Includes 262 country flags in **png** format (can be updated)
- Detects localhost and local network IPs
- Single dll file, compiled with Costura.Fody
- Built with .Net 4.8

## Usage

```csharp
using GeoIpLite;
var geo = new GeoIpMain();

string ipInfo = geo.GetIpInf("123.456.78.910");
// Returns: "123.456.78.910:United States:US"

string ipInfo = geo.GetIpInf("192.168.1.1");
// Returns: "192.168.1.1:LocalNetwork:XY"

string ipInfo = geo.GetIpInf("127.0.0.1");
// Returns: "127.0.0.1:LocalHost:XY"

// Use with ListView
listView1.SmallImageList = geo.flagImageList;
listView1.LargeImageList = geo.flagImageList;

```

## Database

Includes DB-IP Lite country database (dbip-country-lite-2025-11.mmdb). (can be updated)
Source: https://db-ip.com/db/download/ip-to-country-lite

Note: GeoLite2 Country .mmdb database works too, just a little larger on size

