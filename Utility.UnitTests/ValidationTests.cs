using System;
using DevelApp.Utility.Exceptions;
using DevelApp.Utility.Model;
using Shouldly;
using Xunit;

namespace Utility.UnitTests
{
    public class ValidationTests
    {
        [Fact]
        public void KeyString_NullThrows()
        {
            Should.Throw<KeyStringException>(() => new KeyString(null));
        }

        [Fact]
        public void KeyString_TooLongThrows()
        {
            Should.Throw<KeyStringException>(() => new KeyString(new string('a', KeyString.MaxKeyLength + 1)));
        }

        [Fact]
        public void KeyString_MaxLengthIsAllowed()
        {
            KeyString keyString = new KeyString(new string('a', KeyString.MaxKeyLength));

            keyString.ToString().Length.ShouldBe(KeyString.MaxKeyLength);
        }

        [Fact]
        public void IdentifierString_NullThrows()
        {
            Should.Throw<IdentifierStringException>(() => new IdentifierString(null));
        }

        [Theory]
        [InlineData("1abc")]
        [InlineData("a-b")]
        [InlineData("")]
        [InlineData("a b")]
        public void IdentifierString_InvalidFormatThrows(string invalidIdentifier)
        {
            Should.Throw<IdentifierStringException>(() => new IdentifierString(invalidIdentifier));
        }

        [Fact]
        public void NamespaceString_NullThrows()
        {
            Should.Throw<NamespaceStringException>(() => new NamespaceString(null));
        }

        [Fact]
        public void SemanticVersionNumber_NegativePartThrows()
        {
            Should.Throw<SemanticVersionStringException>(() => new SemanticVersionNumber(-1, 0, 0));
            Should.Throw<SemanticVersionStringException>(() => new SemanticVersionNumber(0, -1, 0));
            Should.Throw<SemanticVersionStringException>(() => new SemanticVersionNumber(0, 0, -1));
        }

        [Fact]
        public void SemanticVersionNumber_NullStringThrows()
        {
            Should.Throw<SemanticVersionStringException>(() =>
            {
                SemanticVersionNumber semanticVersionNumber = (string)null;
            });
        }

        [Fact]
        public void CodeBuilder_IndentUnderflowThrows()
        {
            CodeBuilder codeBuilder = new CodeBuilder();

            Should.Throw<CodeBuilderException>(() => codeBuilder.IndentDecrease());
        }

        [Fact]
        public void CodeBuilder_NegativeIndentSpacesThrows()
        {
            Should.Throw<CodeBuilderException>(() => new CodeBuilder(-1));
        }

        [Fact]
        public void CodeBuilder_BuildWithUnbalancedIndentThrows()
        {
            CodeBuilder codeBuilder = new CodeBuilder();
            codeBuilder.IndentIncrease();

            Should.Throw<CodeBuilderException>(() => codeBuilder.Build());
        }

        [Fact]
        public void CodeBuilder_GeneratesIndentedCode()
        {
            CodeBuilder codeBuilder = new CodeBuilder();
            codeBuilder.L("namespace Test")
                .IndentIncrease()
                .L("class Foo")
                .EmptyLine()
                .IndentDecrease()
                .L("end");

            codeBuilder.Build().ShouldBe(
                "namespace Test" + Environment.NewLine +
                "    class Foo" + Environment.NewLine +
                Environment.NewLine +
                "end" + Environment.NewLine);
        }
    }
}