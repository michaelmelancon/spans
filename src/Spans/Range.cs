using System;

namespace Spans
{
    /// <summary>
    /// Represents a range of values between a start and end limit that can be either inclusive or exclusive.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class Range<T> : IComparable<Range<T>>, IEquatable<Range<T>> where T : IComparable<T>, IEquatable<T>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Range&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range.</param>
        public Range(T start, T end, bool startInclusive = true, bool endInclusive = true)
        {
            if (start.CompareTo(end) > 0)
            {
                throw new ArgumentOutOfRangeException("Start value must be less than End value");
            }

            Start = start;
            End = end;
            IsStartIncluded = startInclusive;
            IsEndIncluded = endInclusive;
        }

        /// <summary>
        /// Returns true if the range includes its start value.
        /// </summary>
        public bool IsStartIncluded { get; }

        /// <summary>
        /// Returns true if the range includes its end value.
        /// </summary>
        public bool IsEndIncluded { get; }

        /// <summary>
        /// Gets the start value of the range.
        /// </summary>
        /// <value>The start.</value>
        public T Start { get; }

        /// <summary>
        /// Gets the end value of the range.
        /// </summary>
        /// <value>The end.</value>
        public T End { get; }

        /// <summary>
        /// Creates a <see cref="Range&lt;T&gt;"/> object that is made up by the intersection of two ranges.
        /// </summary>
        /// <param name="range">The range to intersect with.</param>
        /// <returns>A <see cref="Range&lt;T&gt;"/> object.</returns>
        public Range<T> Intersection(Range<T> range)
        {
            return !Includes(range) ? !range.Includes(this) ? !Includes(range.Start) ? !Includes(range.End) ? null : new Range<T>(Start, range.End, IsStartIncluded, range.IsEndIncluded) : new Range<T>(range.Start, End, range.IsStartIncluded, IsEndIncluded) : this : range;
        }

        /// <summary>
        /// Determines whether the specified object of type <c>T</c> is contained within this range.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>
        ///     <c>true</c> if the specified object is within the range; otherwise, <c>false</c>.
        /// </returns>
        public bool Includes(T obj)
        {
            return (IsStartIncluded ? StartCompareTo(obj) <= 0 : StartCompareTo(obj) < 0) && (IsEndIncluded ? EndCompareTo(obj) >= 0 : EndCompareTo(obj) > 0);
        }


        /// <summary>
        /// Determines whether the specified <see cref="Range&lt;T&gt;"/> object is contained within this range.
        /// </summary>
        /// <param name="range">The range to check.</param>
        /// <returns>
        ///     <c>true</c> if the specified range is within the range; otherwise, <c>false</c>.
        /// </returns>
        public bool Includes(Range<T> range)
        {
            return Includes(range.Start) || !IsStartIncluded && StartEquals(range.Start) && Includes(range.End);
        }

        /// <summary>
        /// Determines whether this range overlaps the specified <see cref="Range&lt;T&gt;"/>.
        /// </summary>
        /// <param name="range">The range to check.</param>
        /// <returns>
        ///     <c>true</c> if the the two ranges overlap; otherwise, <c>false</c>.
        /// </returns>
        public bool Overlaps(Range<T> range)
        {
            return Includes(range.Start) || Includes(range.End) || range.Includes(this);
        }

        /// <summary>
        /// Determines if two <see cref="Range&lt;T&gt;"/> objects are equal.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="target">The target object.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Range<T> source, Range<T> target)
        {
            return Equals(source, target) || (!Equals(source, null) && source.Equals(target));
        }

        /// <summary>
        /// Determines if two <see cref="Range&lt;T&gt;"/> objects are not equal.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="target">The target object.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Range<T> source, Range<T> target)
        {
            return !(source == target);
        }

