using DevelApp.Utility.Exceptions;
using System;
using System.Globalization;

namespace DevelApp.Utility.Model
{
    /// <summary>
    /// Represent semantic versioning string consisting of
    /// Major: when you make incompatible API changes
    /// Minor: when you add functionality in a backwards compatible manner
    /// Patch: when you make backwards compatible bug fixes
    /// Implements value equality and ordering so instances can be used as dictionary keys and sorted.
    /// </summary>
    public sealed class SemanticVersionNumber : IEquatable<SemanticVersionNumber>, IComparable<SemanticVersionNumber>
    {
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }

        /// <summary>
        /// Throws if any part is below zero
        /// </summary>
        public SemanticVersionNumber(int major, int minor, int patch)
        {
            if (major < 0 || minor < 0 || patch < 0)
            {
                throw new SemanticVersionStringException($"A version number is below 0: {major}.{minor}.{patch}");
            }

            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }

        public SemanticVersionNumber Clone()
        {
            return new SemanticVersionNumber(Major, Minor, Patch);
        }

        public bool Equals(SemanticVersionNumber other)
        {
            return !(other is null) && Major == other.Major && Minor == other.Minor && Patch == other.Patch;
        }

        public override bool Equals(object obj)
        {
            return obj is SemanticVersionNumber other && Equals(other);
        }

        /// <summary>
        /// Orders by Major, then Minor, then Patch. Any instance sorts after null.
        /// </summary>
        public int CompareTo(SemanticVersionNumber other)
        {
            if (other is null)
            {
                return 1;
            }

            int majorComparison = Major.CompareTo(other.Major);
            if (majorComparison != 0)
            {
                return majorComparison;
            }

            int minorComparison = Minor.CompareTo(other.Minor);
            if (minorComparison != 0)
            {
                return minorComparison;
            }

            return Patch.CompareTo(other.Patch);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = hashCode * 23 + Major.GetHashCode();
                hashCode = hashCode * 23 + Minor.GetHashCode();
                hashCode = hashCode * 23 + Patch.GetHashCode();
                return hashCode;
            }
        }

        #region Operators

        public static bool operator ==(SemanticVersionNumber left, SemanticVersionNumber right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(SemanticVersionNumber left, SemanticVersionNumber right)
        {
            return !(left == right);
        }

        public static bool operator <(SemanticVersionNumber left, SemanticVersionNumber right)
        {
            if (left is null)
            {
                return !(right is null);
            }
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(SemanticVersionNumber left, SemanticVersionNumber right)
        {
            return right < left;
        }

        public static bool operator <=(SemanticVersionNumber left, SemanticVersionNumber right)
        {
            return !(left > right);
        }

        public static bool operator >=(SemanticVersionNumber left, SemanticVersionNumber right)
        {
            return !(left < right);
        }

        /// <summary>
        /// Parses a major.minor.patch string. Signs and whitespace are not allowed.
        /// </summary>
        public static implicit operator SemanticVersionNumber(string rhs)
        {
            if (rhs is null)
            {
                throw new SemanticVersionStringException("Versioning string cannot be null");
            }

            string[] parts = rhs.Split('.');
            if (parts.Length != 3)
            {
                throw new SemanticVersionStringException($"Versioning is not valid format major.minor.patch :{rhs}");
            }

            if (!int.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out int major))
            {
                throw new SemanticVersionStringException($"Versioning is not valid format for major :{parts[0]}");
            }
            if (!int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out int minor))
            {
                throw new SemanticVersionStringException($"Versioning is not valid format for minor :{parts[1]}");
            }
            if (!int.TryParse(parts[2], NumberStyles.None, CultureInfo.InvariantCulture, out int patch))
            {
                throw new SemanticVersionStringException($"Versioning is not valid format for patch :{parts[2]}");
            }

            return new SemanticVersionNumber(major, minor, patch);
        }

        public static implicit operator string(SemanticVersionNumber rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Version(SemanticVersionNumber rhs)
        {
            return new Version(rhs.Major, rhs.Minor, rhs.Patch);
        }

        /// <summary>
        /// An unspecified build number (patch) is treated as 0 and the revision is ignored.
        /// </summary>
        public static implicit operator SemanticVersionNumber(Version rhs)
        {
            if (rhs is null)
            {
                throw new SemanticVersionStringException("Version cannot be null");
            }
            if (rhs.Minor < 0)
            {
                throw new SemanticVersionStringException($"Version does not contain a minor number: {rhs}");
            }

            int patch = rhs.Build < 0 ? 0 : rhs.Build;
            return new SemanticVersionNumber(rhs.Major, rhs.Minor, patch);
        }

        #endregion
    }
}