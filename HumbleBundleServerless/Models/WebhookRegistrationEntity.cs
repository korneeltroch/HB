﻿using Microsoft.WindowsAzure.Storage.Table;
using System.Text;
using System.Web.Security;

namespace HumbleBundleServerless.Models
{
    public class WebhookRegistrationEntity : TableEntity
    {
        public WebhookRegistrationEntity(BundleTypes type, string webhook, bool shouldRecieveUpdates)
        {
            PartitionKey = type.ToString();
            RowKey = System.Guid.NewGuid().ToString();

            EncryptedWebhook = MachineKey.Protect(Encoding.UTF8.GetBytes(webhook));
            ShouldRecieveUpdates = shouldRecieveUpdates;
            BundleType = (int) type;
        }

        public WebhookRegistrationEntity() { }

        public byte[] EncryptedWebhook { get; set; }

        public string GetDecryptedWebhook()
        {
            return Encoding.UTF8.GetString(MachineKey.Unprotect(EncryptedWebhook));
        }

        public bool ShouldRecieveUpdates { get; set; } = false;

        public int BundleType { get; set;  }
    }
}
