using System.Numerics;
using FsCheck.Xunit;
using LeetCode.AddTwoNumbers;

namespace LeetCode.Tests.AddTwoNumbers;

public class AddTwoNumbersTests
{
    // int[](先頭 = 一の位)⇔ ListNode の変換ヘルパー
    private static ListNode ToList(int[] digits)
    {
        ListNode? head = null;
        // 末尾の桁から先頭へ向かって繋ぐと、先頭 = 一の位のリストになる
        for (var i = digits.Length - 1; i >= 0; i--)
            head = new ListNode(digits[i], head);
        return head!;
    }

    private static int[] ToDigits(ListNode? node)
    {
        var digits = new List<int>();
        for (; node != null; node = node.next)
            digits.Add(node.val);
        return [.. digits];
    }

    // 先頭 = 一の位の桁の並びを数値に戻す
    private static BigInteger ToNumber(int[] digits)
    {
        BigInteger number = 0;
        for (var i = digits.Length - 1; i >= 0; i--)
            number = number * 10 + digits[i];
        return number;
    }

    // 具体例による確認: 基本形、ゼロ、長さ違い + 繰り上がり連鎖、最後の繰り上がり
    [Theory]
    [InlineData(new[] { 2, 4, 3 }, new[] { 5, 6, 4 }, new[] { 7, 0, 8 })]   // 342 + 465 = 807
    [InlineData(new[] { 0 }, new[] { 0 }, new[] { 0 })]
    [InlineData(new[] { 9, 9, 9, 9, 9, 9, 9 }, new[] { 9, 9, 9, 9 }, new[] { 8, 9, 9, 9, 0, 0, 0, 1 })]
    [InlineData(new[] { 5 }, new[] { 5 }, new[] { 0, 1 })]                  // 5 + 5 = 10: 最後の繰り上がりで桁が増える
    [InlineData(new[] { 1, 8 }, new[] { 0 }, new[] { 1, 8 })]
    public void KnownCases(int[] l1, int[] l2, int[] expected)
    {
        var actual = new Solution().AddTwoNumbers(ToList(l1), ToList(l2));

        Assert.Equal(expected, ToDigits(actual));
    }

    // ランダムなバイト列を問題の前提を満たす桁の並びに整える:
    // 各要素を [0, 9] に丸め、前ゼロ(リストでは末尾の 0)を取り除く。空なら 1 桁の 0
    private static int[] ToValidDigits(byte[]? source)
    {
        var digits = (source ?? []).Select(x => x % 10).ToList();
        while (digits.Count > 1 && digits[^1] == 0)
            digits.RemoveAt(digits.Count - 1);
        return digits.Count == 0 ? [0] : [.. digits];
    }

    // プロパティ: 任意の桁の並び 2 つに対して、リスト同士の加算結果を数値に戻すと
    // 2 数の和(BigInteger で別経路計算)に一致し、答えのリストも正しい形式になっている
    [Property]
    public bool RandomDigits_SumMatchesBigIntegerAddition(byte[] xs, byte[] ys)
    {
        var d1 = ToValidDigits(xs);
        var d2 = ToValidDigits(ys);

        var result = ToDigits(new Solution().AddTwoNumbers(ToList(d1), ToList(d2)));

        // 値の検証: 別経路(BigInteger)で計算した和と一致すること
        return ToNumber(result) == ToNumber(d1) + ToNumber(d2)
            // 形式の検証: 1 桁以上、各桁は 0〜9、答えが 0 でない限り前ゼロがないこと
            && result.Length >= 1
            && result.All(d => d is >= 0 and <= 9)
            && (result.Length == 1 || result[^1] != 0);
    }
}