        /// <summary>
        /// Compares this <see cref="Range&lt;T&gt;"/> range object with the specified <see cref="Range&lt;T&gt;"/> object 
        /// and returns an integer that indicates their relationship to one another in the sort order.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns></returns>
        public int CompareTo(Range<T> other)
        {
            return StartEquals(other.Start) ? IsStartIncluded == other.IsStartIncluded ? IsEndIncluded == other.IsEndIncluded ? EndCompareTo(other.End) : (IsEndIncluded ? -1 : 1) : (IsStartIncluded ? 1 : -1) : StartCompareTo(other.Start);
        }

        /// <summary>
        /// Determines whether the <see cref="Range&lt;T&gt;"/> instances are considered equal.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>
        ///     <c>true</c> if the the the start and end values are equal; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Range<T> other)
        {
            return other != null && StartEquals(other.Start) && EndEquals(other.End) && IsStartIncluded == other.IsStartIncluded && IsEndIncluded && other.IsEndIncluded;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this <see cref="Range&lt;T&gt;"/> instance.
        /// </summary>
        /// <param name="other">The <see cref="System.Object"/> to compare with this <see cref="Range&lt;T&gt;"/> instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this <see cref="Range&lt;T&gt;"/> instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object other)
        {
            return other is Range<T> ? Equals((Range<T>)other) : false;
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Range&lt;T&gt;"/> instance.
        /// </summary>
        /// <returns>
        /// A hash code for this <see cref="Range&lt;T&gt;"/> instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (Start == null) ? (End == null) ? 0 : End.GetHashCode() : Start.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this <see cref="Range&lt;T&gt;"/> instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String"/> that represents this <see cref="Range&lt;T&gt;"/> instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Start} to {End}";
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this <see cref="Range&lt;T&gt;"/> instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String"/> that represents this <see cref="Range&lt;T&gt;"/> instance.
        /// </returns>
        public string ToString(string format)
        {
            format = format == null ? format : $":{format}";
            return string.Format($"{{0{format}}} to {{1{format}}}", Start, End);
        }

        /// <summary>
        /// Returns an identical range save for the start being excluded.
        /// </summary>
        /// <returns></returns>
        public Range<T> ExcludeStart()
        {
            return IsStartIncluded ? Range.Create(Start, End, false, IsEndIncluded) : this;

        }

        /// <summary>
        /// Returns an identical range save for the start being included.
        /// </summary>
        /// <returns></returns>
        public Range<T> IncludeStart()
        {
            return IsStartIncluded ? this : Range.Create(Start, End, true, IsEndIncluded);
        }

        /// <summary>
        /// Returns an identical range save for the end being excluded.
        /// </summary>
        /// <returns></returns>
        public Range<T> ExcludeEnd()
        {
            return IsEndIncluded ? Range.Create(Start, End, IsStartIncluded, false) : this;
        }

        /// <summary>
        /// Returns an identical range save for the end being included.
        /// </summary>
        /// <returns></returns>
        public Range<T> IncludeEnd()
        {
            return IsEndIncluded ? this : Range.Create(Start, End, IsStartIncluded, true);
        }

        private int StartCompareTo(T other)
        {
            return (Start == null) ? -1 : Start.CompareTo(other);
        }

        private bool StartEquals(T other)
        {
            return (Start == null) ? other == null : Start.Equals(other);
        }

        private bool EndEquals(T other)
        {
            return (End == null) ? other == null : End.Equals(other);
        }

        private int EndCompareTo(T other)
        {
            return (End == null) ? 1 : End.CompareTo(other);
        }
    }

    public static class Range
    {
        public static Range<T> Create<T>(T start, T end, bool inclusiveStart = true, bool inclusiveEnd = true)
            where T : IComparable<T>, IEquatable<T>
        {
            return new Range<T>(start, end, inclusiveStart, inclusiveEnd);
        }

        public static Range<T> To<T>(this T start, T end, bool inclusiveStart = true, bool inclusiveEnd = true)
            where T : IComparable<T>, IEquatable<T>
        {
            return Create(start, end, inclusiveStart, inclusiveEnd);
        }
    }
}
