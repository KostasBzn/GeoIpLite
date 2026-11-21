# GeoIpLite - Simple GeoIP Library for .NET

I created this project because I couldn't find something similar to resolve a simple IPv4 into flag and country. I updated my old version and now handles more cases and supports IPv6. It can be easily customised and updated with the latest **mmdb** databases and your own flag collection, or target different Framework.

## Features

- Convert any IP to country name and code
- Includes 262 country flags in **png** format (can be updated)
- Detects localhost and local network IPs
- Single dll file, compiled with Costura.Fody
- SDK-style project, built with .NET Framework 4.8 but you can target modern .NET maybe with some changes in the code

## Requirements

- Windows (uses WinForms and System.Drawing)
- .NET Framework 4.8 installed (or the Framework you are going to target)

## Usage

```csharp
// Compile the project and add the compiled GeoIpLite.dll file as reference in the project.

using GeoIpLite;

var geo = new GeoIpResolver();

var (country, countryCode) = geo.GetIpInf("8.8.8.8");
// Result: country = "United States", countryCode = "US"

var (country, countryCode) = geo.GetIpInf("192.168.1.1");
// Result: country = "LocalNetwork", countryCode = "XY"

var (country, countryCode) = geo.GetIpInf("127.0.0.1");
// Result: country = "LocalHost", countryCode = "XY"

// How to get the flag Image

// For UI elements example
pictureBox1.Image = geo.FlagImageList.Images[countryCode];

// For ListView example
// Assign the ImageList to the ListView
listView1.SmallImageList = geo.FlagImageList; // small 
listView1.LargeImageList = geo.FlagImageList; // or large 

// Add an item with the country name
var item = new ListViewItem(country);
item.ImageKey = countryCode; // ListView will show the flag and the country name next
listView1.Items.Add(item);

// GeoIpResolver is IDisposable, dispose it when you are done. For example when the form closes
// Dont dispose it before that because the flags will disappear because the ImageList gets disposed too
geo.Dispose();
```

## Database

Includes DB-IP Lite country database (dbip-country-lite-2026-11.mmdb), embedded in the project resources. It can be updated with the latest one. DB-IP updates it monthly.

To update it, replace the mmdb file in the project resources.

Database Source: https://db-ip.com/db/download/ip-to-country-lite

Note: GeoLite2 Country.mmdb database works too, just a little larger on size

## Flags

Flags are png files in the project resources, named by country code like US DE ect. `XY` is a generic grey flag I used when there is no flag available eg localhost localNetwork or unknown

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](/LICENSE)

This project is licensed under the MIT License - see the [LICENSE](/LICENSE) file for details
