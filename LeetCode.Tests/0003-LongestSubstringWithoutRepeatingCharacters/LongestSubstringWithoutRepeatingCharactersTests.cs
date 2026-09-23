using FsCheck.Xunit;
using LeetCode.LongestSubstringWithoutRepeatingCharacters;

namespace LeetCode.Tests.LongestSubstringWithoutRepeatingCharacters;

public class LongestSubstringWithoutRepeatingCharactersTests
{
    // 具体例による確認: 基本形、同じ文字の連続、重複が中央に来るケース、空文字列
    [Theory]
    [InlineData("abcabcbb", 3)]
    [InlineData("bbbbb", 1)]
    [InlineData("pwwkew", 3)]
    [InlineData("", 0)]
    public void KnownCases(string s, int expected)
    {
        var actual = new Solution().LengthOfLongestSubstring(s);

        Assert.Equal(expected, actual);
    }

    // 総当たり(オラクル)で求めた「重複なし部分文字列の最大長」を別経路で計算し、
    // ランダムな文字列に対して Solution の結果と一致することを検証する
    private static int BruteForceLongestUniqueSubstring(string s)
    {
        var maxLength = 0;
        for (var start = 0; start < s.Length; start++)
        {
            var seen = new HashSet<char>();
            for (var end = start; end < s.Length; end++)
            {
                if (!seen.Add(s[end]))
                    break;

                maxLength = Math.Max(maxLength, end - start + 1);
            }
        }

        return maxLength;
    }

    [Property]
    public bool RandomString_MatchesBruteForceOracle(string? s)
    {
        s ??= "";

        var expected = BruteForceLongestUniqueSubstring(s);
        var actual = new Solution().LengthOfLongestSubstring(s);

        return actual == expected;
    }
}
