using DevelApp.Utility.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace DevelApp.Utility.Model
{
    /// <summary>
    /// An ASCII identifier: an ASCII letter followed by ASCII letters or numbers.
    /// Implements value equality so instances can be used as dictionary keys.
    /// </summary>
    public sealed class IdentifierString : IEquatable<IdentifierString>
    {
        private static readonly Regex ValidIdentifierRegex = new Regex("^[a-zA-Z][a-zA-Z0-9]*$", RegexOptions.Compiled);

        private string _innerString;

        public IdentifierString()
        {
            _innerString = string.Empty;
        }

        public IdentifierString(string identifierString)
        {
            ValidateString(identifierString);
            _innerString = identifierString;
        }

        /// <summary>
        /// Throws if the string is null or not a valid identifier
        /// </summary>
        private static void ValidateString(string identifierString)
        {
            if (identifierString is null)
            {
                throw new IdentifierStringException("IdentifierString cannot be null");
            }
            if (!ValidIdentifierRegex.IsMatch(identifierString))
            {
                throw new IdentifierStringException($"IdentifierString is not in correct format of ascii letter followed by ascii letter or number: {identifierString}");
            }
        }

        /// <summary>
        /// Serialization property that might change as interface so please do not use
        /// </summary>
        public string InnerString
        {
            get
            {
                return _innerString;
            }
            set
            {
                ValidateString(value);
                _innerString = value;
            }
        }

        public IdentifierString Clone()
        {
            return new IdentifierString(_innerString);
        }

        public bool Equals(IdentifierString other)
        {
            return !(other is null) && string.Equals(_innerString, other._innerString, StringComparison.Ordinal);
        }

        public bool Equals(string other)
        {
            return !(other is null) && string.Equals(_innerString, other, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is IdentifierString other && Equals(other);
        }

        public override int GetHashCode()
        {
            return StringComparer.Ordinal.GetHashCode(_innerString);
        }

        public override string ToString()
        {
            return _innerString;
        }

        #region Operators

        public static bool operator ==(IdentifierString left, IdentifierString right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(IdentifierString left, IdentifierString right)
        {
            return !(left == right);
        }

        public static implicit operator IdentifierString(string rhs)
        {
            return new IdentifierString(rhs);
        }

        public static implicit operator string(IdentifierString rhs)
        {
            return rhs.ToString();
        }

        #endregion
    }
}