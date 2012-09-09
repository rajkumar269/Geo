﻿using System;
using System.IO;
using Geo.Gps.Metadata;

namespace Geo.Gps.Serialization.Xml
{
    public abstract class GpsXmlSerializer<T> : GpsXmlDeSerializer<T>, IGpsFileSerializer
    {
        public void Serialize(Stream stream, GpsData data)
        {
            _xmlSerializer.Serialize(stream, Serialize(data));
        }

        public string SerializeToString(GpsData data)
        {
            var textWriter = new StringWriter();
            _xmlSerializer.Serialize(textWriter, Serialize(data));
            return textWriter.ToString();
        }

        protected abstract T Serialize(GpsData data);

        protected void SerializeMetadata(GpsData data, T xml, Func<GpsMetadata.MetadataKeys, string> attribute, Action<T, string> action)
        {
            var value = data.Metadata.Attribute(attribute);
            if (!value.IsNullOrWhitespace())
                action(xml, value);
        }

        protected void SerializeTrackMetadata<TTrack>(Track data, TTrack xml, Func<TrackMetadata.MetadataKeys, string> attribute, Action<TTrack, string> action)
        {
            var value = data.Metadata.Attribute(attribute);
            if (!value.IsNullOrWhitespace())
                action(xml, value);
        }

        protected void SerializeRouteMetadata<TRoute>(Route data, TRoute xml, Func<RouteMetadata.MetadataKeys, string> attribute, Action<TRoute, string> action)
        {
            var value = data.Metadata.Attribute(attribute);
            if (!value.IsNullOrWhitespace())
                action(xml, value);
        }
    }
}
