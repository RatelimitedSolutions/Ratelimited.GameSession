﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do one of these:
//
//    using QuickType;
//
//    var listAllSshKeys = ListAllSshKeys.FromJson(jsonString);
//    var listAllDroplets = ListAllDroplets.FromJson(jsonString);
//    var createNewDroplet = CreateNewDroplet.FromJson(jsonString);
//    var getASingleDroplet = GetASingleDroplet.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// list all ssh keys
    ///
    /// GET {{baseUri}}/account/keys
    /// </summary>
    public partial class ListAllSshKeys
    {
        [JsonProperty("ssh_keys")]
        public List<SshKey> SshKeys { get; set; }

        [JsonProperty("links")]
        public ListAllSshKeysLinks Links { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

    public partial class ListAllSshKeysLinks
    {
    }

    public partial class Meta
    {
        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class SshKey
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// list all droplets
    ///
    /// GET {{baseUri}}/droplets
    /// </summary>
    public partial class ListAllDroplets
    {
        [JsonProperty("droplets")]
        public List<Droplet> Droplets { get; set; }

        [JsonProperty("links")]
        public ListAllSshKeysLinks Links { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

    public partial class Droplet
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("memory")]
        public long Memory { get; set; }

        [JsonProperty("vcpus")]
        public long Vcpus { get; set; }

        [JsonProperty("disk")]
        public long Disk { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("kernel")]
        public object Kernel { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("features")]
        public List<string> Features { get; set; }

        [JsonProperty("backup_ids")]
        public List<object> BackupIds { get; set; }

        [JsonProperty("next_backup_window")]
        public object NextBackupWindow { get; set; }

        [JsonProperty("snapshot_ids")]
        public List<object> SnapshotIds { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("volume_ids")]
        public List<object> VolumeIds { get; set; }

        [JsonProperty("size")]
        public Size Size { get; set; }

        [JsonProperty("size_slug")]
        public string SizeSlug { get; set; }

        [JsonProperty("networks")]
        public Networks Networks { get; set; }

        [JsonProperty("region")]
        public Region Region { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("distribution")]
        public string Distribution { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("public")]
        public bool Public { get; set; }

        [JsonProperty("regions")]
        public List<string> Regions { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("min_disk_size")]
        public long MinDiskSize { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("size_gigabytes")]
        public double SizeGigabytes { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Networks
    {
        [JsonProperty("v4")]
        public List<V4> V4 { get; set; }

        [JsonProperty("v6")]
        public List<V6> V6 { get; set; }
    }

    public partial class V4
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("netmask")]
        public string Netmask { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class V6
    {
        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("netmask")]
        public long Netmask { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class Region
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("features")]
        public List<string> Features { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("sizes")]
        public List<string> Sizes { get; set; }
    }

    public partial class Size
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("memory")]
        public long Memory { get; set; }

        [JsonProperty("vcpus")]
        public long Vcpus { get; set; }

        [JsonProperty("disk")]
        public long Disk { get; set; }

        [JsonProperty("transfer")]
        public long Transfer { get; set; }

        [JsonProperty("price_monthly")]
        public long PriceMonthly { get; set; }

        [JsonProperty("price_hourly")]
        public double PriceHourly { get; set; }

        [JsonProperty("regions")]
        public List<string> Regions { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }
    }

    /// <summary>
    /// Create new droplet
    ///
    /// POST {{baseUri}}/droplets
    /// </summary>
    public partial class CreateNewDroplet
    {
        [JsonProperty("droplet")]
        public Droplet Droplet { get; set; }

        [JsonProperty("links")]
        public CreateNewDropletLinks Links { get; set; }
    }

    public partial class CreateNewDropletLinks
    {
        [JsonProperty("actions")]
        public List<Action> Actions { get; set; }
    }

    public partial class Action
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }
    }

    /// <summary>
    /// get a single droplet
    ///
    /// GET {{baseUri}}/droplets/{{dropletId}}
    /// </summary>
    public partial class GetASingleDroplet
    {
        [JsonProperty("droplet")]
        public Droplet Droplet { get; set; }
    }

    public partial class ListAllSshKeys
    {
        public static ListAllSshKeys FromJson(string json) => JsonConvert.DeserializeObject<ListAllSshKeys>(json, QuickType.Converter.Settings);
    }

    public partial class ListAllDroplets
    {
        public static ListAllDroplets FromJson(string json) => JsonConvert.DeserializeObject<ListAllDroplets>(json, QuickType.Converter.Settings);
    }

    public partial class CreateNewDroplet
    {
        public static CreateNewDroplet FromJson(string json) => JsonConvert.DeserializeObject<CreateNewDroplet>(json, QuickType.Converter.Settings);
    }

    public partial class GetASingleDroplet
    {
        public static GetASingleDroplet FromJson(string json) => JsonConvert.DeserializeObject<GetASingleDroplet>(json, QuickType.Converter.Settings);
    }

    public partial class CreateDropletContent
    {
        public static CreateDropletContent FromJson(string json) => JsonConvert.DeserializeObject<CreateDropletContent>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CreateDropletContent self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        public static string ToJson(this ListAllSshKeys self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        public static string ToJson(this ListAllDroplets self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        public static string ToJson(this CreateNewDroplet self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
        public static string ToJson(this GetASingleDroplet self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public partial class CreateDropletContent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("ssh_keys")]
        public List<long> SshKeys { get; set; }

        [JsonProperty("backups")]
        public bool Backups { get; set; }

        [JsonProperty("ipv6")]
        public bool Ipv6 { get; set; }

        [JsonProperty("user_data")]
        public object UserData { get; set; }

        [JsonProperty("private_networking")]
        public object PrivateNetworking { get; set; }

        [JsonProperty("volumes")]
        public object Volumes { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}
