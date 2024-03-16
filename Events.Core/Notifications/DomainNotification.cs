using Events.Core.Events;
using System;

namespace Events.Core.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value, int version = 1)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = version;
            Key = key;
            Value = value;
        }
    }
}
