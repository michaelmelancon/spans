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

        [Fact]
        public void ExcludeEndOfRangeWithInclusiveEndCreatesIdenticalRangeExceptWithExclusiveEnd()
        {
            Assert.Equal(0.To(5, inclusiveEnd: false), 0.To(5, inclusiveEnd: true).ExcludeEnd());
        }

        [Fact]
        public void ExcludeEndOfRangeWithExclusiveEndReturnsItself()
        {
            var original = 0.To(5, inclusiveEnd: false);
            Assert.Same(original, original.ExcludeEnd());
        }

        [Fact]
        public void IncludeEndOfRangeWithExclusiveEndCreatesIdenticalRangeExceptWithInclusiveEnd()
        {
            Assert.Equal(0.To(5, inclusiveEnd: true), 0.To(5, inclusiveEnd: false).IncludeEnd());
        }

        [Fact]
        public void IncludeEndOfRangeWithInclusiveEndReturnsItself()
        {
            var original = 0.To(5, inclusiveEnd: true);
            Assert.Same(original, original.IncludeEnd());
        }

        [Fact]
        public void ExcludeStartOfRangeWithInclusiveStartCreatesIdenticalRangeExceptWithExclusiveStart()
        {
            Assert.Equal(0.To(5, inclusiveStart: false), 0.To(5, inclusiveStart: true).ExcludeStart());
        }

        [Fact]
        public void ExcludeStartOfRangeWithExclusiveStartReturnsItself()
        {
            var original = 0.To(5, inclusiveStart: false);
            Assert.Same(original, original.ExcludeStart());
        }

        [Fact]
        public void IncludeStartOfRangeWithExclusiveStartCreatesIdenticalRangeExceptWithInclusiveStart()
        {
            Assert.Equal(0.To(5, inclusiveStart: true), 0.To(5, inclusiveStart: false).IncludeStart());
        }

        [Fact]
        public void IncludeStartOfRangeWithInclusiveStartReturnsItself()
        {
            var original = 0.To(5, inclusiveStart: true);
            Assert.Same(original, original.IncludeStart());
        }

        [Theory]
        [InlineData(0, 5, true, true)]
        [InlineData(0, 5, true, false)]
        [InlineData(0, 5, false, true)]
        [InlineData(0, 5, false, false)]
        public void ToMethodIsAConstructorAlias(int start, int end, bool inclusiveStart, bool inclusiveEnd)
        {
            Assert.Equal(new Range<int>(start, end, inclusiveStart, inclusiveEnd), start.To(end, inclusiveStart, inclusiveEnd));
        }


        [Theory]
        [InlineData(0, 5, true, true)]
        [InlineData(0, 5, true, false)]
        [InlineData(0, 5, false, true)]
        [InlineData(0, 5, false, false)]
        public void FromMethodIsAConstructorAlias(int start, int end, bool inclusiveStart, bool inclusiveEnd)
        {
            Assert.Equal(new Range<int>(start, end, inclusiveStart, inclusiveEnd), end.From(start, inclusiveStart, inclusiveEnd));
        }


        [Theory]
        [InlineData(0, 5, true, true)]
        [InlineData(0, 5, true, false)]
        [InlineData(0, 5, false, true)]
        [InlineData(0, 5, false, false)]
        public void CreateMethodIsAConstructorAlias(int start, int end, bool inclusiveStart, bool inclusiveEnd)
        {
            Assert.Equal(new Range<int>(start, end, inclusiveStart, inclusiveEnd), Range.Create(start, end, inclusiveStart, inclusiveEnd));
        }


        [Fact]
        public void TheIntersectionOfARangeAndASuperRangeIsTheOriginalRange()
        {
            var original = 1.To(5);
            Assert.Same(original, original.Intersection(0.To(6)));
        }

        [Fact]
        public void TheIntersectionOfARangeAndASubRangeIsTheSubRange()
        {
            var subrange = 1.To(5);
            Assert.Same(subrange, 0.To(6).Intersection(subrange));
        }

        [Theory]
        [InlineData(1, 5, true, true, 0, 4, true, true, 1, 4, true, true)]
        [InlineData(1, 5, false, false, 0, 4, false, false, 1, 4, false, false)]
        [InlineData(1, 5, true, true, 2, 6, true, true, 2, 5, true, true)]
        [InlineData(0, 5, true, false, 1, 5, true, true, 1, 5, true, false)]
        [InlineData(1, 6, false, true, 1, 5, true, true, 1, 5, false, true)]
        public void CreateIntersectionOfTwoRanges(int aStart, int aEnd, bool aInclusiveStart, bool aInclusiveEnd, int bStart, int bEnd, bool bInclusiveStart, bool bInclusiveEnd, int cStart, int cEnd, bool cInclusiveStart, bool cInclusiveEnd)
        {
            var expected = Range.Create(cStart, cEnd, cInclusiveStart, cInclusiveEnd);
            var actual = Range.Create(aStart, aEnd, aInclusiveStart, aInclusiveEnd)
                .Intersection(Range.Create(bStart, bEnd, bInclusiveStart, bInclusiveEnd));
            Assert.Equal(expected, actual);
        }
    }
}
