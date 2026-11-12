using System;
using DevelApp.Utility.Exceptions;
using DevelApp.Utility.Model;
using Shouldly;
using Xunit;

namespace Utility.UnitTests
{
    public class ModelImplicitConversion
    {
        [Fact]
        public void KeyString_ImplicitConversion()
        {
            string key = Guid.NewGuid().ToString();

            KeyString keyString = key;

            string output = keyString;
            output.ShouldBe(key);
        }

        [Fact]
        public void SemanticVersionNumber_ImplicitConversion()
        {
            SemanticVersionNumber semanticVersionNumber = "2.3.4";
            SemanticVersionNumber semanticVersionNumber2 = new Version(1, 2, 3);

            string output = semanticVersionNumber;
            output.ShouldBe("2.3.4");

            Version outputVersion = semanticVersionNumber2;
            outputVersion.ShouldBe(new Version(1, 2, 3));
        }

        [Fact]
        public void SemanticVersionNumber_ImplicitConversion_FromTwoPartVersion_TreatsMissingPatchAsZero()
        {
            SemanticVersionNumber semanticVersionNumber = new Version(1, 2);

            semanticVersionNumber.ToString().ShouldBe("1.2.0");
        }

        [Fact]
        public void SemanticVersionNumber_ImplicitConversion_InvalidFormatThrows()
        {
            Should.Throw<SemanticVersionStringException>(() =>
            {
                SemanticVersionNumber semanticVersionNumber = "1.2";
            });
            Should.Throw<SemanticVersionStringException>(() =>
            {
                SemanticVersionNumber semanticVersionNumber = "a.b.c";
            });
            Should.Throw<SemanticVersionStringException>(() =>
            {
                SemanticVersionNumber semanticVersionNumber = "1.2.3.4";
            });
        }
    }
}