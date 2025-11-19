# GeoIpLite - Simple GeoIP Library for .NET

Lightweight .NET library that resolves IP addresses to countries and displays corresponding flags. Can be easily customised and updated with the latest **mmdb** databases and your own flag collection.

## Features

- Convert any IP to country name and code
- Includes 262 country flags in **png** format (can be updated)
- Detects localhost and local network IPs
- Single dll file, compiled with Costura.Fody
- Built with .Net 4.8

## Requirements

- .NET Framework 4.8 installed

## Usage

```csharp
// Compile the project and add the compiled GeoIpLite.dll file as reference in the project.

using GeoIpLite;
var geo = new GeoIpMain();

string ipInfo = geo.GetIpInf("123.456.78.910");
// Returns: "123.456.78.910:United States:US"

string ipInfo = geo.GetIpInf("192.168.1.1");
// Returns: "192.168.1.1:LocalNetwork:XY"

string ipInfo = geo.GetIpInf("127.0.0.1");
// Returns: "127.0.0.1:LocalHost:XY"

string[] infoList = ipInfo.Split(':'); 
var ipa = infoList[0];
var country = infoList[1];
var countryCode = infoList[2];

// How to get the flag Image

// For UI elements example
pictureBox1.Image = geo.flagImageList.Images[countryCode];;

// For ListView example
// Assign the ImageList to the ListView
listView1.SmallImageList = geo.flagImageList;
listView1.LargeImageList = geo.flagImageList;

// Add an item with the country name
var item = new ListViewItem(country);
listView1.Items.Add(item);

item.ImageKey = countryCode; // ListView will show the flag and the country name next

```

## Database

Includes DB-IP Lite country database (dbip-country-lite-2025-11.mmdb). (can be updated)

Database Source: https://db-ip.com/db/download/ip-to-country-lite

Note: GeoLite2 Country.mmdb database works too, just a little larger on size

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](/LICENSE)

This project is licensed under the MIT License - see the [LICENSE](/LICENSE) file for details
