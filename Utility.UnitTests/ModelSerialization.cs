using System;
using DevelApp.Utility.Model;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Utility.UnitTests
{
    public class ModelSerialization
    {
        [Fact]
        public void KeyString_Serialization()
        {
            KeyString keyString = new KeyString(Guid.NewGuid().ToString());

            string output = JsonConvert.SerializeObject(keyString);

            KeyString deserializedKeyString = JsonConvert.DeserializeObject<KeyString>(output);

            deserializedKeyString.ShouldBe(keyString);
        }

        [Fact]
        public void IdentifierString_Serialization()
        {
            IdentifierString identifierString = new IdentifierString("SomeIdentifier");

            string output = JsonConvert.SerializeObject(identifierString);

            IdentifierString deserializedIdentifierString = JsonConvert.DeserializeObject<IdentifierString>(output);

            deserializedIdentifierString.ShouldBe(identifierString);
        }

        [Fact]
        public void SemanticVersionNumber_Serialization()
        {
            SemanticVersionNumber semanticVersionNumber = new SemanticVersionNumber(2, 3, 4);

            string output = JsonConvert.SerializeObject(semanticVersionNumber);

            SemanticVersionNumber deserializedSemanticVersionNumber = JsonConvert.DeserializeObject<SemanticVersionNumber>(output);

            deserializedSemanticVersionNumber.ShouldBe(semanticVersionNumber);
        }
    }
}