﻿namespace NServiceBus.Encryption.MessageProperty
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A string whose value will be encrypted when sent over the wire.
    /// </summary>
    [Serializable]
    public class EncryptedString : ISerializable
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EncryptedString" />.
        /// </summary>
        public EncryptedString()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EncryptedString" />.
        /// </summary>
        public EncryptedString(SerializationInfo info, StreamingContext context)
        {
            Guard.AgainstNull(nameof(info), info);
            EncryptedValue = info.GetValue("EncryptedValue", typeof(EncryptedValue)) as EncryptedValue;
        }

        /// <summary>
        /// The unencrypted string.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The encrypted value of this string.
        /// </summary>
        public EncryptedValue EncryptedValue
        {
            get { return encryptedValue; }
            set { encryptedValue = value; }
        }

        // we need to duplicate to make versions > 3.2.7 backwards compatible with 2.X

        /// <summary>
        /// Method for making default XML serialization work properly for this type.
        /// </summary>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Guard.AgainstNull(nameof(info), info);
            info.AddValue("EncryptedValue", EncryptedValue);
        }

        /// <summary>
        /// Gets the string value from the WireEncryptedString.
        /// </summary>
        public static implicit operator string(EncryptedString s)
        {
            return s?.Value;
        }

        /// <summary>
        /// Creates a new WireEncryptedString from the given string.
        /// </summary>
        public static implicit operator EncryptedString(string s)
        {
            return new EncryptedString
            {
                Value = s
            };
        }

        EncryptedValue encryptedValue;
    }
}