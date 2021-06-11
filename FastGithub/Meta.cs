﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace FastGithub
{
    class Meta
    {
        [JsonPropertyName("hooks")]
        public string[] Hooks { get; set; } = Array.Empty<string>();

        [JsonPropertyName("web")]
        public string[] Web { get; set; } = Array.Empty<string>();

        [JsonPropertyName("api")]
        public string[] Api { get; set; } = Array.Empty<string>();

        [JsonPropertyName("git")]
        public string[] Git { get; set; } = Array.Empty<string>();

        [JsonPropertyName("packages")]
        public string[] Packages { get; set; } = Array.Empty<string>();

        [JsonPropertyName("pages")]
        public string[] Pages { get; set; } = Array.Empty<string>();

        [JsonPropertyName("importer")]
        public string[] Importer { get; set; } = Array.Empty<string>();

        [JsonPropertyName("actions")]
        public string[] Actions { get; set; } = Array.Empty<string>();

        [JsonPropertyName("dependabot")]
        public string[] Dependabot { get; set; } = Array.Empty<string>();


        public IEnumerable<DomainAddress> ToDomainAddress()
        {
            foreach (var item in this.Web)
            {
                if (IPv4CIDR.TryParse(item, out var cidr))
                {
                    foreach (var address in cidr.GetAllIPAddress())
                    {
                        yield return new DomainAddress("github.com", address);
                    }
                }
            }

            foreach (var item in this.Api)
            {
                if (IPv4CIDR.TryParse(item, out var cidr))
                {
                    foreach (var address in cidr.GetAllIPAddress())
                    {
                        yield return new DomainAddress("api.github.com", address);
                    }
                }
            }
        }

        public record DomainAddress(string Domain, IPAddress Address);
    }
}