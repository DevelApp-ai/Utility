using DevelApp.Utility.Exceptions;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DevelApp.Utility.Model
{
    /// <summary>
    /// Represents a namespace where each dot represents a sub namespace.
    /// Backslashes are translated to dots and spaces are removed.
    /// Implements value equality so instances can be used as dictionary keys.
    /// </summary>
    public sealed class NamespaceString : IEquatable<NamespaceString>
    {
        private static readonly Regex ValidNamespaceRegex = new Regex("^[a-zA-Z][a-zA-Z0-9\\.]*$", RegexOptions.Compiled);

        private string _innerString;

        public NamespaceString()
        {
            _innerString = string.Empty;
        }

        public NamespaceString(string identifierString)
        {
            _innerString = ValidateAndCorrectString(identifierString);
        }

        /// <summary>
        /// Throws if the string is null or not a valid namespace after correction,
        /// otherwise returns the corrected string
        /// </summary>
        private static string ValidateAndCorrectString(string identifierString)
        {
            if (identifierString is null)
            {
                throw new NamespaceStringException("NamespaceString cannot be null");
            }

            string correctedString = identifierString.Replace('\\', '.').Replace(" ", string.Empty);

            if (!ValidNamespaceRegex.IsMatch(correctedString))
            {
                throw new NamespaceStringException($"NamespaceString is not in correct format of ascii letter followed by ascii letter, dot or number: {identifierString}");
            }
            return correctedString;
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
                _innerString = ValidateAndCorrectString(value);
            }
        }

        public NamespaceString Clone()
        {
            return new NamespaceString(_innerString);
        }

        public bool Equals(NamespaceString other)
        {
            return !(other is null) && string.Equals(_innerString, other._innerString, StringComparison.Ordinal);
        }

        public bool Equals(string other)
        {
            return !(other is null) && string.Equals(_innerString, other, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is NamespaceString other && Equals(other);
        }

        public override int GetHashCode()
        {
            return StringComparer.Ordinal.GetHashCode(_innerString);
        }

        public override string ToString()
        {
            return _innerString;
        }

        /// <summary>
        /// Translates every dot in the namespace to a sub directory using Path.Combine
        /// fx DevelApp.Modules.Default translates into DevelApp/Modules/Default on a unix machine
        /// </summary>
        public string ToFilePath
        {
            get
            {
                return Path.Combine(_innerString.Split('.'));
            }
        }

        #region Operators

        public static bool operator ==(NamespaceString left, NamespaceString right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(NamespaceString left, NamespaceString right)
        {
            return !(left == right);
        }

        public static implicit operator NamespaceString(string rhs)
        {
            return new NamespaceString(rhs);
        }

        public static implicit operator string(NamespaceString rhs)
        {
            return rhs.ToString();
        }

        #endregion
    }
}