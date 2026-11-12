using DevelApp.Utility.Model;
using Shouldly;
using Xunit;

namespace Utility.UnitTests
{
    public class SemanticVersionNumber_Compare
    {
        [Fact]
        public void CompareTo_Major()
        {
            SemanticVersionNumber version = "2.3.4";

            version.CompareTo((SemanticVersionNumber)"1.9.9").ShouldBe(1);
            version.CompareTo((SemanticVersionNumber)"3.0.0").ShouldBe(-1);
            version.CompareTo((SemanticVersionNumber)"2.3.4").ShouldBe(0);
        }

        [Fact]
        public void CompareTo_Minor()
        {
            SemanticVersionNumber version = "2.3.4";

            version.CompareTo((SemanticVersionNumber)"2.2.9").ShouldBe(1);
            version.CompareTo((SemanticVersionNumber)"2.4.0").ShouldBe(-1);
        }

        [Fact]
        public void CompareTo_Patch()
        {
            SemanticVersionNumber version = "2.3.4";

            version.CompareTo((SemanticVersionNumber)"2.3.3").ShouldBe(1);
            version.CompareTo((SemanticVersionNumber)"2.3.5").ShouldBe(-1);
        }

        [Fact]
        public void CompareTo_Null_SortsBeforeAnyInstance()
        {
            SemanticVersionNumber version = "1.0.0";

            version.CompareTo(null).ShouldBe(1);
        }

        [Fact]
        public void Operators()
        {
            SemanticVersionNumber lower = "1.0.0";
            SemanticVersionNumber equal = "1.0.0";
            SemanticVersionNumber higher = "1.0.1";

            (lower == equal).ShouldBeTrue();
            (lower != higher).ShouldBeTrue();
            (lower < higher).ShouldBeTrue();
            (higher > lower).ShouldBeTrue();
            (lower <= equal).ShouldBeTrue();
            (higher >= lower).ShouldBeTrue();
            (lower == null).ShouldBeFalse();
        }
    }
}