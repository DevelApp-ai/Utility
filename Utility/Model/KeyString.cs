using DevelApp.Utility.Exceptions;
using System;

namespace DevelApp.Utility.Model
{
    /// <summary>
    /// A string key with a maximum allowed length of 100 characters.
    /// Implements value equality so instances can be used as dictionary keys.
    /// </summary>
    public sealed class KeyString : IEquatable<KeyString>
    {
        /// <summary>
        /// Maximum allowed length of the inner string
        /// </summary>
        public const int MaxKeyLength = 100;

        private string _innerKeyString;

        public KeyString()
        {
            _innerKeyString = string.Empty;
        }

        public KeyString(string keyString)
        {
            ValidateString(keyString);
            _innerKeyString = keyString;
        }

        /// <summary>
        /// Throws if the string is null or longer than MaxKeyLength
        /// </summary>
        private static void ValidateString(string keyString)
        {
            if (keyString is null)
            {
                throw new KeyStringException("KeyString cannot be null");
            }
            if (keyString.Length > MaxKeyLength)
            {
                throw new KeyStringException($"KeyString is above the allowed {MaxKeyLength} characters: {keyString}");
            }
        }

        /// <summary>
        /// Serialization property that might change as interface so please do not use
        /// </summary>
        public string InnerString
        {
            get
            {
                return _innerKeyString;
            }
            set
            {
                ValidateString(value);
                _innerKeyString = value;
            }
        }

        public KeyString Clone()
        {
            return new KeyString(_innerKeyString);
        }

        public bool Equals(KeyString other)
        {
            return !(other is null) && string.Equals(_innerKeyString, other._innerKeyString, StringComparison.Ordinal);
        }

        public bool Equals(string other)
        {
            return !(other is null) && string.Equals(_innerKeyString, other, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is KeyString other && Equals(other);
        }

        public override int GetHashCode()
        {
            return StringComparer.Ordinal.GetHashCode(_innerKeyString);
        }

        public override string ToString()
        {
            return _innerKeyString;
        }

        #region Operators

        public static bool operator ==(KeyString left, KeyString right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(KeyString left, KeyString right)
        {
            return !(left == right);
        }

        public static implicit operator KeyString(string rhs)
        {
            return new KeyString(rhs);
        }

        public static implicit operator string(KeyString rhs)
        {
            return rhs.ToString();
        }

        #endregion
    }
}