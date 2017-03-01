using Xunit;

namespace Spans.Test
{
    public class RangeTests
    {
        [Theory]
        [InlineData(1, 5, true, true, 1, 5, true, true)]
        [InlineData(1, 5, true, true, 1, 5, false, false)]
        [InlineData(1, 5, true, true, 5, 10, true, true)]
        [InlineData(1, 5, true, true, 0, 3, true, true)]
        [InlineData(1, 5, true, true, 3, 4, true, true)]
        [InlineData(1, 5, true, true, 3, 6, true, true)]
        [InlineData(1, 5, true, true, 0, 6, false, false)]
        public void OverlapsRange(int sourceStart, int sourceEnd, bool sourceInclusiveStart, bool sourceInclusiveEnd, int targetStart, int targetEnd, bool targetInclusiveStart, bool targetInclusiveEnd)
        {
            Assert.True(sourceStart.To(sourceEnd, sourceInclusiveStart, sourceInclusiveEnd).Overlaps(targetStart.To(targetEnd, targetInclusiveStart, targetInclusiveEnd)));
        }

        [Theory]
        [InlineData(1, 5, true, false, 5, 10, true, true)]
        [InlineData(1, 5, true, true, 5, 10, false, true)]
        [InlineData(1, 5, true, true, 6, 10, true, true)]
        public void DoesNotOverlapRange(int sourceStart, int sourceEnd, bool sourceInclusiveStart, bool sourceInclusiveEnd, int targetStart, int targetEnd, bool targetInclusiveStart, bool targetInclusiveEnd)
        {
            Assert.False(sourceStart.To(sourceEnd, sourceInclusiveStart, sourceInclusiveEnd).Overlaps(targetStart.To(targetEnd, targetInclusiveStart, targetInclusiveEnd)));
        }

        [Theory]
        [InlineData(1, 5, true, true, 1, 5, true, true)]
        [InlineData(1, 5, true, true, 1, 5, false, false)]
        [InlineData(1, 5, false, false, 1, 5, false, false)]
        [InlineData(1, 5, true, true, 2, 4, true, true)]
        public void IncludesRange(int sourceStart, int sourceEnd, bool sourceInclusiveStart, bool sourceInclusiveEnd, int targetStart, int targetEnd, bool targetInclusiveStart, bool targetInclusiveEnd)
        {
            Assert.True(sourceStart.To(sourceEnd, sourceInclusiveStart, sourceInclusiveEnd).Includes(targetStart.To(targetEnd, targetInclusiveStart, targetInclusiveEnd)));
        }

        [Theory]
        [InlineData(1, 5, true, true, 1)]
        [InlineData(1, 5, true, true, 3)]
        [InlineData(1, 5, false, false, 3)]
        [InlineData(1, 5, true, true, 5)]
        public void IncludesValue(int sourceStart, int sourceEnd, bool sourceInclusiveStart, bool sourceInclusiveEnd, int target)
        {
            Assert.True(sourceStart.To(sourceEnd, sourceInclusiveStart, sourceInclusiveEnd).Includes(target));
        }      

        [Theory]
        [InlineData(1, 5, false, false, 1, 5, true, true)]
        [InlineData(1, 5, true, true, 1, 6, true, true)]
        [InlineData(1, 5, true, true, 0, 5, true, true)]
        public void DoesNotIncludeRange(int sourceStart, int sourceEnd, bool sourceInclusiveStart, bool sourceInclusiveEnd, int targetStart, int targetEnd, bool targetInclusiveStart, bool targetInclusiveEnd)
        {
            Assert.False(sourceStart.To(sourceEnd, sourceInclusiveStart, sourceInclusiveEnd).Includes(targetStart.To(targetEnd, targetInclusiveStart, targetInclusiveEnd)));
        }
        
        [Theory]
        [InlineData(1, 5, false, false, 0)]
        [InlineData(1, 5, false, false, 1)]
        [InlineData(1, 5, false, false, 5)]
        [InlineData(1, 5, false, false, 6)]
        public void DoesNotIncludeValue(int sourceStart, int sourceEnd, bool sourceInclusiveStart, bool sourceInclusiveEnd, int target)
        {
            Assert.False(sourceStart.To(sourceEnd, sourceInclusiveStart, sourceInclusiveEnd).Includes(target));
        }  
    }
}
