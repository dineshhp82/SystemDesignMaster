using SplitBillAmongFriends.Strategies;

namespace SplitBillAmongFriends.Factory
{
    /// <summary>
    /// Factory for creating ISplitStrategy instances based on split type.
    /// </summary>
    public class SplitFactory
    {
        /// <summary>
        /// Creates an ISplitStrategy based on the provided SplitType.
        /// </summary>
        /// <param name="type">The split type.</param>
        /// <param name="customAmounts">Custom amounts for custom split (optional).</param>
        /// <returns>An ISplitStrategy instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if type is null.</exception>
        /// <exception cref="ArgumentException">Thrown if split type is invalid.</exception>
        public static ISplitStrategy CreateSplitStrategy(SplitType type, Dictionary<Guid, decimal> customAmounts = null)
        {
            return type switch
            {
                SplitType.Equal => new EqualSplitStrategy(),
                SplitType.Custom => customAmounts != null
                    ? new CustomSplitStrategy(customAmounts)
                    : throw new ArgumentNullException(nameof(customAmounts), "Custom amounts must be provided for custom split."),
                _ => throw new ArgumentException($"Invalid split type {type}", nameof(type))
            };
        }
    }
}
