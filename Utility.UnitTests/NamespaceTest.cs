using System.IO;
using DevelApp.Utility.Exceptions;
using DevelApp.Utility.Model;
using Shouldly;
using Xunit;

namespace Utility.UnitTests
{
    public class NamespaceTest
    {
        [Fact]
        public void ToFilePathTest()
        {
            // Path.Combine uses the platform directory separator, so this works on all platforms
            NamespaceString namespaceString = new NamespaceString("Funny.Onion");
            string[] stringParts = namespaceString.ToFilePath.Split(Path.DirectorySeparatorChar);
            stringParts.Length.ShouldBe(2);
            stringParts[0].ShouldBe("Funny");
            stringParts[1].ShouldBe("Onion");
        }

        [Fact]
        public void NamingPositive1()
        {
            string teststring = "F1unny.Onion";
            NamespaceString namespaceString = new NamespaceString(teststring);
            namespaceString.ToString().ShouldBe(teststring);
        }

        [Fact]
        public void NamingNegative1()
        {
            string teststring = "1Funny.Onion";
            Should.Throw<NamespaceStringException>(() => new NamespaceString(teststring));
        }

        [Fact]
        public void Naming_CorrectsBackslashesAndRemovesSpaces()
        {
            NamespaceString namespaceString = new NamespaceString("Funny .Onion\\Deeper");
            namespaceString.ToString().ShouldBe("Funny.Onion.Deeper");
        }

        [Fact]
        public void Equality()
        {
            NamespaceString namespaceString1 = "Funny.Onion";
            NamespaceString namespaceString2 = "Funny.Onion";
            NamespaceString namespaceString3 = "Funny.Onion2";

            (namespaceString1 == namespaceString2).ShouldBeTrue();
            (namespaceString1 != namespaceString3).ShouldBeTrue();
            namespaceString1.GetHashCode().ShouldBe(namespaceString2.GetHashCode());
        }
    }
}