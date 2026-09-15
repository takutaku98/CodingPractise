using FsCheck.Xunit;
using LeetCode.TwoSum;

namespace LeetCode.Tests.TwoSum;

public class TwoSumTests
{
    // 具体例による確認: 基本形、自分自身とのペア防止、重複値、負数
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
    [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]   // 3+3=6 と誤答しないこと
    [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]      // 同じ値の別インデックスは正解
    [InlineData(new[] { -1, -2, -3, -4, -5 }, -8, new[] { 2, 4 })]
    public void KnownCases(int[] nums, int target, int[] expected)
    {
        var actual = new Solution().TwoSum(nums, target);

        Assert.Equal(expected, actual);
    }

    // プロパティ: 答えが必ず存在するように target を仕込んだ任意の入力に対して、
    // 「相異なる有効なインデックスの組で、和が target になる」ものが返る
    [Property]
    public bool PlantedPair_ReturnsValidDistinctIndicesSummingToTarget(int[] xs, int seedA, int seedB)
    {
        if (xs is null || xs.Length < 2)
            return true; // 2要素未満は問題の前提外なので対象にしない

        // 和のオーバーフローを避けるため値を [-999, 999] に丸める
        var nums = xs.Select(x => x % 1000).ToArray();
        var n = nums.Length;

        // seed から相異なる2つのインデックス a, b を決め、答えが必ず存在する target を作る
        var a = ((seedA % n) + n) % n;
        var offset = ((seedB % (n - 1)) + (n - 1)) % (n - 1); // [0, n-2]
        var b = (a + 1 + offset) % n;                          // a 以外の全インデックスを取り得る
        var target = nums[a] + nums[b];

        var result = new Solution().TwoSum(nums, target);
        var (i, j) = (result[0], result[1]);

        // 仕込んだ (a, b) そのものが返るとは限らない(同じ和のペアが他にもあり得る)ので、
        // 「返ったペアが条件を満たすこと」だけを検証する
        return i >= 0 && i < n
            && j >= 0 && j < n
            && i != j
            && nums[i] + nums[j] == target;
    }
}
