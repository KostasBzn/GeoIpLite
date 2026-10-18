# GeoIpLite - Simple GeoIP Library for .NET

I created this project because I could find something similar to resolve a simple IPv4 into flag and country. I know it does not support IPv6 and it might has some bugs for extreme cases but it works for my needs. It can be easily customised and updated with the latest **mmdb** databases and your own flag collection.

## Features

- Convert any IP to country name and code
- Includes 262 country flags in **png** format (can be updated)
- Detects localhost and local network IPs
- Single dll file, compiled with Costura.Fody
- SDK-style project, built with .NET Framework 4.8 but you can target modern .NET maybe with some changes in the code

## Requirements

- Windows (uses WinForms and System.Drawing)
- .NET Framework 4.8 installed

## Usage

```csharp
// Compile the project and add the compiled GeoIpLite.dll file as reference in the project.

using GeoIpLite;
var geo = new GeoIpResolver();

string ipInfo = geo.GetIpInf("8.8.8.8");
// Returns: "8.8.8.8:United States:US"

string ipInfo2 = geo.GetIpInf("192.168.1.1");
// Returns: "192.168.1.1:LocalNetwork:XY"

string ipInfo3 = geo.GetIpInf("127.0.0.1");
// Returns: "127.0.0.1:LocalHost:XY"

string[] infoList = ipInfo.Split(':');
var ipa = infoList[0];
var country = infoList[1];
var countryCode = infoList[2];

// How to get the flag Image

// For UI elements example
pictureBox1.Image = geo.FlagImageList.Images[countryCode];

// For ListView example
// Assign the ImageList to the ListView
listView1.SmallImageList = geo.FlagImageList;
listView1.LargeImageList = geo.FlagImageList;

// Add an item with the country name
var item = new ListViewItem(country);
listView1.Items.Add(item);

item.ImageKey = countryCode; // ListView will show the flag and the country name next

// GeoIpResolver is IDisposable, dispose it when you are done. For example when the form closes
// Dont dispose it before that because the flags will disappear because the ImageList gets disposed too
geo.Dispose();
```

## Database

Includes DB-IP Lite country database (dbip-country-lite-2025-11.mmdb), embedded in the project resources. It can be updated with the latest

To update it, replace the mmdb file in the project resources.

Database Source: https://db-ip.com/db/download/ip-to-country-lite

Note: GeoLite2 Country.mmdb database works too, just a little larger on size

## Flags

Flags are png files in the project resources, named by country code like US DE ect. `XY` is a generic grey flag I used when there is no flag available eg localhost localNetwork or unknown

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](/LICENSE)

This project is licensed under the MIT License - see the [LICENSE](/LICENSE) file for details
