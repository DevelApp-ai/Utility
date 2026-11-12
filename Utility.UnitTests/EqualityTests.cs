using System.Collections.Generic;
using DevelApp.Utility.Model;
using Shouldly;
using Xunit;

namespace Utility.UnitTests
{
    /// <summary>
    /// Regression tests for the dictionary lookup bug caused by missing value equality
    /// (see README): freshly created instances must behave as equal keys.
    /// </summary>
    public class EqualityTests
    {
        [Fact]
        public void KeyString_DictionaryLookup_WithFreshInstance_Succeeds()
        {
            var dictionary = new Dictionary<KeyString, string>
            {
                { "my_key", "value" }
            };

            dictionary.TryGetValue(new KeyString("my_key"), out string value).ShouldBeTrue();
            value.ShouldBe("value");
        }

        [Fact]
        public void KeyString_Equality()
        {
            KeyString key1 = "alpha";
            KeyString key2 = "alpha";
            KeyString key3 = "beta";

            key1.Equals(key2).ShouldBeTrue();
            key1.Equals((object)key2).ShouldBeTrue();
            (key1 == key2).ShouldBeTrue();
            (key1 != key3).ShouldBeTrue();
            key1.GetHashCode().ShouldBe(key2.GetHashCode());
        }

        [Fact]
        public void KeyString_NullIsSafe()
        {
            KeyString key = "alpha";
            KeyString nullKey = null;

            (key == nullKey).ShouldBeFalse();
            key.Equals(nullKey).ShouldBeFalse();
            key.Equals((object)null).ShouldBeFalse();
            (nullKey == null).ShouldBeTrue();
        }

        [Fact]
        public void IdentifierString_DictionaryLookup_WithFreshInstance_Succeeds()
        {
            var dictionary = new Dictionary<IdentifierString, string>
            {
                { "SomeIdentifier", "value" }
            };

            dictionary.TryGetValue(new IdentifierString("SomeIdentifier"), out string value).ShouldBeTrue();
            value.ShouldBe("value");
        }

        [Fact]
        public void IdentifierString_Equality()
        {
            IdentifierString identifier1 = "Identifier";
            IdentifierString identifier2 = "Identifier";
            IdentifierString identifier3 = "Identifier2";

            (identifier1 == identifier2).ShouldBeTrue();
            (identifier1 != identifier3).ShouldBeTrue();
            identifier1.GetHashCode().ShouldBe(identifier2.GetHashCode());
        }

        [Fact]
        public void NamespaceString_DictionaryLookup_WithFreshInstance_Succeeds()
        {
            var dictionary = new Dictionary<NamespaceString, string>
            {
                { "Funny.Onion", "value" }
            };

            dictionary.TryGetValue(new NamespaceString("Funny.Onion"), out string value).ShouldBeTrue();
            value.ShouldBe("value");
        }

        [Fact]
        public void SemanticVersionNumber_HashSetDeduplicatesEqualValues()
        {
            var hashSet = new HashSet<SemanticVersionNumber>
            {
                "1.0.0",
                "1.0.0",
                "1.0.1"
            };

            hashSet.Count.ShouldBe(2);
            hashSet.Contains(new SemanticVersionNumber(1, 0, 0)).ShouldBeTrue();
        }

        [Fact]
        public void SemanticVersionNumber_EqualsAndGetHashCode_AreConsistent()
        {
            SemanticVersionNumber version1 = "2.3.4";
            SemanticVersionNumber version2 = new SemanticVersionNumber(2, 3, 4);

            version1.Equals(version2).ShouldBeTrue();
            version1.GetHashCode().ShouldBe(version2.GetHashCode());
        }
    }
}